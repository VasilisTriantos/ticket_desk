namespace ticket_desk.domain.ValueObjects;

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
}