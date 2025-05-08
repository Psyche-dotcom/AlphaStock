using AlpaStock.Core.DTOs;
using AlpaStock.Core.DTOs.Request.Community;
using AlpaStock.Core.DTOs.Response.Community;
using AlpaStock.Core.Entities;

namespace AlpaStock.Infrastructure.Service.Interface
{
    public interface ICommunityService
    {
        Task<ResponseDto<IEnumerable<CommunityChannel>>> RetrieveChannel(string? userid);
        Task<ResponseDto<string>> AddChannel(string catId, string userid, string name);
        Task<ResponseDto<string>> AddUserToChannel(string userid, string channelid);
        Task<ResponseDto<string>> CreateCommunityCategory(string CategoryName, string UserId);
        Task<ResponseDto<IEnumerable<ChannelCategory>>> RetrieveCommunityCategory();
        Task<ResponseDto<IEnumerable<CommunityMesaagesReponse>>> RetrieveChannelMessages(string roomId, string userid);
        Task<ResponseDto<IEnumerable<ChannelRepDto>>> RetrieveCommunityCategory(string currentUserId);
        Task<ResponseDto<CommunityChannelMessage>> AddMessage(string RoomId, string message, string messageType, string sentById);

        Task<ResponseDto<IEnumerable<CommunityMesaagesReponse>>> RetrieveFavChannelMessages(string userid);
        Task<ResponseDto<string>> MessageLike(LikeReq req, string userid);
        Task<ResponseDto<string>> MessageUnLike(LikeReq req, string userid);
        Task<ResponseDto<string>> SaveMessageToFAV(LikeReq req, string userid);
        Task<ResponseDto<IEnumerable<CommunityMesaagesReply>>> RetrieveChannelMessagesReply(string messageid);
        Task<ResponseDto<ChannelMesageReply>> AddMessageReply(string MessageId, string message, string messageType, string sentById);
    }
}
