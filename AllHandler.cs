using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherTestTask
{
    class AllHandler : IFileHandleStrategy
    {
        public String fileHandle(String fullName)
        {
            // do nothing
            return fullName;
        }
    }
}
