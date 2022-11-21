namespace Elections.Objects;

public interface IRankedVote : IVote
{
    int Rank { get; }
}
