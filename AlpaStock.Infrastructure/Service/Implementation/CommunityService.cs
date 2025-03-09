using AlpaStock.Core.Entities;
using AlpaStock.Core.Repositories.Interface;
using Microsoft.Extensions.Logging;

namespace AlpaStock.Infrastructure.Service.Implementation
{
    public class CommunityService
    {
        private readonly IAlphaRepository<ChannelCategory> _channelCategoryRepo;
        private readonly ILogger<CommunityService> _logger;
        private readonly IAlphaRepository<ChannelMesageReply> _channelMesageReplyRepo;
        private readonly IAlphaRepository<ChannelMessageLike> _channelMessageLikeRepo;
        private readonly IAlphaRepository<ChannelMessageUnLike> _ChannelMessageUnLikeRepo;
        private readonly IAlphaRepository<CommunityChannel> _communityChannelRepo;
        private readonly IAlphaRepository<CommunityChannelMessage> _communityChannelMessageRepo;
        private readonly IAlphaRepository<UserCommunityChannel> _userCommunityChannelMessageRepo;

        public CommunityService(IAlphaRepository<UserCommunityChannel> userCommunityChannelMessageRepo,
            IAlphaRepository<CommunityChannelMessage> communityChannelMessageRepo,
            IAlphaRepository<CommunityChannel> communityChannelRepo,
            IAlphaRepository<ChannelMessageUnLike> channelMessageUnLikeRepo,
            IAlphaRepository<ChannelMessageLike> channelMessageLikeRepo,
            IAlphaRepository<ChannelMesageReply> channelMesageReplyRepo,
            ILogger<CommunityService> logger,
            IAlphaRepository<ChannelCategory> channelCategoryRepo)
        {
            _userCommunityChannelMessageRepo = userCommunityChannelMessageRepo;
            _communityChannelMessageRepo = communityChannelMessageRepo;
            _communityChannelRepo = communityChannelRepo;
            _ChannelMessageUnLikeRepo = channelMessageUnLikeRepo;
            _channelMessageLikeRepo = channelMessageLikeRepo;
            _channelMesageReplyRepo = channelMesageReplyRepo;
            _logger = logger;
            _channelCategoryRepo = channelCategoryRepo;
        }
    }
}
