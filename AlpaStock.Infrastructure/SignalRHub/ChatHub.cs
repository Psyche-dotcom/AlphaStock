using AlpaStock.Core.Repositories.Interface;
using AlpaStock.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.SignalR;

namespace AlpaStock.Infrastructure.SignalRHub
{
    public class ChatHub : Hub
    {
        private readonly ICommunityService _communityService;
        private readonly IAccountRepo _accountRepo;
        public ChatHub(ICommunityService communityService,
            IAccountRepo accountRepo)
        {
            _communityService = communityService;
            _accountRepo = accountRepo;
        }

        public async Task JoinMultipleChannels(List<string> channelRoooms)
        {
            foreach (var roomid in channelRoooms)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, roomid);
            }
        }

        public async Task SendMessageToChannel(string roomId, string message, string messageType, string sentById, bool isReply)
        {
            if (isReply)
            {
                var retrieveUserInfo = await _accountRepo.FindUserByIdAsync(sentById);
                var delivery = await _communityService.AddMessageReply(roomId, message, messageType, sentById);
                if (delivery.StatusCode == 200 && retrieveUserInfo != null)
                {
                    var messageDto = new
                    {
                        RoomId = roomId,
                        Message = message,
                        MessageType = messageType,
                        SentByImgUrl = retrieveUserInfo.ProfilePicture,
                        SenderName = retrieveUserInfo.FirstName + " " + retrieveUserInfo.LastName,
                        Created = delivery.Result.Created,
                        Id = delivery.Result.Id,
                        isReply = isReply

                    };
                    await Clients.Group(roomId).SendAsync("ReceiveChannelMessage", messageDto);
                }

            }
            else
            {
                var retrieveUserInfo = await _accountRepo.FindUserByIdAsync(sentById);
              
                var delivery = await _communityService.AddMessage(roomId, message, messageType, sentById);
                if (delivery.StatusCode == 200 && retrieveUserInfo != null)
                {
                    var messageDto = new
                    {
                        RoomId = roomId,
                        Message = message,
                        MessageType = messageType,
                        SentByImgUrl = retrieveUserInfo.ProfilePicture,
                        SenderName = retrieveUserInfo.FirstName + " " + retrieveUserInfo.LastName,
                        Created= delivery.Result.Created,
                        Id = delivery.Result.Id,
                          isReply = isReply

                    };
                    await Clients.Group(roomId).SendAsync("ReceiveChannelMessage", messageDto);
                }

            }




        }

        public async Task JoinChannel(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }

        public async Task LeaveChannel(string roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
        }
    }

}
