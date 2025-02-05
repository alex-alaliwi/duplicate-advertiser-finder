using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DuplicateAdvertiserFinder

{
    public class DuplicateAdvertiserFinder
    {
        public static void Main(string[] args)
        {
            string path = "advertisers.txt";

            var rawAdvertisers = File.ReadAllLines(path).ToList();
            var standardizedNames = rawAdvertisers.Select(Standardize).Distinct().ToList();
            var similarEntries = MatchSimilarNames(standardizedNames);

            if (similarEntries.Any())
            {
                foreach (var pair in similarEntries)
                {
                    Console.WriteLine($"- {pair.First} ↔ {pair.Second}");
                }
            }
        }

        public static string Standardize(string input)
        {
            return Regex.Replace(input.ToLower(), "[^a-z0-9 ]", "").Trim();
        }

        public static List<(string First, string Second)> MatchSimilarNames(List<string> entries)
        {
            var potentialMatches = new List<(string, string)>();
            var processedPairs = new HashSet<(string, string)>();

            for (int i = 0; i < entries.Count; i++)
            {
                if (i % 50 == 0)
                    Console.WriteLine($"Processing {i + 1}/{entries.Count}...");

                for (int j = i + 1; j < entries.Count; j++)
                {
                    string nameA = entries[i];
                    string nameB = entries[j];

                    if (processedPairs.Contains((nameA, nameB)) || processedPairs.Contains((nameB, nameA)))
                        continue;

                    processedPairs.Add((nameA, nameB));

                    if (AreNamesSimilar(nameA, nameB))
                    {
                        potentialMatches.Add((nameA, nameB));
                    }
                }
            }

            return potentialMatches;
        }

        public static bool AreNamesSimilar(string a, string b)
        {
            if (Math.Abs(a.Length - b.Length) > 5)
                return false;

            if (!a.Contains(' ') && !b.Contains(' '))
            {
                return EditDistance(a, b) <= 1;
            }

            double tokenSimilarity = TokenSimilarity(a, b);
            if (tokenSimilarity > 0.75)
                return true;

            return EditDistance(a, b) <= 2;
        }

        public static double TokenSimilarity(string text1, string text2)
        {
            var set1 = new HashSet<string>(text1.Split(' '));
            var set2 = new HashSet<string>(text2.Split(' '));

            var intersection = set1.Intersect(set2).Count();
            var union = set1.Union(set2).Count();

            return union == 0 ? 0 : (double)intersection / union;
        }

        public static int EditDistance(string s1, string s2)
        {
            int len1 = s1.Length;
            int len2 = s2.Length;
            int[,] dp = new int[len1 + 1, len2 + 1];

            for (int i = 0; i <= len1; i++) dp[i, 0] = i;
            for (int j = 0; j <= len2; j++) dp[0, j] = j;

            for (int i = 1; i <= len1; i++)
            {
                for (int j = 1; j <= len2; j++)
                {
                    int cost = (s1[i - 1] == s2[j - 1]) ? 0 : 1;
                    dp[i, j] = Math.Min(
                        Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1),
                        dp[i - 1, j - 1] + cost
                    );
                }
            }

            return dp[len1, len2];
        }
    }
}
