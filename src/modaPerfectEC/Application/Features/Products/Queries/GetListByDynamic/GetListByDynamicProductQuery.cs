using Amazon.Runtime.Internal;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Dynamic;
using NArchitecture.Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Application.Features.Products.Constants.ProductsOperationClaims;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetListByDynamic;
public class GetListByDynamicProductQuery: IRequest<GetListResponse<GetListByDynamicProductListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery DynamicQuery { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetListByDynamicProductQueryHandler: IRequestHandler<GetListByDynamicProductQuery, GetListResponse<GetListByDynamicProductListItemDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetListByDynamicProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByDynamicProductListItemDto>> Handle(GetListByDynamicProductQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Product>? products = await _productRepository.GetListByDynamicAsync(
                dynamic:request.DynamicQuery,
                include:opt => opt.Include(p => p.ProductVariants)!.Include(p => p.ProductImages)!.Include(p => p.Category)!.Include(p => p.SubCategory)!,
                size:request.PageRequest.PageSize,
                index:request.PageRequest.PageIndex,
                cancellationToken:cancellationToken
                );

            GetListResponse<GetListByDynamicProductListItemDto> response = _mapper.Map<GetListResponse<GetListByDynamicProductListItemDto>>(products);
            return response;
        }
    }
}
