namespace CtrlV2.Data
{
    public struct UploadResponse
    {
        public float Delay { get; set; }

        public string Link { get; set; }

        public int State { get; set; }

        public string Time { get; set; }

        public string Token { get; set; }

        public UploadResponse(float delay, string link, int state, string time, string token)
        {
            Delay = delay;
            Link = link;
            State = state;
            Time = time;
            Token = token;
        }
    }
}
