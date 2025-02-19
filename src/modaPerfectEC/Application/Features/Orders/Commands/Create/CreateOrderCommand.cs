using Application.Features.Orders.Constants;
using Application.Features.Orders.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using Domain.Enums;
using static Application.Features.Orders.Constants.OrdersOperationClaims;
using Application.Services.Baskets;
using Application.Features.Baskets.Rules;
using Microsoft.EntityFrameworkCore;
using Application.Features.ProductVariants.Rules;
using Microsoft.AspNetCore.Http;
using MimeKit;
using NArchitecture.Core.Mailing;
using Application.Services.UsersService;

namespace Application.Features.Orders.Commands.Create;

public class CreateOrderCommand : IRequest<CreatedOrderResponse>, ITransactionalRequest , ISecuredRequest
{
    public Guid UserId { get; set; }
    public CreateOrderRequest CreateOrderRequest { get; set; }

    public string[] Roles => [Admin, OrdersOperationClaims.Create];

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreatedOrderResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly OrderBusinessRules _orderBusinessRules;
        private readonly IBasketService _basketService;
        private readonly BasketBusinessRules _basketBusinessRules;
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly ProductVariantBusinessRules _productVariantBusinessRules;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMailService _mailService;

        public CreateOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository, OrderBusinessRules orderBusinessRules, IBasketService basketService, BasketBusinessRules basketBusinessRules, IProductVariantRepository productVariantRepository, ProductVariantBusinessRules productVariantBusinessRules, IHttpContextAccessor httpContextAccessor, IMailService mailService)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _orderBusinessRules = orderBusinessRules;
            _basketService = basketService;
            _basketBusinessRules = basketBusinessRules;
            _productVariantRepository = productVariantRepository;
            _productVariantBusinessRules = productVariantBusinessRules;
            _httpContextAccessor = httpContextAccessor;
            _mailService = mailService;
        }

        public async Task<CreatedOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            Basket? basket = await _basketService.GetAsync(
                predicate: b => b.Id == request.CreateOrderRequest.BasketId && b.IsOrderBasket == false && b.UserId == request.UserId,
                include: opt => opt.Include(b => b.User)!.Include(b => b.BasketItems)!,
                cancellationToken: cancellationToken
                );
            
            await _basketBusinessRules.BasketShouldExistWhenSelected(basket);
            await _basketBusinessRules.UserShouldHasOneActiveBasket(basket!.UserId);

            Order order = new()
            {
                Id = Guid.NewGuid(),
                UserId = basket.UserId,
                BasketId = basket.Id,
                OrderPrice = request.CreateOrderRequest.OrderPrice,
                IsInvoiceSended = false,
                OrderNo = null,
                IsUsdPrice = request.CreateOrderRequest.IsUsdPrice,
                OrderState = OrderState.Pending,
                TrackingNumber = null
            };

            Order addedOrder = await _orderRepository.AddAsync(order);

            foreach(BasketItem basketItem in basket.BasketItems!)
            {
                ProductVariant? productVariant = await _productVariantRepository.GetAsync(pv => pv.Id == basketItem.ProductVariantId);
                await _productVariantBusinessRules.ProductVariantShouldExistWhenSelected(productVariant);

                productVariant!.StockAmount -= basketItem.ProductAmount;
                await _productVariantRepository.UpdateAsync(productVariant);
            }

            basket.IsOrderBasket = true;
            await _basketService.UpdateAsync(basket);

            Basket newBasket = new()
            {
                Id = Guid.NewGuid(),
                UserId = basket.UserId,
                TotalPrice = 0,
                TotalPriceUSD = 0,
                IsOrderBasket = false
            };

            await _basketService.AddAsync(newBasket);

            List<MailboxAddress> mails = new List<MailboxAddress> { new MailboxAddress(name: basket.User!.Email, basket.User!.Email) };

            string acceptedLanguage = _httpContextAccessor.HttpContext.Request.Headers["Accept-Language"].ToString();

            string firstLanguage = acceptedLanguage?.Split(',').FirstOrDefault()?.Split("-").FirstOrDefault()?.Split(";").FirstOrDefault();

            string htmlBody = firstLanguage switch
            {
                "tr" => $"<!DOCTYPE html><html lang=\"en\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Document</title><style>.email-container{{width:100%;background-color:#ffffff;padding:20px;font-family:'Arial',sans-serif;}}.email-content{{width:600px;margin:0 auto;background-color:#ffffff;padding:20px;border:2px solid #000000;border-radius:5px;}}.email-header{{text-align:center;background-color:#000000;padding:20px;border-radius:5px 5px 0 0;}}.email-title{{color:#ffffff;margin:0;font-size:24px;font-weight:bold;letter-spacing:1px;}}.email-body{{padding:20px;font-size:14px;color:#333333;line-height:1.6;}}.email-greeting{{font-size:16px;margin-bottom:10px;}}.email-introduction{{margin-bottom:20px;}}.order-details{{width:100%;border-collapse:collapse;margin-bottom:20px;}}.order-details .detail-label{{text-align:left;font-weight:bold;padding:10px;border-bottom:1px solid #000000;}}.order-details .detail-value{{text-align:right;padding:10px;border-bottom:1px solid #000000;}}.order-total{{font-weight:bold;border-top:2px solid #000000;}}.order-total .total-label{{text-align:left;padding:10px;}}.order-total .total-value{{text-align:right;padding:10px;}}.email-footer{{text-align:center;padding-top:10px;font-size:12px;color:#666666;}}.contact-info{{margin:10px 0;}}.email-link{{color:#000000;text-decoration:none;font-weight:bold;}}.thank-you{{margin-top:10px;}}</style></head><body><div class=\"email-container\"><table class=\"email-content\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td class=\"email-header\"><h1 class=\"email-title\">ModaPerfect Sipariş Özeti</h1></td></tr><tr><td class=\"email-body\"><p class=\"email-greeting\">Merhaba,</p><p class=\"email-introduction\">{addedOrder.CreatedDate} tarihli {addedOrder.OrderPrice} ₺ tutarlı siparişiniz alınmıştır. Siparişlerim kısmında takibini yapabilirsiniz.</p><p class=\"email-footer\">Siparişinizin durumu hakkında daha fazla bilgi için bizimle iletişime geçebilirsiniz.</p></td></tr><tr><td class=\"email-footer\"><p class=\"contact-info\">Bize ulaşın: <a href=\"mailto:contact@modaperfect.com.tr\" class=\"email-link\">contact@modaperfect.com.tr</a></p><p class=\"thank-you\">ModaPerfect'i tercih ettiğiniz için teşekkür ederiz!</p></td></tr></tbody></table></div></body></html>",
                "en" => $"<div class=\"email-container-en\"><table class=\"email-content\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td class=\"email-header\"><h1 class=\"email-title\">ModaPerfect Order Summary</h1></td></tr><tr><td class=\"email-body\"><p class=\"email-greeting\">Hello,</p><p class=\"email-introduction\">Your order dated {addedOrder.CreatedDate}, priced at {addedOrder.OrderPrice} $, has been received. You can check it from the My Orders section.</p><p class=\"email-footer\">For more information about the status of your order, feel free to contact us.</p></td></tr><tr><td class=\"email-footer\"><p class=\"contact-info\">Contact us: <a href=\"mailto:contact@modaperfect.com.tr\" class=\"email-link\">contact@modaperfect.com.tr</a></p><p class=\"thank-you\">Thank you for choosing ModaPerfect!</p></td></tr></tbody></table></div><style>.email-container-en{{width:100%;background-color:#ffffff;padding:20px;font-family:'Arial',sans-serif;}}.email-content{{width:600px;margin:0 auto;background-color:#ffffff;padding:20px;border:2px solid #000000;border-radius:5px;}}.email-header{{text-align:center;background-color:#000000;padding:20px;border-radius:5px 5px 0 0;}}.email-title{{color:#ffffff;margin:0;font-size:24px;font-weight:bold;}}.email-body{{padding:20px;font-size:14px;color:#333333;line-height:1.6;}}.email-greeting{{font-size:16px;margin-bottom:10px;}}.email-introduction{{margin-bottom:20px;}}.order-details{{width:100%;border-collapse:collapse;margin-bottom:20px;}}.order-details .detail-label{{text-align:left;font-weight:bold;padding:10px;border-bottom:1px solid #000000;}}.order-details .detail-value{{text-align:right;padding:10px;border-bottom:1px solid #000000;}}.order-total{{font-weight:bold;border-top:2px solid #000000;}}.order-total .total-label{{text-align:left;padding:10px;}}.order-total .total-value{{text-align:right;padding:10px;}}.email-footer{{text-align:center;padding-top:10px;font-size:12px;color:#666666;}}.contact-info{{margin:10px 0;}}.email-link{{color:#000000;text-decoration:none;font-weight:bold;}}.thank-you{{margin-top:10px;}}</style>",
                "ar" => $"<div class=\"email-container-ar\"><table class=\"email-content\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td class=\"email-header\"><h1 class=\"email-title-ar\">ملخص الطلب من مودا بيرفيكت</h1></td></tr><tr><td class=\"email-body\"><p class=\"email-greeting-ar\">مرحباً،</p><p class=\"email-introduction-ar\">{addedOrder.OrderPrice} $ لقد ت{addedOrder.CreatedDate}م استلام طلبك بتاريخ  بسعر </p><p class=\"email-footer-ar\">لمزيد من المعلومات حول حالة طلبك، لا تتردد في الاتصال بنا.</p></td></tr><tr><td class=\"email-footer\"><p class=\"contact-info\">اتصل بنا: <a href=\"mailto:contact@modaperfect.com.tr\" class=\"email-link\">contact@modaperfect.com.tr</a></p><p class=\"thank-you-ar\">شكراً لاختيارك مودا بيرفيكت!</p></td></tr></tbody></table></div><style>.email-container-ar{{width:100%;background-color:#ffffff;padding:20px;font-family:'Arial',sans-serif;}}.email-content{{width:600px;margin:0 auto;background-color:#ffffff;padding:20px;border:2px solid #000000;border-radius:5px;}}.email-header{{text-align:center;background-color:#000000;padding:20px;border-radius:5px 5px 0 0;}}.email-title-ar{{color:#ffffff;margin:0;font-size:24px;font-weight:bold;}}.email-body{{padding:20px;font-size:14px;color:#333333;line-height:1.6;}}.email-greeting-ar{{font-size:16px;margin-bottom:10px;text-align:right;}}.email-introduction-ar{{margin-bottom:20px;text-align:right;}}.order-details{{width:100%;border-collapse:collapse;margin-bottom:20px;}}.order-details .detail-label{{text-align:right;font-weight:bold;padding:10px;border-bottom:1px solid #000000;}}.order-details .detail-value{{text-align:left;padding:10px;border-bottom:1px solid #000000;}}.order-total{{font-weight:bold;border-top:2px solid #000000;}}.order-total .total-label{{text-align:right;padding:10px;}}.order-total .total-value{{text-align:left;padding:10px;}}.email-footer-ar{{text-align:right;padding-top:10px;font-size:12px;color:#666666;}}.contact-info{{margin:10px 0;}}.email-link{{color:#000000;text-decoration:none;font-weight:bold;}}.thank-you-ar{{margin-top:10px;}}</style>",
                _ => $"<!DOCTYPE html><html lang=\"en\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Document</title><style>.email-container{{width:100%;background-color:#ffffff;padding:20px;font-family:'Arial',sans-serif;}}.email-content{{width:600px;margin:0 auto;background-color:#ffffff;padding:20px;border:2px solid #000000;border-radius:5px;}}.email-header{{text-align:center;background-color:#000000;padding:20px;border-radius:5px 5px 0 0;}}.email-title{{color:#ffffff;margin:0;font-size:24px;font-weight:bold;letter-spacing:1px;}}.email-body{{padding:20px;font-size:14px;color:#333333;line-height:1.6;}}.email-greeting{{font-size:16px;margin-bottom:10px;}}.email-introduction{{margin-bottom:20px;}}.order-details{{width:100%;border-collapse:collapse;margin-bottom:20px;}}.order-details .detail-label{{text-align:left;font-weight:bold;padding:10px;border-bottom:1px solid #000000;}}.order-details .detail-value{{text-align:right;padding:10px;border-bottom:1px solid #000000;}}.order-total{{font-weight:bold;border-top:2px solid #000000;}}.order-total .total-label{{text-align:left;padding:10px;}}.order-total .total-value{{text-align:right;padding:10px;}}.email-footer{{text-align:center;padding-top:10px;font-size:12px;color:#666666;}}.contact-info{{margin:10px 0;}}.email-link{{color:#000000;text-decoration:none;font-weight:bold;}}.thank-you{{margin-top:10px;}}</style></head><body><div class=\"email-container\"><table class=\"email-content\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td class=\"email-header\"><h1 class=\"email-title\">ModaPerfect Sipariş Özeti</h1></td></tr><tr><td class=\"email-body\"><p class=\"email-greeting\">Merhaba,</p><p class=\"email-introduction\">{addedOrder.CreatedDate} tarihli {addedOrder.OrderPrice} ₺ tutarlı siparişiniz alınmıştır. Siparişlerim kısmında takibini yapabilirsiniz.</p><p class=\"email-footer\">Siparişinizin durumu hakkında daha fazla bilgi için bizimle iletişime geçebilirsiniz.</p></td></tr><tr><td class=\"email-footer\"><p class=\"contact-info\">Bize ulaşın: <a href=\"mailto:contact@modaperfect.com.tr\" class=\"email-link\">contact@modaperfect.com.tr</a></p><p class=\"thank-you\">ModaPerfect'i tercih ettiğiniz için teşekkür ederiz!</p></td></tr></tbody></table></div></body></html>",

            };

            string subject = firstLanguage switch
            {
                "tr" => "Siparşiniz Alındı",
                "en" => "Your order has been received",
                "ar" => "لقد تم استلام طلبك",
                _ => "Siparşiniz Alındı"
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
                        Subject = "Yeni Sipariş Alındı",
                        HtmlBody = $"<!DOCTYPE html><html lang=\"en\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Document</title></head><style>.email-container{{width:100%;background-color:#ffffff;padding:20px;font-family:'Arial',sans-serif;}}.email-content{{width:600px;margin:0 auto;background-color:#ffffff;padding:20px;border:2px solid #000000;border-radius:5px;}}.email-header{{text-align:center;background-color:#000000;padding:20px;border-radius:5px 5px 0 0;}}.email-title{{color:#ffffff;margin:0;font-size:24px;font-weight:bold;letter-spacing:1px;}}.email-body{{padding:20px;font-size:14px;color:#333333;line-height:1.6;}}.email-greeting{{font-size:16px;margin-bottom:10px;}}.email-introduction{{margin-bottom:20px;}}.order-details{{width:100%;border-collapse:collapse;margin-bottom:20px;}}.order-details .detail-label{{text-align:left;font-weight:bold;padding:10px;border-bottom:1px solid #000000;}}.order-details .detail-value{{text-align:right;padding:10px;border-bottom:1px solid #000000;}}.order-total{{font-weight:bold;border-top:2px solid #000000;}}.order-total .total-label{{text-align:left;padding:10px;}}.order-total .total-value{{text-align:right;padding:10px;}}.email-footer{{text-align:center;padding-top:10px;font-size:12px;color:#666666;}}.contact-info{{margin:10px 0;}}.email-link{{color:#000000;text-decoration:none;font-weight:bold;}}.thank-you{{margin-top:10px;}}</style><body><div class=\"email-container\"><table class=\"email-content\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\"><tbody><tr><td class=\"email-header\"><h1 class=\"email-title\">ModaPerfect Yeni Sipariş</h1></td></tr><tr><td class=\"email-body\"><p class=\"email-greeting\">Merhaba,</p><p class=\"email-introduction\">{addedOrder.CreatedDate} tarihli {addedOrder.OrderPrice} tutarlı yeni sipariş alınmıştır. Siparişler sayfasından kontrolleri sağlayabilirsiniz.</p></td></tr><tr><td class=\"email-footer\"><p class=\"contact-info\">Eğer bir sorun yaşıyorsanız lütfen iletişime geçin <a href=\"mailto:devs@modaperfect.com.tr\" class=\"email-link\">devs@modaperfect.com.tr</a></p><p class=\"thank-you\">ModaPerfect'i tercih ettiğiniz için teşekkür ederiz!</p></td></tr></tbody></table></div></body></html>"
                    }
                );

            CreatedOrderResponse response = _mapper.Map<CreatedOrderResponse>(order);
            return response;
        }
    }
}