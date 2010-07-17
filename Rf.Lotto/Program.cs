using System;
using System.IO;
using System.Linq;
using Rf.Lotto.Analysis;

namespace Rf.Lotto
{
  class Program
  {
    private static Drawing[] drawings;
    private static IAnalysis[] analysis;

    static void Main(string[] args)
    {
      drawings = DrawingProvider.GetDrawings();

      createAnalysisInstances();

      while (true)
      {
        for (int i = 0; i < analysis.Length;i++)
          Console.WriteLine("{0} - {1}", i, analysis[i]);
        Console.Write("Your selection (x ends the program): ");
        string s = Console.ReadLine();
        if (s == "x") break;
        runAnalysis(s);
        Console.WriteLine("Hit any key to continue...");
        Console.ReadLine();
      }

    }

    private static void runAnalysis(string input)
    {
      try
      {
        analysis[Int32.Parse(input)].Analyze(drawings);
      }
      catch (FormatException)
      {
        Console.WriteLine("Your input did not pass as a number.");
      }
      catch (ArgumentOutOfRangeException)
      {
        Console.WriteLine("The number does not point to an existing analysis.");
      }
    }

    private static void createAnalysisInstances()
    {
      analysis = (from t in typeof(Program).Assembly.GetTypes()
                  where t.GetInterfaces().Contains(typeof (IAnalysis))
                  select (IAnalysis)Activator.CreateInstance(t)).ToArray();
    }
  }
}
