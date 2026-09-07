namespace ticket_desk.domain.ValueObjects;

using System.Diagnostics.CodeAnalysis;

public sealed class PriorityLevel : IEquatable<PriorityLevel>
{
    public static readonly PriorityLevel Low = new("Low", 1);
    public static readonly PriorityLevel Medium = new("Medium", 2);
    public static readonly PriorityLevel High = new("High", 3);
    

    public string Name { get; }
    public int Rank { get; }

    private PriorityLevel(string name, int rank)
    {
        Name = name;
        Rank = rank;
    }

    public PriorityLevel Escalate()
    {
        return this switch
        {
            var c when c == Low => Medium,
            var c when c == Medium => High,
            var c when c == High => High,
            _ => throw new InvalidOperationException("Invalid priority level.")
        };
    }

    public PriorityLevel Deescalate()
    {
        return this switch
        {
            var c when c == Low => Low,
            var c when c == Medium => Low,
            var c when c == High => Medium,
            _ => throw new InvalidOperationException("Invalid priority level.")

        };
    }

    public static PriorityLevel ParseOrDefault(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Medium;

        if (TryParse(input, out var priority))
            return priority;

        throw new ArgumentException("Priority must be Low, Medium, or High.",nameof(input));
    }

    public bool Equals(PriorityLevel? other)
    {
        return other is not null && Name == other.Name && Rank == other.Rank;
    }
    public override bool Equals(object? obj)
    {
        return Equals(obj as PriorityLevel);
    }

    public override int GetHashCode() => HashCode.Combine(Name, Rank);

    public override string ToString() => Name;

    private static bool TryParse(string? input, [NotNullWhen(true)] out PriorityLevel? priority)
    {
        priority = input?.Trim().ToLowerInvariant() switch
        {
            "low" => Low,
            "medium" => Medium,
            "high" => High,
            _ => null
        };

        return priority is not null;
    }
}