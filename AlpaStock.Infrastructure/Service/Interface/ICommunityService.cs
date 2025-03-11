using AlpaStock.Core.DTOs;
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
    }
}
