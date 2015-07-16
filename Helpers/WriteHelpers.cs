﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TatoebaParser.Helpers
{
    class WriteHelpers
    {
        protected internal static void WriteToFileNoDuplicate(bool sameSourceSameLine, Dictionary<string, List<string>> results, StreamWriter writer)
        {
            //if the answer duplicates are not shown on the same line add several lines
            if (!sameSourceSameLine)
            {
                foreach (var outerElement in results.Keys)
                {
                    foreach (var innerElement in results[outerElement])
                        writer.WriteLine(String.Format("{0}{1}{2}", outerElement, "\t".ToString(), innerElement));
                }
            }
            //if the answer duplicates are shown on the same line concat them
            else
            {
                foreach (var outerElement in results.Keys)
                {
                    var result = "";
                    foreach (var innerElement in results[outerElement])
                    {
                        result = innerElement + " / " + result;
                    }
                    if (result.Length > 3)
                        result = result.Substring(0, result.Length - 3);
                    writer.WriteLine(String.Format("{0}{1}{2}", outerElement, "\t".ToString(), result));
                }
            }
        }
    }
}