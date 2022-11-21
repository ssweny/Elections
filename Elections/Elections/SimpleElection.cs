using Elections.Interfaces;

namespace Elections.Elections;

public class SimpleElection : IElection<ISimpleBallot>
{
    public ICandidate Run(IReadOnlyList<ISimpleBallot> ballots, IReadOnlyList<ICandidate> candidates)
    {
        return null;
    }
}
