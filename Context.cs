using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherTestTask
{
    class Context
    {
        private IFileHandleStrategy _strategy;

        public IFileHandleStrategy Strategy
        {
            private get { return _strategy; }
            set
            {
                if (value == null) throw new ArgumentNullException(Utils.MSG_NULL_STRATEGY);
                this._strategy = value;
            }
        }

        public String ExecuteOperation(String s)
        {
            return Strategy.fileHandle(s.Substring(Utils.startFolder.Length + 1));
        }
    }
}
