using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
public static class Words
{
    static HashSet<string> words;
    public static void LoadWords()
    {
        string path = Application.dataPath + "/Resources/words.txt";
        Debug.Log(path);
        words = new(File.ReadAllLines(path));
    }
    public static bool Contains(string word)
    {
        if (words == null || words.Count <= 0) LoadWords();
        return words.Contains(word);
    }
    public static string RandomWord()
    {
        if (words == null || words.Count == 0) LoadWords();
        return words.ElementAt(Random.Range(0, words.Count));
    }
}
