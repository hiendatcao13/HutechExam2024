using Hutech.Exam.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Hutech.Exam.Client.Pages.Admin.MessageBox
{
    public partial class MBSuaSV
    {
        [Parameter]
        public string? mssv { get; set; }
        [Parameter]
        public string? hoVaTenLot { get; set; }
        [Parameter]
        public string? tenSV { get; set; }
        public ChiTietCaThi? chiTietCaThi { get; set; }
        [Parameter]
        public EventCallback onClickThoat { get; set; }
        [Parameter]
        public EventCallback onClickLuu { get; set; }
        [Parameter]
        public List<long>? listMaDes { get; set; }
        [Parameter]
        public long? selected_ma_de { get; set; }
        public void stateHasChange()
        {
            this.StateHasChanged();
        }
    }
}
