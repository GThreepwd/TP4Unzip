using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

static class ArrayExtensions
{
  private static readonly int[] Empty = new int[0];

  public static T[] Range<T>(this T[] data, int startIndex, int length)
  {
    T[] result = new T[length];

    Array.Copy(data, startIndex, result, 0, length);

    return result;
  }

  public static int[] FindAll(this byte[] self, byte[] candidate)
  {
    if (IsEmptyLocate(self, candidate))
      return Empty;

    var list = new List<int>();

    for (int i = 0; i < self.Length; i++)
    {
      if (!IsMatch(self, i, candidate))
        continue;

      list.Add(i);
    }

    return list.Count == 0 ? Empty : list.ToArray();
  }

  static bool IsMatch(byte[] array, int position, byte[] candidate)
  {
    if (candidate.Length > (array.Length - position))
      return false;

    for (int i = 0; i < candidate.Length; i++)
      if (array[position + i] != candidate[i])
        return false;

    return true;
  }

  static bool IsEmptyLocate(byte[] array, byte[] candidate)
  {
    return array == null
        || candidate == null
        || array.Length == 0
        || candidate.Length == 0
        || candidate.Length > array.Length;
  }

  public static uint LittleEndianToUInt(this byte[] data, int startIndex)
  {
    return (uint)((data[startIndex + 3] << 24)
                | (data[startIndex + 2] << 16)
                | (data[startIndex + 1] << 8)
                | (data[startIndex + 0]));
  }

  public static uint BigEndianToUInt(this byte[] data, int startIndex)
  {
    return (uint)((data[startIndex + 0] << 24)
                | (data[startIndex + 1] << 16)
                | (data[startIndex + 2] << 8)
                | (data[startIndex + 3]));
  }

  public static int LittleEndianToInt32(this byte[] data, int startIndex)
  {
    return (data[startIndex + 1] << 8)
         | (data[startIndex + 0]);
  }

  public static int BigEndianToInt32(this byte[] data, int startIndex)
  {
    return (data[startIndex + 0] << 8)
         | (data[startIndex + 1]);
  }

  public static string GetString(byte[] bytes, int length)
  {
    return GetString(bytes, 0, length);
  }

  public static string GetString(byte[] bytes, int startIndex, int length)
  {
    if(bytes == null)
      throw new ArgumentNullException(nameof(bytes));

    return Encoding.GetEncoding(1252).GetString(bytes, startIndex, length).TrimEnd(new char[] { '\0', ' ' });
  }

  public static string GetUnicodeString(byte[] bytes, int startIndex, int length)
  {
    if(bytes == null)
      throw new ArgumentNullException(nameof(bytes));

    return Encoding.Unicode.GetString(bytes, startIndex, length).TrimEnd(new char[] { '\0', ' ' });
  }

  public static string GetNullString(this byte[] bytes)
  {
    if(bytes == null)
      throw new ArgumentNullException(nameof(bytes));

    var lStr = string.Empty;

    // Search for 0
    var lIndex = Array.FindIndex(bytes, 0, x => x == 0);

    if(lIndex >= 0)
    {
      lStr = Encoding.GetEncoding(1252).GetString(bytes, 0, lIndex);
    }

    return lStr.TrimEnd(new char[] { '\0', ' ' });
  }
}
