using CloudinaryDotNet.Actions;

namespace RunningGroupsWeb.Interfaces
{
    public interface ICloudinaryInterface
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile formFile); // adding a image
        Task<DeletionResult> DeletePhotoAsync(string publicId); // deleting a image
    }
}
