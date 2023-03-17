using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Araclar barındıran kume
/// </summary>
namespace Library.Common.Utils
{
    /// <summary>
    /// pa jsd
    /// </summary>
    public class Parser
    {
        /// <summary>
        /// Parses the specified text.
        /// </summary>
        /// <param name="txt">The text.</param>
        /// <param name="findedItemInfos">The finded item infos.</param>
        /// <returns></returns>
        public Dictionary<string, string> Parse(string txt, List<FID> findedItemInfos)
        {
            Dictionary<string, string> findedItems = new Dictionary<string, string>();
            int start = 0;
            string data = "";
            int currentIterator = 0;
            foreach (var item in findedItemInfos)
            {
                FindNew(txt, item, ref currentIterator, ref start, ref data);
                findedItems.Add(item.Name, data);
            }
            return findedItems;
        }
        /// <summary>
        /// Parses the numerics.
        /// </summary>
        /// <param name="txt">The text.</param>
        /// <param name="findedItemInfos">The finded item infos.</param>
        /// <returns></returns>
        public Dictionary<string, double> ParseNumerics(string txt, List<FID> findedItemInfos)
        {
            Dictionary<string, double> findedItems = new Dictionary<string, double>();
            int start = 0;
            string data = "";
            int currentIterator = 0;
            foreach (var item in findedItemInfos)
            {
                FindNew(txt, item, ref currentIterator, ref start, ref data);
                findedItems.Add(item.Name, double.Parse(data));
            }
            return findedItems;
        }
        /// <summary>
        /// Finds the new.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="findInfo">The find information.</param>
        /// <param name="currentIterator">The current iterator.</param>
        /// <param name="start">The start.</param>
        /// <param name="data">The data.</param>
        private static void FindNew(string text, FID findInfo, ref int currentIterator, ref int start, ref string data)
        {
            findInfo.CurrentIterator = currentIterator;
            bool ok = false;
            for (int i = start; i < text.Length; i++)
            {
                var item = text[i];
                if (item == findInfo.Begin)
                {
                    findInfo.Ok();
                }
                if (findInfo.IsReaded)
                {
                    for (int j = i + 1; j < text.Length; j++)
                    {
                        item = text[j];
                        if (item == findInfo.End)
                        {
                            data = text.Substring(i + 1, j - i - 1);
                            ok = true;
                            start = i + data.Length + 1;
                            currentIterator = findInfo.CurrentIterator;
                            break;

                        }
                    }
                }
                if (ok)
                {
                    break;
                }
            }
        }
    }
}
