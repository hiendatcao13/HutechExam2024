namespace Hutech.Exam.Server.Configurations
{
    public class JwtConfiguration
    {
        public string SecurityKey { get; set; } = string.Empty;

        public int TokenValidityMinutes_Student { get; set; }

        public int TokenValidityMinutes_Admin { get; set; }
    }
}
