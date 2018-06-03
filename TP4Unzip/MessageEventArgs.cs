using System;

namespace TP4Unzip
{
  public sealed class MessageEventArgs : EventArgs
  {
    public MessageEventArgs(string text)
    {
      Text = text;
    }

    public string Text
    {
      get;
      private set;
    }
  }
}
