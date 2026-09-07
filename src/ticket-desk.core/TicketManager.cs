namespace ticket_desk.core;

using domain.Entities;
using ticket_desk.domain.ValueObjects;
using ticket_desk.domain.Dto;
using ticket_desk.core.LookupSpecifications;

public class TicketManager
{
    private readonly List<Ticket> _tickets = [];

    public Ticket CreateTicket(string title, string description, string? priority)
    {
        var parsedPriority = PriorityLevel.ParseOrDefault(priority);
        
        var ticket = Ticket.Create(title, description, parsedPriority);
        _tickets.Add(ticket);
        return ticket;
    }

    public bool CloseTicket(string? ticketId)
    {
        if (string.IsNullOrWhiteSpace(ticketId))
            return false;

        Ticket? ticket;

        try
        {
            var specification = new TicketSearchByIdSpecification(ticketId);
            ticket = _tickets.SingleOrDefault(specification.IsSatisfiedBy);
        }
        catch (InvalidOperationException)
        {
            throw new InvalidOperationException(
                "The ticket ID is ambiguous. Please enter more of the ID.");
        }

        if (ticket is null)
            return false;

        ticket.Close();
        return true;
    }

    public IReadOnlyCollection<Ticket> GetAllTickets()
    {
        return _tickets.AsReadOnly();
    }

    public Ticket? GetTicketById(string ticketId)
    {
        if (string.IsNullOrWhiteSpace(ticketId))
            return null;

        var specification = new TicketSearchByIdSpecification(ticketId);
        return _tickets.AsReadOnly().FirstOrDefault(specification.IsSatisfiedBy);
    }

    public Ticket EscalateTicketPriority(string? ticketId)
    {
        var ticket = GetTicketById(ticketId ?? string.Empty)
            ?? throw new KeyNotFoundException($"Ticket with ID '{ticketId}' was not found.");
        
        ticket.EscalatePriority();
        
        return ticket;
    }

    public Ticket DeescalateTicketPriority(string? ticketId)
    {
        var ticket = GetTicketById(ticketId ?? string.Empty)
            ?? throw new KeyNotFoundException($"Ticket with ID '{ticketId}' was not found.");
        
        ticket.DeescalatePriority();
        
        return ticket;
    }

    public IReadOnlyCollection<Ticket> FilterTickets(string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return _tickets.AsReadOnly();

        return _tickets
            .Where(ticket => TicketStatusOrPrioritySpecification.IsSatisfiedBy(ticket, searchTerm))
            .ToList()
            .AsReadOnly();
    }
    
    public IReadOnlyCollection<Ticket> SearchByTitle(string? titleSearchTerm)
    {
        if (string.IsNullOrWhiteSpace(titleSearchTerm))
            return _tickets.AsReadOnly();

        var specification = new TicketSearchByTitleSpecification(titleSearchTerm);

        return _tickets
            .Where(specification.IsSatisfiedBy)
            .ToList()
            .AsReadOnly();
    }

    public TicketStatistics GetTicketStatistics()
    {
        return new TicketStatistics(
            _tickets.Count,
            _tickets.Count(t => t.Status == Status.Open),
            _tickets.Count(t => t.Status == Status.Closed),
            _tickets.Count(t => t.Priority == PriorityLevel.High),
            _tickets.Count(t => t.Priority == PriorityLevel.Medium),
            _tickets.Count(t => t.Priority == PriorityLevel.Low)
        );
    }
}