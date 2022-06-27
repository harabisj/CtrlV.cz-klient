using Newtonsoft.Json;

namespace CtrlV.Data
{
    public class UploadedImage
    {
        [JsonProperty]
        public string link;

        [JsonProperty]
        public string token;

        [JsonProperty]
        public string day;

        [JsonProperty]
        public string month;

        [JsonProperty]
        public string year;

        public UploadedImage() { }

        public UploadedImage(UploadResponse uploadResponse)
        {
            link = uploadResponse.link;
            token = uploadResponse.token;

            string[] date = uploadResponse.time.Split(' ')[0].Split('.');
            day = date[0];
            month = date[1];
            year = date[2];
        }
    }
}
