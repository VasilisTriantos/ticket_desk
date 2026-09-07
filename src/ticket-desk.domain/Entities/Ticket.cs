namespace ticket_desk.domain.Entities;

using ticket_desk.domain.ValueObjects;

public sealed class Ticket
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public PriorityLevel Priority { get; private set; }
    public Status Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ClosedAt { get; private set; }


    private Ticket (string title, string description, PriorityLevel priority, Status status)
    {
        ValidateTicket(title, description);
        Id = Guid.CreateVersion7();
        Title = title;
        Description = description;
        Priority = priority;
        Status = status;
        CreatedAt = DateTime.UtcNow;
        ClosedAt = null;
    }

    public static Ticket Create (string title, string description, PriorityLevel priority)
    {

        return new Ticket(title, description, priority, Status.Open);
    }

    public void Close()
    {
        if (Status == Status.Closed)
            throw new InvalidOperationException("Ticket is already closed.");

        Status = Status.Closed;
        ClosedAt = DateTime.UtcNow;
    }

    public void EscalatePriority()
    {
        if (Status == Status.Closed)
            throw new InvalidOperationException("Cannot change priority of a closed ticket.");

        Priority = Priority.Escalate();
    }

    public void DeescalatePriority()
    {
        if (Status == Status.Closed)
            throw new InvalidOperationException("Cannot change priority of a closed ticket.");

        Priority = Priority.Deescalate();
    }

    private static void ValidateTicket(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.", nameof(description));
    }
}
