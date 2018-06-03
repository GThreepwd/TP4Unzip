using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace TP4Unzip
{
  static class Program
  {
    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    const int SW_HIDE = 0;
    const int SW_SHOW = 5;

    /// <summary>
    /// Der Haupteinstiegspunkt für die Anwendung.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
      // Add the event handler for handling UI thread exceptions to the event.
      Application.ThreadException += OnThreadException;

      // Set the unhandled exception mode to force for all Windows Forms errors.
      Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

      // Add the event handler for handling non-UI thread exceptions to the event. 
      AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

      if(args.Length > 0)
      {
        EventService.Instance.OnMessage += OnMessage;

        foreach(var lArg in args)
        {
          Console.WriteLine("Verarbeite Datei: {0}", lArg);
          Console.WriteLine("================================================================================");

          try
          {
            var lTp4File = new TP4File(lArg);

            lTp4File.Extract();
          }
          catch(Exception ex)
          {
            Console.WriteLine(ex.Message);
          }

          Console.WriteLine("================================================================================");
        }

        Console.WriteLine("Fertig");

        // Console.ReadLine();

        return;
      }

      var lPtr = GetConsoleWindow();

      // Konsole ausblenden
      ShowWindow(lPtr, SW_HIDE);

      Application.EnableVisualStyles();

      Application.SetCompatibleTextRenderingDefault(false);

      Application.Run(new AppMain());
    }

    private static void ConsoleMain(string[] args)
    {
      Console.WriteLine("Command line = {0}", Environment.CommandLine);

      for(int ix = 0; ix < args.Length; ++ix)
        Console.WriteLine("Argument{0} = {1}", ix + 1, args[ix]);

      Console.ReadLine();
    }

    private static void OnMessage(object sender, MessageEventArgs e)
    {
      Console.WriteLine(e.Text);
    }

    private static void OnThreadException(object sender, ThreadExceptionEventArgs e)
    {
      // Logger.LogError("ThreadException: {0}", e.Exception.Message);

      EventService.CreateMsg(null, string.Format("ThreadException: {0}", e.Exception.Message));
    }

    private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      var lEx = e.ExceptionObject as Exception;

      // Logger.LogError("UnhandledException: {0}", lEx.Message);

      EventService.CreateMsg(null, string.Format("UnhandledException: {0}", lEx.Message));
    }
  }
}
