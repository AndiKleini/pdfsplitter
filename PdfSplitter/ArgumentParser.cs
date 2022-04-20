using System;

namespace DocumentSplitter
{
    public static class ArgumentParser
    {
        private const string DefaultOutputPath = "./";

        public static SplittingArguments Parse(string[] arguments)
        {
            if (arguments == null || arguments.Length == 0)
            {
                throw new ArgumentException("Arguments are missing.");
            }
            SplittingArguments splittingArguments = new();
            var enumerator = arguments.GetEnumerator();
            while (enumerator.MoveNext())
            {
                string currentSwitch = (string)enumerator.Current;
                Action<string> propertySetter = currentSwitch switch
                {
                    "-s" => extractedValue => splittingArguments.InputPath = extractedValue,
                    "-o" => extractedValue => splittingArguments.OutputPath = extractedValue,
                    _ => throw new ArgumentException($"Unsupported switch {currentSwitch} supplied. Check your input arguments."),
                };
                if (enumerator.MoveNext())
                {
                    propertySetter((string)enumerator.Current);
                }
                else
                {
                    throw new ArgumentException($"Subsequent value for switch {currentSwitch} was missing. Check your input arguments.");
                }
            }
            CompleteWithDefaultValues(splittingArguments);
            return splittingArguments;

            static void CompleteWithDefaultValues(SplittingArguments splittingArguments)
            {
                if (splittingArguments.OutputPath == null)
                {
                    splittingArguments.OutputPath = DefaultOutputPath;
                }
            }  
        }
    }
}