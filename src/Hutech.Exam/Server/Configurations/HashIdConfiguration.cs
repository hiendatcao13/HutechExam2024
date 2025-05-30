namespace Hutech.Exam.Server.Configurations
{
    public class HashIdConfiguration
    {
        public string Salt { get; set; } = string.Empty;

        public int MinHashLength { get; set; }
    }
}
