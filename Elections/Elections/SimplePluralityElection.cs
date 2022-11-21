using Elections.Exceptions;
using Elections.Objects;
using static System.Collections.Specialized.BitVector32;

namespace Elections.Elections;

public class SimplePluralityElection : IElection<ISimpleBallot>
{
    public ElectionResult Run(IReadOnlyList<ISimpleBallot> ballots, IReadOnlyList<ICandidate> candidates)
    {
        var candidateVotes = candidates.ToDictionary(x => x.Id, _ => 0);

        foreach (var ballot in ballots)
        {
            if (candidateVotes.ContainsKey(ballot.Vote.Candidate.Id)) // is vote for official candidate
                candidateVotes[ballot.Vote.Candidate.Id]++;
        }

        var maxVoteCount = candidateVotes.Max(x => x.Value);
        var ordered = candidateVotes.Where(x => x.Value == maxVoteCount).Select(x => x.Key).ToHashSet();
        var winners = candidates.Where(x => ordered.Contains(x.Id));

        if (winners.Count() > 1)
            throw new ElectionTieException(winners);

        return new ElectionResult(winners.Single(), maxVoteCount);
    }
}
