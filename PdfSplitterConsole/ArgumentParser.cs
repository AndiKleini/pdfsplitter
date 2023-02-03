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
                    throw new ArgumentException("Input path not specified. Check whether you have applied -s correctly.");
                }
            }

            static SplittingArguments ExtractArgumentsFrom(string[] arguments)
            {
                SplittingArguments splittingArguments = new();
                Action<string>? addArgumentToCurrentSwitchProperty = null;
                return arguments.Aggregate(
                    new(),
                    (Func<SplittingArguments, string, SplittingArguments>)((currentSplittingArguments, argument) =>
                    {
                        if (commandSwitches.Contains(argument))
                        {
                            addArgumentToCurrentSwitchProperty = argument switch
                            {
                                "-s" => extractedValue => currentSplittingArguments.InputPath += (currentSplittingArguments.InputPath == null ? String.Empty : " ") + extractedValue,
                                "-o" => extractedValue => currentSplittingArguments.OutputPath += (currentSplittingArguments.OutputPath == null ? String.Empty : " ") + extractedValue,
                                _ => throw new ArgumentException($"Unsupported switch {argument} supplied. Check your input arguments."),
                            };
                        }
                        else if(addArgumentToCurrentSwitchProperty != null)
                        {
                            addArgumentToCurrentSwitchProperty(argument);
                        }
                        else
                        {
                            throw new ArgumentException("Invalid sequence of input arguments. Check if required switch (e.g.: -o, -s) is missing.");
                        }
                        return currentSplittingArguments;
                    }));
            }
        }
    }
}