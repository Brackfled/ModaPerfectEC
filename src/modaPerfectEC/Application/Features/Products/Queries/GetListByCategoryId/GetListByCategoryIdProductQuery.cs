using Amazon.Runtime.Internal;
using Application.Features.Categories.Rules;
using Application.Features.Products.Rules;
using Application.Services.Categories;
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
        private readonly ICategoryService _categoryService;
        private readonly CategoryBusinessRules _categoryBusinessRules;
        private IMapper _mapper;

        public GetListByCategoryIdProductQueryHandler(IProductRepository productRepository, ProductBusinessRules productBusinessRules, ICategoryService categoryService, CategoryBusinessRules categoryBusinessRules, IMapper mapper)
        {
            _productRepository = productRepository;
            _productBusinessRules = productBusinessRules;
            _categoryService = categoryService;
            _categoryBusinessRules = categoryBusinessRules;
            _mapper = mapper;
        }

        public async Task<ICollection<GetListByCategoryIdProductListItemDto>> Handle(GetListByCategoryIdProductQuery request, CancellationToken cancellationToken)
        {
            Category? category = await _categoryService.GetAsync(c => c.Name == request.Name);
            await _categoryBusinessRules.CategoryShouldExistWhenSelected(category);

            ICollection<Product>? products = await _productRepository.GetAllAsync(
                    predicate: p => p.CategoryId == category!.Id,
                    include:opt => opt.Include(p => p.ProductVariants)!.Include(p => p.ProductImages)!
                );

            ICollection<GetListByCategoryIdProductListItemDto> response = _mapper.Map<ICollection<GetListByCategoryIdProductListItemDto>>(products);
            return response;
        }
    }
}
