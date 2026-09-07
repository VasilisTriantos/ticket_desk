namespace ticket_desk.core.LookupSpecifications;

using domain.Entities;

public sealed class TicketSearchByTitleSpecification(string? titleSearchTerm)
{
    public bool IsSatisfiedBy(Ticket ticket)
    {
        return ticket.Title.Contains(titleSearchTerm ?? string.Empty, StringComparison.OrdinalIgnoreCase);
    }
}