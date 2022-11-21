using Elections.Interfaces;

namespace Elections.Elections;

public class RankedChoiceElection : IElection<IRankedChoiceBallot>
{
    public ICandidate Run(IReadOnlyList<IRankedChoiceBallot> ballots, IReadOnlyList<ICandidate> candidates)
    {
        return null;
    }
}
