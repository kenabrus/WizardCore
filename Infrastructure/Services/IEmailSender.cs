using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);

        Task<bool> SendEmailAsync(string email, string subject, string body, bool isHtml = false);
    }
}