using Elections.Exceptions;
using Elections.Objects;

namespace Elections.Elections;

public class RankedChoiceElection : IElection<IRankedChoiceBallot>
{
    public ElectionResult Run(IReadOnlyList<IRankedChoiceBallot> ballots, IReadOnlyList<ICandidate> candidates)
    {
        throw new NoWinnerException();
    }
}
