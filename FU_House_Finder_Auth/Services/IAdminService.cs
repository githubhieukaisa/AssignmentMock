using FU_House_Finder_Auth.Dtos;

namespace FU_House_Finder_Auth.Services
{
    public interface IAdminService
    {
        Task<IEnumerable<UserPublicDto>> GetPendingLandlordsAsync();
        Task ApproveLandlordAsync(int landlordId);
        Task<UserPublicDto?> GetUserPublicAsync(int id);

    }
}
