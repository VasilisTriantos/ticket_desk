namespace ticket_desk.core.LookupSpecifications;

using domain.Entities;

public sealed class TicketSearchByIdSpecification(string? ticketId)
{
    public bool IsSatisfiedBy(Ticket ticket)
    {
        return ticket.Id.ToString() == ticketId;
    }
}