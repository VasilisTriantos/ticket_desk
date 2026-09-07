namespace ticket_desk.core.LookupSpecifications;

using domain.Entities;

public sealed class TicketSearchByIdSpecification(string? ticketId)
{
    private readonly string _ticketId = ticketId?.Trim() ?? string.Empty;

    public bool IsSatisfiedBy(Ticket ticket)
    {
        return ticket.Id.ToString().Contains(_ticketId, StringComparison.OrdinalIgnoreCase);
    }
}