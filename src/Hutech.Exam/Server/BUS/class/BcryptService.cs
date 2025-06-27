namespace Hutech.Exam.Server.BUS
{
    public class BcryptService
    {
        // Tạo mật khẩu băm với số vòng (work factor)
        public string HashPassword(string password, int workFactor = 10)
        {
            // Work factor mặc định thường là 10, riêng tài khoản là 12
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor);
        }

        // Kiểm tra mật khẩu thường có giống mật khẩu băm không
        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
