using AlpaStock.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.SignalR;

namespace AlpaStock.Infrastructure.SignalRHub
{
    public class ChatHub : Hub
    {
        private readonly ICommunityService _communityService;

        public ChatHub(ICommunityService communityService)
        {
            _communityService = communityService;
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

            }
            else
            {
                var messageDto = new
                {
                    RoomId = roomId,
                    Message = message,
                    MessageType = messageType,
                    SentById = sentById,

                };
                var delivery = await _communityService.AddMessage(roomId, message, messageType, sentById);
                if (delivery.StatusCode == 200)
                {
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
