namespace ticket_desk.core.LookupSpecifications;

using domain.Entities;
using ticket_desk.domain.ValueObjects;

public sealed class TicketCanChangePrioritySpecification(string? ticketId)
{
    private readonly string _ticketId = ticketId?.Trim() ?? string.Empty;

    public bool IsSatisfiedBy(Ticket ticket)
    {
        return ticket.Status != Status.Closed &&
            ticket.Id.ToString().Contains(_ticketId, StringComparison.OrdinalIgnoreCase);
    }
}