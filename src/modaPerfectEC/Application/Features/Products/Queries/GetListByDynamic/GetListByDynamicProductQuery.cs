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
using System.Linq.Dynamic.Core;

namespace Application.Features.Products.Queries.GetListByDynamic;
public class GetListByDynamicProductQuery: IRequest<ICollection<GetListByDynamicProductListItemDto>>, ISecuredRequest
{
    public GetListByDynamicProductRequest GetListByDynamicProductRequest { get; set; }


    public string[] Roles => [Admin, Read];

    public class GetListByDynamicProductQueryHandler: IRequestHandler<GetListByDynamicProductQuery, ICollection<GetListByDynamicProductListItemDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetListByDynamicProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<GetListByDynamicProductListItemDto>> Handle(GetListByDynamicProductQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Product>? products = await _productRepository.GetListByDynamicAsync(
                dynamic:request.GetListByDynamicProductRequest.DynamicQuery,
                include:opt => opt.Include(p => p.ProductVariants)!.Include(p => p.ProductImages)!.Include(p => p.Category)!.Include(p => p.SubCategory)!,
                size:1000,
                index:0,
                cancellationToken:cancellationToken
                );

            ICollection<Product> matchingProducts = await _productRepository.GetMatching(products.Items, request.GetListByDynamicProductRequest.Hex, request.GetListByDynamicProductRequest.Size);


            ICollection<GetListByDynamicProductListItemDto> response = _mapper.Map<ICollection<GetListByDynamicProductListItemDto>>(matchingProducts);
            return response;
        }
    }
}
