using Elections;
using Elections.Ballots;
using Elections.Elections;
using System.Diagnostics;
using Elections.Exceptions;
using System.Reflection;
using Elections.Objects;

const int numVoters = 100_000;
var voters = Voters.Create(numVoters, Candidates.Official);

RunSimpleElection(voters);

RunRankedChoiceElection(voters);

static void RunSimpleElection(IReadOnlyList<IVoter> voters)
{
    var ballots = SimpleBallotFactory.Create(voters, Candidates.Official);
    RunElection<SimplePluralityElection, ISimpleBallot>(ballots, "Simple Plurality Election");
}

static void RunRankedChoiceElection(IReadOnlyList<IVoter> voters)
{
    var ballots = RankedChoiceBallotFactory.Create(voters, Candidates.Official);
    RunElection<RankedChoiceElection, IRankedChoiceBallot>(ballots, "Ranked Choice Election");
}

static void RunElection<TElection, TBallot>(IReadOnlyList<TBallot> ballots, string electionDescription)
    where TElection : IElection<TBallot>, new()
    where TBallot : IBallot
{
    var stopwatch = Stopwatch.StartNew();
    try
    {
        var election = new TElection();
        var result = election.Run(ballots, Candidates.Official);
        PrintResult($"{electionDescription} - Winner is {result.Candidate.Name} with {result.VoteCount} votes", stopwatch.Elapsed);
    }
    catch (ElectionTieException tie)
    {
        PrintResult($"{electionDescription} - {tie.Message}", stopwatch.Elapsed);
    }
    catch (NoWinnerException noWinner)
    {
        PrintResult($"{electionDescription} - {noWinner.Message}", stopwatch.Elapsed);
    }
}

static void PrintResult(string message, TimeSpan elapsed)
{
    Console.WriteLine($"{message} [{elapsed.TotalMilliseconds} ms]");
}