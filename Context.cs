using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
            if (!String.IsNullOrEmpty(s.Trim()))
                return Strategy.fileHandle(GetRelativePath(s, Utils.startFolder));
            else return s;
        }

        // public access is for tests only!!!
        public string GetRelativePath(string filespec, string folder)
        {
            Uri pathUri = new Uri(filespec);
            // Folders must end in a slash
            if (!folder.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                folder += Path.DirectorySeparatorChar;
            }
            Uri folderUri = new Uri(folder);
            return Uri.UnescapeDataString(folderUri.MakeRelativeUri(pathUri).ToString().Replace('/', Path.DirectorySeparatorChar));
        }
    }
}
