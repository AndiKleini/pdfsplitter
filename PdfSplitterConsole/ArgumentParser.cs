using System;

namespace PdfSplitterConsole
{
    public static class ArgumentParser
    {
        private const string DefaultOutputPath = "./";
        private readonly static string[] commandSwitches = { "-s", "-o" };

        public static SplittingArguments Parse(string[] arguments)
        {
            if (arguments == null || arguments.Length == 0)
            {
                throw new ArgumentException("Arguments are missing.");
            }
            SplittingArguments splittingArguments = ExtractArgumentsFrom(arguments);
            CompleteWithDefaultValuesOrThrow(splittingArguments);
            return splittingArguments;

            static void CompleteWithDefaultValuesOrThrow(SplittingArguments splittingArguments)
            {
                if (splittingArguments.OutputPath == null)
                {
                    splittingArguments.OutputPath = DefaultOutputPath;
                }

                if (splittingArguments.InputPath == null)
                {
                    throw new ArgumentException($"Input path not specified. Check whether you have applied -s correctly.");
                }
            }

            static SplittingArguments ExtractArgumentsFrom(string[] arguments)
            {
                SplittingArguments splittingArguments = new();
                string? currentSwitch = null;
                return arguments.Aggregate(
                    new(),
                    (Func<SplittingArguments, string, SplittingArguments>)((currentSplittingArguments, argument) =>
                    {
                        if (commandSwitches.Contains(argument))
                        {
                            currentSwitch = argument;
                        }
                        else
                        {
                            Action<string> propertySetter = currentSwitch switch
                            {
                                "-s" => extractedValue => currentSplittingArguments.InputPath += (currentSplittingArguments.InputPath == null ? String.Empty : " ") + extractedValue,
                                "-o" => extractedValue => currentSplittingArguments.OutputPath += (currentSplittingArguments.OutputPath == null ? String.Empty : " ") + extractedValue,
                                _ => throw new ArgumentException($"Unsupported switch {currentSwitch} supplied. Check your input arguments."),
                            };
                            propertySetter(argument);
                        }
                        return currentSplittingArguments;
                    }));
            }
        }
    }
}