using System.Collections.Generic;

namespace Elections.Objects;

public interface IElection<TBallot>
    where TBallot : IBallot
{
    ElectionResult Run(IReadOnlyList<TBallot> ballots, IReadOnlyList<ICandidate> candidates);
}
