using HashidsNet;

namespace Hutech.Exam.Shared.Helper
{
    public class HashIdHelper : IHashIdHelper
    {
        private readonly Hashids _hashids;

        public HashIdHelper()
        {
            // Salt độ dài 50
            _hashids = new Hashids(">cK,MkA_Sv1E/\"LH8bh£>;3u9T9m)Ch+?cfOP9=7vgCOc*w:^5", 12);  // 12 là độ dài tối thiểu của mã hóa
        }
        public string EncodeId(int id)
        {
            return _hashids.Encode(id);
        }
        public string EncodeLongId(long id)
        {
            return _hashids.EncodeLong(id);
        }

        public int DecodeId(string hash)
        {
            var numbers = _hashids.Decode(hash);
            return numbers.Length > 0 ? numbers[0] : throw new Exception("Invalid Hash ID");
        }

        public long DecodeLongId(string hash)
        {
            var numbers = _hashids.DecodeLong(hash);
            return numbers.Length > 0 ? numbers[0] : throw new Exception("Invalid Hash ID");
        }

    }
}
