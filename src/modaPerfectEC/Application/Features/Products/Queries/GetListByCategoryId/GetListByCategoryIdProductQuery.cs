using Amazon.Runtime.Internal;
using Application.Features.Products.Rules;
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

namespace Application.Features.Products.Queries.GetListByCategoryId;
public class GetListByCategoryIdProductQuery: IRequest<ICollection<GetListByCategoryIdProductListItemDto>>
{
    public int CategoryId { get; set; }

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
                    predicate: p => p.CategoryId == request.CategoryId,
                    include:opt => opt.Include(p => p.ProductVariants)!.Include(p => p.ProductImages)!
                );

            ICollection<GetListByCategoryIdProductListItemDto> response = _mapper.Map<ICollection<GetListByCategoryIdProductListItemDto>>(products);
            return response;
        }
    }
}
