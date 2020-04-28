using PizzaPortal.Model.Models;
using System.Threading.Tasks;

namespace PizzaPortal.BLL.Services.Abstract
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage emailMessage);       
    }
}
