using Elections;
using Elections.Interfaces;
using Elections.Ballots;
using Elections.Elections;
using System.Diagnostics;

const int numVoters = 100_000;
var voters = Voters.Create(numVoters, Candidates.Official);

RunSimpleElection(voters);

RunRankedChoiceElection(voters);

static void RunSimpleElection(IReadOnlyList<IVoter> voters)
{
    var ballots = SimpleBallotFactory.Create(voters, Candidates.Official);
    RunElection<SimpleElection, ISimpleBallot>(ballots, "Simple Majority Election");
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

    var election = new TElection();
    var winner = election.Run(ballots, Candidates.Official);

    Console.WriteLine($"The {electionDescription} winner is {winner?.Name} [{stopwatch.Elapsed.TotalMilliseconds} ms]");
}