using Hutech.Exam.Client.API;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Hutech.Exam.Client.Authentication;
using System.Net.Http.Headers;
using Hutech.Exam.Client.Components.Dialogs;
using MudBlazor;
using Hutech.Exam.Client.Pages.Admin.ManageAccount.Dialog;

namespace Hutech.Exam.Client.Pages.Admin.ManageAccount
{
    public partial class ManageAccount
    {
        #region Private Fields
        [Inject] private HttpClient Http { get; set; } = default!;

        [Inject] private NavigationManager Nav { get; set; } = default!;

        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [Inject] private ISenderAPI SenderAPI { get; set; } = default!;

        List<UserDto> users = [];

        private const string NO_SELECT = "Vui lòng chọn ít nhất 1 đối tượng";
        private const string DELETE_USER_MESSAGE = "Bạn có chắc chắn muốn xóa người dùng này. Chỉ có phép xóa an toàn";

        #endregion

        #region Initial Methods

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
            await StartAsync();
            await base.OnInitializedAsync();
        }

        private async Task StartAsync()
        {
            await FetchUsersAsync();
        }

        #endregion

        #region Fetch Methods
        private async Task FetchUsersAsync()
        {
            (users, totalPages_User, totalRecords_User) = await Users_GetAll_PagedAPI(currentPage_User, rowsPerPage_User);
            CreateFakeData_User();
        }

        #endregion

        #region OnClick Methods

        private async Task OnClickAddUserAsync()
        {

            var result = await OpenAddUserDialogAsync();

            if (result != null && !result.Canceled && result.Data is UserDto newUser)
            {
                users?.Insert(0, newUser);
            }
        }

        private async Task OnClickEditUserAsync()
        {
            if (selectedUser == null)
            {
                Snackbar.Add(NO_SELECT, Severity.Warning);
                return;
            }

            var result = await OpenEditUserDialogAsync(selectedUser);

            if (result != null && !result.Canceled && result.Data is UserDto newUser && users != null)
            {
                int index = users.FindIndex(k => k.MaNguoiDung == newUser.MaNguoiDung);
                if (index != -1)
                {
                    users[index] = newUser;
                }
            }
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
                { x => x.User, selectedUser }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<ChangePasswordDialog>("THAY ĐỔI MẬT KHẨU", parameters, options);
        }

        private async Task OnClickDeleteUserAsync()
        {
            if (selectedUser == null)
            {
                Snackbar.Add(NO_SELECT, Severity.Warning);
                return;
            }

            var parameters = new DialogParameters<Delete_Dialog>
            {
                { x => x.ContentText, DELETE_USER_MESSAGE },
                { x => x.onHandleRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteUserAsync(false))   },
                { x => x.onHandleForceRemove, EventCallback.Factory.Create(this, () => { })   }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Delete_Dialog>("XÓA NGƯỜI DÙNG", parameters, options);

        }

        #endregion

        #region Dialog Methods
        private async Task<DialogResult?> OpenAddUserDialogAsync()
        {
            var parameters = new DialogParameters<AddUserDialog>{};
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<AddUserDialog>("THÊM NGƯỜI DÙNG", parameters, options);
            return await dialog.Result;
        }



        private async Task<DialogResult?> OpenEditUserDialogAsync(UserDto user)
        {
            var parameters = new DialogParameters<EditUserDialog>
            {
                { x => x.User, user },
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<EditUserDialog>("SỬA NGƯỜI DÙNG", parameters, options);
            return await dialog.Result;
        }

        #endregion

        #region HandleDialog Methods
        private async Task HandleDeleteUserAsync(bool isForce)
        {
            if (selectedUser != null)
            {
                var result = await User_DeleteAPI(selectedUser.MaNguoiDung);

                if (result)
                {
                    users?.Remove(selectedUser);
                    selectedUser = null;
                }
            }
        }

        #endregion

        #region Other Methods
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
        #endregion
    }
}
