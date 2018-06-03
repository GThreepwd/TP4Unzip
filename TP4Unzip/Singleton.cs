using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TP4Unzip
{
  public class Singleton<TSingleton> : IDisposable
  {
    #region Attributes

    // Hilfsfeld für eine sichere Threadsynchronisierung
    private static readonly object SyncRoot;

    private static TSingleton mInstance;

    private static bool mDisposed;

    #endregion

    #region Constructors

    static Singleton()
    {
      SyncRoot = new object();

      mInstance = default(TSingleton);

      mDisposed = false;
    }

    #endregion

    #region Methods

    public void Dispose()
    {
      Dispose(true);

      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!mDisposed)
      {
        if (disposing)
        {
          // Verwalteten Zustand (verwaltete Objekte) entsorgen
        }

        mDisposed = true;
      }
    }

    #endregion

    #region Properties

    public static TSingleton Instance
    {
      [DebuggerStepThrough]
      get
      {
        try
        {
          if (mInstance == null || mDisposed)
          {
            lock (SyncRoot)
            {
              if (mInstance == null || mDisposed)
              {
                var lType = typeof(TSingleton);

                var lConstructors = lType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);

                if (lConstructors.Length <= 0)
                  throw new NotSupportedException(string.Format("Die Klasse '{0}' benötigt einen nicht-public Instance-Konstruktor für das Singleton-Template.", lType));

                var lConstructor = lConstructors[0];

                mInstance = (TSingleton)lConstructor.Invoke(null);
                mDisposed = false;
              }
            }
          }
        }
        catch (Exception ex)
        {
          throw ex;

          // Logger.LogError("Singleton konnte nicht erstellt werden!");
          // Logger.LogError(ex);
        }

        return mInstance;
      }
    }

    #endregion
  }
}
