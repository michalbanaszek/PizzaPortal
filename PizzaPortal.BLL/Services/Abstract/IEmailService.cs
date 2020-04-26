using PizzaPortal.Model.Models;
using System.Collections.Generic;

namespace PizzaPortal.BLL.Services.Abstract
{
    public interface IEmailService
    {
        void Send(EmailMessage emailMessage);
        List<EmailMessage> ReceiveEmail(int maxCount = 10);
    }
}
