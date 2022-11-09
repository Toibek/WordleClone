using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    internal GameManager manager;
    string[] LetterRows = { "qwertyuiop", "asdfghjkl", "zxcvbnm" };

    [SerializeField] Dictionary<char, Key> buttons = new();

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
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
                Remove();
            else if (Input.GetKeyDown(KeyCode.Return))
                Submit();
            else if (Input.inputString != "")
                SendLetter(Input.inputString[0]);
        }
    }
    public void Clear()
    {
        foreach (var key in buttons)
        {
            key.Value.SetState(letterState.Default);
        }
    }
    public void SendLetter(char c) { manager.SetLetter(c); }
    public void Submit() { manager.Submit(); }
    public void Remove() { manager.RemoveLetter(); }
    public void SetLettersState(char c, letterState state) { buttons[c].SetState(state); }
}
