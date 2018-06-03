namespace TP4Unzip
{
  public class DataSegment : Segment
  {
    public DataSegment(long offset, byte[] segment) : base(offset, segment)
    {
      //                                   | ---> | Lenght (512)
      // 0400 0000 | 0300 0000 | 1446 0000 | 0002 |
      Length = segment.LittleEndianToInt32(12);
    }

    public int Length { get; set; }

    public override string ToString()
    {
      return string.Format("Data: SegmentNo=0x{0:X08}, Offset=0x{1:X08}, PrevSegment=0x{2:X08}, NextSegment=0x{3:X08}, Length={4}",
         SegmentNo, Offset, SegmentPrevValue, SegmentNextValue, Length);
    }
  }
}
