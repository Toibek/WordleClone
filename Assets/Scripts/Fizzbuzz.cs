using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Fizzbuzz : MonoBehaviour
{
    [SerializeField] Vector2Int section;
    [SerializeField] bool trigger;
    Dictionary<int, string> words;
    Hashtable w2 = new Hashtable();
    HashSet<string> hs = new HashSet<string>();

    private void Update()
    {
        if (trigger)
        {
            Run();
            trigger = false;
        }
    }
    private void Run()
    {
        if (words == null)
        {
            words = new Dictionary<int, string>();
            words.Add(3, "Fizz");
            words.Add(5, "Buzz");
            words.Add(7, "Fazz");
            words.Add(9, "bazz");
        }
        string output = "";
        for (int i = section.x; i < section.y; i++)
        {
            string s = "";
            foreach (var item in words)
                if (i % item.Key == 0) s += item.Value;
            if (s == "") s = i.ToString();
            output += s + "\n";
        }
        Debug.Log(output);
    }
}
