using Blocks.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Submission.Domain.ValueObjects;

public class FileExtensions
{
    public IReadOnlyList<string> Extensions { get; init; } = null!;

    public bool IsValidExtionsion(string extionsion)
        => Extensions.IsEmpty() || Extensions.Contains(extionsion, StringComparer.OrdinalIgnoreCase);
}
