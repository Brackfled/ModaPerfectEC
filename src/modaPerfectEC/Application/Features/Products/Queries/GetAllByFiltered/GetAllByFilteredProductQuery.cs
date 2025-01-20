using Amazon.Runtime.Internal;
using Application.Services.ProductVariants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetAllByFiltered;
public class GetAllByFilteredProductQuery: IRequest<ICollection<GetAllByFilteredProductListItemDto>>
{

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
            ICollection<Product>? products = await _productRepository.GetAllAsync(
                include: opt => opt.Include(p => p.ProductVariants)!.Include(p => p.ProductImages)!
                );

            ICollection<GetAllByFilteredProductListItemDto> response = _mapper.Map<ICollection<GetAllByFilteredProductListItemDto>>(products);
            return response;
        }
    }
}
