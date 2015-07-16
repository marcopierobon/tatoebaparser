using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using TatoebaParser.Helpers;

namespace TatoebaParser
{
    public class TatoebaParserClass
    {
        private string SentencesFilePath = null;
        private string LinksFilePath = null;
        private string SourceLang = null;
        private string DestLang = null;

        public static void Main()
        {
        }

        public async Task<int> Run()
        {
            var t = new Task<int>(() =>
            {
                var destFile = "output.txt";
                var duplicatesEnabled = false;
                var sameSourceSameLine = true;

                var directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                var matchDictionary = new Dictionary<int, List<int>>();
                var results = new Dictionary<string, List<string>>();

                var sentenceList = ReadHelpers.ReadSentenceList(SentencesFilePath, SourceLang, DestLang);
                ReadHelpers.ReadLinkList(LinksFilePath, matchDictionary);

                var writer = new StreamWriter(File.OpenWrite(directory + "\\" + destFile));
                ExtractMatchingStrings(matchDictionary, sentenceList, duplicatesEnabled, SourceLang, DestLang, results, writer);
                //if the duplicates are not enabled, the writing is performed at the end
                if (!duplicatesEnabled)
                {
                    WriteHelpers.WriteToFileNoDuplicate(sameSourceSameLine, results, writer);
                }
                writer.Close();
                return 0;
            });
            t.Start();
            return 0;
        }

        public TatoebaParserClass(string sentencesFilePath, string linksFilePath, string sourceLang, string destLang)
        {
            SentencesFilePath = sentencesFilePath;
            LinksFilePath = linksFilePath;
            SourceLang = sourceLang;
            DestLang = destLang;
        }

        public static void ExtractMatchingStrings(Dictionary<int, List<int>> matchDictionary, Dictionary<int, string> sentenceList, bool duplicatesEnabled,
            string sourceLang, string destLang, Dictionary<string, List<string>> results, StreamWriter writer)
        {
            foreach (var outerElement in matchDictionary.Keys)
            {
                foreach (var innerElement in matchDictionary[outerElement])
                {
                    //direct translation
                    if (sentenceList.ContainsKey(outerElement) && sentenceList.ContainsKey(innerElement))
                    {
                        if (!duplicatesEnabled)
                            DictionaryHelpers.InsertMatchNoDuplicates(sentenceList, outerElement, innerElement, sourceLang, destLang, results);
                        else
                        {
                            DictionaryHelpers.InsertMatchWithDuplicates(sentenceList, outerElement, innerElement, sourceLang, destLang, writer);
                        }
                    }
                        //go through another language: if only the outer element is a match, another inner element is also, can skip the current
                    else if (sentenceList.ContainsKey(outerElement))
                    {
                        continue;
                    }
                        //go through another language: if only the inner element is a match, another inner element is also, must find it in the current list
                    else if (sentenceList.ContainsKey(innerElement))
                    {
                        int element = innerElement;
                        var availableSentencesForSearch = matchDictionary[outerElement].Where(x => x != element);
                        foreach (var altInnerElement in availableSentencesForSearch)
                        {
                            //direct translation
                            if (sentenceList.ContainsKey(altInnerElement) && sentenceList.ContainsKey(innerElement))
                            {
                                if (!duplicatesEnabled)
                                    DictionaryHelpers.InsertMatchNoDuplicates(sentenceList, altInnerElement, innerElement, sourceLang, destLang,
                                        results);
                                else
                                {
                                    DictionaryHelpers.InsertMatchWithDuplicates(sentenceList, altInnerElement, innerElement, sourceLang, destLang,
                                        writer);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
