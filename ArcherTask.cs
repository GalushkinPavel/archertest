using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherTestTask
{
    class ArcherTask
    {
        static void Main(string[] args)
        {
            Utils.checkParam(args);

            Context context = new Context();
            if (Utils.actionName.Equals(Commands.ALL.ToString()))
                context.Strategy = new AllHandler();
            else if (Utils.actionName.Equals(Commands.CPP.ToString()))
                context.Strategy = new CppHandler();
            else if (Utils.actionName.Equals(Commands.REVERSED1.ToString()))
                context.Strategy = new ReverseOneHandler();
            else
                context.Strategy = new ReverseTwoHandler();

            // Dependency injection
            DirCrawler fs = new DirCrawler(context);
            fs.CollectFolders(Utils.startFolder);

            try
            {
                //file with results must be in any case
                File.WriteAllLines(Utils.path2file, fs.getQueue(), Encoding.UTF8);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally { Console.WriteLine(Utils.MSG_EXIT); Console.ReadKey(); }
        }
    }
}
