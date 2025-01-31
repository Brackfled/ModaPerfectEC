using Amazon.Runtime.Internal;
using Application.Features.BasketItems.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BasketItems.Queries.GetFromAuth;
public class GetByBasketIdBasketItemQuery: IRequest<ICollection<GetByBasketIdBasketItemListItemDto>>
{
    public Guid BasketId { get; set; }

    public class GetByBasketIdBasketItemQueryHandler: IRequestHandler<GetByBasketIdBasketItemQuery, ICollection<GetByBasketIdBasketItemListItemDto>>
    {
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly BasketItemBusinessRules _basketItemBusinessRules;
        private IMapper _mapper;

        public GetByBasketIdBasketItemQueryHandler(IBasketItemRepository basketItemRepository, BasketItemBusinessRules basketItemBusinessRules, IMapper mapper)
        {
            _basketItemRepository = basketItemRepository;
            _basketItemBusinessRules = basketItemBusinessRules;
            _mapper = mapper;
        }

        public async Task<ICollection<GetByBasketIdBasketItemListItemDto>> Handle(GetByBasketIdBasketItemQuery request, CancellationToken cancellationToken)
        {
            ICollection<BasketItem> basketItems = await _basketItemRepository.GetAllAsync(
                    predicate:bi => bi.BasketId == request.BasketId,
                    include: opt => opt.Include(bi => bi.Basket)!.Include(bi => bi.Product)!.ThenInclude(p => p.ProductImages).Include(bi => bi.ProductVariant)!
                );

            ICollection<GetByBasketIdBasketItemListItemDto> response = _mapper.Map<ICollection<GetByBasketIdBasketItemListItemDto>>(basketItems);
            return response;
        }
    }
}
