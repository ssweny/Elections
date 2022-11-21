namespace Elections.Interfaces;

public interface ISimpleBallot : IBallot
{
    IVote Vote { get; }
}
