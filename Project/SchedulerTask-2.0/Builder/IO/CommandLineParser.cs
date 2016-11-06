using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Builder.IO
{
    public class CommandLineParser
    {
        private string[] comm_args;
        private bool is_correct;
        private string input_dir;
        private string output_dir;

        public CommandLineParser(string[] args)
        {
            comm_args = args;
            input_dir = "";
            output_dir = "";

            switch (comm_args.Length)
            {
                case 0:
                    comm_args = Environment.GetCommandLineArgs();
                    if (comm_args.Length != 0)
                    {
                        int position = comm_args[0].LastIndexOf("\\");
                        if (position == -1)
                        {
                            comm_args[0] = Directory.GetCurrentDirectory();
                            position = comm_args[0].LastIndexOf("\\");
                        }

                        for (int i = 0; i <= position; i++)
                        {
                            input_dir += comm_args[0][i];
                            output_dir += comm_args[0][i];
                        }
                        is_correct = true;
                    }
                    else
                    {
                        Console.WriteLine("Не удалось получить параметры");
                        is_correct = false;
                    }
                    break;
                case 1:
                    if (Directory.Exists(comm_args[0]))
                    {
                        input_dir = comm_args[0];
                        output_dir = Directory.GetCurrentDirectory();
                        is_correct = true;
                    }
                    else
                    {
                        is_correct = false;
                    }
                    break;
                case 2:
                    if (Directory.Exists(comm_args[0]) && Directory.Exists(comm_args[1]))
                    {
                        input_dir = comm_args[0];
                        output_dir = comm_args[1];
                        is_correct = true;
                    }
                    else
                    {
                        is_correct = false;
                    }
                    break;
            }
        }

        public string GetInputDir()
        {
            return input_dir;
        }

        public string GetOutputDir()
        {
            return output_dir;
        }

        public bool IsCorrect()
        {
            return is_correct;
        }
    }
}
