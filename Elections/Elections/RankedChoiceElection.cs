using System.Security.Cryptography.X509Certificates;
using Elections.Exceptions;
using Elections.Objects;
using static System.Collections.Specialized.BitVector32;

namespace Elections.Elections;

public class RankedChoiceElection : IElection<IRankedChoiceBallot>
{
    public ElectionResult Run(IReadOnlyList<IRankedChoiceBallot> ballots, IReadOnlyList<ICandidate> candidates)
    {
        var eligibleCandidates = candidates.ToList();

        for (var round = 1; round < candidates.Count; round++)
        {
            var roundVotes = CountRoundVotes(ballots, eligibleCandidates);
            var roundMajorityThreshold = roundVotes.Sum(x => x.Value) / 2 + 1;
            var roundMaxVotes = roundVotes.Max(x => x.Value);

            if (roundMaxVotes >= roundMajorityThreshold)
                return ProcessWinners(eligibleCandidates, roundVotes, roundMaxVotes);

            RebalanceEligibleCandidates(eligibleCandidates, roundVotes);
        }

        throw new NoWinnerException("No winner was determined after running all rounds");
    }

    private static Dictionary<int, int> CountRoundVotes(IReadOnlyList<IRankedChoiceBallot> ballots, IReadOnlyList<ICandidate> roundCandidates)
    {
        var candidateIds = roundCandidates.Select(x => x.Id).ToHashSet();
        var votes = roundCandidates.ToDictionary(x => x.Id, _ => 0);

        foreach (var ballot in ballots)
        {
            var vote = ballot.Votes
                .Where(x => candidateIds.Contains(x.Candidate.Id))
                .OrderBy(x => x.Rank)
                .FirstOrDefault();

            if (vote != null && votes.ContainsKey(vote.Candidate.Id))
                votes[vote.Candidate.Id]++;
        }

        return votes;
    }

    private static ElectionResult ProcessWinners(List<ICandidate> eligibleCandidates, Dictionary<int, int> roundVotes, int roundMaxVotes)
    {
        var winnerIds = roundVotes.Where(x => x.Value == roundMaxVotes).Select(x => x.Key).ToHashSet();
        var winners = eligibleCandidates.Where(x => winnerIds.Contains(x.Id));
        if (winners.Count() > 1)
            throw new ElectionTieException(winners);

        return new ElectionResult(winners.Single(), roundMaxVotes);
    }

    private static void RebalanceEligibleCandidates(List<ICandidate> eligibleCandidates, Dictionary<int, int> roundVotes)
    {
        var roundMinVotes = roundVotes.Min(x => x.Value);
        var eliminateIds = roundVotes.Where(x => x.Value == roundMinVotes).Select(x => x.Key).ToHashSet();
        foreach (var id in eliminateIds)
        {
            var candidate = eligibleCandidates.SingleOrDefault(x => x.Id == id);
            if (candidate != null)
                eligibleCandidates.Remove(candidate);
        }
    }
}
