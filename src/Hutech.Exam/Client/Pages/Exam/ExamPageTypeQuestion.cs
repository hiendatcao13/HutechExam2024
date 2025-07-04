using HtmlAgilityPack;
using Hutech.Exam.Shared.DTO.Custom;
using Microsoft.JSInterop;
using System.Text.RegularExpressions;

namespace Hutech.Exam.Client.Pages.Exam
{
    public partial class ExamPage
    {
        private bool isFirstTimeGetAudio = true;
        private void ModifyGroupQuestion()
        {
            int ma_nhom = 0;
            if (CustomExams != null)
            {
                int thu_tu_cau_hoi = 0;
                foreach (var item in CustomExams)
                {
                    // thêm các câu hỏi vào danh sách check đã khoanh hay chưa?
                    SelectedAnswers?.Add(item.MaCauHoi, (++thu_tu_cau_hoi, null));

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
                        if (item.KieuNoiDungCauHoiNhom == 2 && item.NoiDungCauHoiNhom.Contains("<audio") && ExamSessionDetail != null)
                        {
                            //xử lí dsAudioListened
                            AudioListeneds.Add(item.MaNhom, new () { AudioUrl = HandleAudioSource(item.NoiDungCauHoiNhom), AudioText = HandleTextBeforeAudio(item.NoiDungCauHoiNhom)});
                            item.GhiChu = 0 + "";
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
        private static string? HandleAudioSource(string text)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(text);

            var audioNode = doc.DocumentNode.SelectSingleNode("//audio");

            if (audioNode != null)
            {
                // Ưu tiên src trực tiếp trong thẻ <audio>
                var audioSrc = audioNode.GetAttributeValue("src", string.Empty);
                if (!string.IsNullOrEmpty(audioSrc))
                    return audioSrc;

                // Nếu không có, tìm thẻ <source>
                var sourceNode = audioNode.SelectSingleNode(".//source");
                if (sourceNode != null)
                {
                    var sourceSrc = sourceNode.GetAttributeValue("src", string.Empty);
                    if (!string.IsNullOrEmpty(sourceSrc))
                        return sourceSrc;
                }
            }

            return null;
        }
        private string HandleTextBeforeAudio(string text)
        {
            int index_audio = text.IndexOf("<audio");
            if(index_audio == -1)
                return string.Empty;
            return text.Substring(0, index_audio);
        }
        private async Task OnPlayAudioAsync(CustomDeThi deThi, int ma_nhom)
        {
            // Nếu lần đầu tiên, gọi API và cập nhật GhiChu
            if (isFirstTimeGetAudio || deThi.GhiChu != "-1")
            {
                var soLanNghe = await GetTotalAudioListenedAPI(new Shared.DTO.AudioListenedDto
                {
                    MaChiTietCaThi = ExamSessionDetail.MaChiTietCaThi,
                    TenFile = AudioListeneds[ma_nhom].AudioUrl
                });

                deThi.GhiChu = soLanNghe.ToString();
                isFirstTimeGetAudio = false;
                TheSateHasChanged();

                // Nếu không còn lượt nghe, dừng lại
                if (deThi.GhiChu == "-1")
                {
                    await Js.InvokeVoidAsync("stopAudio", ma_nhom);
                    return;
                }
            }

            // Nếu không còn lượt nghe, dừng lại
            if (deThi.GhiChu == "-1")
            {
                await Js.InvokeVoidAsync("stopAudio", ma_nhom);
                return;
            }

            await Js.InvokeVoidAsync("playAudioFromStart", ma_nhom);
        }
    }
}
