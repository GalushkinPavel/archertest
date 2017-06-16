using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherTestTask
{
    class DirCrawler
    {
        private readonly ConcurrentQueue<string> folderQueue = new ConcurrentQueue<string>();
        private readonly ConcurrentBag<Task> tasks = new ConcurrentBag<Task>();

        private Context _context;

        // property injection (in case the context should be replaced)
        public Context context
        {
            set
            {
                if (value == null) throw new ArgumentNullException(Utils.MSG_NULL_CONTEXT);
                this._context = value;
            }
            private get { return _context; }
        }

        // constructor injection
        public DirCrawler(Context context) {
            if (context == null) throw new ArgumentNullException(Utils.MSG_NULL_CONTEXT);
            this._context = context;
        }

        public void CollectFolders(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            tasks.Add(Task.Run(() => CrawlFolder(directoryInfo)));

            Task taskToWaitFor;
            while (tasks.TryTake(out taskToWaitFor))
                taskToWaitFor.Wait();
        }

        public ConcurrentQueue<string> getQueue() { return folderQueue; }

        private void CrawlFolder(DirectoryInfo dir)
        {
            try
            {
                DirectoryInfo[] directoryInfos = dir.GetDirectories();
                foreach (DirectoryInfo childInfo in directoryInfos)
                {
                    DirectoryInfo di = childInfo;
                    tasks.Add(Task.Run(() => CrawlFolder(di)));
                }
                // Do something with the current folder
                foreach (FileInfo fi in dir.GetFiles()) 
                {
                    string s = context.ExecuteOperation(fi.FullName);
                    if (!String.IsNullOrEmpty(s)) folderQueue.Enqueue(s);
                }
            }
            catch (Exception ex)
            {
                while (ex != null)
                {
                    Console.WriteLine(ex.Message);
                    ex = ex.InnerException;
                }
            }
        }
    }
}
