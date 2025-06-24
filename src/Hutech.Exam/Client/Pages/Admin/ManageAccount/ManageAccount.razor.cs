using Hutech.Exam.Client.API;
using Hutech.Exam.Client.DAL;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Hutech.Exam.Shared.Models;
using Hutech.Exam.Client.Authentication;
using System.Net.Http.Headers;
using Hutech.Exam.Client.Components.Dialogs;
using MudBlazor;
using Hutech.Exam.Client.Pages.Admin.ManageLop.Dialog;
using Hutech.Exam.Client.Pages.Admin.ManageAccount.Dialog;

namespace Hutech.Exam.Client.Pages.Admin.ManageAccount
{
    public partial class ManageAccount
    {
        [Inject] private HttpClient Http { get; set; } = default!;

        [Inject] private NavigationManager Nav { get; set; } = default!;

        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [Inject] private Blazored.SessionStorage.ISessionStorageService SessionStorage { get; set; } = default!;

        [CascadingParameter] private Task<AuthenticationState>? AuthenticationState { get; set; }

        [Inject] private AdminHubService AdminHub { get; set; } = default!;

        [Inject] private ISenderAPI SenderAPI { get; set; } = default!;

        private List<UserDto> users { get; set; } = [];

        private const string NO_SELECT = "Vui lòng chọn ít nhất 1 đối tượng";
        private const string DELETE_USER_MESSAGE = "Bạn có chắc chắn muốn xóa người dùng này. Chỉ có phép xóa an toàn";


        protected override async Task OnInitializedAsync()
        {
            //xác thực người dùng
            var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;
            var token = (customAuthStateProvider != null) ? await customAuthStateProvider.GetToken() : null;
            if (!string.IsNullOrWhiteSpace(token))
            {
                Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
            else
            {
                Nav.NavigateTo("/admin", true);
            }
            await Start();
            await base.OnInitializedAsync();
        }

        private async Task Start()
        {
            await FetchUsers();
        }


        private async Task FetchUsers()
        {
            (users, totalPages_User, totalRecords_User) = await Users_GetAll_PagedAPI(currentPage_User, rowsPerPage_User);
            CreateFakeData_User();
        }

        private async Task OnClickThemUser()
        {

            var result = await OpenThemUserDialog();

            if (result != null && !result.Canceled && result.Data is UserDto newUser)
            {
                users?.Insert(0, newUser);
            }
        }

        private async Task<DialogResult?> OpenThemUserDialog()
        {
            var parameters = new DialogParameters<ThemUserDialog>{};
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<ThemUserDialog>("THÊM NGƯỜI DÙNG", parameters, options);
            return await dialog.Result;
        }

        private async Task OnClickSuaUser()
        {
            if (selectedUser == null)
            {
                Snackbar.Add(NO_SELECT, Severity.Warning);
                return;
            }

            var result = await OpenSuaUserDialog(selectedUser);

            if (result != null && !result.Canceled && result.Data is UserDto newUser && users != null)
            {
                int index = users.FindIndex(k => k.UserId == newUser.UserId);
                if (index != -1)
                {
                    users[index] = newUser;
                }
            }
        }

        private async Task<DialogResult?> OpenSuaUserDialog(UserDto user)
        {
            var parameters = new DialogParameters<SuaUserDialog>
            {
                { x => x.User, user },
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<SuaUserDialog>("SỬA NGƯỜI DÙNG", parameters, options);
            return await dialog.Result;
        }

        private async Task OnClickChangePassword()
        {
            if (selectedUser == null)
            {
                Snackbar.Add(NO_SELECT, Severity.Warning);
                return;
            }

            var parameters = new DialogParameters<ChangePasswordDialog>
            {
                { x => x.User, selectedUser },
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<ChangePasswordDialog>("THAY ĐỔI MẬT KHẨU", parameters, options);
        }

        private async Task OnClickXoaUser()
        {
            if (selectedUser == null)
            {
                Snackbar.Add(NO_SELECT, Severity.Warning);
                return;
            }

            var parameters = new DialogParameters<Delete_Dialog>
            {
                { x => x.ContentText, DELETE_USER_MESSAGE },
                { x => x.onHandleRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteUser(false))   },
                { x => x.onHandleForceRemove, EventCallback.Factory.Create(this, () => { })   }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Delete_Dialog>("XÓA NGƯỜI DÙNG", parameters, options);

        }

        private async Task HandleDeleteUser(bool isForce)
        {
            if (selectedUser != null)
            {
                var result = await User_DeleteAPI(selectedUser.UserId);

                if (result)
                {
                    users?.Remove(selectedUser);
                    selectedUser = null;
                }
            }
        }

        private void CreateFakeData_User()
        {
            if (users != null && users.Count != 0)
            {
                int count_fake = totalRecords_User - users.Count;
                bool isFake = totalRecords_User > users.Count;
                if (isFake)
                {
                    for (int i = 0; i < count_fake; i++)
                        users.Add(new UserDto());
                }
            }
        }

        private void PadEmptyRows(List<UserDto>? newUsers)
        {
            if (newUsers == null || newUsers.Count == 0)
                return;
            // tìm phần tử đầu tiên của trang đó
            int startRow = currentPage_User * rowsPerPage_User;
            if (users != null && users.Count != 0)
            {
                for (int i = 0; i < newUsers.Count; i++)
                {

                    users[startRow++] = newUsers[i];
                }
            }
            StateHasChanged();
        }
    }
}
