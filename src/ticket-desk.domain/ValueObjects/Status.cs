namespace ticket_desk.domain.ValueObjects;

public sealed class Status : IEquatable<Status>
{
    public string State { get; }
    public static readonly Status Open = new("Open");
    public static readonly Status Closed = new("Closed");

    private Status(string state)
    {
        State = state;
    }
    public bool Equals(Status? other) {
        return other is not null && State == other.State;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Status);
    }

    public override int GetHashCode() => State.GetHashCode();
    public override string ToString() => State.ToString();

}