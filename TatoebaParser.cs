using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using TatoebaParser.Helpers;

namespace TatoebaParser
{
    class TatoebaParser
    {
        static void Main(string[] args)
        {
            //if (args.Length != 4)
            //{
            //    Console.WriteLine("Expected arguments: sentence file (full path), link files (full path), source lang, destination lang");
            //    return;
            //}

            var sentencesFile = "sentences.csv";
            var linksFile = "links.csv";
            var sourceLang = "ron";
            var destLang = "spa";
            var destFile = "output.txt";
            var duplicatesEnabled = false;
            var sameSourceSameLine = true;

            var directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            var matchDictionary = new Dictionary<int, List<int>>();
            var results = new Dictionary<string, List<string>>();

            var sentenceList = ReadHelpers.ReadSentenceList(directory, sentencesFile, sourceLang, destLang);

            ReadHelpers.ReadLinkList(directory, linksFile, matchDictionary);

            var writer = new StreamWriter(File.OpenWrite(directory + "\\" + destFile));
            ExtractMatchingStrings(matchDictionary, sentenceList, duplicatesEnabled, sourceLang, destLang, results, writer);
            //if the duplicates are not enabled, the writing is performed at the end
            if (!duplicatesEnabled)
            {
                WriteHelpers.WriteToFileNoDuplicate(sameSourceSameLine, results, writer);
            }
            writer.Close();

        }

        private static void ExtractMatchingStrings(Dictionary<int, List<int>> matchDictionary, Dictionary<int, string> sentenceList, bool duplicatesEnabled,
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
