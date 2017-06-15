using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherTestTask
{
    class ReverseTwoHandler : IFileHandleStrategy
    {
        public String fileHandle(String fullName)
        {
            return new string(fullName.ToCharArray().Reverse().ToArray());
        }
    }
}
