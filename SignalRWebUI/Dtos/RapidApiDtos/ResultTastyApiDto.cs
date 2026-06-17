namespace SignalRWebUI.Dtos.RapidApiDtos
{
    public class RootTastyApi
    {
        public List<ResultTastyApiDto> Results { get; set; }
    }
    public class ResultTastyApiDto
    {
        public string Name { get; set; }
        public string original_video_url { get; set; }
        public int total_time_minutes { get; set; }
        public string thumbnail_url { get; set; }
    }
}
