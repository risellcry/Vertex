#pragma warning disable
using System;
using System.IO;
using System.Collections.Generic;

namespace Vertex
{
    class Program
    {
        private static void Error(int number,string token)
        {
            if (number == 1) Console.WriteLine("Invalid token : " + token);
            if (number == 2) Console.WriteLine("Unable to set " + token.GetType().ToString().ToLower() + "(" + token + ") inside an int");
            if (number == 3) Console.WriteLine("Unable to set " + token.GetType().ToString().ToLower() + "(" + token + ") inside an int");
            if (number == 4) Console.WriteLine("Unable to compile");
            if (number == 5) Console.WriteLine("Invalid file type");
        }
        private static void Compile(string path)
        {
            if (File.Exists(path))
            {
                StreamReader reader = new StreamReader(path);
                Dictionary<string,string> _str = new Dictionary<string,string>();
                Dictionary<string,int> _int = new Dictionary<string,int>();
                Dictionary<string,float> _flt = new Dictionary<string,float>();
                Dictionary<string,bool> _bln = new Dictionary<string,bool>();
                string[] lines = {};
                string line = reader.ReadLine();
                string first = "";
                string middle = "";
                string last = "";
                int cline = 0;
                int intTester = 0;
                float fltTester = 0F;
                while (line != null)
                {
                    foreach (var token in line.Split(' '))
                    {
                        if (first != "" && middle != "" && last != "") lines[cline] = ""; cline++;
                        if (token == "int") {first = "i";}
                        if (token == "str") {first = "s";}
                        if (token == "flt") {first = "f";}
                        if (token == "bln") {first = "b";}
                        if (token == "pac") {first = "k";}
                        if (token == "print") {first = "p";}
                        if (token == "set") {first = "t";}
                        if (token == "if") {first = "f";}
                        if (token == "==") {middle = "=";}
                        if (token == "<") {middle = "<";}
                        if (token == ">") {middle = ">";}
                        else
                        {
                            if (token.StartsWith("@"))
                            {
                                string mToken = token.Replace("@","");
                                if (_str.ContainsKey(token) && first == "") {first = mToken;};
                                if (_int.ContainsKey(token) && first == "") {first = mToken;};
                                if (_flt.ContainsKey(token) && first == "") {first = mToken;};
                                if (_flt.ContainsKey(token) && first == "") {first = mToken;};
                            }
                            else
                            {
                                if ((first == "i" && (middle != "")) && int.TryParse(token,out intTester))
                                {
                                    last = token;
                                }
                                else
                                {
                                    Error(2,token);
                                }
                                if ((first == "s" && (middle != "")) && int.TryParse(token,out intTester))
                                {
                                    last = token;
                                }
                                else
                                {
                                    Error(2,token);
                                }
                                if ((first == "f" && (middle != "")) && float.TryParse(token,out fltTester))
                                {
                                    last = token;
                                }
                                else
                                {
                                    Error(3,token);
                                }
                                if (first != "" && (middle == "=" || middle == ">" || middle == "<") && last != "")
                                {
                                    last = token;
                                }
                                else
                                {
                                    Error(1,token);
                                }
                            }
                        }
                    }
                    if (reader.ReadLine() != null)
                    {
                        line = reader.ReadLine();
                    }
                    else
                    {
                        break;
                    }
                }
                reader.Close();
                StreamWriter writer = new StreamWriter(path.Replace(".vt",".cvt"));
                if (lines.Length > 0)
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        writer.WriteLine(lines[i]);
                    }
                    Console.WriteLine("Finished compiled the file");
                }
                else
                {
                    Error(4,"~");
                }
            }
        }
        private static void Run(string path)
        {
            if (File.Exists(path))
            {
                StreamReader reader = new StreamReader(path);
                if (path.EndsWith(".cvt"))
                {
                    //
                }
                else
                {
                    Error(5,"~");
                }
            }
        }
        private static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string sArg = "";
                foreach (var arg in args)
                {
                    if (arg == "-c" && sArg == "") sArg = "-c";
                    if (arg == "-r" && sArg == "") sArg = "-r";
                    if (arg == "-v" && sArg == "") Console.WriteLine("0.1.0");
                    else
                    {
                        if (sArg == "-c" && arg != sArg)
                        {
                            sArg = "";
                            Compile(arg);
                        }
                        if (sArg == "-r" && arg != sArg)
                        {
                            sArg = "";
                            Run(arg);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Vertex - Programming Language");
                Console.WriteLine("-c <file.vt> (Compile)");
                Console.WriteLine("<file.cvt> (Run)");
                Console.WriteLine("-v (Version)");
            }
        }
    }
}