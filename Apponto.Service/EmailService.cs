using Apponto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Apponto.Service
{
    public class EmailService
    {
        public void SendWelcomeEmail(UserModel user)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("atendimento@apponto.net");
            message.To.Add(new MailAddress(user.Email));
            message.Subject = "Equipe Apponto";

            if (user.Company != null)
                message.Body = "Olá, " + user.Email + "<br>"
                        + "Nós estamos muito felizes em te receber. O Apponto é uma plataforma brasileira de registro de ponto web e mobile. Aqui você poderá registrar e organizar seus horários, visualizar relatórios e o melhor, tudo na palma da sua mão."
                        + " <br><br> <b>Estas são algumas informações sobre sua conta que você precisa guardar: </b><br>"
                        + "Primeiro sua conta: <br>"
                        + "E-mail: " + user.Email + "<br><br>"
                        + "Agora as informações da sua empresa: <br>"
                        + "Empresa: " + user.Company.Name + "<br>"
                        + "Token: " + user.Company.Token + "<br>"
                        + "*Os funcionários deverão fornecer o Token da empresa no momento do cadastro, dessa forma saberemos para quem ele trabalha.";
            else
                message.Body = "Olá, "+ user.Email +"<br>"
                    + "Nós estamos muito felizes em te receber. O Apponto é uma plataforma brasileira de registro de ponto web e mobile. Aqui você poderá registrar e organizar seus horários, visualizar relatórios e o melhor, tudo na palma da sua mão."
                    + " <br><br> <b>Estas são algumas informações sobre sua conta que você precisa guardar: </b><br>"
                    + "E-mail: " + user.Email+"<br><br>"
                    + "Você pode fazer o login na sua conta <a href src='http://app.apponto.net/App/#/autenticacao/entrar'>clicando aqui</a>.";

            message.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "send.one.com";

            smtp.UseDefaultCredentials = true;
            smtp.Port = 25;
            smtp.Credentials = new System.Net.NetworkCredential("atendimento@apponto.net", "!Jlagj030793");

            smtp.EnableSsl = true;

            Thread T1 = new Thread(delegate()
            {
                smtp.Send(message);
            });

            T1.Start();
        }
    }
}
