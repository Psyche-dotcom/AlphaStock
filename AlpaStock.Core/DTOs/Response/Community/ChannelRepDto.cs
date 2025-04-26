namespace AlpaStock.Core.DTOs.Response.Community
{
    public class ChannelRepDto
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<ChannelDto> Channels { get; set; }

    }
}
