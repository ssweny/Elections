namespace Elections.Interfaces;

public interface IRankedChoiceBallot : IBallot
{
    IReadOnlyList<IRankedVote> Votes { get; }
}
