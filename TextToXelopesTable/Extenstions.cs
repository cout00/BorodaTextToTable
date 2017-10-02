using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToXelopesTable
{
    static class Extenstions
    {
        public static bool IsLargeSymbol (this char symb)
        {
            var coll = Enumerable.Range(65, 35).ToList();
            coll.AddRange(Enumerable.Range(1040, 31));
            return coll.Contains(symb);
        }

        public static bool isDigit(this string str)
        {
            var res = 0;
            return int.TryParse(str,out res);
        }

        public static bool IsBlackList(this char symb)
        {
            var list= Enumerable.Range(33, 13).ToList();
            list.Add(47);
            list.AddRange(Enumerable.Range(58, 6));
            return list.Contains(symb);
        }

    }
}
