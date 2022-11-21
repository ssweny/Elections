using System.Collections.Generic;

namespace Elections.Objects;

public interface IBallot
{
    IVoter Voter { get; }
}
