using Amazon.Runtime.Internal;
using Application.Features.MPFile.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MPFile.Commands.UpdateCollectionStateCollectionVideo;
public class UpdateCollectionStateCollectionVideoCommand: IRequest<UpdatedCollectionStateCollectionVideoResponse>, ITransactionalRequest
{
    public Guid Id { get; set; }
    public CollectionState CollectionState { get; set; }

    public class UpdateCollectionStateCollectionVideoCommandHandler: IRequestHandler<UpdateCollectionStateCollectionVideoCommand, UpdatedCollectionStateCollectionVideoResponse>
    {
        private readonly ICollectionVideoRepository _collectionVideoRepository;
        private readonly MPFileBusinessRules _mPFileBusinessRules;
        private IMapper _mapper;

        public UpdateCollectionStateCollectionVideoCommandHandler(ICollectionVideoRepository collectionVideoRepository, MPFileBusinessRules mPFileBusinessRules, IMapper mapper)
        {
            _collectionVideoRepository = collectionVideoRepository;
            _mPFileBusinessRules = mPFileBusinessRules;
            _mapper = mapper;
        }

        public async Task<UpdatedCollectionStateCollectionVideoResponse> Handle(UpdateCollectionStateCollectionVideoCommand request, CancellationToken cancellationToken)
        {
            CollectionVideo? collectionVideo = await _collectionVideoRepository.GetAsync(cv => cv.Id == request.Id);

            collectionVideo!.CollectionState = request.CollectionState;
            CollectionVideo updatedCollectionVideo = await _collectionVideoRepository.UpdateAsync(collectionVideo);

            UpdatedCollectionStateCollectionVideoResponse response = _mapper.Map<UpdatedCollectionStateCollectionVideoResponse>(updatedCollectionVideo);
            return response;

        }
    }
}
