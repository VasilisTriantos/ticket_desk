namespace ticket_desk.core.IOHandlers;

public static class InputHandler
{
    public static string? ReadInput(string prompt)
    {
        Console.Write(prompt);
        var input = Console.ReadLine();
        Console.WriteLine();
        
        return input;
    }
}
