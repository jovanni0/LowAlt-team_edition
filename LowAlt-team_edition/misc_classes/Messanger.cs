namespace LowAlt_team_edition.misc_classes;

public class Messages
{
    protected void ShowError(string message) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    protected void ShowMessage(string message) {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    protected void WaitForConfirmation()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    protected void ShowErrorAndWaitConfirmation(string message)
    {
        ShowError(message);
        WaitForConfirmation();
    }

    protected void ShowMessageAndWaitConfirmation(string message)
    {
        ShowMessage(message);
        WaitForConfirmation();
    }

#region parsers
    /// <summary>
    /// parse a string to TimeOnly and catch potential exceptions.
    /// </summary>
    protected TimeOnly? ParseTimeOnly(string time)
    {
        TimeOnly parsedTime;
        try {
            parsedTime = TimeOnly.Parse(time);
        }
        catch (ArgumentNullException e) {
            ShowError("TimeOnly null exception.");
            return null;
        }
        catch (FormatException e) {
            ShowError($"TimeOnly format exception for {time}.");
            return null;
        }

        return parsedTime;
    }

    /// <summary>
    /// parse a string to int and catch potential exceptions.
    /// </summary>
    protected int? ParseInt(string parsable) 
    {
        int parsedValue;
        try {
            parsedValue = int.Parse(parsable);
        }
        catch (ArgumentNullException e) {
            ShowError("int null exception.");
            return null;
        }
        catch (FormatException e) {
            ShowError($"int format exception for {parsable}.");
            return null;
        }

        return parsedValue;
    }


    /// <summary>
    /// parse a string to greater-then-value int and catch potential exceptions.
    /// </summary>
    protected int? ParseGreaterThen_Int(string parsable, int value) 
    {
        int parsedValue;
        try {
            parsedValue = int.Parse(parsable);
        }
        catch (ArgumentNullException e) {
            ShowError("int null exception.");
            return null;
        }
        catch (FormatException e) {
            ShowError($"int format exception for {parsable}.");
            return null;
        }

        if (parsedValue <= value) {
            ShowError($"value {parsedValue} is not greater then {value}");
            return null;
        }

        return parsedValue;
    }

    /// <summary>
    /// parse a string to greater-or-equal-then-value int and catch potential exceptions.
    /// </summary>
    protected int? ParseGreaterOrEqualTo_Int(string parsable, int value) 
    {
        int parsedValue;
        try {
            parsedValue = int.Parse(parsable);
        }
        catch (ArgumentNullException e) {
            ShowError("int null exception.");
            return null;
        }
        catch (FormatException e) {
            ShowError($"int format exception for {parsable}.");
            return null;
        }

        if (parsedValue < value) {
            ShowError($"value {parsedValue} is not greater or equal then {value}");
            return null;
        }

        return parsedValue;
    }

#endregion
}