using Hutech.Exam.Client.API;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Hutech.Exam.Client.Authentication;
using System.Net.Http.Headers;
using Hutech.Exam.Shared.DTO.Custom;

namespace Hutech.Exam.Client.Pages.Admin.HealthCheck
{
    public partial class HealthCheck : IDisposable
    {
        #region Private Fields
        [Inject] private HttpClient Http { get; set; } = default!;

        [Inject] private NavigationManager Nav { get; set; } = default!;

        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [Inject] private ISenderAPI SenderAPI { get; set; } = default!;

        List<CustomHealthCheck> healthChecks = [];
        List<CustomThongKeDoPhanManh> healthFragments = [];
        System.Timers.Timer? timer;

        const string ALERT_NO_WARNING_REBUID_INDEX = "Không phát hiện chỉ mục có độ phân mảnh cảnh báo >= 5% và tổng số trang >= 100";
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
            await FetchHealthConnectionAsync();
            await FetchHealthFragmentAsync();
            HandleTime();
        }

        private void HandleTime()
        {
            timer = new System.Timers.Timer();
            timer.Interval = 10000; // 1000 = 1ms
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Elapsed += (sender, e) =>
            {
                InvokeAsync(async () =>
                {
                    await FetchHealthConnectionAsync();
                    StateHasChanged();
                });
            };
        }
        #endregion

        #region Fetch Methods

        private async Task FetchHealthConnectionAsync()
        {
            healthChecks = await GetHealthCheckConnectionAPI();
        }

        private async Task FetchHealthFragmentAsync()
        {
            healthFragments = await GetHealthCheckFragmentAPI();
        }
        #endregion

        #region OnClick Methods

        private async Task OnClickReorganizeIndexAsync()
        {
            if(!ValidateIndex())
            {
                Snackbar.Add(ALERT_NO_WARNING_REBUID_INDEX, MudBlazor.Severity.Warning);
                return;
            } 
            
            if(await RebuildOrReorganizeIndexAPI())
            {
                await FetchHealthFragmentAsync();
            }    
        }

        #endregion

        #region Other Methods

        private bool ValidateIndex()
        {
            return healthFragments.Any(_ => _.DoPhanManh >= 5 && _.SoLuongTrang >= 100);
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        #endregion
    }
}
