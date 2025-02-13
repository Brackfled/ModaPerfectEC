using Amazon.Runtime.Internal;
using Application.Features.MPFile.Constants;
using Application.Features.MPFile.Rules;
using Application.Services.CollectionVideos;
using Application.Services.Repositories;
using Application.Services.Stroage;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MPFile.Commands.CreateCollectionVideo;
public class CreateCollectionVideoCommand: IRequest<CreatedCollectionVideoResponse>, ITransactionalRequest//, ISecuredRequest
{
    public CreatedCollectionVideoRequest CreatedCollectionVideoRequest { get; set; }

    public string[] Roles => [ MPFilesOperationClaims.Create];

    public class CreateCollectionVideoCommandHandler: IRequestHandler<CreateCollectionVideoCommand, CreatedCollectionVideoResponse>
    {
        private readonly ICollectionVideoRepository _collectionVideoRepository;
        private readonly IStroageService _stroageService;
        private readonly MPFileBusinessRules _mPFileBusinessRules;
        private IMapper _mapper;

        public CreateCollectionVideoCommandHandler(ICollectionVideoRepository collectionVideoRepository, IStroageService stroageService, MPFileBusinessRules mPFileBusinessRules, IMapper mapper)
        {
            _collectionVideoRepository = collectionVideoRepository;
            _stroageService = stroageService;
            _mPFileBusinessRules = mPFileBusinessRules;
            _mapper = mapper;
        }

        public async Task<CreatedCollectionVideoResponse> Handle(CreateCollectionVideoCommand request, CancellationToken cancellationToken)
        {
            await _mPFileBusinessRules.CollectionVideoRequestCountMustBeCorrectCount(request.CreatedCollectionVideoRequest.FormFiles, 1);

            IList<CollectionVideo> collectionVideos = new List<CollectionVideo>();  

            foreach (IFormFile formFile in request.CreatedCollectionVideoRequest.FormFiles)
            {
                await _mPFileBusinessRules.FileIsVideoFile(formFile);

                Domain.Dtos.FileOptions fileOptions = await _stroageService.UploadFileAsync(formFile, "mp-collection-videos");

                CollectionVideo collectionVideo = new()
                {
                    Id = Guid.NewGuid(),
                    FileName = fileOptions.FileName,
                    FilePath = fileOptions.BucketName,
                    FileUrl = fileOptions.FileUrl,
                    CollectionName = request.CreatedCollectionVideoRequest.CollectionName,
                    CollectionState = Domain.Enums.CollectionState.Passive
                };

                CollectionVideo addedCollectionVideo = await _collectionVideoRepository.AddAsync(collectionVideo);
                collectionVideos.Add(collectionVideo);
            }

            CreatedCollectionVideoResponse response = new() { CollectionVideos = collectionVideos, CollectionName = request.CreatedCollectionVideoRequest.CollectionName};
            return response;
        }
    }
}
