using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherTestTask
{
    class CppHandler : IFileHandleStrategy
    {
        public String fileHandle(String fullName) {
            return fullName.EndsWith(".cpp") ? fullName + " /" : String.Empty;
        }
    }
}
