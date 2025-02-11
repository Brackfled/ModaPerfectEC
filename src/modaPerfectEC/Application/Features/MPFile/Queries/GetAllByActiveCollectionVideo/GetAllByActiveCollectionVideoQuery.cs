using Amazon.Runtime.Internal;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MPFile.Queries.GetAllByActiveCollectionVideo;
public class GetAllByActiveCollectionVideoQuery:IRequest<ICollection<GetAllByActiveCollectionVideoListItemDto>>
{
    public class GetAllByCollectionVideoQueryHandler: IRequestHandler<GetAllByActiveCollectionVideoQuery, ICollection<GetAllByActiveCollectionVideoListItemDto>>
    {
        private readonly ICollectionVideoRepository _collectionVideoRepository;
        private IMapper _mapper;

        public GetAllByCollectionVideoQueryHandler(ICollectionVideoRepository collectionVideoRepository, IMapper mapper)
        {
            _collectionVideoRepository = collectionVideoRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<GetAllByActiveCollectionVideoListItemDto>> Handle(GetAllByActiveCollectionVideoQuery request, CancellationToken cancellationToken)
        {
            ICollection<CollectionVideo>? collectionVideos = await _collectionVideoRepository.GetAllAsync(cv => cv.CollectionState == Domain.Enums.CollectionState.Active);

            ICollection<GetAllByActiveCollectionVideoListItemDto> response = _mapper.Map<ICollection<GetAllByActiveCollectionVideoListItemDto>>(collectionVideos);
            return response;
        }
    }
}
