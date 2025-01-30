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

namespace Application.Features.Products.Queries.GetListBySubCategoryId;
public class GetListBySubCategoryIdProductQuery: IRequest<ICollection<GetListBySubCategoryIdProductListItemDto>>
{
    public int SubCategoryId { get; set; }

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
                    predicate: p => p.SubCategoryId == request.SubCategoryId,
                    include: opt => opt.Include(p => p.ProductVariants)!.Include(p => p.ProductImages)!
                );

            ICollection<GetListBySubCategoryIdProductListItemDto> response = _mapper.Map<ICollection<GetListBySubCategoryIdProductListItemDto>>(products);
            return response;
        }
    }
}
