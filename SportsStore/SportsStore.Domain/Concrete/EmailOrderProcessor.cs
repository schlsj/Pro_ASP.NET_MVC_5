using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SportsStore.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "orders@example.com";
        public string MailFromAddress = "sportsstore@example.com";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpPassword";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"E:\sports_store_emails";
    }

    public class EmailOrderProcessor:IOrderProcessor
    {
        private EmailSettings emailSetting;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSetting = settings;
        }
        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            using (var stmpClient=new SmtpClient())
            {
                stmpClient.EnableSsl = emailSetting.UseSsl;
                stmpClient.Host = emailSetting.ServerName;
                stmpClient.Port = emailSetting.ServerPort;
                stmpClient.UseDefaultCredentials = false;
                stmpClient.Credentials = new NetworkCredential(emailSetting.Username, emailSetting.Password);

                if (emailSetting.WriteAsFile)
                {
                    stmpClient.DeliveryMethod=SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    stmpClient.PickupDirectoryLocation = emailSetting.FileLocation;
                    stmpClient.EnableSsl = false;
                }
                StringBuilder body = new StringBuilder().AppendLine("A new order has been sumitted")
                    .AppendLine("----").AppendLine("Items:");
                foreach (CartLine cartLine in cart.Lines)
                {
                    var subtotal = cartLine.Product.Price * cartLine.Quantity;
                    body.AppendFormat("{0} x {1} (subtotal:{2:c}", cartLine.Quantity, cartLine.Product.Name, subtotal);
                }
                body.AppendFormat("Total order value:{0:c}", cart.ComputeTotalValue())
                    .AppendLine("----")
                    .AppendLine("Ship to:")
                    .AppendLine(shippingDetails.Name)
                    .AppendLine(shippingDetails.Line1)
                    .AppendLine(shippingDetails.Line2 ?? "")
                    .AppendLine(shippingDetails.Line3 ?? "")
                    .AppendLine(shippingDetails.City)
                    .AppendLine(shippingDetails.State ?? "")
                    .AppendLine(shippingDetails.Country)
                    .AppendLine(shippingDetails.Zip)
                    .AppendLine("----")
                    .AppendFormat("Gift wrap: {0}", shippingDetails.GiftWrap ? "Yes" : "No");
                MailMessage mailMessage = new MailMessage(emailSetting.MailFromAddress, emailSetting.MailToAddress,
                    "New order submitted!", body.ToString());
                if (emailSetting.WriteAsFile)
                {
                    mailMessage.BodyEncoding=Encoding.ASCII;
                }
                stmpClient.Send(mailMessage);
            }


        }
    }
}
