namespace Elections.Objects;

public interface ISimpleBallot : IBallot
{
    IVote Vote { get; }
}
