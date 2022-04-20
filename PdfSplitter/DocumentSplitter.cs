using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using System.Linq;

namespace DocumentSplitter
{
    public class DocumentSplitter : iText.Kernel.Utils.PdfSplitter
    {
        private readonly string targetPath;
        private int currentPage = 1;
        private string targetFileName;

        public static void SplitByPages(PdfDocument pdfDocument, string targetPath, string targetFileName)
        {
            var pageSequence = Enumerable.Range(1, pdfDocument.GetNumberOfPages());
            var splitter = new DocumentSplitter(pdfDocument, targetPath, targetFileName);
            var pages = splitter.SplitByPageNumbers(pageSequence.ToList()).GetEnumerator();
            while (pages.MoveNext())
            {
                pages.Current.Close();
            }
        }

        private DocumentSplitter(PdfDocument document, string targetPath, string targetFileName) : base(document)
        {
            this.targetPath = targetPath;
            this.targetFileName = targetFileName;
        }

        protected override PdfWriter GetNextPdfWriter(PageRange documentPageRange)
        {
            return new PdfWriter($"{this.targetPath}/{this.targetFileName}_{this.currentPage++}.pdf");
        }
    }
}