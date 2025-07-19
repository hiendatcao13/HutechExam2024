using System.Net.Http.Headers;
using Hutech.Exam.Client.API;
using Hutech.Exam.Client.Authentication;
using Hutech.Exam.Client.Components.Dialogs;
using Hutech.Exam.Client.DAL;
using Hutech.Exam.Client.Pages.Admin.ManageClassroom.Dialog;
using Hutech.Exam.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace Hutech.Exam.Client.Pages.Admin.ManageClassroom
{
    public partial class ManageClassroom
    {
        #region Private Fields
        [Inject] private HttpClient Http { get; set; } = default!;

        [Inject] private NavigationManager Nav { get; set; } = default!;

        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [Inject] private Blazored.SessionStorage.ISessionStorageService SessionStorage { get; set; } = default!;

        [CascadingParameter] private Task<AuthenticationState>? AuthenticationState { get; set; }

        [Inject] private AdminHubService AdminHub { get; set; } = default!;

        [Inject] private ISenderAPI SenderAPI { get; set; } = default!;

        private List<KhoaDto>? departments = [];

        private List<LopDto>? classrooms = [];

        private List<SinhVienDto>? students = [];

        private const string NO_SELECT_KHOA = "Vui lòng chọn khoa trước";

        private const string NO_SELECT_LOP = "Vui lòng chọn lớp trước";

        private const string NO_SELECT_SINHVIEN = "Vui lòng chọn sinh viên trước";

        private const string DELETE_KHOA_MESSAGE = "Bạn có chắc chắn muốn xóa khoa này. Các lớp và toàn bộ sinh viên thuộc khoa này sẽ bị xóa?";
        private const string DELETE_LOP_MESSAGE = "Bạn có chắc chắn muốn xóa lớp này. Tất cả sinh viên thuộc lớp này sẽ bị xóa?";
        private const string DELETE_SINHVIEN_MESSAGE = "Bạn có chắc chắn muốn xóa sinh viên này?";

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
            await FetchDepartmentAsync();
        }

        #endregion

        #region SessionStorage

        private async Task GetItemsInSessionStorageAsync()
        {
            var storedData = await SessionStorage.GetItemAsync<StoredDataMC>("storedDataML");

            if (storedData != null)
            {
                selectedDepartment = storedData.Khoa;
                selectedClassroom = storedData.Lop;
            }
            if (selectedDepartment != null)
            {
                await FetchClassroomAsync();
                await FetchStudentAsync();
            }
        }

        private async Task SaveDataAsync()
        {
            var selectedData = new StoredDataMC
            {
                Khoa = selectedDepartment,
                Lop = selectedClassroom
            };
            await SessionStorage.SetItemAsync("storedDataML", selectedData);
        }

        #endregion

        #region Fetch Methods

        private async Task FetchDepartmentAsync()
        {
            // lấy danh sách khoa
            (departments, totalPages_Department, totalRecords_Department) = await Departments_GetAll_PagedAPI(currentPage_Department, rowsPerPage_Department);
            CreateFakeData_Khoa();
        }


        private async Task FetchClassroomAsync()
        {
            if (selectedDepartment != null)
            {
                (classrooms, totalPages_Classroom, totalRecords_Classroom) = await Classroom_SelectBy_DepartmentId_PagedAPI(selectedDepartment.MaKhoa, currentPage_Classroom, rowsPerPage_Classroom);
                CreateFakeData_Lop();
            }
        }

        private async Task FetchStudentAsync()
        {
            if (selectedClassroom != null)
            {
                (students, totalPages_Student, totalRecords_Student) = await Students_SelectBy_ClassroomId_PagedAPI(selectedClassroom.MaLop, currentPage_Student, rowsPerPage_Student);
                CreateFakeData_SinhVien();
            }
        }

        #endregion

        #region OnClick Methods

        private async Task OnClickAddDepartment()
        {
            var result = await OpenDepartmentDialogAsync(false, null);

            if (result != null && !result.Canceled && result.Data is KhoaDto newKhoa)
            {
                departments?.Insert(0, newKhoa);
            }
        }

        private async Task OnClickEditDepartmentAsync()
        {
            if (selectedDepartment == null)
            {
                Snackbar.Add(NO_SELECT_KHOA, Severity.Warning);
                return;
            }
            var result = await OpenDepartmentDialogAsync(true, selectedDepartment);

            if (result != null && !result.Canceled && result.Data is KhoaDto newKhoa && departments != null)
            {
                int index = departments.FindIndex(k => k.MaKhoa == newKhoa.MaKhoa);
                if (index != -1)
                {
                    departments[index] = newKhoa;
                }
            }
        }

        private async Task OnClickDeleteDepartmentAsync()
        {
            if (selectedDepartment == null)
            {
                Snackbar.Add(NO_SELECT_KHOA, Severity.Warning);
                return;
            }

            var parameters = new DialogParameters<Delete_Dialog>
            {
                { x => x.ContentText, DELETE_KHOA_MESSAGE },
                { x => x.onHandleRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteDepartmentAsync(false))   },
                { x => x.onHandleForceRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteDepartmentAsync(true))   }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Delete_Dialog>("XÓA KHOA", parameters, options);

        }

        private async Task OnClickAddClassroomAsync()
        {
            if (selectedDepartment == null)
            {
                Snackbar.Add(NO_SELECT_KHOA, Severity.Warning);
                return;
            }
            var result = await OpenClassroomDialogAsync(false, selectedDepartment, null);

            if (result != null && !result.Canceled && result.Data is LopDto newLop && classrooms != null)
            {
                classrooms?.Insert(0, newLop);
            }
        }

        private async Task OnClickEditClassroomAsync()
        {
            if (selectedDepartment == null)
            {
                Snackbar.Add(NO_SELECT_KHOA, Severity.Warning);
                return;
            }
            if (selectedClassroom == null)
            {
                Snackbar.Add(NO_SELECT_LOP, Severity.Warning);
                return;
            }

            var result = await OpenClassroomDialogAsync(true, selectedDepartment, selectedClassroom);

            if (result != null && !result.Canceled && result.Data is LopDto newLop && classrooms != null)
            {
                int index = classrooms.FindIndex(l => l.MaLop == newLop.MaLop);
                if (index != -1)
                {
                    classrooms[index] = newLop;
                }
            }
        }

        private async Task OnClickDeleteClassroomAsync()
        {
            if (selectedClassroom == null)
            {
                Snackbar.Add(NO_SELECT_LOP, Severity.Warning);
                return;
            }

            if (selectedDepartment == null)
            {
                Snackbar.Add(NO_SELECT_KHOA, Severity.Warning);
                return;
            }

            var parameters = new DialogParameters<Delete_Dialog>
            {
                { x => x.ContentText, DELETE_LOP_MESSAGE },
                { x => x.onHandleRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteClassroomAsync(false))   },
                { x => x.onHandleForceRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteClassroomAsync(true))   }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Delete_Dialog>("XÓA LỚP", parameters, options);

        }

        private async Task OnClickAddStudentAsync()
        {
            if (selectedClassroom == null)
            {
                Snackbar.Add(NO_SELECT_LOP, Severity.Warning);
                return;
            }

            var result = await OpenStudentDialogAsync(false, selectedClassroom, null);

            if (result != null && !result.Canceled && result.Data is SinhVienDto newSinhVien && students != null)
            {
                students.Insert(0, newSinhVien);
            }
        }

        private async Task OnClickEditStudentAsync()
        {
            if (selectedClassroom == null)
            {
                Snackbar.Add(NO_SELECT_LOP, Severity.Warning);
                return;
            }
            if (selectedStudent == null)
            {
                Snackbar.Add(NO_SELECT_SINHVIEN, Severity.Warning);
                return;
            }

            var result = await OpenStudentDialogAsync(true, selectedClassroom, selectedStudent);

            if (result != null && !result.Canceled && result.Data is SinhVienDto newSinhVien && students != null)
            {
                int index = students.FindIndex(sv => sv.MaSinhVien == newSinhVien.MaSinhVien);
                if (index != -1)
                {
                    students[index] = newSinhVien;
                }
            }
        }

        private async Task OnClickDeleteStudentAsync()
        {
            if (selectedStudent == null)
            {
                Snackbar.Add(NO_SELECT_SINHVIEN, Severity.Warning);
                return;
            }

            var parameters = new DialogParameters<Delete_Dialog>
            {
                { x => x.ContentText, DELETE_SINHVIEN_MESSAGE },
                { x => x.onHandleRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteStudentAsync(false))   },
                { x => x.onHandleForceRemove, EventCallback.Factory.Create(this, async () => await HandleDeleteStudentAsync(true))   }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, BackgroundClass = "my-custom-class" };
            await Dialog.ShowAsync<Delete_Dialog>("XÓA SINH VIÊN", parameters, options);
        }

        private async Task OnClickAddStudentExcelAsync()
        {
            if (selectedClassroom == null)
            {
                Snackbar.Add(NO_SELECT_LOP, Severity.Warning);
                return;
            }

            var parameters = new DialogParameters<AddStudentExcelDialog>
            {
                { x => x.ClassRoom, selectedClassroom },
                { x => x.ExistStudents, students }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<AddStudentExcelDialog>("NHẬP SINH VIÊN TỪ FILE EXCEL", parameters, options);
            var result = await dialog.Result;

            if (result != null && !result.Canceled && result.Data != null)
            {
                if ((bool)result.Data)
                {
                    await FetchStudentAsync();
                }
            }
        }

        #endregion

        #region HandleOnClick Methods

        private async Task HandleDeleteDepartmentAsync(bool isForce)
        {
            if (selectedDepartment != null)
            {
                var result = (isForce) ? await Department_ForceDeleteAPI(selectedDepartment.MaKhoa) : await Department_DeleteAPI(selectedDepartment.MaKhoa);

                if (result)
                {
                    departments?.Remove(selectedDepartment);
                    selectedDepartment = null;
                }
            }
        }

        private async Task HandleDeleteClassroomAsync(bool isForce)
        {
            if (selectedClassroom != null)
            {
                var result = (isForce) ? await Classroom_ForceDeleteAPI(selectedClassroom.MaLop) : await Classroom_DeleteAPI(selectedClassroom.MaLop);

                if (result)
                {
                    classrooms?.Remove(selectedClassroom);
                    selectedClassroom = null;
                }
            }
        }

        private async Task HandleDeleteStudentAsync(bool isForce)
        {
            if (selectedStudent != null)
            {
                var result = (isForce) ? await Student_ForceDeleteAPI(selectedStudent.MaSinhVien) : await Student_DeleteAPI(selectedStudent.MaSinhVien);
                if (result)
                {
                    students?.Remove(selectedStudent);
                    selectedStudent = null;
                }
            }
        }

        #endregion

        #region Dialog Methods

        private async Task<DialogResult?> OpenDepartmentDialogAsync(bool isEdit, KhoaDto? khoa)
        {
            var parameters = new DialogParameters<DepartmentDialog>
            {
                { x => x.Department, khoa },
                { x => x.IsEdit, isEdit }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<DepartmentDialog>((isEdit) ? "SỬA KHOA" : "THÊM KHOA", parameters, options);
            return await dialog.Result;
        }

        private async Task<DialogResult?> OpenClassroomDialogAsync(bool isEdit, KhoaDto? khoa, LopDto? lop)
        {
            var parameters = new DialogParameters<ClassroomDialog>
            {
                { x => x.Department, khoa },
                { x => x.Classroom, lop },
                { x => x.IsEdit, isEdit }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<ClassroomDialog>((isEdit) ? "SỬA LỚP" : "THÊM LỚP", parameters, options);
            return await dialog.Result;
        }

        private async Task<DialogResult?> OpenStudentDialogAsync(bool isEdit, LopDto? lop, SinhVienDto? sinhVien)
        {
            var parameters = new DialogParameters<StudentDialog>
            {
                { x => x.Student, sinhVien },
                { x => x.Classroom, lop },
                { x => x.IsEdit, isEdit }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, BackgroundClass = "my-custom-class" };
            var dialog = await Dialog.ShowAsync<StudentDialog>((isEdit) ? "SỬA SINH VIÊN" : "THÊM SINH VIÊN", parameters, options);
            return await dialog.Result;
        }

        #endregion

        #region Other Methods

        private void PadEmptyRows(List<KhoaDto>? newKhoas)
        {
            if (newKhoas == null || newKhoas.Count == 0)
                return;
            // tìm phần tử đầu tiên của trang đó
            int startRow = currentPage_Department * rowsPerPage_Department;
            if (departments != null && departments.Count != 0)
            {
                for (int i = 0; i < newKhoas.Count; i++)
                {

                    departments[startRow++] = newKhoas[i];
                }
            }
            StateHasChanged();
        }

        private void PadEmptyRows(List<LopDto>? newLops)
        {
            if (newLops == null || newLops.Count == 0)
                return;
            // tìm phần tử đầu tiên của trang đó
            int startRow = currentPage_Classroom * rowsPerPage_Classroom;
            if (classrooms != null && classrooms.Count != 0)
            {
                for (int i = 0; i < newLops.Count; i++)
                {

                    classrooms[startRow++] = newLops[i];
                }
            }
            StateHasChanged();
        }

        private void PadEmptyRows(List<SinhVienDto>? newSinhViens)
        {
            if (newSinhViens == null || newSinhViens.Count == 0)
                return;
            // tìm phần tử đầu tiên của trang đó
            int startRow = currentPage_Student * rowsPerPage_Student;
            if (students != null && students.Count != 0)
            {
                for (int i = 0; i < newSinhViens.Count; i++)
                {

                    students[startRow++] = newSinhViens[i];
                }
            }
            StateHasChanged();
        }

        private void CreateFakeData_Khoa()
        {
            if (departments != null && departments.Count != 0)
            {
                int count_fake = totalRecords_Department - departments.Count;
                bool isFake = totalRecords_Department > departments.Count;
                if (isFake)
                {
                    for (int i = 0; i < count_fake; i++)
                        departments.Add(new KhoaDto());
                }
            }
        }

        private void CreateFakeData_Lop()
        {
            if (classrooms != null && classrooms.Count != 0)
            {
                int count_fake = totalRecords_Classroom - classrooms.Count;
                bool isFake = totalRecords_Classroom > classrooms.Count;
                if (isFake)
                {
                    for (int i = 0; i < count_fake; i++)
                        classrooms.Add(new LopDto());
                }
            }
        }

        private void CreateFakeData_SinhVien()
        {
            if (students != null && students.Count != 0)
            {
                int count_fake = totalRecords_Student - students.Count;
                bool isFake = totalRecords_Student > students.Count;
                if (isFake)
                {
                    for (int i = 0; i < count_fake; i++)
                        students.Add(new SinhVienDto());
                }
            }
        }

        #endregion

    }
}
