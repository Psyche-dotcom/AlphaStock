using AlpaStock.Core.DTOs;
using AlpaStock.Core.DTOs.Request.Community;
using AlpaStock.Core.DTOs.Response.Community;
using AlpaStock.Core.Entities;
using AlpaStock.Core.Repositories.Interface;
using AlpaStock.Infrastructure.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AlpaStock.Infrastructure.Service.Implementation
{
    public class CommunityService : ICommunityService
    {
        private readonly IAlphaRepository<ChannelCategory> _channelCategoryRepo;
        private readonly ILogger<CommunityService> _logger;
        private readonly IAccountRepo _accountRepo;
        private readonly IAlphaRepository<FavoriteChannelMessage> _favoriteChannelMessageRepo;
        private readonly IAlphaRepository<ChannelMesageReply> _channelMesageReplyRepo;
        private readonly IAlphaRepository<ChannelMessageLike> _channelMessageLikeRepo;
        private readonly IAlphaRepository<ChannelMessageUnLike> _ChannelMessageUnLikeRepo;
        private readonly IAlphaRepository<CommunityChannel> _communityChannelRepo;
        private readonly IAlphaRepository<CommunityChannelMessage> _communityChannelMessageRepo;
        private readonly IAlphaRepository<UserCommunityChannel> _userCommunityChannelRepo;

        public CommunityService(IAlphaRepository<UserCommunityChannel> userCommunityChannelMessageRepo,
            IAlphaRepository<CommunityChannelMessage> communityChannelMessageRepo,
            IAlphaRepository<CommunityChannel> communityChannelRepo,
            IAlphaRepository<ChannelMessageUnLike> channelMessageUnLikeRepo,
            IAlphaRepository<ChannelMessageLike> channelMessageLikeRepo,
            IAlphaRepository<ChannelMesageReply> channelMesageReplyRepo,
            ILogger<CommunityService> logger,
            IAccountRepo accountRepo,
            IAlphaRepository<ChannelCategory> channelCategoryRepo,
            IAlphaRepository<FavoriteChannelMessage> favoriteChannelMessageRepo)
        {
            _userCommunityChannelRepo = userCommunityChannelMessageRepo;
            _communityChannelMessageRepo = communityChannelMessageRepo;
            _communityChannelRepo = communityChannelRepo;
            _ChannelMessageUnLikeRepo = channelMessageUnLikeRepo;
            _channelMessageLikeRepo = channelMessageLikeRepo;
            _channelMesageReplyRepo = channelMesageReplyRepo;
            _logger = logger;
            _accountRepo = accountRepo;
            _channelCategoryRepo = channelCategoryRepo;
            _favoriteChannelMessageRepo = favoriteChannelMessageRepo;
        }
        public async Task<ResponseDto<string>> AddChannel(string catId, string userid, string name)
        {
            var response = new ResponseDto<string>();
            try
            {
                var check = await _communityChannelRepo.GetQueryable().FirstOrDefaultAsync(u => u.CreatedByUserId == userid && u.Name == name);
                if (check != null)
                {
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    response.ErrorMessages = new List<string>() { "Channel name already exist" };
                    return response;
                }
                var result = await _communityChannelRepo.Add(new CommunityChannel()
                {
                    CreatedByUserId = userid,
                    CategoryId = catId,
                    Name = name,
                    ChannelRoomId = _accountRepo.GenerateToken().ToString(),
                });

                await _communityChannelRepo.SaveChanges();
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = "Channel created successfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "Channel not added successfully" };
                response.StatusCode = 400;
                return response;
            }
        }
        public async Task<ResponseDto<string>> AddUserToChannel(string userid, string channelid)
        {
            var response = new ResponseDto<string>();
            try
            {
                var check = await _communityChannelRepo.GetByIdAsync(channelid);
                if (check == null)
                {
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    response.ErrorMessages = new List<string>() { "Channel id not valid" };
                    return response;
                }
                var checkUserChannel = await _userCommunityChannelRepo
                    .GetQueryable()
                    .FirstOrDefaultAsync(u => u.UserId == userid &&
                    u.CommunityChannelId == channelid);
                if (checkUserChannel != null)
                {
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "User already exist in the channel" };
                    response.StatusCode = 400;
                    return response;
                }
                var result = await _userCommunityChannelRepo.Add(new UserCommunityChannel()
                {
                    CommunityChannelId = channelid,
                    UserId = userid,
                });

                await _communityChannelRepo.SaveChanges();
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = "User added to channel successfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "User not added to channel successfully" };
                response.StatusCode = 400;
                return response;
            }
        }
        public async Task<ResponseDto<string>> MessageLike(LikeReq req, string userid)
        {
            var response = new ResponseDto<string>();
            try
            {
                var check = await _communityChannelMessageRepo.GetQueryable().FirstOrDefaultAsync(u => u.Id == req.MessageId);
                if (check == null)
                {

                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Invalid Message Id" };
                    return response;

                }
                var checkCurrent = await _channelMessageLikeRepo.GetQueryable().FirstOrDefaultAsync(u => u.UserId == userid && u.MessageId == req.MessageId);
                if (checkCurrent != null)
                {
                    _channelMessageLikeRepo.Delete(checkCurrent);
                    await _channelMessageLikeRepo.SaveChanges();
                    response.StatusCode = 200;
                    response.DisplayMessage = "Success";
                    response.Result = "Message like remove successfully";
                    return response;

                }
                var result = await _channelMessageLikeRepo.Add(new ChannelMessageLike()
                {

                    UserId = userid,
                    MessageId = req.MessageId,

                });

                await _channelMessageLikeRepo.SaveChanges();
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = "Message like successfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "Message like service not available" };
                response.StatusCode = 400;
                return response;
            }
        }
        public async Task<ResponseDto<string>> SaveMessageToFAV(LikeReq req, string userid)
        {
            var response = new ResponseDto<string>();
            try
            {
                var check = await _favoriteChannelMessageRepo.GetQueryable().FirstOrDefaultAsync(u => u.MessageId == req.MessageId && u.UserId == userid);
                if (check != null)
                {
                    _favoriteChannelMessageRepo.Delete(check);
                    await _channelMessageLikeRepo.SaveChanges();

                    response.StatusCode = 200;
                    response.DisplayMessage = "Success";
                    response.Result = "Message Unsaved Successfully";
                    return response;

                }

                var result = await _favoriteChannelMessageRepo.Add(new FavoriteChannelMessage()
                {

                    UserId = userid,
                    MessageId = req.MessageId,

                });

                await _channelMessageLikeRepo.SaveChanges();
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = "Message saved successfully as favourite";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "Message favourite service not available" };
                response.StatusCode = 400;
                return response;
            }
        }
        public async Task<ResponseDto<string>> MessageUnLike(LikeReq req, string userid)
        {
            var response = new ResponseDto<string>();
            try
            {
                var check = await _communityChannelMessageRepo.GetQueryable().FirstOrDefaultAsync(u => u.Id == req.MessageId);
                if (check == null)
                {

                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Invalid Message Id" };
                    return response;

                }
                var checkCurrent = await _ChannelMessageUnLikeRepo.GetQueryable().FirstOrDefaultAsync(u => u.UserId == userid && u.MessageId == req.MessageId);
                if (checkCurrent != null)
                {
                    _ChannelMessageUnLikeRepo.Delete(checkCurrent);
                    await _ChannelMessageUnLikeRepo.SaveChanges();
                    response.StatusCode = 200;
                    response.DisplayMessage = "Success";
                    response.Result = "Message downvote remove successfully";
                    return response;

                }
                var result = await _ChannelMessageUnLikeRepo.Add(new ChannelMessageUnLike()
                {

                    UserId = userid,
                    MessageId = req.MessageId,

                });

                await _ChannelMessageUnLikeRepo.SaveChanges();
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = "Message downvote successfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "Message downvote service not available" };
                response.StatusCode = 400;
                return response;
            }
        }
        public async Task<ResponseDto<string>> CreateCommunityCategory(string CategoryName, string UserId)
        {
            var response = new ResponseDto<string>();
            try
            {
                var AddCommunitityCat = await _channelCategoryRepo.Add(new ChannelCategory()
                {
                    CreatedByUserId = UserId,
                    Name = CategoryName,
                });

                await _channelCategoryRepo.SaveChanges();
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = "Blog Created Successfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "Channel category not created successfully" };
                response.StatusCode = 400;
                return response;
            }
        }
        public async Task<ResponseDto<IEnumerable<ChannelCategory>>> RetrieveCommunityCategory()
        {
            var response = new ResponseDto<IEnumerable<ChannelCategory>>();
            try
            {
                var RetrieveCat = await _channelCategoryRepo.GetQueryable().Include(u => u.CommunityChannels)
                    .ToListAsync();

                response.Result = RetrieveCat;
                response.StatusCode = 200;
                response.DisplayMessage = "Success";

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "Channel category  not retrieve successfully" };
                response.StatusCode = 400;
                return response;
            }
        }


        public async Task<ResponseDto<IEnumerable<ChannelRepDto>>> RetrieveCommunityCategory(string currentUserId)
        {
            var response = new ResponseDto<IEnumerable<ChannelRepDto>>();
            try
            {
                var categories = await _channelCategoryRepo.GetQueryable()
                    .Include(c => c.CommunityChannels)
                        .ThenInclude(ch => ch.CommunityChannelMessages)
                            .ThenInclude(m => m.MessageReads)
                            .OrderByDescending(u => u.Created)
                    .ToListAsync();

                var result = categories.Select(category => new ChannelRepDto
                {
                    CategoryId = category.Id,
                    CategoryName = category.Name,
                    Channels = category.CommunityChannels.Select(channel => new ChannelDto
                    {
                        Id = channel.Id,
                        Name = channel.Name,
                        ChannelRoomName = channel.ChannelRoomId,
                        UnreadCount = channel.CommunityChannelMessages
                            .Count(msg => !msg.MessageReads.Any(read => read.UserId == currentUserId))
                    }).ToList()
                });

                response.Result = result;
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string> { "Channel category not retrieved successfully" };
                response.StatusCode = 400;
                return response;
            }
        }
        public async Task<ResponseDto<IEnumerable<CommunityChannel>>> RetrieveChannel(string? userid)
        {
            var response = new ResponseDto<IEnumerable<CommunityChannel>>();
            try
            {
                if (userid != null)
                {
                    var allResult = await _communityChannelRepo.GetQueryable().
                      Where(u => u.CreatedByUserId == userid).ToListAsync();
                    response.StatusCode = 200;
                    response.DisplayMessage = "Success";
                    response.Result = allResult;
                    return response;
                }

                var result = await _communityChannelRepo.GetQueryable().Include(u => u.Category)
                    .Include(u => u.CreatedByUser)
                 .ToListAsync();
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = result;
                return response;


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "Channel not retrieved successfully" };
                response.StatusCode = 400;
                return response;
            }
        }

        public async Task<ResponseDto<CommunityChannelMessage>> AddMessage(string RoomId, string message, string messageType, string sentById)
        {
            var response = new ResponseDto<CommunityChannelMessage>();
            try
            {
                var check = await _communityChannelRepo.GetQueryable().FirstOrDefaultAsync(u => u.ChannelRoomId == RoomId);
                if (check == null)
                {
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    response.ErrorMessages = new List<string>() { "invalid Channel name" };
                    return response;
                }
                var result = await _communityChannelMessageRepo.Add(new CommunityChannelMessage()
                {
                    ChannelId = check.Id,
                    Message = message,
                    MessageType = messageType,
                    SentById = sentById,
                });

                await _communityChannelMessageRepo.SaveChanges();
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = result;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "message not sent successfully" };
                response.StatusCode = 400;
                return response;
            }
        }  public async Task<ResponseDto<ChannelMesageReply>> AddMessageReply(string MessageId, string message, string messageType, string sentById)
        {
            var response = new ResponseDto<ChannelMesageReply>();
            try
            {
                var check = await _communityChannelMessageRepo.GetQueryable().FirstOrDefaultAsync(u => u.Id == MessageId);
                if (check == null)
                {
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    response.ErrorMessages = new List<string>() { "invalid Channel name" };
                    return response;
                }
                var result = await _channelMesageReplyRepo.Add(new ChannelMesageReply()
                {
                    ChannelMessageId = check.Id,
                    Message = message,
                    MessageType = messageType,
                    SentById = sentById,
                });

                await _communityChannelMessageRepo.SaveChanges();
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = result;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string>() { "message reply not sent successfully" };
                response.StatusCode = 400;
                return response;
            }
        }
        public async Task<ResponseDto<IEnumerable<CommunityMesaagesReponse>>> RetrieveChannelMessages(string roomId, string userid)
        {
            var response = new ResponseDto<IEnumerable<CommunityMesaagesReponse>>();
            try
            {
                var retrieveChannelId = await _communityChannelRepo.GetQueryable().FirstOrDefaultAsync(u => u.ChannelRoomId == roomId);
                if (retrieveChannelId == null)
                {
                    response.DisplayMessage = "Error";
                    response.ErrorMessages = new List<string>() { "Invalid Channel RoomId" };
                    response.StatusCode = 400;
                    return response;
                }
                var messages = await _communityChannelMessageRepo.GetQueryable().
                    Where(u => u.ChannelId == retrieveChannelId.Id).
                    OrderBy(u => u.Created).
                    Select(u => new CommunityMesaagesReponse
                    {
                        Id = u.Id,
                        IsLiked = u.ChannelMessageLikes.FirstOrDefault(u => u.UserId == userid) != null,
                        IsUnLiked = u.ChannelMessageUnLikes.FirstOrDefault(u => u.UserId == userid) != null,
                        Message = u.Message,
                        MessageType = u.MessageType,
                        LikeCount = u.ChannelMessageLikes.Count(),
                        UnLikeCount = u.ChannelMessageUnLikes.Count(),
                        SentByImgUrl = u.SentBy.ProfilePicture,
                        SenderName = u.SentBy.FirstName + " " + u.SentBy.LastName,
                        Created = u.Created,
                        IsSaved = u.MessageFave.FirstOrDefault(u => u.UserId == userid) != null,

                    })

                    .ToListAsync();


                response.Result = messages;
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string> { "Channel messages not retrieved successfully" };
                response.StatusCode = 400;
                return response;
            }
        }
        public async Task<ResponseDto<IEnumerable<CommunityMesaagesReponse>>> RetrieveFavChannelMessages(string userid)
        {
            var response = new ResponseDto<IEnumerable<CommunityMesaagesReponse>>();
            try
            {

                var messages = await _favoriteChannelMessageRepo.GetQueryable().
                    Where(u => u.UserId == userid).
                    OrderBy(u => u.Created).
                    Select(u => new CommunityMesaagesReponse
                    {
                        Id = u.Message.Id,
                        IsLiked = u.Message.ChannelMessageLikes.FirstOrDefault(u => u.UserId == userid) != null,
                        IsUnLiked = u.Message.ChannelMessageUnLikes.FirstOrDefault(u => u.UserId == userid) != null,
                        Message = u.Message.Message,
                        MessageType = u.Message.MessageType,
                        LikeCount = u.Message.ChannelMessageLikes.Count(),
                        UnLikeCount = u.Message.ChannelMessageUnLikes.Count(),
                        SentByImgUrl = u.Message.SentBy.ProfilePicture,
                        SenderName = u.Message.SentBy.FirstName + " " + u.Message.SentBy.LastName,
                        Created = u.Message.Created,
                        IsSaved = u.Message.MessageFave.FirstOrDefault(u => u.UserId == userid) != null,
                    })

                    .ToListAsync();


                response.Result = messages;
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string> { "Saved Channel messages not retrieved successfully" };
                response.StatusCode = 400;
                return response;
            }
        }
        public async Task<ResponseDto<IEnumerable<CommunityMesaagesReply>>> RetrieveChannelMessagesReply(string messageid)
        {
            var response = new ResponseDto<IEnumerable<CommunityMesaagesReply>>();
            try
            {

                var messages = await _channelMesageReplyRepo.GetQueryable().
                    Where(u => u.ChannelMessageId == messageid).
                    OrderBy(u => u.Created).
                    Select(r => new CommunityMesaagesReply
                    {

                        MessageId = r.ChannelMessageId,
                        ReplyId = r.Id,
                        Message = r.Message,
                        MessageType = r.MessageType,
                        SentByImgUrl = r.SentBy.ProfilePicture,
                        SenderName = r.SentBy.FirstName + " " + r.SentBy.LastName,
                        Created= r.Created
                    }).ToListAsync();
                response.Result = messages;
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.DisplayMessage = "Error";
                response.ErrorMessages = new List<string> { "Channel messages reply not retrieved successfully" };
                response.StatusCode = 400;
                return response;
            }
        }
    }
}
