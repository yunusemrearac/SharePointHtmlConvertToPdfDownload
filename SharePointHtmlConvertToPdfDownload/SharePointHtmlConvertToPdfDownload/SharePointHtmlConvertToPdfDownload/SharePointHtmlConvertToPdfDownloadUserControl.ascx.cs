using Microsoft.SharePoint;
using SharePointHtmlConvertToPdfDownloadBrowser;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SharePointHtmlConvertToPdfDownload.SharePointHtmlConvertToPdfDownload
{
    public partial class SharePointHtmlConvertToPdfDownloadUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                string fileName = tbFileName.Text;
                string html = tbPdfContent.Text;

                byte[] data = Pdf.ConvertToPdf(html);

                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
                Response.BufferOutput = false;
                Response.OutputStream.Write(data, 0, data.Length);
                Response.End();
            });
        }
    }
}
