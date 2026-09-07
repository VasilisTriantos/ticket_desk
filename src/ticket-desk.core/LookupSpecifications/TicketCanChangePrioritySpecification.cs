namespace ticket_desk.core.LookupSpecifications;

using domain.Entities;
using ticket_desk.domain.ValueObjects;

public sealed class TicketCanChangePrioritySpecification(string? ticketId)
{
    public bool IsSatisfiedBy(Ticket ticket)
    {
        return ticket.Status != Status.Closed &&
            ticket.Id.ToString() == ticketId;
    }
}