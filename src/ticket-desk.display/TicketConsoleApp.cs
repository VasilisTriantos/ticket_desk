namespace ticket_desk.display;

using ticket_desk.core;
using ticket_desk.core.InputModel;
using ticket_desk.core.IOHandlers;

class TicketConsoleApp
{
    private bool IsRunning = true;
    private readonly TicketManager core = new();
    public void Run(string[] args)
    {
        while (IsRunning)
        {
            DisplayMenu();
            HandleMenuSelection();
            if (!IsRunning)
            {
                break;
            }
        }
    }

    private static void DisplayMenu()
    {
        Console.WriteLine("1. Create ticket");
        Console.WriteLine("2. Read all tickets");
        Console.WriteLine("3. Read ticket by ID");
        Console.WriteLine("4. Escalate Ticket Priority");
        Console.WriteLine("5. Deescalate Ticket Priority");
        Console.WriteLine("6. Filter Tickets by status or priority");
        Console.WriteLine("7. Search by Title");
        Console.WriteLine("8. View Ticket Statistics");
        Console.WriteLine("9. Close Tickets");
        Console.WriteLine("Q. Quit");
    }

    private void HandleMenuSelection()
    {
        var choice = Console.ReadLine();

        switch (choice?.ToUpper())
        {
            case "1":
                try
                {
                    Console.WriteLine("Creating a ticket...");

                    var inputModel = new InputModel(
                        InputHandler.ReadInput("Enter ticket title: ") ?? "Default Title",
                        InputHandler.ReadInput("Enter ticket description: ") ?? "Default Description",
                        InputHandler.ReadInput("Enter ticket priority:  (High, Medium, Low)")
                    );

                    core.CreateTicket(inputModel.Title, inputModel.Description, inputModel.Priority);
                    Console.WriteLine("Ticket created successfully.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Could not create a ticket \n{ex.Message}");
                }
                break;
            case "2":
                var allTickets = core.GetAllTickets();
                OutputHandler.ShowMultipleTickets(allTickets);
                break;
            case "3":
                var ticketId = InputHandler.ReadInput("Enter ticket ID: ");
                var ticket = core.GetTicketById(ticketId ?? string.Empty);
                OutputHandler.ShowSingleTicket(ticket);
                break;
            case "4":
                try
                {
                    var changeTicketId = InputHandler.ReadInput("Enter ticket ID to change priority: ");
                    core.EscalateTicketPriority(changeTicketId);
                    Console.WriteLine("Ticket priority updated successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"There was a problem with changing the ticket priority\n{ex.Message}");
                }
                break;
            case "5":
                try
                {
                    var changeTicketId = InputHandler.ReadInput("Enter ticket ID to change priority: ");
                    core.DeescalateTicketPriority(changeTicketId);
                    Console.WriteLine("Ticket priority updated successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"There was a problem with changing the ticket priority\n{ex.Message}");
                }
                break;
            case "6":
                var searchTerm = InputHandler.ReadInput("Enter search term: ");
                var searchResults = core.FilterTickets(searchTerm);
                OutputHandler.ShowMultipleTickets(searchResults);
                break;
            case "7":
                var titleSearchTerm = InputHandler.ReadInput("Enter title search term: ");
                var titleSearchResults = core.SearchByTitle(titleSearchTerm);
                OutputHandler.ShowMultipleTickets(titleSearchResults);
                break;
            case "8":
                var ticketStatistics = core.GetTicketStatistics();
                OutputHandler.ShowTicketStatistics(ticketStatistics);
                break;
            case "9":
                try
                {
                    var closeTicketId = InputHandler.ReadInput("Enter ticket ID to close: ");

                    var message = core.CloseTicket(closeTicketId) ?
                        "Ticket closed successfully." :
                        "Failed to close the ticket. It may not exist or is already closed.";
                    Console.WriteLine(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"There was a problem with closing the ticket\n{ex.Message}");
                }
                break;
            case "Q":
                IsRunning = false;
                break;
            default:
                Console.WriteLine("Invalid selection. Please try again.");
                break;
        }
    }
}
