using System;
using System.Threading;

namespace TP4Unzip
{
  public class EventService : Singleton<EventService>
  {
    private static SynchronizationContext mSyncContext;

    // Die Klasse benötigt einen nicht-public Instance-Konstruktor für das Singleton-Template
    private EventService()
    {
      mSyncContext = new SynchronizationContext();
    }

    public static void SetSynchronizationContext(SynchronizationContext syncContext)
    {
      mSyncContext = syncContext ?? throw new ArgumentNullException(nameof(syncContext));
    }

    public event EventHandler<MessageEventArgs> OnMessage;

    public static void CreateMsg(object sender, string text)
    {
      CreateMsg(sender, text, null);
    }

    public static void CreateMsg(object sender, string msg, params object[] args)
    {
      var lMessage = msg;

      if (args != null && args.Length > 0)
        lMessage = string.Format(msg, args);

      if (Instance.OnMessage != null)
        mSyncContext?.Send(d => Instance.OnMessage(sender, new MessageEventArgs(lMessage)), null);
    }
  }
}
