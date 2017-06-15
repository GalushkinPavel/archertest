using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArcherTestTask
{
    class ReverseOneHandler : IFileHandleStrategy
    {
        public String fileHandle(String fullName)
        {
            char sep = Path.DirectorySeparatorChar;
            string[] strArr = fullName.Split(new char[] { sep });
            return String.Join(sep.ToString(), strArr.Reverse());
        }
    }
}
