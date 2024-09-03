# PdfSplitter

In my daily job I need to split large pdf documents on page level so that each page of a larger document ends up as a single pdf file. There a a couple of tools outside which can do that but I found nothing that is free and open source. 
That motivated me to create this tiny app here based on the itext7/7.2.1 library.

After compiling the project PdfSplitterConsoleyou can execute the command with the following arguments
* **-s** path to the source pdf file (the file you want to split up). In case of omitting a path and specifying filename only, "./filename" will be used as default.
* **-o** path to the output folder where the new created pdf filed are placed. In case of omitting this parameter "./" will be set as default.

```console
myName@myMacBook % ./PdfSplitterConsole -s /Users/myName/Downloads/some.pdf -o /Users/myName/Downloads/Extracted
```
