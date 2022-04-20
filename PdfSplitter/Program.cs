using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.IO;

namespace DocumentSplitter
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var arguments = ArgumentParser.Parse(args);
                IEnumerable<string> cannotExecuteArguments = null;
                if (!arguments.CanExecute(out cannotExecuteArguments))
                {
                    Console.WriteLine("Unable to execute application.");
                    var cannotExecuteEnumerator = cannotExecuteArguments.GetEnumerator();
                    while (cannotExecuteEnumerator.MoveNext())
                    {
                        Console.WriteLine(cannotExecuteEnumerator.Current);
                    }
                    return;
                }
                DocumentSplitter.SplitByPages(
                        new PdfDocument(new PdfReader(arguments.InputPath)),
                        arguments.OutputPath,
                        Path.GetFileNameWithoutExtension(arguments.InputPath));
            } catch (Exception ex)
            {
                Console.Write(ex);
                Console.WriteLine("Could not parse due to internal error");
                Console.ReadKey();
            }
        }
    }
}
