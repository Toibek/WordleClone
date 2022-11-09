using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Key : MonoBehaviour
{
    internal Keyboard Keyboard;
    private Color[] _colors;
    private Color[] _textColors;
    private letterState _currentState = letterState.Default;
    private char _character;

    Image _background;
    TMP_Text _text;

    private void Awake()
    {
        _background = GetComponent<Image>();
        _text = GetComponentInChildren<TMP_Text>();
        _colors = GameManager.Instance.Colors;
        _textColors = GameManager.Instance.TextColors;
        GetComponent<Button>().onClick.AddListener(SendLetter);
        UpdateVisuals();
    }
    public void SetLetter(char c)
    {
        _text.text = c.ToString().ToUpper();
        _character = c;
    }
    public void SetState(letterState state)
    {
        _currentState = state;
        UpdateVisuals();
    }
    private void SendLetter()
    {
        Keyboard.SendLetter(_character);
    }
    private void UpdateVisuals()
    {
        _background.color = _colors[(int)_currentState];
        _text.color = _textColors[(int)_currentState];
    }
}
