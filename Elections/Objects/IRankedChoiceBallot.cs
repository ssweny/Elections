namespace Elections.Objects;

public interface IRankedChoiceBallot : IBallot
{
    IReadOnlyList<IRankedVote> Votes { get; }
}
