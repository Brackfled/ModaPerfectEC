using Amazon.Runtime.Internal;
using Application.Features.MPFile.Constants;
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

namespace Application.Features.MPFile.Queries.GetAllCollectionVideo;
public class GetAllCollectionVideoQuery: IRequest<ICollection<GetAllCollectionVideoListItemDto>>, ISecuredRequest
{
    public string[] Roles => [MPFilesOperationClaims.Read];

    public class GetAllCollectionVideoQueryHandler: IRequestHandler<GetAllCollectionVideoQuery, ICollection<GetAllCollectionVideoListItemDto>>
    {
        private readonly ICollectionVideoRepository _collectionVideoRepository;
        private IMapper _mapper;

        public GetAllCollectionVideoQueryHandler(ICollectionVideoRepository collectionVideoRepository, IMapper mapper)
        {
            _collectionVideoRepository = collectionVideoRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<GetAllCollectionVideoListItemDto>> Handle(GetAllCollectionVideoQuery request, CancellationToken cancellationToken)
        {
            ICollection<CollectionVideo>? collectionVideos = await _collectionVideoRepository.GetAllAsync();

            ICollection<GetAllCollectionVideoListItemDto> response = _mapper.Map<ICollection<GetAllCollectionVideoListItemDto>>(collectionVideos);
            return response;
        }
    }
}
