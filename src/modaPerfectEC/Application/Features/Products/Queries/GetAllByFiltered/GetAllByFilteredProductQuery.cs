using Amazon.Runtime.Internal;
using Application.Services.ProductVariants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Pipelines.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Application.Features.Products.Constants.ProductsOperationClaims;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetAllByFiltered;
public class GetAllByFilteredProductQuery: IRequest<ICollection<GetAllByFilteredProductListItemDto>>, ISecuredRequest
{

    public ProductState? ProductState { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetAllByFilteredProductQueryHandler: IRequestHandler<GetAllByFilteredProductQuery, ICollection<GetAllByFilteredProductListItemDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductVariantService _productVariantService;
        private IMapper _mapper;

        public GetAllByFilteredProductQueryHandler(IProductRepository productRepository, IProductVariantService productVariantService, IMapper mapper)
        {
            _productRepository = productRepository;
            _productVariantService = productVariantService;
            _mapper = mapper;
        }

        public async Task<ICollection<GetAllByFilteredProductListItemDto>> Handle(GetAllByFilteredProductQuery request, CancellationToken cancellationToken)
        {
            ICollection<Product>? products = request.ProductState switch
            {
                Domain.Enums.ProductState.Active => await _productRepository.GetAllAsync(predicate: p => p.ProductState == Domain.Enums.ProductState.Active || p.ProductState == Domain.Enums.ProductState.Showcase, include: opt => opt.Include(p => p.ProductVariants)!.Include(p => p.ProductImages)!.Include(p => p.Category)!.Include(p => p.SubCategory)!),
                Domain.Enums.ProductState.Passive => await _productRepository.GetAllAsync(predicate: p => p.ProductState == Domain.Enums.ProductState.Passive, include: opt => opt.Include(p => p.ProductVariants)!.Include(p => p.ProductImages)!.Include(p => p.Category)!.Include(p => p.SubCategory)!),
                Domain.Enums.ProductState.Showcase => await _productRepository.GetAllAsync(predicate: p => p.ProductState == Domain.Enums.ProductState.Showcase, include: opt => opt.Include(p => p.ProductVariants)!.Include(p => p.ProductImages)!.Include(p => p.Category)!.Include(p => p.SubCategory)!),
                _ => await _productRepository.GetAllAsync(include: opt => opt.Include(p => p.ProductVariants)!.Include(p => p.ProductImages)!.Include(p => p.Category)!.Include(p => p.SubCategory)!),

            };


            ICollection<GetAllByFilteredProductListItemDto> response = _mapper.Map<ICollection<GetAllByFilteredProductListItemDto>>(products);
            return response;
        }
    }
}
