using FluentAssertions;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using NUnit.Framework;
using FluentAssertions.Execution;
using PdfSplitter;

namespace DocumentSplitterTests
{
    [TestFixture]
    [Category("RequiresCompareVisually")]
    public class Tests
    {
        [Test]
        public void SplitByPages_DocumentOfTwoPagesPassed_ForeachPageSeperateDocumentExtracted()
        {
            var src = "./Data/Bestellformular_VOR_Klimaticket.pdf";
            var pdfDocument = new PdfDocument(new PdfReader(src));
            const string TargetFileName = "Target";
            DocumentSplitter.SplitByPages(pdfDocument, "./", TargetFileName);

            using (new AssertionScope());
            ShouldBeEqual("./Data/Bestellformular_VOR_Klimaticket_Page_1.pdf", $"./{TargetFileName}_1.pdf");
            ShouldBeEqual("./Data/Bestellformular_VOR_Klimaticket_Page_2.pdf", $"./{TargetFileName}_2.pdf");
        }

        private static void ShouldBeEqual(string leftPdfFile, string rightPdfFile)
        {
            new CompareTool().CompareVisually(leftPdfFile, rightPdfFile, "./", "diff").Should().BeNull();
        }

        [Test]
        public void SplitByPages_DocumentOfMultiplePagesPassed_ForeachPageSeperateDocumentExtracted()
        {
            var src = "./Data/TestData.pdf";
            var pdfDocument = new PdfDocument(new PdfReader(src));
            const string TargetFileName = "TestDataReceived";
            DocumentSplitter.SplitByPages(pdfDocument, "./", TargetFileName);

            using (new AssertionScope());
            ShouldBeEqual("./Data/TestDataExpected_1.pdf", $"./{TargetFileName}_1.pdf");
            ShouldBeEqual("./Data/TestDataExpected_2.pdf", $"./{TargetFileName}_2.pdf");
            ShouldBeEqual("./Data/TestDataExpected_3.pdf", $"./{TargetFileName}_3.pdf");
            ShouldBeEqual("./Data/TestDataExpected_4.pdf", $"./{TargetFileName}_4.pdf");
        }
    }
}