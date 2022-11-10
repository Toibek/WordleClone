using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    private string[] _letterRows = { "qwertyuiop", "asdfghjkl", "zxcvbnm" };
    private Dictionary<char, Key> _buttons = new();

    private void Start()
    {
        List<Key>[] keys = new List<Key>[_letterRows.Length];
        Key[] first = transform.GetChild(0).GetComponentsInChildren<Key>();
        for (int r = 0; r < keys.Length; r++)
        {
            keys[r] = new List<Key>(transform.GetChild(r).GetComponentsInChildren<Key>());
            for (int k = 0; k < keys[r].Count; k++)
            {
                keys[r][k].SetLetter(_letterRows[r][k]);
                keys[r][k].Keyboard = this;
                _buttons.Add(_letterRows[r][k], keys[r][k]);
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
        foreach (var key in _buttons)
        {
            key.Value.SetState(letterState.Default);
        }
    }
    public void SendLetter(char c) { GameManager.Instance.SetLetter(c); }
    public void Submit() { GameManager.Instance.Submit(); }
    public void Remove() { GameManager.Instance.RemoveLetter(); }
    public void SetLettersState(char c, letterState state) { if (state == letterState.Default || state > _buttons[c].GetState()) _buttons[c].SetState(state); }
}
