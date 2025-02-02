using Amazon.Runtime.Internal;
using Application.Features.Baskets.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Baskets.Constants.BasketsOperationClaims;

namespace Application.Features.Baskets.Queries.GetFromAuth;
public class GetFromAuthBasketQuery: IRequest<GetFromAuthBasketResponse>, ISecuredRequest
{
    public Guid UserId { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetFromAuthBasketQueryHandler: IRequestHandler<GetFromAuthBasketQuery, GetFromAuthBasketResponse>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly BasketBusinessRules _basketBusinessRules;
        private IMapper _mapper;

        public GetFromAuthBasketQueryHandler(IBasketRepository basketRepository, BasketBusinessRules basketBusinessRules, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _basketBusinessRules = basketBusinessRules;
            _mapper = mapper;
        }

        public async Task<GetFromAuthBasketResponse> Handle(GetFromAuthBasketQuery request, CancellationToken cancellationToken)
        {
            Basket? basket = await _basketRepository.GetAsync(b => b.UserId == request.UserId && b.IsOrderBasket == false);
            await _basketBusinessRules.BasketShouldExistWhenSelected(basket);

            GetFromAuthBasketResponse response = _mapper.Map<GetFromAuthBasketResponse>(basket);
            return response;
        }
    }
}
