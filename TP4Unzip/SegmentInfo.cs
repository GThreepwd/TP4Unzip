using System;
using System.Linq;
using System.Text;

namespace TP4Unzip
{
  public class InfoSegment : Segment
  {
    public InfoSegment(long offset, byte[] segment) : base(offset, segment)
    {
      // Offset 2
      // ----------------------------> | Unknown
      // f792 dc50 0821 075b 0000 0000 | 0200 0000 0400 0000 2b06 0000

      //                               | --------> | A
      // f792 dc50 0821 075b 0000 0000 | 0200 0000 | 0400 0000 2b06 0000

      //                                           | --------> | B
      // f792 dc50 0821 075b 0000 0000 | 0200 0000 | 0400 0000 | 2b06 0000

      //                                                       | --------> | C
      // f792 dc50 0821 075b 0000 0000 | 0200 0000 | 0400 0000 | 2b06 0000 |
    }

    public void AddInfoBlock(byte[] infoBlock)
    {
      if(infoBlock == null)
        throw new ArgumentNullException(nameof(infoBlock));

      if(infoBlock.Length != 512)
        throw new ArgumentException("Die Länge des Segments muss 512 Bytes betragen", nameof(infoBlock));

      Name = infoBlock.Take(260).ToArray().GetNullString();

      // Next Block
      infoBlock = infoBlock.Skip(260).ToArray();

      Unknown = ByteArrayToString(infoBlock.Take(12).ToArray());

      DataSegmentNo = infoBlock.LittleEndianToUInt(12);

      JunkCount = infoBlock.LittleEndianToUInt(16);

      Size = infoBlock.LittleEndianToUInt(20);
    }

    public string Name { get; set; }

    public string Unknown { get; set; } = "";

    public uint DataSegmentNo { get; set; }

    public uint JunkCount { get; set; }

    public uint Size { get; set; }

    public static string ByteArrayToString(byte[] bytes)
    {
      var lSb = new StringBuilder(bytes.Length * 2);

      foreach(byte b in bytes)
        lSb.AppendFormat("{0:x2}", b);

      return lSb.ToString();
    }

    public override string ToString()
    {
      return string.Format("Info: SegmentNo=0x{0:X08}, DataSegmentNo=0x{1:X08}, DataJunks={2}, DataSize={3}, Name={4}",
         SegmentNo, DataSegmentNo, JunkCount, Size, Name);
    }
  }
}
