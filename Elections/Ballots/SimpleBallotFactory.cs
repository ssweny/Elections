﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using Elections.Objects;

namespace Elections.Ballots;

public static class SimpleBallotFactory
{
    public static IReadOnlyList<ISimpleBallot> Create(IReadOnlyList<IVoter> voters, IReadOnlyList<ICandidate> candidates)
    {
        return voters.Select(x => CreateSimpleBallot(x, candidates)).ToList();
    }

    private static SimpleBallot CreateSimpleBallot(IVoter voter, IReadOnlyList<ICandidate> candidates)
    {
        var vote = CreateSimpleVote(voter, candidates);
        return new SimpleBallot(voter, vote);
    }

    private static SimpleVote CreateSimpleVote(IVoter voter, IReadOnlyList<ICandidate> candidates)
    {
        if (voter is ICandidate candidate)
            return new SimpleVote(candidate);

        var voterCandidate = Candidates.SelectRandom(candidates);
        return new SimpleVote(voterCandidate);
    }

    private record SimpleBallot(IVoter Voter, IVote Vote) : ISimpleBallot;

    private record SimpleVote(ICandidate Candidate) : IVote;
}