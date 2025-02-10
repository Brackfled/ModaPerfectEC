using Amazon.Runtime.Internal;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Pipelines.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.ProductVariants.Constants.ProductVariantsOperationClaims;

namespace Application.Features.ProductVariants.Queries.GetAllDecrasing;
public class GetAllDecrasingProductVariantQuery: IRequest<ICollection<GetAllDecrasingProductVariantListItemDto>>, ISecuredRequest
{

    public string[] Roles => [Admin, Read];
    public class GetAllDecrasingProductVariantQueryHandler: IRequestHandler<GetAllDecrasingProductVariantQuery, ICollection<GetAllDecrasingProductVariantListItemDto>>
    {
        private readonly IProductVariantRepository _productVariantRepository;
        private IMapper _mapper;

        public GetAllDecrasingProductVariantQueryHandler(IProductVariantRepository productVariantRepository, IMapper mapper)
        {
            _productVariantRepository = productVariantRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<GetAllDecrasingProductVariantListItemDto>> Handle(GetAllDecrasingProductVariantQuery request, CancellationToken cancellationToken)
        {
            ICollection<ProductVariant>? productVariants = await _productVariantRepository.GetAllAsync(
                    predicate: pv => pv.StockAmount <= 5,
                    include: opt => opt.Include(pv => pv.Product)!
                );

            ICollection<GetAllDecrasingProductVariantListItemDto> response = _mapper.Map<ICollection<GetAllDecrasingProductVariantListItemDto>>(productVariants);
            return response;
        }
    }
}
