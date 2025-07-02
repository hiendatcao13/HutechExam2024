using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Hutech.Exam.Client.Authentication;
using System.Net.Http.Headers;
using Hutech.Exam.Shared.DTO;
using Hutech.Exam.Shared.DTO.Custom;
using AutoMapper;
using Hutech.Exam.Client.Components.Dialogs;
using MudBlazor;
using Hutech.Exam.Client.Pages.Admin.ExamQuestion.Dialog;
using Hutech.Exam.Client.API;
using Hutech.Exam.Client.Pages.Admin.OrganizeExam.Dialog;

namespace Hutech.Exam.Client.Pages.Admin.ExamQuestion
{
    public partial class ExamQuestion
    {
        #region Private Fields
        [Inject] private HttpClient Http { get; set; } = default!;

        [Inject] private NavigationManager Nav { get; set; } = default!;

        [Inject] private IMapper Mapper { get; set; } = default!;

        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
        
        [Inject] private Blazored.SessionStorage.ISessionStorageService SessionStorage { get; set; } = default!;

        [Inject] private ISenderAPI SenderAPI { get; set; } = default!;

        List<MonHocDto>? subjects = [];
        MonHocDto? selectedSubject;

        List<DeThiDto>? exams = [];
        DeThiDto? selectedExam;

        List<NhomCauHoiDto>? groupQuestions = []; // ds các nhóm câu hỏi gốc
        List<CustomNhomCauHoi>? customNhomCauHois = [];
        CustomNhomCauHoi? selectedGroupQuesion;

        CauHoiDto? selectedQuestion;

        List<CauHoiDto>? cauHois = [];

        List<CloDto>? clos = [];
        CloDto? selectedClo;

        private const string WARNING_ALREADY_DETHIHOANVI = "Đề thi này đã tạo ra các đề hoán vị. Việc thao tác thêm sẽ không ảnh hưởng đến các bộ đề hoán vị, sửa hoặc xóa nội dung câu hỏi, mã nhóm gốc vẫn được thực hiện.";
        private const string NOT_SELECT_OBJECT = "Vui lòng chọn đối tượng cần thao tác";
        private const string FORBIDDEN_ADD_CAUHOI = "Bạn không thể thêm câu hỏi cho chương. Vui lòng thêm câu hỏi cho phần (chương con) của chương này.";

        private const string DELETE_MONHOC_MESSAGE = "Bạn có chắc chắn muốn xóa môn học này không? Mối quan hệ phụ thuộc: CLO &rarr; DETHI &rarr; NHOMCAUHOI &rarr; CAUHOI &rarr; CAUTRALOI &rarr; DETHIHV &rarr; " +
            "NHOMCAUHOIHV &rarr; CHITIETDETHIHV, LOPAO &rarr; CHITIETDOTTHI &rarr; CATHI &rarr; CHITIETCATHI &rarr; CHITIETBAITHI";

        private const string DELETE_DETHI_MESSAGE = "Bạn có chắc chắn muốn xóa đề thi này không? Mối quan hệ phụ thuộc: NHOMCAUHOI &rarr; CAUHOI &rarr; CAUTRALOI &rarr; DETHIHV &rarr; NHOMCAUHOIHV &rarr; CHITIETDETHIHV";
        private const string DELETE_NHOMCAUHOI_MESSAGE = "Bạn có chắc chắn muốn xóa nhóm câu hỏi này không? Mối quan hệ phụ thuộc: CHITIETDETHIHV &rarr; NHOMCAUHOIHV, CAUHOI &rarr; CAUTRALOI";
        private const string DELETE_CAUHOI_MESSAGE = "Bạn có chắc chắn muốn xóa câu hỏi này không? Mối quan hệ phụ thuộc: CHITIETDETHIHV, CAUTRALOI";
        private const string DELETE_CLO_MESSAGE = "Bạn có chắc chắn muốn xóa CLO này không? Mối quan hệ phụ thuộc: CAUHOI &rarr; CHITIETDETHIHV &rarr; CAUTRALOI";

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
            await GetItemsInSessionStorageAsync();
            await base.OnInitializedAsync();
        }

        private async Task StartAsync()
        {
            await FetchSubjectAsync();
        }

        #endregion

        #region Fetch Methods

        private async Task FetchAllDataAsync()
        {
            if (selectedSubject != null)
            {
                await FetchCloAsync();
                await FetchExamAsync();
                await FetchGroupExamAsync();
                await FetchQuestionAsync();
            }
        }

        private async Task FetchSubjectAsync()
        {
            (subjects, totalPages_Subject, totalRecords_Subject) = await Subjects_GetAll_PagedAPI(currentPage_Subject, rowsPerPage_Subject);
            CreateFakeData_MonHoc();

            selectedSubject = null;
            selectedClo = null;
            selectedExam = null;
            selectedGroupQuesion = null;
            selectedQuestion = null;
        }

        private async Task FetchExamAsync()
        {
            if (selectedSubject != null)
            {
                (exams, totalPages_Exam, totalRecords_Exam) = await Exams_SelectBy_SubjectId_PagedAPI(selectedSubject.MaMonHoc, currentPage_Exam, rowsPerPage_Exam);
                CreateFakeData_DeThi();
            }

            selectedExam = null;
            selectedGroupQuesion = null;
            selectedQuestion = null;
        }

        private async Task FetchCloAsync()
        {
            if (selectedSubject != null)
            {
                clos = await Clos_SelectBy_SubjectIdAPI(selectedSubject.MaMonHoc);
            }

            selectedClo = null;
            selectedGroupQuesion = null;
            selectedQuestion = null;
        }

        private async Task FetchGroupExamAsync()
        {
            if (selectedExam != null)
            {
                groupQuestions = await GroupQuestions_SelectBy_ExamIdAPI(selectedExam.MaDeThi);
                cauHois?.Clear();
                selectedQuestion = null;

                // convert nhomCauHois to CustomNhomCauHoi
                if (groupQuestions != null)
                    customNhomCauHois = HandleGroupQuestion(groupQuestions);
                if (selectedExam.TongSoDeHoanVi > 0)
                {
                    await OpenDialogAlreadyHasShuffleExamAsync();
                }
                return;
            }
            selectedGroupQuesion = null;
            selectedQuestion = null;
        }

        private async Task FetchQuestionAsync()
        {
            if (selectedGroupQuesion != null)
            {
                cauHois = await Questions_SelectBy_GroupQuestionIdAPI(selectedGroupQuesion.MaNhom);
                return;
            }
            selectedQuestion = null;
        }

        #endregion

        #region OnClick Methods

        private async Task OnClickViewQuestionContentAsync(string? text)
        {
            var parameters = new DialogParameters<QuestionContentDialog>
            {
                { x => x.Text, text},
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<QuestionContentDialog>("XEM CHUYỂN", parameters, options);
        }

        private async Task OnClickAddSubjectAsync()
        {
            var result = await OpenSubjectDialogAsync(false);
            if (result != null && !result.Canceled && subjects != null && result.Data != null)
            {
                var newMonHoc = (MonHocDto)result.Data;
                if (newMonHoc != null)
                {
                    subjects.Insert(0, newMonHoc);
                    selectedSubject = newMonHoc;
                }
            }
        }

        private async Task OnClickEditSubjectAsync()
        {
            var result = await OpenSubjectDialogAsync(true);
            if (result != null && !result.Canceled && subjects != null && result.Data != null)
            {
                var newMonHoc = (MonHocDto)result.Data;
                if (newMonHoc != null && selectedSubject != null)
                {
                    int index = subjects.FindIndex(m => m.MaMonHoc == newMonHoc.MaMonHoc);
                    if (index != -1)
                    {
                        subjects[index] = newMonHoc;
                        selectedSubject = newMonHoc;
                    }
                }
            }
        }

        private async Task OnClickDeleteSubjectAsync()
        {
            if (selectedSubject == null)
            {
                Snackbar.Add(NOT_SELECT_OBJECT, Severity.Warning);
                return;
            }

            var parameters = new DialogParameters<Delete_Dialog>
            {
                { x => x.ContentText, DELETE_MONHOC_MESSAGE },
                { x => x.onHandleRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteSubjectAsync(false))   },
                { x => x.onHandleForceRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteSubjectAsync(true))   }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Delete_Dialog>("XÓA KHOA", parameters, options);
        }

        private async Task OnClickAddExamAsync()
        {
            var result = await OpenExamDialogAsync(false);
            if (result != null && !result.Canceled && exams != null && result.Data != null)
            {
                var newDeThi = (DeThiDto)result.Data;
                if (exams != null)
                {
                    exams.Insert(0, newDeThi);
                    selectedExam = newDeThi;
                }
            }
        }

        private async Task OnClickEditExamAsync()
        {
            var result = await OpenExamDialogAsync(true);
            if (result != null && !result.Canceled && subjects != null && result.Data != null)
            {
                var newdeThi = (DeThiDto)result.Data;
                if (exams != null && selectedExam != null)
                {
                    int index = exams.FindIndex(m => m.MaDeThi == newdeThi.MaDeThi);
                    if (index != -1)
                    {
                        exams[index] = newdeThi;
                        selectedExam = newdeThi;
                    }
                }
            }
        }

        private async Task OnClickDeleteExamAsync()
        {
            if (selectedExam == null)
            {
                Snackbar.Add(NOT_SELECT_OBJECT, Severity.Warning);
                return;
            }

            var parameters = new DialogParameters<Delete_Dialog>
            {
                { x => x.ContentText, DELETE_DETHI_MESSAGE },
                { x => x.onHandleRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteExamAsync(false))   },
                { x => x.onHandleForceRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteExamAsync(true))   }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Delete_Dialog>("XÓA ĐỀ THI", parameters, options);
        }

        private async Task OnClickAddGroupQuestionAsync()
        {
            if (selectedExam == null)
            {
                Snackbar.Add(NOT_SELECT_OBJECT + "và để biết thêm vào vị trí nào", Severity.Warning);
                return;
            }
            NhomCauHoiDto? nhomCauHoi = groupQuestions?.FirstOrDefault(p => p.MaNhom == selectedGroupQuesion?.MaNhom);
            var result = await OpenGroupQuestionDialogAsync(false, nhomCauHoi, null, selectedExam);
            if (result != null && result.Data != null && !result.Canceled)
            {
                if (selectedExam != null)
                {
                    selectedGroupQuesion = null;
                    groupQuestions = await GroupQuestions_SelectBy_ExamIdAPI(selectedExam.MaDeThi);

                    // convert nhomCauHois to CustomNhomCauHoi
                    if (groupQuestions != null)
                        customNhomCauHois = HandleGroupQuestion(groupQuestions);
                }
            }
        }

        private async Task OnClickEditGroupQuestionAsync()
        {
            if (selectedGroupQuesion == null || selectedExam == null)
            {
                Snackbar.Add(NOT_SELECT_OBJECT, Severity.Warning);
                return;
            }
            NhomCauHoiDto? nhomCauHoi = groupQuestions?.FirstOrDefault(p => p.MaNhom == selectedGroupQuesion.MaNhom);
            var result = await OpenGroupQuestionDialogAsync(true, null, nhomCauHoi, selectedExam);
            if (result != null && result.Data != null && !result.Canceled)
            {
                if (selectedExam != null)
                {
                    selectedGroupQuesion = null;
                    groupQuestions = await GroupQuestions_SelectBy_ExamIdAPI(selectedExam.MaDeThi);

                    // convert nhomCauHois to CustomNhomCauHoi
                    if (groupQuestions != null)
                        customNhomCauHois = HandleGroupQuestion(groupQuestions);
                }
            }
        }

        private async Task OnClickDeleteGroupQuestionAsync()
        {
            if (selectedGroupQuesion == null)
            {
                Snackbar.Add(NOT_SELECT_OBJECT, Severity.Warning);
                return;
            }

            var parameters = new DialogParameters<Delete_Dialog>
            {
                { x => x.ContentText, DELETE_NHOMCAUHOI_MESSAGE },
                { x => x.onHandleRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteGroupQuestionAsync(false))   },
                { x => x.onHandleForceRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteGroupQuestionAsync(true))   }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Delete_Dialog>("XÓA NHÓM CÂU HỎI", parameters, options);
        }

        private async Task OnClickAddQuestionAsync()
        {
            if (selectedGroupQuesion == null)
            {
                Snackbar.Add(NOT_SELECT_OBJECT, Severity.Warning);
                return;
            }
            // không thể thêm câu hỏi cho chương
            if (selectedGroupQuesion.LaCauHoiNhom == false)
            {
                Snackbar.Add(FORBIDDEN_ADD_CAUHOI, Severity.Error);
                return;
            }
            NhomCauHoiDto? nhomCauHoi = groupQuestions?.FirstOrDefault(x => x.MaNhom == selectedGroupQuesion.MaNhom);
            var result = await OpenQuestionDialogAsync(false, nhomCauHoi);
            if (result != null && !result.Canceled && result.Data != null)
            {
                var newCauHoi = (CauHoiDto)result.Data;
                if (cauHois != null && selectedGroupQuesion != null)
                {
                    cauHois.Add(newCauHoi);
                    selectedQuestion = newCauHoi;
                }
            }
        }

        private async Task OnClickEditQuestionAsync()
        {
            if (selectedGroupQuesion == null || selectedQuestion == null)
            {
                Snackbar.Add(NOT_SELECT_OBJECT, Severity.Warning);
                return;
            }
            NhomCauHoiDto? nhomCauHoi = groupQuestions?.FirstOrDefault(x => x.MaNhom == selectedGroupQuesion.MaNhom);
            var result = await OpenQuestionDialogAsync(true, nhomCauHoi);
            if (result != null && !result.Canceled && result.Data != null && cauHois != null)
            {
                var newCauHoi = (CauHoiDto)result.Data;
                int index = cauHois.FindIndex(m => m.MaCauHoi == newCauHoi.MaCauHoi);
                if (index != -1)
                {
                    cauHois[index] = newCauHoi;
                }
            }
        }

        private async Task OnClickDeleteQuestionAsync()
        {
            if (selectedQuestion == null)
            {
                Snackbar.Add(NOT_SELECT_OBJECT, Severity.Warning);
                return;
            }

            var parameters = new DialogParameters<Delete_Dialog>
            {
                { x => x.ContentText, DELETE_CAUHOI_MESSAGE },
                { x => x.onHandleRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteQuestionAsync(false))   },
                { x => x.onHandleForceRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteQuestionAsync(true))   }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Delete_Dialog>("XÓA CÂU HỎI", parameters, options);
        }

        private async Task OnClickAddCloAsync()
        {
            if (selectedSubject == null)
            {
                Snackbar.Add(NOT_SELECT_OBJECT, Severity.Warning);
                return;
            }
            var result = await OpenCloDialogAsync(false);
            if (result != null && !result.Canceled && result.Data != null)
            {
                var newClo = (CloDto)result.Data;
                if (clos != null)
                {
                    clos.Insert(0, newClo);
                    selectedClo = newClo;
                }
            }
        }

        private async Task OnClickEditCloAsync()
        {
            if (selectedSubject == null || selectedClo == null)
            {
                Snackbar.Add(NOT_SELECT_OBJECT, Severity.Warning);
                return;
            }
            var result = await OpenCloDialogAsync(true);
            if (result != null && !result.Canceled && result.Data != null)
            {
                var newClo = (CloDto)result.Data;
                if (clos != null && selectedExam != null)
                {
                    int index = clos.FindIndex(m => m.MaClo == newClo.MaClo);
                    if (index != -1)
                    {
                        clos[index] = newClo;
                        selectedClo = newClo;
                    }
                }
            }
        }

        private async Task OnClickDeleteCloAsync()
        {
            if (selectedClo == null)
            {
                Snackbar.Add(NOT_SELECT_OBJECT, Severity.Warning);
                return;
            }

            var parameters = new DialogParameters<Delete_Dialog>
            {
                { x => x.ContentText, DELETE_CLO_MESSAGE },
                { x => x.onHandleRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteCloAsync(false))   },
                { x => x.onHandleForceRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteCloAsync(true))   }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Delete_Dialog>("XÓA CLO", parameters, options);
        }

        private async Task OnClickCreateShuffleExamAsync()
        {
            if (selectedExam == null)
            {
                Snackbar.Add(NOT_SELECT_OBJECT, Severity.Warning);
                return;
            }

            await OpenCreateShuffleExamDialogAsync();
        }

        #endregion

        #region HandleOnClick Methods

        private List<CustomNhomCauHoi> HandleGroupQuestion(List<NhomCauHoiDto> nhomCauHoiGoc)
        {
            List<CustomNhomCauHoi> result = new();
            // handle các câu hỏi cha trước
            foreach (var item in nhomCauHoiGoc)
            {
                var nhomCauHoi = Mapper.Map<CustomNhomCauHoi>(item);
                if (item.MaNhomCha == -1)
                    result.Add(nhomCauHoi);
            }
            // handle các câu hỏi nhóm con
            foreach (var item in nhomCauHoiGoc)
            {
                if (item.MaNhomCha != -1)
                {
                    var parent = result.FirstOrDefault(x => x.MaNhom == item.MaNhomCha);
                    if (parent != null)
                    {
                        var nhomCauHoi = Mapper.Map<CustomNhomCauHoi>(item);
                        parent.NhomCauHoiCons.Add(nhomCauHoi);
                    }
                }
            }
            return result;
        }



        private async Task HandleDeleteSubjectAsync(bool isForce)
        {
            if (selectedSubject != null)
            {
                var result = (isForce) ? await Subject_ForceDeleteAPI(selectedSubject.MaMonHoc) : await Subject_DeleteAPI(selectedSubject.MaMonHoc);

                if (result)
                {
                    subjects?.Remove(selectedSubject);
                    selectedSubject = null;
                    exams?.Clear();
                    customNhomCauHois?.Clear();
                    cauHois?.Clear();
                }
            }
        }



        private async Task HandleDeleteExamAsync(bool isForce)
        {
            if (selectedExam != null)
            {
                var result = (isForce) ? await Exam_ForceDeleteAPI(selectedExam.MaDeThi) : await Exam_DeleteAPI(selectedExam.MaDeThi);

                if (result)
                {
                    exams?.Remove(selectedExam);
                    selectedExam = null;

                    customNhomCauHois?.Clear();
                    cauHois?.Clear();
                }
            }
        }



        private async Task HandleDeleteGroupQuestionAsync(bool isForce)
        {
            if (selectedGroupQuesion != null)
            {
                var result = (isForce) ? await GroupQuestion_ForceDeleteAPI(selectedGroupQuesion.MaNhom) : await GroupQuestion_DeleteAPI(selectedGroupQuesion.MaNhom);

                if (result)
                {
                    await FetchGroupExamAsync();

                    cauHois?.Clear();
                }
            }
        }



        private async Task HandleDeleteQuestionAsync(bool isForce)
        {
            if (selectedQuestion != null)
            {
                var result = (isForce) ? await Question_ForceDeleteAPI(selectedQuestion.MaCauHoi) : await Question_DeleteAPI(selectedQuestion.MaCauHoi);

                if (result)
                {
                    cauHois?.Remove(selectedQuestion);
                }
            }
        }



        private async Task HandleDeleteCloAsync(bool isForce)
        {
            if (selectedClo != null)
            {
                var result = (isForce) ? await Clo_DeleteAPI(selectedClo.MaClo) : await Clo_ForceDeleteAPI(selectedClo.MaClo);

                if (result)
                {
                    clos?.Remove(selectedClo);
                }
            }
        }

        #endregion

        #region SessionStorage
        private async Task GetItemsInSessionStorageAsync()
        {
            var storedData = await SessionStorage.GetItemAsync<StoredDataEQ>("storedDataEQ");

            if (storedData != null)
            {
                selectedSubject = storedData.Subject;
                selectedExam = storedData.Exam;
            }
            await FetchAllDataAsync();
        }

        private async Task SaveData()
        {
            var selectedData = new StoredDataEQ
            {
                Subject = selectedSubject,
                Exam = selectedExam,
                GroupQuestion = groupQuestions?.FirstOrDefault(x => x.MaNhom == selectedGroupQuesion?.MaNhom),
                Clo = selectedClo
            };
            await SessionStorage.SetItemAsync("storedDataEQ", selectedData);
        }

        #endregion

        #region Dialog Methods

        private async Task<DialogResult?> OpenCloDialogAsync(bool isEdit)
        {
            var parameters = new DialogParameters<CloDialog>
            {
                { x => x.IsEdit, isEdit},
                { x => x.Subject, selectedSubject },
                { x => x.Clo, selectedClo }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<CloDialog>((isEdit) ? "SỬA MÔN HỌC" : "THÊM MÔN HỌC", parameters, options);
            return await dialog.Result;
        }


        private async Task<DialogResult?> OpenSubjectDialogAsync(bool isEdit)
        {
            var parameters = new DialogParameters<SubjectDialog>
            {
                { x => x.IsEdit, isEdit},
                { x => x.Subject, selectedSubject },
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<SubjectDialog>((isEdit) ? "SỬA MÔN HỌC" : "THÊM MÔN HỌC", parameters, options);
            return await dialog.Result;
        }

        private async Task<DialogResult?> OpenExamDialogAsync(bool isEdit)
        {
            var parameters = new DialogParameters<ExamDialog>
            {
                { x => x.IsEdit, isEdit},
                { x => x.Exam, selectedExam }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<ExamDialog>((isEdit) ? "SỬA ĐỀ THI" : "THÊM ĐỀ THI", parameters, options);
            return await dialog.Result;
        }

        private async Task OpenDialogAlreadyHasShuffleExamAsync()
        {
            var parameters = new DialogParameters<Simple_Dialog>
            {
                { x => x.ContentText, WARNING_ALREADY_DETHIHOANVI },
                { x => x.ButtonText, "OK" },
                { x => x.Color, Color.Error },
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };

            await Dialog.ShowAsync<Simple_Dialog>("Thông báo", parameters, options);
        }

        private async Task<DialogResult?> OpenGroupQuestionDialogAsync(bool isEdit, NhomCauHoiDto? nhomCauHoiCha, NhomCauHoiDto? nhomCauHoi, DeThiDto? deThi)
        {
            var thuTu = (groupQuestions?.Count ?? 0) + 1;
            var parameters = new DialogParameters<GroupQuestionDialog>
            {
                { x => x.GroupQuestion, nhomCauHoi },
                { x => x.ParentGropQuestion, nhomCauHoiCha },
                { x => x.IsEdit, isEdit },
                { x => x.Exam, deThi},
                { x => x.IsIgnoreChaper, selectedExam?.BoChuongPhan},
                { x => x.Order, thuTu + 1}
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<GroupQuestionDialog>((isEdit) ? "SỬA CHƯƠNG/NHÓM CÂU HỎI" : "THÊM CHƯƠNG/NHÓM CÂU HỎI", parameters, options);
            return await dialog.Result;
        }

        private async Task<DialogResult?> OpenQuestionDialogAsync(bool isEdit, NhomCauHoiDto? nhomCauHoi)
        {
            var parameters = new DialogParameters<QuestionDialog>
            {
                { x => x.Question, selectedQuestion },
                { x => x.Clos, clos},
                { x => x.GroupExam, nhomCauHoi },
                { x => x.IsEdit, isEdit },
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<QuestionDialog>((isEdit) ? "SỬA CÂU HỎI" : "THÊM CÂU HỎI", parameters, options);
            return await dialog.Result;
        }

        private async Task<DialogResult?> OpenCreateShuffleExamDialogAsync()
        {
            var parameters = new DialogParameters<MatrixExamDialog>
            {
                { x => x.Exam, selectedExam }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<MatrixExamDialog>("TẠO MA TRẬN ĐỀ THI", parameters, options);
            return await dialog.Result;
        }

        #endregion

        #region Other Methods

        private void PadEmptyRows(List<MonHocDto>? newMonHocs)
        {
            if (newMonHocs == null || newMonHocs.Count == 0)
                return;
            // tìm phần tử đầu tiên của trang đó
            int startRow = currentPage_Subject * rowsPerPage_Subject;
            if (subjects != null && subjects.Count != 0)
            {
                for (int i = 0; i < newMonHocs.Count; i++)
                {

                    subjects[startRow++] = newMonHocs[i];
                }
            }
            StateHasChanged();
        }

        private void PadEmptyRows(List<DeThiDto>? newDeThis)
        {
            if (newDeThis == null || newDeThis.Count == 0)
                return;
            // tìm phần tử đầu tiên của trang đó
            int startRow = currentPage_Exam * rowsPerPage_Exam;
            if (exams != null && exams.Count != 0)
            {
                for (int i = 0; i < newDeThis.Count; i++)
                {

                    exams[startRow++] = newDeThis[i];
                }
            }
            StateHasChanged();
        }

        private void CreateFakeData_MonHoc()
        {
            if (subjects != null && subjects.Count != 0)
            {
                int count_fake = totalRecords_Subject - subjects.Count;
                bool isFake = totalRecords_Subject > subjects.Count;
                if (isFake)
                {
                    for (int i = 0; i < count_fake; i++)
                        subjects.Add(new MonHocDto());
                }
            }
        }

        private void CreateFakeData_DeThi()
        {
            if (exams != null && exams.Count != 0)
            {
                int count_fake = totalRecords_Exam - exams.Count;
                bool isFake = totalRecords_Exam > exams.Count;
                if (isFake)
                {
                    for (int i = 0; i < count_fake; i++)
                        exams.Add(new DeThiDto());
                }
            }
        }

        #endregion

    }
}
