using System;
using System.IO;
using System.Collections.Generic;

namespace CommonTypes
{
    public class CommandLineParser
    {
        //private string[] commArgs;
        private List<string> commArgs;
        private bool isCorrect;
        private string inputDir;
        private string outputDir;
        private bool isInfologEnabled = false;

        public CommandLineParser(string[] args)
        {
            commArgs = new List<string>();
            foreach(var arg in args)
            {
                if(arg[0] == '-')
                    setFlags(arg);
                else
                    commArgs.Add(arg);
            }
            inputDir = "";
            outputDir = "";

            switch (commArgs.Count)
            {
                case 0:
                    isCorrect = false;
                    Console.WriteLine("Параметры не были переданы");
                    //commArgs = Environment.GetCommandLineArgs();
                    //if (commArgs.Length != 0)
                    //{
                    //    int position = commArgs[0].LastIndexOf(@"\");
                    //    if (position == -1)
                    //    {
                    //        commArgs[0] = Directory.GetCurrentDirectory();
                    //        position = commArgs[0].LastIndexOf(@"\");
                    //    }

                    //    for (int i = 0; i <= position; i++)
                    //    {
                    //        inputDir += commArgs[0][i];
                    //        outputDir += commArgs[0][i];
                    //    }
                    //    isCorrect = true;
                    //}
                    //else
                    //{
                    //    Console.WriteLine("Не удалось получить параметры");
                    //    isCorrect = false;
                    //}
                    break;
                case 1:
                    if (Directory.Exists(commArgs[0]))
                    {
                        inputDir = commArgs[0];
                        outputDir = commArgs[0];
                        isCorrect = true;
                    }
                    else
                    {
                        isCorrect = false;
                        Console.WriteLine("Переданы неверные параметры");
                    }
                    break;
                case 2:
                    if (Directory.Exists(commArgs[0]) && Directory.Exists(commArgs[1]))
                    {
                        inputDir = commArgs[0];
                        outputDir = commArgs[1];
                        isCorrect = true;
                    }
                    else
                    {
                        isCorrect = false;
                        Console.WriteLine("Переданы неверные параметры");
                    }
                    break;
            }
        }

        public string GetInputDir()
        {
            return inputDir;
        }

        public string GetOutputDir()
        {
            return outputDir;
        }

        public bool IsCorrect()
        {
            return isCorrect;
        }

        public bool isInfolog()
        {
            return isInfologEnabled;
        }

        private bool setFlags(string arg)
        {
            switch(arg)
            {
                case "-infolog":
                    isInfologEnabled = true;
                    return true;
            }

            return false;
        }

    }
}
