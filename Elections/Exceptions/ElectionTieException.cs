using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elections.Objects;

namespace Elections.Exceptions;

public class ElectionTieException : Exception
{
    public ElectionTieException(IEnumerable<ICandidate> candidates) : base(FormatMessage(candidates))
    {
        Candidates = candidates;
    }

    public IEnumerable<ICandidate> Candidates { get; }

    private static string FormatMessage(IEnumerable<ICandidate> candidates)
    {
        var candidateList = string.Join(", ", candidates.OrderBy(x => x.Name).Select(x => x.Name));
        return $"A tie occurred between {candidates.Count()} candidates: {candidateList}";
    }
}

public class NoWinnerException : Exception
{
    public NoWinnerException() : base("No Winner determined")
    {

    }
}
