namespace ticket_desk.core.LookupSpecifications;

using domain.Entities;

public static class TicketStatusOrPrioritySpecification
{
    public static bool IsSatisfiedBy(Ticket ticket, string searchTerm)
    {
        ArgumentNullException.ThrowIfNull(ticket);
        ArgumentException.ThrowIfNullOrWhiteSpace(searchTerm);

        var normalizedSearchTerm = searchTerm.Trim();

        return ticket.Priority.ToString()
            .Contains(
                   normalizedSearchTerm,
                   StringComparison.OrdinalIgnoreCase) ||
               ticket.Status.ToString().Contains(
                   normalizedSearchTerm,
                   StringComparison.OrdinalIgnoreCase);
    }
}