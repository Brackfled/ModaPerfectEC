using Application.Features.Auth.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Domain.Dtos;
using Domain.Entities;
using MailKit;
using MediatR;
using Microsoft.AspNetCore.Http;
using MimeKit;
using NArchitecture.Core.Application.Dtos;
using NArchitecture.Core.Mailing;
using NArchitecture.Core.Security.Hashing;
using NArchitecture.Core.Security.JWT;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<RegisteredResponse>
{
    public RegisterDto RegisterDto { get; set; }
    public string IpAddress { get; set; }

    public RegisterCommand()
    {
        RegisterDto = null;
        IpAddress = string.Empty;
    }

    public RegisterCommand(RegisterDto registerDto, string ıpAddress)
    {
        RegisterDto = registerDto;
        IpAddress = ıpAddress;
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly NArchitecture.Core.Mailing.IMailService _mailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthBusinessRules _authBusinessRules;

        public RegisterCommandHandler(IUserRepository userRepository, IAuthService authService, NArchitecture.Core.Mailing.IMailService mailService, IHttpContextAccessor httpContextAccessor, AuthBusinessRules authBusinessRules)
        {
            _userRepository = userRepository;
            _authService = authService;
            _mailService = mailService;
            _httpContextAccessor = httpContextAccessor;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<RegisteredResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.UserEmailShouldBeNotExists(request.RegisterDto.Email);
                     

            HashingHelper.CreatePasswordHash(
                request.RegisterDto.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );

            

            User newUser =
                new()
                {
                    Email = request.RegisterDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    IdentityNumberHash = null,
                    IdentityNumberSalt = null,
                    TradeName = request.RegisterDto.TradeName,
                    FirstName = request.RegisterDto.FirstName,
                    LastName = request.RegisterDto.LastName,
                    Country = request.RegisterDto.Country,
                    City = request.RegisterDto.City,
                    District = request.RegisterDto.District,
                    Address = request.RegisterDto.Address,
                    GsmNumber = request.RegisterDto.GsmNumber,
                    TaxNumber = request.RegisterDto.TaxNumber,
                    TaxOffice = request.RegisterDto.TaxOffice,
                    Reference = request.RegisterDto.Reference,
                    CustomerCode = request.RegisterDto.CustomerCode,
                    CarrierCompanyInfo = request.RegisterDto.CarrierCompanyInfo,
                    UserState = Domain.Enums.UserState.Pending,
                };

            if (request.RegisterDto.IdentityNumber is not null)
            {
                await _authBusinessRules.UserIdentityShouldBeNotExists(request.RegisterDto.IdentityNumber);
                await _authBusinessRules.IdentityNumberIsAccurate(request.RegisterDto.IdentityNumber);

                HashingHelper.CreatePasswordHash(
                    request.RegisterDto.IdentityNumber,
                    passwordHash: out byte[] identityHash,
                    passwordSalt: out byte[] identitySalt
                );

                newUser.IdentityNumberHash = identityHash;
                newUser.IdentityNumberSalt = identitySalt;
            }

            User createdUser = await _userRepository.AddAsync(newUser);

            List<MailboxAddress> mails = new List<MailboxAddress> { new MailboxAddress(name: createdUser.Email, createdUser.Email) };

            string acceptedLanguage = _httpContextAccessor.HttpContext.Request.Headers["Accept-Language"].ToString();

            string firstLanguage = acceptedLanguage?.Split(',').FirstOrDefault()?.Split("-").FirstOrDefault()?.Split(";").FirstOrDefault();

            string htmlBody= firstLanguage switch
            {
                "tr" => "<!DOCTYPE html><html lang=\"en\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Document</title><style>.email-container-tr {width: 100%;background-color: #ffffff;padding: 20px;font-family: 'Arial', sans-serif;}.email-content {width: 600px;margin: 0 auto;background-color: #ffffff;padding: 20px;border: 2px solid #000000;border-radius: 5px;}.email-header {text-align: center;background-color: #000000;padding: 20px;border-radius: 5px 5px 0 0;}.email-title-tr {color: #ffffff;margin: 0;font-size: 24px;font-weight: bold;}.email-body {padding: 20px;font-size: 14px;color: #333333;line-height: 1.6;text-align: left;}.email-greeting-tr {font-size: 16px;margin-bottom: 10px;}.email-introduction-tr {margin-bottom: 20px;}.verification-button {width: 100%;text-align: center;}.button-container {margin-top: 20px;}.verification-link {background-color: #000000;color: white;padding: 15px 30px;font-size: 16px;text-decoration: none;border-radius: 5px;font-weight: bold;}.verification-link:hover {background-color: rgb(41, 41, 41);}.email-footer-tr {text-align: left;padding-top: 10px;font-size: 12px;color: #666666;}.contact-info {margin: 10px 0;}.email-link {color: #000000;text-decoration: none;font-weight: bold;}.thank-you-tr {margin-top: 10px;}</style></head><body><div class=\"email-container-tr\"><table class=\"email-content\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td class=\"email-header\"><h1 class=\"email-title-tr\">E-posta Doğrulaması</h1></td></tr><tr><td class=\"email-body\"><p class=\"email-greeting-tr\">Merhaba,</p><p class=\"email-introduction-tr\">ModaPerfect'e kaydolduğunuz için teşekkür ederiz. Kaydınız incelenip en kısa sürede dönüş yapılacaktır.</p><p class=\"email-footer-tr\">Eğer modaperfect.com'a kayıt olmadıysanız, lütfen bu mesajı dikkate almayın.</p></td></tr><tr><td class=\"email-footer\"><p class=\"contact-info\">Yardım almak için bizimle iletişime geçin: <a href=\"mailto:contact@modaperfect.com.tr\" class=\"email-link\">contact@modaperfect.com.tr</a></p><p class=\"thank-you-tr\">ModaPerfect’i tercih ettiğiniz için teşekkür ederiz!</p></td></tr></tbody></table></div></body></html>",
                "en" => "<!DOCTYPE html><html lang=\"en\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Document</title><style>.email-container-en {width: 100%;background-color: #ffffff;padding: 20px;font-family: 'Arial', sans-serif;}.email-content {width: 600px;margin: 0 auto;background-color: #ffffff;padding: 20px;border: 2px solid #000000;border-radius: 5px;}.email-header {text-align: center;background-color: #000000;padding: 20px;border-radius: 5px 5px 0 0;}.email-title {color: #ffffff;margin: 0;font-size: 24px;font-weight: bold;}.email-body {padding: 20px;font-size: 14px;color: #333333;line-height: 1.6;}.email-greeting {font-size: 16px;margin-bottom: 10px;}.email-introduction {margin-bottom: 20px;}.verification-button {width: 100%;text-align: center;}.button-container {margin-top: 20px;}.verification-link {background-color: #000000;color: white;padding: 15px 30px;font-size: 16px;text-decoration: none;border-radius: 5px;font-weight: bold;}.verification-link:hover {background-color: rgb(41, 41, 41);}.email-footer-ar {text-align: right;padding-top: 10px;font-size: 12px;color: #666666;}.contact-info {margin: 10px 0;}.email-link {color: #000000;text-decoration: none;font-weight: bold;}.thank-you {margin-top: 10px;}</style></head><body><div class=\"email-container-en\"><table class=\"email-content\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td class=\"email-header\"><h1 class=\"email-title\">Email Verification</h1></td></tr><tr><td class=\"email-body\"><p class=\"email-greeting\">Hello,</p><p class=\"email-introduction\">Thank you for registering with ModaPerfect. Your registration will be reviewed and you will be contacted as soon as possible.</p><p class=\"email-footer-tr\">If you have not registered to modaperfect.com, please ignore this message.</p></td></tr><tr><td class=\"email-footer\"><p class=\"contact-info\">For assistance, please contact us: <a href=\"mailto:contact@modaperfect.com.tr\" class=\"email-link\">contact@modaperfect.com.tr</a></p><p class=\"thank-you\">Thank you for choosing ModaPerfect!</p></td></tr></tbody></table></div></body></html>",
                "ar" => "<!DOCTYPE html><html lang=\"en\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Document</title><style>.email-container-ar {width: 100%;background-color: #ffffff;padding: 20px;font-family: 'Arial', sans-serif;}.email-content {width: 600px;margin: 0 auto;background-color: #ffffff;padding: 20px;border: 2px solid #000000;border-radius: 5px;}.email-header {text-align: center;background-color: #000000;padding: 20px;border-radius: 5px 5px 0 0;}.email-title-ar {color: #ffffff;margin: 0;font-size: 24px;font-weight: bold;}.email-body {padding: 20px;font-size: 14px;color: #333333;line-height: 1.6;text-align: right;}.email-greeting-ar {font-size: 16px;margin-bottom: 10px;}.email-introduction-ar {margin-bottom: 20px;}.verification-button {width: 100%;text-align: center;}.button-container {margin-top: 20px;}.verification-link {background-color: #000000;color: white;padding: 15px 30px;font-size: 16px;text-decoration: none;border-radius: 5px;font-weight: bold;}.verification-link:hover {background-color: rgb(41, 41, 41);}.email-footer-ar {text-align: right;padding-top: 10px;font-size: 12px;color: #666666;}.contact-info {margin: 10px 0;}.email-link {color: #000000;text-decoration: none;font-weight: bold;}.thank-you-ar {margin-top: 10px;}</style></head><body><div class=\"email-container-ar\"><table class=\"email-content\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td class=\"email-header\"><h1 class=\"email-title-ar\">تحقق من البريد الإلكتروني</h1></td></tr><tr><td class=\"email-body\"><p class=\"email-greeting-ar\">مرحباً،</p><p class=\"email-introduction-ar\">.شكراً لتسجيلك في مودا بيرفيكت. سيتم مراجعة تسجيلك وسنقوم بالرد عليك في أقرب وقت ممكن.</p><p class=\"email-footer-tr\">إذا لم تكن قد سجلت في موقع modaperfect.com.tr، يُرجى تجاهل هذه الرسالة.</p></td></tr><tr><td class=\"email-footer\"><p class=\"contact-info\">للمساعدة، يرجى الاتصال بنا: <a href=\"mailto:contact@modaperfect.com.tr\" class=\"email-link\">contact@modaperfect.com.tr</a></p><p class=\"thank-you-ar\">شكراً لاختيارك مودا بيرفيكت!</p></td></tr></tbody></table></div></body></html>",
                _ => "<!DOCTYPE html><html lang=\"en\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Document</title><style>.email-container-tr {width: 100%;background-color: #ffffff;padding: 20px;font-family: 'Arial', sans-serif;}.email-content {width: 600px;margin: 0 auto;background-color: #ffffff;padding: 20px;border: 2px solid #000000;border-radius: 5px;}.email-header {text-align: center;background-color: #000000;padding: 20px;border-radius: 5px 5px 0 0;}.email-title-tr {color: #ffffff;margin: 0;font-size: 24px;font-weight: bold;}.email-body {padding: 20px;font-size: 14px;color: #333333;line-height: 1.6;text-align: left;}.email-greeting-tr {font-size: 16px;margin-bottom: 10px;}.email-introduction-tr {margin-bottom: 20px;}.verification-button {width: 100%;text-align: center;}.button-container {margin-top: 20px;}.verification-link {background-color: #000000;color: white;padding: 15px 30px;font-size: 16px;text-decoration: none;border-radius: 5px;font-weight: bold;}.verification-link:hover {background-color: rgb(41, 41, 41);}.email-footer-tr {text-align: left;padding-top: 10px;font-size: 12px;color: #666666;}.contact-info {margin: 10px 0;}.email-link {color: #000000;text-decoration: none;font-weight: bold;}.thank-you-tr {margin-top: 10px;}</style></head><body><div class=\"email-container-tr\"><table class=\"email-content\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td class=\"email-header\"><h1 class=\"email-title-tr\">E-posta Doğrulaması</h1></td></tr><tr><td class=\"email-body\"><p class=\"email-greeting-tr\">Merhaba,</p><p class=\"email-introduction-tr\">ModaPerfect'e kaydolduğunuz için teşekkür ederiz. Kaydınız incelenip en kısa sürede dönüş yapılacaktır.</p><p class=\"email-footer-tr\">Eğer modaperfect.com'a kayıt olmadıysanız, lütfen bu mesajı dikkate almayın.</p></td></tr><tr><td class=\"email-footer\"><p class=\"contact-info\">Yardım almak için bizimle iletişime geçin: <a href=\"mailto:contact@modaperfect.com.tr\" class=\"email-link\">contact@modaperfect.com.tr</a></p><p class=\"thank-you-tr\">ModaPerfect’i tercih ettiğiniz için teşekkür ederiz!</p></td></tr></tbody></table></div></body></html>",

            };

            string subject = firstLanguage switch
            {
                "tr" => "Kullanıcı Kaydı",
                "en" => "User Registration",
                "ar" => "تسجيل المستخدم",
                _ => "Kullanıcı Kaydı"
            };

            await _mailService.SendEmailAsync(
                    new Mail()
                    {
                        ToList = mails,
                        Subject = subject,
                        HtmlBody = htmlBody
                    }
                );

            List<MailboxAddress> mailBoxAdmin = new List<MailboxAddress> { new MailboxAddress(name: "contact@modaperfect.com.tr", "contact@modaperfect.com.tr") };

            await _mailService.SendEmailAsync(
                    new Mail()
                    {
                        ToList = mailBoxAdmin,
                        Subject = "Yeni Kayıt Talebi",
                        HtmlBody = $"<!DOCTYPE html><html lang=\"en\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Document</title></head><style>.email-container{{width:100%;background-color:#ffffff;padding:20px;font-family:'Arial',sans-serif;}}.email-content{{width:600px;margin:0 auto;background-color:#ffffff;padding:20px;border:2px solid #000000;border-radius:5px;}}.email-header{{text-align:center;background-color:#000000;padding:20px;border-radius:5px 5px 0 0;}}.email-title{{color:#ffffff;margin:0;font-size:24px;font-weight:bold;letter-spacing:1px;}}.email-body{{padding:20px;font-size:14px;color:#333333;line-height:1.6;}}.email-greeting{{font-size:16px;margin-bottom:10px;}}.email-introduction{{margin-bottom:20px;}}.order-details{{width:100%;border-collapse:collapse;margin-bottom:20px;}}.order-details .detail-label{{text-align:left;font-weight:bold;padding:10px;border-bottom:1px solid #000000;}}.order-details .detail-value{{text-align:right;padding:10px;border-bottom:1px solid #000000;}}.order-total{{font-weight:bold;border-top:2px solid #000000;}}.order-total .total-label{{text-align:left;padding:10px;}}.order-total .total-value{{text-align:right;padding:10px;}}.email-footer{{text-align:center;padding-top:10px;font-size:12px;color:#666666;}}.contact-info{{margin:10px 0;}}.email-link{{color:#000000;text-decoration:none;font-weight:bold;}}.thank-you{{margin-top:10px;}}</style><body><div class=\"email-container\"><table class=\"email-content\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td class=\"email-header\"><h1 class=\"email-title\">ModaPerfect Yeni Kayıt Talebi</h1></td></tr><tr><td class=\"email-body\"><p class=\"email-greeting\">Merhaba,</p><p class=\"email-introduction\">{createdUser.TradeName} ünvanlı , {createdUser.FirstName} {createdUser.LastName} isimli kullanıcı kayıt talebinde bulundu. Kullanıcılar listesinden gerekli işlemleri sağlayabilirisiniz.</p></td></tr><tr><td class=\"email-footer\"><p class=\"contact-info\">Eğer bir sorun yaşıyorsanız lütfen iletişime geçin <a href=\"mailto:devs@modaperfect.com.tr\" class=\"email-link\">devs@modaperfect.com.tr</a></p><p class=\"thank-you\">ModaPerfect'i tercih ettiğiniz için teşekkür ederiz!</p></td></tr></tbody></table></div></body></html>"
                    }
                );

            Domain.Entities.RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(
                createdUser,
                request.IpAddress
            );
            Domain.Entities.RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

            RegisteredResponse registeredResponse = new() { UserState = createdUser.UserState, RefreshToken = addedRefreshToken };
            return registeredResponse;
        }
    }
}
