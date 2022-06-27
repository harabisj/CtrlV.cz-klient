using CtrlV.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CtrlV
{
    class CtrlvApi
    {
        static readonly HttpClient client = new HttpClient();

        public static string UploadImage(byte[] bytes)
        {
            HttpWebRequest request = WebRequest.Create("https://ctrlv.cz/upload.php") as HttpWebRequest;
            request.AllowAutoRedirect = true;
            request.Method = "POST";
            request.ContentType = "application/lukyer-binary-paster-data";

            Stream postStream = request.GetRequestStream();
            postStream.Write(bytes, 0, bytes.Length);
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

        public static Bitmap FetchImage(UploadedImage ui)
        {
            return FetchImage(
                "https://ctrlv.cz/shots/" +
                ui.year + "/" +
                ui.month + "/" +
                ui.day + "/" +
                ui.link + ".png"
            );
        }

        public static Bitmap FetchImage(string url)
        {
            WebRequest TmpRequest = WebRequest.Create(url);
            WebResponse TmpResponse = TmpRequest.GetResponse();
            Stream TmpReader = TmpResponse.GetResponseStream();
            Bitmap TmpImage = new Bitmap(TmpReader);
            return TmpImage;
        }
    }
}
