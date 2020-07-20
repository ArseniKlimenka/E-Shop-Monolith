using ElectroStore.Domain.Abstract;
using ElectroStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ElectroStore.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "a.klimenko.vironit@vironit.ru";
        public string MailFromAddress = "arseni.klimenka@gmail.com";
       // public bool UseSsl = true;
        public string Username = "arseni.klimenka@gmail.com";
        public string Password = "PanShcadun2904";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"c:\emails";
    }
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            StringBuilder body = new StringBuilder()
                .AppendLine("Новый заказ обработан")
                .AppendLine("---")
                .AppendLine("Товары:");

            foreach (var line in cart.Lines)
            {
                var subtotal = line.Product.Price * line.Quantity;
                body.AppendFormat("{0} x {1} (итого: {2:c}",
                    line.Quantity, line.Product.Name, subtotal);
            }

            body.AppendFormat("Общая стоимость: {0:c}", cart.ComputeTotalValue())
                .AppendLine("---")
                .AppendLine("Доставка:")
                .AppendLine(shippingInfo.Name)
                .AppendLine(shippingInfo.Line1)
                .AppendLine(shippingInfo.Line2 ?? "")                
                .AppendLine(shippingInfo.City)
                .AppendLine(shippingInfo.Country)
                .AppendLine("---")
                .AppendFormat("Подарочная упаковка: {0}",
                    shippingInfo.GiftWrap ? "Да" : "Нет");




            //emailSettings.MailToAddress = shippingInfo.Line2;
            MailMessage mailMessage = new MailMessage(emailSettings.MailFromAddress, emailSettings.MailToAddress);
            mailMessage.Subject = "Новый заказ отправлен!";     // Тема
            mailMessage.Body = body.ToString();

            using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.gmail.com")) //используем сервера Google
            {
                //if (emailSettings.WriteAsFile)
                //{
                //    client.EnableSsl = false;
                //    client.DeliveryMethod
                //        = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                //    client.PickupDirectoryLocation = emailSettings.FileLocation;
                    
                //}
                //if (emailSettings.WriteAsFile)
                //{
                //    mailMessage.BodyEncoding = Encoding.UTF8;
                //}

                client.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password); //логин-пароль от аккаунта
                client.Port = 587; //порт 587 либо 465
                client.EnableSsl = true; //SSL обязательно
                client.Send(mailMessage);
            }                     
                        
         
        }
     
    }
}


//MailMessage mailMessage = new MailMessage("arseni.klimenka@gmail.com", "a.klimenko.vironit@vironit.ru");
//mailMessage.Subject = "Новый заказ отправлен!";     // Тема
//            mailMessage.Body = "Hello";

//            using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.gmail.com")) //используем сервера Google
//            {
//                client.Credentials = new NetworkCredential("arseni.klimenka@gmail.com", "PanShcadun2904"); //логин-пароль от аккаунта
//client.Port = 587; //порт 587 либо 465
//                client.EnableSsl = true; //SSL обязательно
//                client.Send(mailMessage);
//            }



//mm.Body = string.Format(@"
//<html>
//<body>
//  <h1>An Error Has Occurred!</h1>
//  <table cellpadding=""5"" cellspacing=""0"" border=""1"">
//  <tr>
//  <tdtext-align: right;font-weight: bold"">URL:</td>
//  <td>{0}</td>
//  </tr>
//  <tr>
//  <tdtext-align: right;font-weight: bold"">User:</td>
//  <td>{1}</td>
//  </tr>
//  <tr>
//  <tdtext-align: right;font-weight: bold"">Exception Type:</td>
//  <td>{2}</td>
//  </tr>
//  <tr>
//  <tdtext-align: right;font-weight: bold"">Message:</td>
//  <td>{3}</td>
//  </tr>
//  <tr>
//  <tdtext-align: right;font-weight: bold"">Stack Trace:</td>
//  <td>{4}</td>
//  </tr> 
//  </table>
//</body>
//</html>",
//               Request.RawUrl,
//               User.Identity.Name,
//               lastErrorTypeName,
//               lastErrorMessage,
//               lastErrorStackTrace.Replace(Environment.NewLine, "<br />"));