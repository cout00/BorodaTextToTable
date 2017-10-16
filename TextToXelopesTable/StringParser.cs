using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TextToXelopesTable
{
    public class StringParser
    {
        private IFileReader filereader;
        List<List<string>> Sentances = new List<List<string>>();

        public StringParser(IFileReader filereader)
        {
            this.filereader = filereader;
            Parse();
        }

        public List<List<string>> GetSentances
        {
            get
            {
                return Sentances;
            }
        }

        void Parse()
        {
            using (filereader.GetStream())
            {
                var parsStr = filereader.GetStream().ReadToEnd();
                var curstr = "";
                var isCommas = false;
                var WordsInSentance = new List<string>();
                for (int i = 0; i < parsStr.Length; i++)
                {
                    if (parsStr[i] == '/')
                    {
                        var me=1;
                    }
                    if (parsStr[i].IsBlackList())
                    {
                        continue;
                    }
                    if (parsStr[i] == '"' && !isCommas)
                    {
                        isCommas = true;
                        continue;
                    }
                    if (isCommas)
                    {
                        if (!(parsStr[i] == '"'))
                        {
                            curstr += parsStr[i];
                            continue;
                        }
                        else
                        {
                            WordsInSentance.Add(curstr);
                            curstr = string.Empty;
                        }
                    }
                    if (parsStr[i] == ' ' || parsStr[i] == '/')
                    {                        
                        if (curstr.isDigit() || curstr.Length < 4)
                        {
                            curstr = string.Empty;
                            continue;
                        }
                        else
                        {
                            WordsInSentance.Add(curstr);
                            curstr = string.Empty;
                            continue;
                        }
                    }
                    if (parsStr[i] == '.' && i < parsStr.Length - 5)
                    {
                        if (parsStr[i + 1] == ' ' && parsStr[i + 2].IsLargeSymbol())
                        {
                            WordsInSentance.Add(curstr);
                            Sentances.Add(WordsInSentance);
                            curstr = string.Empty;
                            WordsInSentance = new List<string>();
                            i++;
                            continue;
                        }
                    }
                    curstr += parsStr[i];
                }
                WordsInSentance.Add(curstr);
                Sentances.Add(WordsInSentance);
            }
        }
    }
}