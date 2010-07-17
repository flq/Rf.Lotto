using System.IO;
using System.Linq;

namespace Rf.Lotto
{
  public class FileSorter
  {
    public FileSorter(string pathToOrigin, string newFile)
    {
      var drawings = DrawingProvider.GetDrawings(File.Open(pathToOrigin, FileMode.Open));

      var ordered = drawings.OrderByDescending(d => d.DayOfDraw);

      using (var w = File.CreateText(newFile))
        foreach (var d in ordered)
          d.Write(w);
    }
  }
}