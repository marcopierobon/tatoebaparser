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
        private readonly string _sentencesFilePath = null;
        private readonly string _linksFilePath = null;
        private readonly string _outputFilePath = null;
        private readonly string _sourceLang = null;
        private readonly string _destLang = null;

        private readonly bool _duplicatesEnabled = false;
        private readonly bool _sameSourceSameLine = false;

        public static void Main()
        {
        }

        public async Task<int> Run(Action callback)
        {
            var t = new Task<int>(() =>
            {
                var matchDictionary = new Dictionary<int, List<int>>();
                var results = new Dictionary<string, List<string>>();

                var sentenceList = ReadHelpers.ReadSentenceList(_sentencesFilePath, _sourceLang, _destLang);
                ReadHelpers.ReadLinkList(_linksFilePath, matchDictionary);

                var writer = new StreamWriter(_outputFilePath, false);
                ExtractMatchingStrings(matchDictionary, sentenceList, _duplicatesEnabled, _sourceLang, _destLang, results, writer);
                //if the duplicates are not enabled, the writing is performed at the end
                if (!_duplicatesEnabled)
                {
                    WriteHelpers.WriteToFileNoDuplicate(_sameSourceSameLine, results, writer);
                }
                writer.Close();
                callback();
                return 0;
            });
            t.Start();
            return 0;
        }

        public TatoebaParserClass(string sentencesFilePath, string linksFilePath, string outputFilePath, string sourceLang, string destLang, bool duplicatesEnabled, bool sameSourceSameLine)
        {
            _sentencesFilePath = sentencesFilePath;
            _linksFilePath = linksFilePath;
            _outputFilePath = outputFilePath;
            _sourceLang = sourceLang;
            _destLang = destLang;
            _duplicatesEnabled = duplicatesEnabled;
            _sameSourceSameLine = sameSourceSameLine;
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
                        var element = innerElement;
                        var availableSentencesForSearch = matchDictionary[outerElement].Where(x => x != element);
                        foreach (var altInnerElement in availableSentencesForSearch)
                        {
                            //direct translation
                            if (!sentenceList.ContainsKey(altInnerElement) || !sentenceList.ContainsKey(innerElement))
                                continue;
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
