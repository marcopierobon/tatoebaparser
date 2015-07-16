using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TatoebaParser.Helpers
{
    class DictionaryHelpers
    {
        protected internal static void InsertMatchWithDuplicates(Dictionary<int, string> sentenceList, int outerElement, int innerElement, string sourceLang,
            string destLang, StreamWriter writer)
        {
            var sentence1 = sentenceList[outerElement];
            var sentence2 = sentenceList[innerElement];

            var values1 = sentence1.Split('\t');
            if (values1.Length < 3)
                return;
            var lang1 = values1[1];
            var sentence1Value = values1[2];

            var values2 = sentence2.Split('\t');
            if (values2.Length < 3)
                return;
            var lang2 = values2[1];
            var sentence2Value = values2[2];

            if (lang1.Equals(sourceLang) && lang2.Equals(destLang))
            {
                writer.WriteLine(String.Format("{0}{1}{2}", sentence1Value, "\t".ToString(), sentence2Value));
            }
            else if (lang2.Equals(sourceLang) && lang1.Equals(destLang))
            {
                writer.WriteLine(String.Format("{0}{1}{2}", sentence2Value, "\t".ToString(), sentence1Value));
            }
        }

        protected internal static void InsertMatchNoDuplicates(Dictionary<int, string> sentenceList, int outerElement, int innerElement, string sourceLang,
            string destLang, Dictionary<string, List<string>> results)
        {
            var sentence1 = sentenceList[outerElement];
            var sentence2 = sentenceList[innerElement];

            var values1 = sentence1.Split('\t');
            if (values1.Length < 3)
                return;
            var lang1 = values1[1];
            var sentence1Value = values1[2];

            var values2 = sentence2.Split('\t');
            if (values2.Length < 3)
                return;
            var lang2 = values2[1];
            var sentence2Value = values2[2];

            if (lang1.Equals(sourceLang) && lang2.Equals(destLang))
            {
                AddValueToStringDictionary(results, sentence1Value, sentence2Value);
            }
            else if (lang2.Equals(sourceLang) && lang1.Equals(destLang))
            {
                AddValueToStringDictionary(results, sentence2Value, sentence1Value);
            }
        }

        protected internal static void AddValueToStringDictionary(Dictionary<string, List<string>> results, string sentence1Value, string sentence2Value)
        {
            if (results.ContainsKey(sentence1Value))
            {
                if (results[sentence1Value].Contains(sentence2Value))
                {
                    return;
                }
                else
                {
                    results[sentence1Value].Add(sentence2Value);
                }
            }
            else
            {
                var newList = new List<string> { sentence2Value };
                results.Add(sentence1Value, newList);
            }
        }

        protected internal static bool AddValueToIntDictionary(Dictionary<int, List<int>> matchDictionary, int sentence1Id, int sentence2Id)
        {
            if (matchDictionary.ContainsKey(sentence1Id))
            {
                if (matchDictionary[sentence1Id].Contains(sentence2Id))
                    return true;
                else
                {
                    matchDictionary[sentence1Id].Add(sentence2Id);
                }
            }
            else
            {
                var newList = new List<int>();
                newList.Add(sentence2Id);
                matchDictionary.Add(sentence1Id, newList);
            }
            return false;
        }
    }
}
