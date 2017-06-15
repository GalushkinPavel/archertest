using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherTestTask
{
    class Utils
    {
        #region constants
        public static string DEFAULT_PATH = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) + Path.DirectorySeparatorChar + "results.txt";
        public const string MSG_NOT_ENOUGH_PARAM = "Not enough actual parameters. Press any key to exit";
        public const string MSG_EXIT = "Press any key to exit";
        public const string MSG_NULL_CONTEXT = "Context must not be null";
        public const string MSG_NULL_STRATEGY = "Strategy must not be null";
        public const string MSG_WRONG_PATH_TO_START_FOLDER = "Invalid path to start folder";
        public const string MSG_WRONG_PATH_TO_RES_FILE = "Invalid path to result file";
        public const string MSG_INVALID_COMM = "Invalid command";
        #endregion

        #region params
        public static string startFolder;
        public static string actionName;
        public static string path2file;
        #endregion

        private static bool FilePathHasInvalidChars(string path)
        {
            return (!string.IsNullOrEmpty(path) && path.IndexOfAny(Path.GetInvalidPathChars()) == -1);
        }

        public static void checkParam(string[] args)
        {
            if (args == null || args.Length < 2) throw new ArgumentException(Utils.MSG_NOT_ENOUGH_PARAM);

            startFolder = args[0];
            if (!FilePathHasInvalidChars(startFolder)) throw new ArgumentException(Utils.MSG_WRONG_PATH_TO_START_FOLDER);

            // check for invalid command
            actionName = args[1].ToUpper();
            if (Array.IndexOf(Enum.GetNames(typeof(Commands)), actionName) == -1) throw new ArgumentException(Utils.MSG_INVALID_COMM);

            path2file = (args.Length == 3) ? args[2] : Utils.DEFAULT_PATH;
            if (!FilePathHasInvalidChars(path2file)) throw new ArgumentException(Utils.MSG_WRONG_PATH_TO_RES_FILE);
        }
    }
}
