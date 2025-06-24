using Hutech.Exam.Client.API;
using Hutech.Exam.Client.DAL;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Page;

namespace Hutech.Exam.Client.Pages.Admin.ManageAccount
{
    public partial class ManageAccount
    {
        private async Task<(List<UserDto>, int pageNumber, int pageSize)> Users_GetAll_PagedAPI(int pageNumber, int pageSize)
        {
            var response = await SenderAPI.GetAsync<Paged<UserDto>>($"api/users?pageNumber={pageNumber + 1}&pageSize={pageSize}");
            return (response.Success && response.Data != null) 
                ? (response.Data.Data, response.Data.TotalPages, response.Data.TotalRecords) 
                : ([], 0, 0);
        }

        private async Task<(List<UserDto>, int pageNumber, int pageSize)> Users_GetAll_Search_PagedAPI(string keyword, int pageNumber, int pageSize)
        {
            var response = await SenderAPI.GetAsync<Paged<UserDto>>($"api/users/search?keyword={keyword}&pageNumber={pageNumber + 1}&pageSize={pageSize}");
            return (response.Success && response.Data != null)
                ? (response.Data.Data, response.Data.TotalPages, response.Data.TotalRecords)
                : ([], 0, 0);
        }

        private async Task<bool> User_DeleteAPI(Guid userId)
        {
            var response = await SenderAPI.DeleteAsync<UserDto>($"api/users/{userId}");
            return response.Success;
        }
    }
}
