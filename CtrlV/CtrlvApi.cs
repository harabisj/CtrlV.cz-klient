using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CtrlV
{
    class CtrlvApi
    {
        static readonly HttpClient client = new HttpClient();

        public static string UploadImage(string uploadUrl, string imgPath, string fileparameter = "file")
        {
            HttpWebRequest request = WebRequest.Create(uploadUrl) as HttpWebRequest;
            request.AllowAutoRedirect = true;
            request.Method = "POST";

            string boundary = DateTime.Now.Ticks.ToString("X"); //  Random separator
            request.ContentType = "	application/lukyer-binary-paster-data";
            byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

            int pos = imgPath.LastIndexOf("/");
            string fileName = imgPath.Substring(pos + 1);

            //Request header information
            StringBuilder sbHeader = new StringBuilder(string.Format("Content-Disposition:form-data;cookie:PHPSESSID=63e9c5ca523519cb74f75d32b63582ea;name=\"" + fileparameter + "\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n", fileName));
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());

            FileStream fs = new FileStream(imgPath, FileMode.Open, FileAccess.Read);
            byte[] bArr = new byte[fs.Length];
            fs.Read(bArr, 0, bArr.Length);
            fs.Close();

            Stream postStream = request.GetRequestStream();
            /*postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
            postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);*/
            postStream.Write(bArr, 0, bArr.Length);
            /*postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);*/
            postStream.Close();

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream instream = response.GetResponseStream();
            StreamReader sr = new StreamReader(instream, Encoding.UTF8);
            return sr.ReadToEnd();
        }

        public static void EditImage(string id, string token, int deleteType)
        {
            client.GetAsync(
                "https://ctrlv.cz/edit.php?"+
                "imgId=" + id +
                "&action=" + "liveliness" +
                "&token=" + token +
                "&deleteType=" + deleteType
            );
        }

        public static void DeleteImage(string id, string token)
        {
            client.GetAsync(
                "https://ctrlv.cz/edit.php?" +
                "imgId=" + id +
                "&action=" + "delete" +
                "&token=" + token
            );
        }
    }
}
