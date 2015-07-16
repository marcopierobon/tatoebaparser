using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TatoebaParser.Helpers
{
    class ReadHelpers
    {
        protected internal static StreamReader ReadLinkList(string directory, string linksFile, Dictionary<int, List<int>> matchDictionary)
        {
            var reader = new StreamReader(File.OpenRead(directory + "\\" + linksFile));
            var linkList = new List<string>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line != null)
                {
                    var values = line.Split('\t');
                    if (values.Length < 2)
                        continue;
                    int sentence1Id;
                    int sentence2Id;

                    int.TryParse(values[0], out sentence1Id);
                    int.TryParse(values[1], out sentence2Id);

                    if (sentence1Id < sentence2Id)
                    {
                        DictionaryHelpers.AddValueToIntDictionary(matchDictionary, sentence1Id, sentence2Id);
                    }
                    else if (sentence1Id > sentence2Id)
                    {
                        DictionaryHelpers.AddValueToIntDictionary(matchDictionary, sentence2Id, sentence1Id);
                    }
                }
            }
            return reader;
        }

        protected internal static Dictionary<int, string> ReadSentenceList(string directory, string sentencesFile, string sourceLang, string destLang)
        {
            var reader = new StreamReader(File.OpenRead(directory + "\\" + sentencesFile));
            var sentenceList = new Dictionary<int, string>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line != null)
                {
                    var values = line.Split('\t');
                    if (values.Length < 3)
                        continue;
                    var id = 0;
                    int.TryParse(values[0], out id);
                    var lang = values[1];

                    if (lang.Equals(sourceLang) || lang.Equals(destLang))
                    {
                        sentenceList.Add(id, line);
                    }
                }
            }
            reader.Close();
            return sentenceList;
        }
    }
}
