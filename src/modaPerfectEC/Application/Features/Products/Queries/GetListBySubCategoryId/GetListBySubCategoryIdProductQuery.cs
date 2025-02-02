using Amazon.Runtime.Internal;
using Application.Features.Products.Rules;
using Application.Features.SubCategories.Rules;
using Application.Services.Repositories;
using Application.Services.SubCategories;
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
        private readonly ISubCategoryService _subCategoryService;
        private readonly SubCategoryBusinessRules _subCategoryBusinessRules;
        private IMapper _mapper;

        public GetListBySubCategoryIdProductQueryHandler(IProductRepository productRepository, ProductBusinessRules productBusinessRules, ISubCategoryService subCategoryService, SubCategoryBusinessRules subCategoryBusinessRules, IMapper mapper)
        {
            _productRepository = productRepository;
            _productBusinessRules = productBusinessRules;
            _subCategoryService = subCategoryService;
            _subCategoryBusinessRules = subCategoryBusinessRules;
            _mapper = mapper;
        }

        public async Task<ICollection<GetListBySubCategoryIdProductListItemDto>> Handle(GetListBySubCategoryIdProductQuery request, CancellationToken cancellationToken)
        {
            SubCategory? subCategory = await _subCategoryService.GetAsync(s => s.Name == request.Name);
            await _subCategoryBusinessRules.SubCategoryShouldExistWhenSelected(subCategory);

            ICollection<Product>? products = await _productRepository.GetAllAsync(
                    predicate: p => p.SubCategoryId == subCategory!.Id,
                    include: opt => opt.Include(p => p.ProductVariants)!.Include(p => p.ProductImages)!
                );

            ICollection<GetListBySubCategoryIdProductListItemDto> response = _mapper.Map<ICollection<GetListBySubCategoryIdProductListItemDto>>(products);
            return response;
        }
    }
}
