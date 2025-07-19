using Blazored.SessionStorage;
using Hutech.Exam.Client.Extensions;
using Hutech.Exam.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Hutech.Exam.Client.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorageService;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ISessionStorageService sessionStorageService)
        {
            _sessionStorageService = sessionStorageService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSession = await _sessionStorageService.ReadEncryptedItemAsync<UserSession>("UserSession");
                if (userSession == null)
                {
                    // không tìm thấy người dùng thì trả về vô danh
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                }
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.Name!),
                    new Claim(ClaimTypes.NameIdentifier, userSession.Username!)
                };

                // Thêm từng role riêng biệt
                foreach (var role in userSession.Roles!)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "JwtAuth"));
                // trả về giấy xác thực người dùng
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                // nếu không xác thực được người dùng hoặc lỗi xác thực thì trả về người vô danh
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }
        public async Task UpdateAuthenticationState(UserSession? userSession)
        {
            // cập nhật về việc người dùng login và logout - cập nhật lại sessionStorage
            // và thông báo tình trạng việc xác thực đã có sự thay đổi
            // khi log out thì tham số userSession sẽ là null
            ClaimsPrincipal claimsPrincipal;
            if (userSession != null) // khi nguời dùng log in
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.Name!),
                    new Claim(ClaimTypes.NameIdentifier, userSession.Username!)
                };

                // Thêm từng role riêng biệt
                foreach (var role in userSession.Roles!)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims));
                userSession.ExpiryTimeStamp = DateTime.Now.AddSeconds(userSession.ExpireIn);
                await _sessionStorageService.SaveItemEncryptedAsync("UserSession", userSession);
            }
            else // khi người dùng log out
            {
                claimsPrincipal = _anonymous;
                await _sessionStorageService.RemoveItemAsync("UserSession");
                // xóa các dữ liệu trong SessionStorage
                await _sessionStorageService.ClearAsync();
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
        public async Task<string> GetToken()
        {
            //để các các razor component lấy mã token
            var result = string.Empty;
            try
            {
                var userSession = await _sessionStorageService.ReadEncryptedItemAsync<UserSession>("UserSession");
                if (userSession != null && DateTime.Now < userSession.ExpiryTimeStamp)
                {
                    result = userSession.Token;
                }
            }
            catch { }
            return result ?? string.Empty;
        }
    }
}
