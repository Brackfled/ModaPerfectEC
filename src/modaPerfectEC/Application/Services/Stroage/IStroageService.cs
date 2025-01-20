using Amazon.S3.Model;
using Domain.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Stroage;
public interface IStroageService
{
    Task<string> CreateBucketAsync(string bucketName);
    Task<ListBucketsResponse> GetListBucketsAsync();
    Task<string> DeleteBucketAsync(string bucketName);
    Task<Domain.Dtos.FileOptions> UploadFileAsync(IFormFile formFile, string bucketName);
    Task<List<S3ObjectDto>> GetListFilesAsync(string bucketName);
    Task<DeleteObjectResponse> DeleteFileAsync(string bucketName, string fileName);

}
