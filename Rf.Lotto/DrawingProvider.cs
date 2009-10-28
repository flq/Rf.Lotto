using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rf.Lotto
{
  public static class DrawingProvider
  {
    public static Drawing[] GetDrawings(Stream stream)
    {
      using (var f = new StreamReader(stream))
      {
        return (from l in enumerate(f) select new Drawing(l)).ToArray();
      }
    }

    public static Drawing[] GetDrawings()
    {
      return
        GetDrawings(
        typeof (DrawingProvider).Assembly
        .GetManifestResourceStream(typeof (DrawingProvider), "lotto.txt"));
    }

    private static IEnumerable<string> enumerate(StreamReader f)
    {
      while (!f.EndOfStream)
        yield return f.ReadLine();
    }
  }
}