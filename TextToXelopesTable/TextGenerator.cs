using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemmaSharp;
using System.Windows.Forms;
using System.IO;
using LemmaSharp.Classes;

namespace TextToXelopesTable
{
    class PartialComparer : IEqualityComparer<string>
    {
        
        public bool Equals(string x, string y)
        {
            //string.CompareOrdinal(x, y);
            return string.CompareOrdinal(x, y)==0;
        }

        public int GetHashCode(string obj)
        {
            return base.GetHashCode();
        }
    }


    public class TextGenerator
    {
        List<List<string>> Sentences;
        List<string> WordsAll;
        struct Record
        {
            public int TransactId { get; set; }
            public int ItemId { get; set; }
            public string Item { get; set; }

            public override string ToString()
            {
                return TransactId.ToString() + ' ' + ItemId.ToString() + ' ' + Item;
            }

        }

        List<Record> recList = new List<Record>();
        string RelationName;
        public enum KeyWord
        {
            relation,
            attribute,
            data,
        }

        public enum Attribute
        {
            transactId,
            itemId,
            itemName,
        }

        string GetKeyWord(KeyWord kw)
        {
            return "@"+kw.ToString();
        }

        string GetAttribute(Attribute a)
        {
            return a.ToString();
        }

        string GetIdList(dynamic list)
        {
            StringBuilder s = new StringBuilder();
            s.Append("{");
            for(int i=0; i< list.Count;i++)
            {
                if (i == list.Count - 1)
                {
                    s.Append(" " + i.ToString() + " }");
                }
                else
                {
                    s.Append(" " + i.ToString() + ",");
                }
            }
            return s.ToString();
        }

        string GetNameList()
        {
            StringBuilder s = new StringBuilder();
            s.Append("{");
            for (int i = 0; i < Sentences.Count; i++)
            {
                for (int j = 0; j < Sentences[i].Count; j++)
                {
                    if (Sentences[i][j] == Sentences[Sentences.Count-1][Sentences[Sentences.Count - 1].Count-1])
                    {
                        s.Append(" " + Sentences[i][j].ToString() + " }");
                    }
                    else
                    {
                        s.Append(" " + Sentences[i][j] + ",");
                    }
                }
            }
            return s.ToString();
        }

        public StringBuilder GetHeader()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(GetKeyWord(KeyWord.relation) + "'"+RelationName+"'");
            sb.AppendLine(Environment.NewLine);
            sb.AppendLine(GetKeyWord(KeyWord.attribute) + " " + GetAttribute(Attribute.transactId) + " " + GetIdList(Sentences));
            sb.AppendLine(GetKeyWord(KeyWord.attribute) + " " + GetAttribute(Attribute.itemId) + " " + GetIdList(WordsAll));
            sb.AppendLine(GetKeyWord(KeyWord.attribute) + " " + GetAttribute(Attribute.itemName) + " " + GetNameList());
            sb.AppendLine(Environment.NewLine);
            sb.AppendLine(GetKeyWord(KeyWord.data));
            foreach (var item in recList)
            {
                sb.AppendLine(item.ToString());
            }
            return sb;
        }



        public TextGenerator(IFileReader inpReader, string RelationName)
        {
            this.RelationName = RelationName;
            Sentences = new StringParser(inpReader).GetSentances;
            var stream = File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + @"\\full7z-mlteast-ru.lem");
            var lemmatizer = new Lemmatizer(stream);
            WordsAll = new List<string>();
            foreach (var Sentence in Sentences)
            {
                for (int i = 0; i < Sentence.Count; i++)
                {
                    Sentence[i] = lemmatizer.Lemmatize(Sentence[i]);
                    WordsAll.Add(Sentence[i]);
                }
            }
            WordsAll= WordsAll.Distinct(new PartialComparer()).ToList();
            var SentenceNum = 0;
            foreach (var Sentence in Sentences)
            {
                foreach (var Word in Sentence)
                {
                    recList.Add(new Record() { TransactId = SentenceNum, ItemId = WordsAll.FindIndex(a => a == Word), Item = Word });
                }
                SentenceNum++;
            }
            //for (int i = 0; i < Sentences.Count; i++)
            //{
            //    for (int j = 0; j < Sentences[i].Count; j++)
            //    {
            //        recList.Add(new Record() { TransactId = i, ItemId = j, Item = Sentences[i][j] });
            //    }
            //}
        }

    }
}
