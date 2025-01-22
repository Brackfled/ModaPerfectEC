using Amazon.Runtime.Internal;
using Application.Features.MPFile.Constants;
using Application.Features.MPFile.Rules;
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

namespace Application.Features.MPFile.Queries.GetById;
public class GetByIdProductImageQuery: IRequest<GetByIdProductImageResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [MPFilesOperationClaims.Read];

    public class GetByIdProductImageQueryHandler: IRequestHandler<GetByIdProductImageQuery, GetByIdProductImageResponse>
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly MPFileBusinessRules _mPFileBusinessRules;
        private IMapper _mapper;

        public GetByIdProductImageQueryHandler(IProductImageRepository productImageRepository, MPFileBusinessRules mPFileBusinessRules, IMapper mapper)
        {
            _productImageRepository = productImageRepository;
            _mPFileBusinessRules = mPFileBusinessRules;
            _mapper = mapper;
        }

        public async Task<GetByIdProductImageResponse> Handle(GetByIdProductImageQuery request, CancellationToken cancellationToken)
        {
            ProductImage? productImage = await _productImageRepository.GetAsync(pi => pi.Id == request.Id);

            await _mPFileBusinessRules.MPFileShouldExistsWhenSelected(productImage!);

            GetByIdProductImageResponse response = _mapper.Map<GetByIdProductImageResponse>(productImage);
            return response;
        }
    }
}
