using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DocumentSplitter
{
    public class SplittingArguments
    {
        public string InputPath { get; set; }
        public string OutputPath { get; set; }

        public bool CanExecute(out IEnumerable<string> cannotExecuteInfos)
        {
            List<string> tmpCannotExecuteInfos = null;
            if (!File.Exists(this.InputPath))
            {
                tmpCannotExecuteInfos = CreateIfNotExistsAndAdd($"InputPath {InputPath} does not exist.");
            } 
            else if (HasNotPdfFormat())
            {
                tmpCannotExecuteInfos = CreateIfNotExistsAndAdd($"InputPath {InputPath} is not a valid pdf.");
            }

            if (!Directory.Exists(this.OutputPath))
            {
                tmpCannotExecuteInfos = CreateIfNotExistsAndAdd($"OutputPath {OutputPath} does not exist or is not pointing to a directory.");
            }
            cannotExecuteInfos = tmpCannotExecuteInfos;
            return cannotExecuteInfos == null;

            List<string> CreateIfNotExistsAndAdd(string cannotExecuteInfo)
            {
                if (tmpCannotExecuteInfos == null)
                {
                    tmpCannotExecuteInfos = new();
                }
                tmpCannotExecuteInfos.Add(cannotExecuteInfo);
                return tmpCannotExecuteInfos;
            }

            bool HasNotPdfFormat()
            {
                bool cannotOpenAsPdf = false;
                PdfReader reader = null;
                try
                {
                    reader = new PdfReader(this.InputPath);
                }
                catch (iText.IO.Exceptions.IOException ex) when (ex.Message == "PDF header not found.")
                {
                    cannotOpenAsPdf = true;
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                }

                return cannotOpenAsPdf;
            }
        } 
    }
}