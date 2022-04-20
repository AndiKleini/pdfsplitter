using FluentAssertions;
using NUnit.Framework;
using DocumentSplitter;
using System;

namespace PdfSplitterTests
{
    [TestFixture]
    public class ArgumentParserTests
    {
        [Test]
        public void From_InputPathOnlySupplied_ReturnsArgumentInstance()
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
        public void From_OutputPathSupplied_ReturnsArgumentInstanceWithProperOutputPath()
        {
            const string InputFilePath = "somethingThatIsNotRelevantForThisTest";
            const string OutputPath = "./Data";
            SplittingArguments instanceUnderTest = ArgumentParser.Parse(new string[] { "-s", InputFilePath, "-o", OutputPath });

            instanceUnderTest.OutputPath.Should().BeEquivalentTo(OutputPath);
        }

        [Test]
        public void From_OutputPathSuppliedBeforeInputPath_ReturnsArgumentInstanceWithProperOutputPath()
        {
            const string InputFilePath = "somethingThatIsNotRelevantForThisTest";
            const string OutputPath = "./Data";
            SplittingArguments instanceUnderTest = ArgumentParser.Parse(new string[] { "-o", OutputPath , "-s", InputFilePath });

            instanceUnderTest.OutputPath.Should().BeEquivalentTo(OutputPath);
        }

        [Test]
        public void From_InvalidSwitchSpecifiedForInputPath_Throws()
        {
            const string InputFilePath = "somethingThatIsNotRelevantForThisTest";
            Action testFunction = () =>  ArgumentParser.Parse(new string[] { "-unknown", InputFilePath });

            testFunction.Should().Throw<ArgumentException>();
        }

        [Test]
        public void From_INoInputPathSpecified_Throws()
        {
            Action testFunction = () => ArgumentParser.Parse(new string[] { "-s" });

            testFunction.Should().Throw<ArgumentException>();
        }

        [Test]
        public void From_EmptyArgumentsPassed_Throws()
        {
            Action testFunction = () => ArgumentParser.Parse(new string[] { });

            testFunction.Should().Throw<ArgumentException>();
        }

        [Test]
        public void From_NullAsArgumentPassed_Throws()
        {
            Action testFunction = () => ArgumentParser.Parse(null);

            testFunction.Should().Throw<ArgumentException>();
        }
    }
}
