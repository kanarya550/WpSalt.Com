using System;
using System.IO;
using System.Net;
using System.Text;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://api.wordpress.org/secret-key/1.1/salt");
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream resStream = response.GetResponseStream();

        byte[] recvBuffer = new byte[8192];
        StringBuilder htmlOutput = new StringBuilder();
        int count;

        do
        {
            count = resStream.Read(recvBuffer, 0, recvBuffer.Length);

            if (count != 0)
            {
                htmlOutput.Append(Encoding.ASCII.GetString(recvBuffer, 0, count));
            }
        } while (count > 0);

        salts.InnerHtml = System.Web.HttpUtility.HtmlEncode(
            htmlOutput.ToString());

        //salts.InnerHtml = salts.InnerHtml.Replace("define(", string.Format("{0}define(", "<br/>"));
    }
}