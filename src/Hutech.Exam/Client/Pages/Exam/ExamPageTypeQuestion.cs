using Hutech.Exam.Shared.DTO.Custom;
using Microsoft.JSInterop;
using System.Text.RegularExpressions;

namespace Hutech.Exam.Client.Pages.Exam
{
    public partial class ExamPage
    {
        private async Task ModifyNhomCauHoi()
        {
            int ma_nhom = 0;
            if (CustomDeThis != null)
            {
                foreach (var item in CustomDeThis)
                {
                    // thêm các câu hỏi vào danh sách check đã khoanh hay chưa?
                    DSKhoanhDapAn?.Add(item.MaCauHoi, null);

                    // thêm các chữ A,B,C,D vào nội dung câu trả lời
                    if (item.CauTraLois != null)
                    {
                        int stt = 0;
                        var keys = item.CauTraLois.Keys.ToList();
                        foreach (var key in keys)
                        {
                            item.CauTraLois[key] = $"{_alphabet[stt++]}. {item.CauTraLois[key]}";
                        }
                    }    
                    
                    if (item.MaNhom != ma_nhom && !string.IsNullOrEmpty(item.NoiDungCauHoiNhom))
                    {
                        ma_nhom = item.MaNhom;

                        // xử lí audio
                        if (item.KieuNoiDungCauHoiNhom == 2 && item.NoiDungCauHoiNhom.Contains("<audio") && ChiTietCaThi != null)
                        {
                            string fileName = HandleAudioSource(item.NoiDungCauHoiNhom);
                            int so_lan_nghe = (ChiTietCaThi.DaThi) ? await GetSoLanNgheAPI(ChiTietCaThi.MaChiTietCaThi, fileName) : 0;
                            item.GhiChu = so_lan_nghe.ToString();
                        }
                        // xử lí câu hỏi điền khuyết
                        if (item.KieuNoiDungCauHoiNhom == 1 && item.NoiDungCauHoiNhom.Contains("(*)"))
                        {
                            item.NoiDungCauHoiNhom = HandleDienKhuyet(item.NoiDungCauHoiNhom, item.ThuTuCauHoi);
                        }
                    }
                }
            }
        }
        private static string HandleDienKhuyet(string text, long STT)
        {
            if (!text.Contains("(*)"))
                return text;
            return Regex.Replace(text, @"\(\*\)", m => "(" + (STT++).ToString() + ")");
        }
        private static string HandleAudioSource(string text)
        {
            text = text.Trim();
            int index_source = text.IndexOf("src=\"");
            int length = "src=\"".Length;
            string source = text.Substring(index_source + length);
            int end_source = source.IndexOf("\"/>");
            source = source.Substring(0, end_source);
            int index_audio = text.IndexOf("<audio");
            return source;
        }
        private string HandleBeforeAudio(CustomDeThi customDeThi, string text, int ma_audio)
        {
            int index_audio = text.IndexOf("<audio");
            if (int.TryParse(customDeThi.GhiChu, out int temp) && ChiTietCaThi != null)
            {
                if (temp == 3)
                    isDisableAudio?.Insert(ma_audio, true);
                else
                    isDisableAudio?.Insert(ma_audio, false);
            }
            return text.Substring(0, index_audio);
        }
        private async Task OnPlayAudio(CustomDeThi customDeThi, int ma_audio, string fileName, string elementId)
        {
            // tăng số lần nghe lên
            if (int.TryParse(customDeThi.GhiChu, out int so_lan_nghe) && ChiTietCaThi != null && Js != null)
            {
                await Js.InvokeVoidAsync("playAudio", elementId, so_lan_nghe);
                if (so_lan_nghe < 3 && isDisableAudio != null)
                {
                    customDeThi.GhiChu = (++so_lan_nghe).ToString();
                    await AddOrUpdateListenAPI(ChiTietCaThi.MaChiTietCaThi, fileName);
                }
            }
            StateHasChanged();
        }
    }
}
