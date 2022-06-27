using Newtonsoft.Json;

namespace CtrlV.Data
{
    public class UploadResponse
    {
        [JsonProperty]
        public float delay;

        [JsonProperty]
        public string link;

        [JsonProperty]
        public int state;

        [JsonProperty]
        public string time;

        [JsonProperty]
        public string token;
    }
}
