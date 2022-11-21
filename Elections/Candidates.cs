﻿using Elections.Objects;

namespace Elections;

public static class Candidates
{
    private static readonly IReadOnlyList<ICandidate> _official = GenerateOfficial().ToList();
    private static readonly IReadOnlyList<ICandidate> _writeIns = GenerateWriteIns().ToList();
    private const int _writeInFactor = 1337;

    public static IReadOnlyList<ICandidate> Official => _official;

    public static ICandidate SelectRandom(IReadOnlyList<ICandidate> candidates)
    {
        var candidatePool = UseWriteIn() ? _writeIns : candidates;
        return candidatePool[Random.Shared.Next(candidatePool.Count)];
    }

    private static IEnumerable<ICandidate> GenerateOfficial()
    {
        yield return new Candidate(10001, "Eric Adams");
        yield return new Candidate(10002, "Shaun Donovan");
        yield return new Candidate(10003, "Kathryn Garcia");
        yield return new Candidate(10004, "Raymond McGuire");
        yield return new Candidate(10005, "Dianne Morales");
        yield return new Candidate(10006, "Scott Stringer");
        yield return new Candidate(10007, "Maya Wiley");
        yield return new Candidate(10008, "Andrew Yang");
    }

    private static IEnumerable<ICandidate> GenerateWriteIns()
    {
        yield return new Candidate(int.MaxValue - 1, "John G. Roberts, Jr.");
        yield return new Candidate(int.MaxValue - 2, "Clarence Thomas");
        yield return new Candidate(int.MaxValue - 3, "Samuel A. Alito, Jr.");
        yield return new Candidate(int.MaxValue - 4, "Sonia Sotomayor");
        yield return new Candidate(int.MaxValue - 5, "Elena Kagan");
        yield return new Candidate(int.MaxValue - 6, "Neil M. Gorsuch");
        yield return new Candidate(int.MaxValue - 7, "Brett M. Kavanaugh");
        yield return new Candidate(int.MaxValue - 8, "Amy Coney Barrett");
        yield return new Candidate(int.MaxValue - 9, "Ketanji Brown Jackson");
    }

    private static bool UseWriteIn()
    {
        var randomNumber = Random.Shared.Next();
        return randomNumber % _writeInFactor == 0;
    }

    private record Candidate(int Id, string Name) : ICandidate, IVoter;
}
