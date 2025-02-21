
using AlpaStock.Core.DTOs.Request.Notification;

namespace AlpaStock.Infrastructure.Service.Interface
{
    public interface IEmailServices
    {
        void SendEmail(Message message);
    }
}
