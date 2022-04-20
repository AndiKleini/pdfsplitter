using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using DocumentSplitter;
using System.Collections.Generic;

namespace PdfSplitterTests
{
    [TestFixture]
    public class SplittingArgumentsTests
    {
        [Test]
        public void CanExecute_InputPathNotFound_ReturnsFalseAndProperCannotExecuteInfo()
        {
            var instanceUnderTest = new SplittingArguments();
            const string nonExistingInputPath = "ThisPathDoesNotExist";
            instanceUnderTest.InputPath = nonExistingInputPath;
            IEnumerable<string> cannotExecuteInfos = null;

            bool canExecute = instanceUnderTest.CanExecute(out cannotExecuteInfos);

            using (new AssertionScope());
            canExecute.Should().BeFalse();
            cannotExecuteInfos.Should().ContainMatch("*InputPath*not*exist*");
        }

        [Test]
        public void CanExecute_OutputPathNotFound_ReturnsFalseAndProperCannotExecuteInfo()
        {
            var instanceUnderTest = new SplittingArguments();
            const string nonExistingOutPutPath = "ThisPathDoesNotExist";
            instanceUnderTest.OutputPath = nonExistingOutPutPath;
            IEnumerable<string> cannotExecuteInfos = null;

            bool canExecute = instanceUnderTest.CanExecute(out cannotExecuteInfos);

            using (new AssertionScope()) ;
            canExecute.Should().BeFalse();
            cannotExecuteInfos.Should().ContainMatch("*OutputPath*not*exist*");
        }

        [Test]
        public void CanExecute_OutputPathIsNotADirectory_ReturnsFalseAndProperCannotExecuteInfo()
        {
            var instanceUnderTest = new SplittingArguments();
            const string outputPatIsNotADirectoy = "./Data/TestData.pdf";
            instanceUnderTest.OutputPath = outputPatIsNotADirectoy;
            IEnumerable<string> cannotExecuteInfos = null;

            bool canExecute = instanceUnderTest.CanExecute(out cannotExecuteInfos);

            using (new AssertionScope()) ;
            canExecute.Should().BeFalse();
            cannotExecuteInfos.Should().ContainMatch("*OutputPath*not*exist*not*pointing*to*directory*");
        }


        [Test]
        public void CanExecute_InputPathIsNotAPdfFile_ReturnsFalseAndProperCannotExecuteInfo()
        {
            var instanceUnderTest = new SplittingArguments();
            const string inputPatIsNotAPdf = "./Data/TestData.docx";
            instanceUnderTest.InputPath = inputPatIsNotAPdf;
            IEnumerable<string> cannotExecuteInfos = null;

            bool canExecute = instanceUnderTest.CanExecute(out cannotExecuteInfos);

            using (new AssertionScope());
            canExecute.Should().BeFalse();
            cannotExecuteInfos.Should().ContainMatch("*InputPath*not*pdf*");
        }

        [Test]
        public void CanExecute_InputPathHasInproperPdfFileExtension_ReturnsFalseAndProperCannotExecuteInfo()
        {
            var instanceUnderTest = new SplittingArguments();
            const string inputPatIsNotAPdf = "./Data/IamNotReallyAPdf.pdf";
            instanceUnderTest.InputPath = inputPatIsNotAPdf;
            IEnumerable<string> cannotExecuteInfos = null;

            bool canExecute = instanceUnderTest.CanExecute(out cannotExecuteInfos);

            using (new AssertionScope()) ;
            canExecute.Should().BeFalse();
            cannotExecuteInfos.Should().ContainMatch("*InputPath*not*pdf*");
        }
    }
}
