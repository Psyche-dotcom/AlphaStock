namespace AlpaStock.Core.DTOs.Response.Community
{
    public class CommunityMesaagesReponse
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public string MessageType { get; set; }
        public string SentByImgUrl { get; set; }
        public string SenderName { get; set; }
        public int LikeCount { get; set; }
        public int UnLikeCount { get; set; }
        public bool IsLiked { get; set; }
        public bool IsSaved { get; set; }
        public bool IsUnLiked { get; set; }
        public DateTime Created { get; set; }
     
    }

    public class CommunityMesaagesReply
    {
        public string MessageId { get; set; }
        public string ReplyId { get; set; }
        public string Message { get; set; }
        public string MessageType { get; set; }
        public string SentByImgUrl { get; set; }
        public string SenderName { get; set; }
        public DateTime Created { get; set; }
    }
}
