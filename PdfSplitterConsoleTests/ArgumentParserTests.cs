using FluentAssertions;
using NUnit.Framework;
using PdfSplitterConsole;

namespace PdfSplitterConsoleTests
{
    [TestFixture]
    public class ArgumentParserTests
    {
        [Test]
        public void Parse_InputPathOnlySupplied_ReturnsArgumentInstance()
        {
            const string InputFilePath = "./Data/TestData.pdf";

            const string DefaultOutputPath = "./";
            SplittingArguments instanceUnderTest = ArgumentParser.Parse(new string[] { "-s",  InputFilePath });

            instanceUnderTest.Should().BeEquivalentTo(
                new SplittingArguments()
                {
                    InputPath = InputFilePath,
                    OutputPath = DefaultOutputPath
                });
        }

        [Test]
        public void Parse_OutputPathSupplied_ReturnsArgumentInstanceWithProperOutputPath()
        {
            const string InputFilePath = "somethingThatIsNotRelevantForThisTest";
            const string OutputPath = "./Data";
            SplittingArguments instanceUnderTest = ArgumentParser.Parse(new string[] { "-s", InputFilePath, "-o", OutputPath });

            instanceUnderTest.OutputPath.Should().BeEquivalentTo(OutputPath);
        }

        [Test]
        public void Parse_OutputPathSuppliedBeforeInputPath_ReturnsArgumentInstanceWithProperOutputPath()
        {
            const string InputFilePath = "somethingThatIsNotRelevantForThisTest";
            const string OutputPath = "./Data";
            SplittingArguments instanceUnderTest = ArgumentParser.Parse(new string[] { "-o", OutputPath , "-s", InputFilePath });

            instanceUnderTest.OutputPath.Should().BeEquivalentTo(OutputPath);
        }

        [Test]
        public void Parse_InvalidSwitchSpecifiedForInputPath_Throws()
        {
            const string InputFilePath = "somethingThatIsNotRelevantForThisTest";
            Action testFunction = () =>  ArgumentParser.Parse(new string[] { "-unknown", InputFilePath });

            testFunction.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Parse_INoInputPathSpecified_Throws()
        {
            Action testFunction = () => ArgumentParser.Parse(new string[] { "-s" });

            testFunction.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Parse_EmptyArgumentsPassed_Throws()
        {
            Action testFunction = () => ArgumentParser.Parse(new string[] { });

            testFunction.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Parse_NullAsArgumentPassed_Throws()
        {
            Action testFunction = () => ArgumentParser.Parse(null);

            testFunction.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Parse_OutputPathContainsWhiteSpaces_ReturnsArgumentInstanceWithProperOuitputPath()
        {
            const string InputFilePath = "./Data";
            const string OutputPathPart1 = "something";
            const string OutputPathPart2 = "containing";
            const string OutputPathPart3 = "whitespaces";
            const string CombinedOutPutPath = OutputPathPart1 + " " + OutputPathPart2 + " " + OutputPathPart3;
            SplittingArguments instanceUnderTest = ArgumentParser.Parse(new string[] { "-s", InputFilePath, "-o", OutputPathPart1, OutputPathPart2, OutputPathPart3 });

            instanceUnderTest.OutputPath.Should().BeEquivalentTo(CombinedOutPutPath);
        }
    }
}
