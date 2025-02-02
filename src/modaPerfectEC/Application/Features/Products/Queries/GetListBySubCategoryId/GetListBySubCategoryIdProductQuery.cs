using Amazon.Runtime.Internal;
using Application.Features.Products.Rules;
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
using static Application.Features.Products.Constants.ProductsOperationClaims;


namespace Application.Features.Products.Queries.GetListBySubCategoryId;
public class GetListBySubCategoryIdProductQuery: IRequest<ICollection<GetListBySubCategoryIdProductListItemDto>>, ISecuredRequest
{
    public string Name { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetListBySubCategoryIdProductQueryHandler: IRequestHandler<GetListBySubCategoryIdProductQuery, ICollection<GetListBySubCategoryIdProductListItemDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ProductBusinessRules _productBusinessRules;
        private IMapper _mapper;

        public GetListBySubCategoryIdProductQueryHandler(IProductRepository productRepository, ProductBusinessRules productBusinessRules, IMapper mapper)
        {
            _productRepository = productRepository;
            _productBusinessRules = productBusinessRules;
            _mapper = mapper;
        }

        public async Task<ICollection<GetListBySubCategoryIdProductListItemDto>> Handle(GetListBySubCategoryIdProductQuery request, CancellationToken cancellationToken)
        {
            ICollection<Product>? products = await _productRepository.GetAllAsync(
                    predicate: p => p.Name == request.Name,
                    include: opt => opt.Include(p => p.ProductVariants)!.Include(p => p.ProductImages)!
                );

            ICollection<GetListBySubCategoryIdProductListItemDto> response = _mapper.Map<ICollection<GetListBySubCategoryIdProductListItemDto>>(products);
            return response;
        }
    }
}
