using Amazon.Runtime.Internal;
using MediatR;
using MimeKit;
using NArchitecture.Core.Mailing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.Contact;
public class ContactCommand: IRequest<bool>
{
    public string Name { get; set; }
    public string Email { get; set; }   
    public string GsmNumber { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }

    public class ContactCommandHandler: IRequestHandler<ContactCommand, bool>
    {
        private readonly IMailService _emailService;

        public ContactCommandHandler(IMailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<bool> Handle(ContactCommand request, CancellationToken cancellationToken)
        {
            List<MailboxAddress> addresses = new List<MailboxAddress> { new MailboxAddress(name:"info@modaperfect.com.tr", "info@modaperfect.com.tr") };

             await _emailService.SendEmailAsync(
                    new Mail()
                    {
                        ToList = addresses,
                        Subject = request.Subject,
                        HtmlBody = $"<!DOCTYPE html><html lang=\"en\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Document</title></head><style>.email-container{{width:100%;background-color:#ffffff;padding:20px;font-family:'Arial',sans-serif;}}.email-content{{width:600px;margin:0 auto;background-color:#ffffff;padding:20px;border:2px solid #000000;border-radius:5px;}}.email-header{{text-align:center;background-color:#000000;padding:20px;border-radius:5px 5px 0 0;}}.email-title{{color:#ffffff;margin:0;font-size:24px;font-weight:bold;letter-spacing:1px;}}.email-body{{padding:20px;font-size:14px;color:#333333;line-height:1.6;}}.email-greeting{{font-size:16px;margin-bottom:10px;}}.email-introduction{{margin-bottom:20px;}}.order-details{{width:100%;border-collapse:collapse;margin-bottom:20px;}}.order-details .detail-label{{text-align:left;font-weight:bold;padding:10px;border-bottom:1px solid #000000;}}.order-details .detail-value{{text-align:right;padding:10px;border-bottom:1px solid #000000;}}.order-total{{font-weight:bold;border-top:2px solid #000000;}}.order-total .total-label{{text-align:left;padding:10px;}}.order-total .total-value{{text-align:right;padding:10px;}}.email-footer{{text-align:center;padding-top:10px;font-size:12px;color:#666666;}}.contact-info{{margin:10px 0;}}.email-link{{color:#000000;text-decoration:none;font-weight:bold;}}.thank-you{{margin-top:10px;}}</style><body><div class=\"email-container\"><table class=\"email-content\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td class=\"email-header\"><h1 class=\"email-title\">ModaPerfect Destek Talebi</h1></td></tr><tr><td class=\"email-body\"><p class=\"email-greeting\">Merhaba, <b>{request.Name}</b> isimli kullanıcının destek talebi.</p><p class=\"email-introduction\"><b>Konu:</b> {request.Subject}</p><p class=\"email-introduction\"><b>Mesaj:</b> {request.Message}</p><p class=\"email-introduction\">Kullanıcı ile iletişime geçmek için <b>{request.Email}</b> veya <b>{request.GsmNumber}</b> ile iletişime geçebilirsiniz.</p></td></tr><tr><td class=\"email-footer\"><p class=\"contact-info\">Eğer bir sorun yaşıyorsanız lütfen iletişime geçin <a href=\"mailto:devs@modaperfect.com.tr\" class=\"email-link\">devs@modaperfect.com.tr</a></p><p class=\"thank-you\">ModaPerfect'i tercih ettiğiniz için teşekkür ederiz!</p></td></tr></tbody></table></div></body></html>"
                    }
                );

            return true;
        }
    }
}
