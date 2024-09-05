using System.Timers;

namespace WinReform.Tests.Process.Fixture;

/// <summary>
/// Defines a class that represents a fixture of a process without a window
/// NOTE: The application automatically closes after x seconds, this to make sure that if the test failed and the dispose isn't triggered the app won't linger in the background forever.
/// </summary>
internal static class Program
{
    /// <summary>
    /// <see cref="Timer"/> that triggers an automatic close of the application
    /// </summary>
    private static System.Timers.Timer? s_autoCloseTimer;

    /// <summary>
    /// Entry point of the application
    /// </summary>
    /// <param name="args">Arguments passed on startup</param>
    private static void Main()
    {
        s_autoCloseTimer = new System.Timers.Timer(20000); // 20 seconds
        s_autoCloseTimer.Elapsed += OnAutoCloseTimer;
        s_autoCloseTimer.Start();

        Console.ReadKey();
    }

    /// <summary>
    /// Raised when the <see cref="s_autoCloseTimer"/> elapsed and automatically closes the application.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An <see cref="ElapsedEventArgs"/> that contains the event data.</param>
    private static void OnAutoCloseTimer(object? sender, ElapsedEventArgs e)
    {
        if (sender == null)
        {
            return;
        }

        // Safely dispose of the timer only if it's not null
        if (s_autoCloseTimer != null)
        {
            s_autoCloseTimer.Close();
            s_autoCloseTimer.Dispose();
            s_autoCloseTimer = null;
        }

        Environment.Exit(0);
    }
}
