namespace ticket_desk.core.LookupSpecifications;

using domain.Entities;

public sealed class TicketStatusOrPrioritySpecification(string searchTerm)
{
    private readonly string _searchTerm = searchTerm.Trim();

    public bool IsSatisfiedBy(Ticket ticket)
    {
        ArgumentNullException.ThrowIfNull(ticket);

        var values = new object?[]
        {
            ticket.Priority,
            ticket.Status
        };

        return values.Any(value => value?.ToString()?.Contains(
            _searchTerm,
            StringComparison.OrdinalIgnoreCase) == true);
    }
}