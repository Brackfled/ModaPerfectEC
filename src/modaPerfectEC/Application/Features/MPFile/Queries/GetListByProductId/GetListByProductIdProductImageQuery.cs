using Amazon.Runtime.Internal;
using Application.Features.MPFile.Constants;
using Application.Features.Products.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MPFile.Queries.GetListByProductId;
public class GetListByProductIdProductImageQuery: IRequest<ICollection<GetListByProductIdProductImageListItemDto>>, ISecuredRequest
{
    public Guid ProductId { get; set; }

    public string[] Roles => [MPFilesOperationClaims.Read];

    public class GetListByProductIdProductImageQueryHandler: IRequestHandler<GetListByProductIdProductImageQuery, ICollection<GetListByProductIdProductImageListItemDto>>
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly ProductBusinessRules _productBusinessRules;
        private IMapper _mapper;

        public GetListByProductIdProductImageQueryHandler(IProductImageRepository productImageRepository, ProductBusinessRules productBusinessRules, IMapper mapper)
        {
            _productImageRepository = productImageRepository;
            _productBusinessRules = productBusinessRules;
            _mapper = mapper;
        }

        public async Task<ICollection<GetListByProductIdProductImageListItemDto>> Handle(GetListByProductIdProductImageQuery request, CancellationToken cancellationToken)
        {

            await _productBusinessRules.ProductIdShouldExistWhenSelected(request.ProductId, cancellationToken);

            ICollection<ProductImage>? productImages = await _productImageRepository.GetAllAsync(pi => pi.ProductId == request.ProductId);

            ICollection<GetListByProductIdProductImageListItemDto> response = _mapper.Map<ICollection<GetListByProductIdProductImageListItemDto>>(productImages);
            return response;
        }
    }
}
