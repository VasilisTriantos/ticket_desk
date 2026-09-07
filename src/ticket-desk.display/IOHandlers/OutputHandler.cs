namespace ticket_desk.display.IOHandlers;

using ticket_desk.domain.Dto;
using ticket_desk.domain.Entities;

public static class OutputHandler
{
    private const string Separator = "--------------------------------------------------";
    public static void ShowSingleTicket(Ticket? ticket)
    {
        Console.WriteLine(Separator);
        
        if (ticket is null)
        {    
            Console.WriteLine("No tickets found");   
        }
        else
        {
            Console.WriteLine($"ID: {ticket.Id} || Title: {ticket.Title} || Description: {ticket.Description} || Priority: {ticket.Priority} || Status: {ticket.Status} || CreatedAt: {ticket.CreatedAt} || ClosedAt: {ticket.ClosedAt}");
        }
        
        Console.WriteLine(Separator);
    }

    public static void ShowMultipleTickets(IEnumerable<Ticket>? tickets)
    {
        if (tickets is null || !tickets.Any())
        {
            ShowSingleTicket(null);
            return;
        }

        foreach (var ticket in tickets ?? [])
        {
            ShowSingleTicket(ticket);
        }
    }

    public static void ShowTicketStatistics(TicketStatistics? ticketStatistics)
    {
        Console.WriteLine(Separator);

        if (ticketStatistics is null)
        {
            Console.WriteLine("No ticket statistics available");
        }
        else
        {
            Console.WriteLine($"Total Tickets: {ticketStatistics.TotalTickets} || Open Tickets: {ticketStatistics.OpenTickets} || Closed Tickets: {ticketStatistics.ClosedTickets} || High Priority Tickets: {ticketStatistics.HighPriorityTickets} || Medium Priority Tickets: {ticketStatistics.MediumPriorityTickets} || Low Priority Tickets: {ticketStatistics.LowPriorityTickets}");
        }

        Console.WriteLine(Separator);
    }
}