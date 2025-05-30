using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hutech.Exam.Server.Helper
{
    public interface IHashIdHelper
    {
        public string EncodeId(int id);

        public string EncodeLongId(long id);

        public int DecodeId(string hash);

        public long DecodeLongId(string hash);
    }
}
