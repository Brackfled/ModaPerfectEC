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


namespace Application.Features.Products.Queries.GetListByCategoryId;
public class GetListByCategoryIdProductQuery: IRequest<ICollection<GetListByCategoryIdProductListItemDto>>, ISecuredRequest
{
    public string Name { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetListByCategoryIdProductQueryHandler: IRequestHandler<GetListByCategoryIdProductQuery, ICollection<GetListByCategoryIdProductListItemDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly ProductBusinessRules _productBusinessRules;
        private IMapper _mapper;

        public GetListByCategoryIdProductQueryHandler(IProductRepository productRepository, ProductBusinessRules productBusinessRules, IMapper mapper)
        {
            _productRepository = productRepository;
            _productBusinessRules = productBusinessRules;
            _mapper = mapper;
        }

        public async Task<ICollection<GetListByCategoryIdProductListItemDto>> Handle(GetListByCategoryIdProductQuery request, CancellationToken cancellationToken)
        {
            ICollection<Product>? products = await _productRepository.GetAllAsync(
                    predicate: p => p.Name == request.Name,
                    include:opt => opt.Include(p => p.ProductVariants)!.Include(p => p.ProductImages)!
                );

            ICollection<GetListByCategoryIdProductListItemDto> response = _mapper.Map<ICollection<GetListByCategoryIdProductListItemDto>>(products);
            return response;
        }
    }
}
