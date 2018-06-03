using System;

namespace TP4Unzip
{
  public class Segment
  {
    private Segment()
    {
    }

    protected Segment(long offset, byte[] segment)
    {
      if(segment == null)
        throw new ArgumentNullException(nameof(segment));

      if(segment.Length != 14)
        throw new ArgumentException("Die Länge des Segments muss 14 Bytes betragen", nameof(segment));

      Offset = offset;

      /* 
      Segment example:
      ==================================
      0200 0000 0000 0000 0300 0000 0002
      0300 0000 0200 0000 0400 0000 0002
      0400 0000 0300 0000 1446 0000 0002
      1446 0000 0400 0000 0000 0000 3300
      ==================================
      */

      // --------> | SegmentNo (17940)
      // 1446 0000 | 0400 0000 | 0000 0000 | 3300
      SegmentNo = segment.LittleEndianToUInt(0);

      //           | --------> | PreviousSegment (4)
      // 1446 0000 | 0400 0000 | 0000 0000 | 3300
      SegmentPrevValue = segment.LittleEndianToUInt(4);

      //                       | --------> | NextSegment (17940)
      // 0400 0000 | 0300 0000 | 1446 0000 | 0002
      SegmentNextValue = segment.LittleEndianToUInt(8);
    }

    public static Segment Create(long offset, byte[] segment)
    {
      if(segment == null)
        throw new ArgumentNullException(nameof(segment));

      if(segment.Length != 14)
        throw new ArgumentException("Die Länge des Segments muss 14 Bytes betragen", nameof(segment));

      var lLength = segment.LittleEndianToInt32(12);

      if(lLength == 0)
        return new InfoSegment(offset, segment);
      else
        return new DataSegment(offset, segment);
    }

    public long Offset { get; set; }

    public uint SegmentNo { get; set; }

    public uint SegmentPrevValue { get; set; }

    public uint SegmentNextValue { get; set; }
  }
}
