using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using TuesPechkin;

namespace SharePointHtmlConvertToPdfDownloadBrowser
{
    public static class Pdf
    {
        private static ThreadSafeConverter _converter;
        private static readonly IToolset toolset = new PdfToolset(new Win64EmbeddedDeployment(new TempFolderDeployment()));

        private static ThreadSafeConverter Converter
        {
            get
            {
                if (_converter == null) { _converter = new ThreadSafeConverter(toolset); }
                return _converter;
            }
        }

        public static byte[] ConvertToPdf(string @html)
        {
            ObjectSettings settings = new ObjectSettings
            {
                HtmlText = @html,
                WebSettings = new WebSettings
                {
                    DefaultEncoding = "UTF-8",
                    LoadImages = true,
                    PrintBackground = true,
                    EnableJavascript = true,
                    EnableIntelligentShrinking = false,
                }
            };
            
            HtmlToPdfDocument document = new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    PaperSize = PaperKind.A4,
                    Orientation = GlobalSettings.PaperOrientation.Portrait,
                    UseCompression=true,
                    Margins = {
                        Unit = Unit.Centimeters,
                        All=0
                    },
                },
                Objects =
                {
                   settings
                }
            };

            return Converter.Convert(document);
        }
    }
}
