using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace MinhaPrimeiraApi.Infra.Email
{
    public class EmailService : IEmailService
    {
        private readonly ConfiguracaoEmail _configuracaoEmail;

        public EmailService(IOptions<ConfiguracaoEmail> configuracaoEmail)
        {
            _configuracaoEmail = configuracaoEmail.Value;
        }

        public async Task Enviar(Mensagem mensagem)
        {
            try
            {
                var mimeMessage = new MimeMessage();

                mimeMessage.From.Add(new MailboxAddress(_configuracaoEmail.NomeEmissor, _configuracaoEmail.Usuario));

                mimeMessage.To.Add(new MailboxAddress(_configuracaoEmail.NomeEmissor, mensagem.Para));

                mimeMessage.Subject = mensagem.Assunto;

                mimeMessage.Body = new TextPart("html")
                {
                    Text = mensagem.CorpoMensagem
                };

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    await client.ConnectAsync(_configuracaoEmail.HostServidor);

                    await client.AuthenticateAsync(_configuracaoEmail.Usuario, _configuracaoEmail.Senha);

                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
