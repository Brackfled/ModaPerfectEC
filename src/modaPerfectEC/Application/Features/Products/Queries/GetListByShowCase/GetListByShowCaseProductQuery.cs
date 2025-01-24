using Amazon.Runtime.Internal;
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

namespace Application.Features.Products.Queries.GetListByShowCase;
public class GetListByShowCaseProductQuery: IRequest<ICollection<GetListByShowCaseProductListItemDto>>
{
    public class GetListByShowCaseProductQueryHandler: IRequestHandler<GetListByShowCaseProductQuery, ICollection<GetListByShowCaseProductListItemDto>>
    {
        private readonly IProductRepository _productRepository;
        private IMapper _mapper;

        public GetListByShowCaseProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<GetListByShowCaseProductListItemDto>> Handle(GetListByShowCaseProductQuery request, CancellationToken cancellationToken)
        {
            ICollection<Product> products = await _productRepository.GetAllAsync(
                p => p.ProductState == Domain.Enums.ProductState.Showcase,
                include: opt => opt.Include(p => p.ProductVariants)!.Include(p => p.ProductImages)!.Include(p => p.Category)!.Include(p => p.SubCategory)!
                );

            ICollection<GetListByShowCaseProductListItemDto> response = _mapper.Map<ICollection<GetListByShowCaseProductListItemDto>>(products);
            return response;

        }
    }
}
