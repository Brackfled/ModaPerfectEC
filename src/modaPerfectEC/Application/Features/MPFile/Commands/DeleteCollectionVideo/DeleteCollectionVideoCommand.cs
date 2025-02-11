using Amazon.Runtime.Internal;
using Application.Features.MPFile.Rules;
using Application.Services.Repositories;
using Application.Services.Stroage;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MPFile.Commands.DeleteCollectionVideo;
public class DeleteCollectionVideoCommand: IRequest<DeletedCollectionVideoResponse>, ITransactionalRequest
{
    public Guid Id { get; set; }

    public class DeleteCollectionVideoCommandHandler: IRequestHandler<DeleteCollectionVideoCommand, DeletedCollectionVideoResponse>
    {
        private readonly ICollectionVideoRepository _collectionVideoRepository;
        private readonly IStroageService _stroageService;
        private readonly MPFileBusinessRules _mPFileBusinessRules;
        private IMapper _mapper;

        public DeleteCollectionVideoCommandHandler(ICollectionVideoRepository collectionVideoRepository, IStroageService stroageService, MPFileBusinessRules mPFileBusinessRules, IMapper mapper)
        {
            _collectionVideoRepository = collectionVideoRepository;
            _stroageService = stroageService;
            _mPFileBusinessRules = mPFileBusinessRules;
            _mapper = mapper;
        }

        public async Task<DeletedCollectionVideoResponse> Handle(DeleteCollectionVideoCommand request, CancellationToken cancellationToken)
        {
            CollectionVideo? collectionVideo = await _collectionVideoRepository.GetAsync(cv => cv.Id == request.Id);

            var fileOptions = await _stroageService.DeleteFileAsync(bucketName: collectionVideo!.FilePath, fileName: collectionVideo.FileName);

            CollectionVideo deletedCollectionVideo = await _collectionVideoRepository.DeleteAsync(collectionVideo, true);

            DeletedCollectionVideoResponse response = _mapper.Map<DeletedCollectionVideoResponse>(deletedCollectionVideo);
            return response;
        }
    }
}
