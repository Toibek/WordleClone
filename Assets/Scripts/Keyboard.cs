using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    string[] LetterRows = { "qwertyuiop", "asdfghjkl", "zxcvbnm" };

    Dictionary<char, Key> buttons = new();

    private void Start()
    {
        List<Key>[] keys = new List<Key>[LetterRows.Length];
        Key[] first = transform.GetChild(0).GetComponentsInChildren<Key>();
        for (int r = 0; r < keys.Length; r++)
        {
            keys[r] = new List<Key>(transform.GetChild(r).GetComponentsInChildren<Key>());
            for (int k = 0; k < keys[r].Count; k++)
            {
                keys[r][k].SetLetter(LetterRows[r][k]);
                keys[r][k].Keyboard = this;
                buttons.Add(LetterRows[r][k], keys[r][k]);
            }
        }
    }
    private void Update()
    {
        if (Input.anyKeyDown && Input.inputString != "")
            sendLetter(Input.inputString[0]);
    }
    public void sendLetter(char c)
    {
        Debug.Log(c);
    }
    public void Submit()
    {

    }
    public void Remove()
    {

    }
}
