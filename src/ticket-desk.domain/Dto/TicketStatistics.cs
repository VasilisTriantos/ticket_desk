namespace ticket_desk.domain.Dto;

public sealed record TicketStatistics 
(
    int TotalTickets,
    int OpenTickets,
    int ClosedTickets,
    int HighPriorityTickets,
    int MediumPriorityTickets,
    int LowPriorityTickets
);
