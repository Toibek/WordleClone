using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Key : MonoBehaviour
{
    [Tooltip("0: Default, 1:Wrong , 2: WrongPlace, 3:Correct")]
    [SerializeField] private Color[] _colors;
    [Tooltip("0: Default, 1:Wrong , 2: WrongPlace, 3:Correct")]
    [SerializeField] private Color[] _textColors;
    [SerializeField] private letterState _currentState = letterState.Default;

    internal Keyboard Keyboard;
    private char _character;

    Image _background;
    TMP_Text _text;

    private void Start()
    {
        _background = GetComponent<Image>();
        _text = GetComponentInChildren<TMP_Text>();
        GetComponent<Button>().onClick.AddListener(SendLetter);
        UpdateVisuals();
    }
    public void SetLetter(char c)
    {
        _text.text = c.ToString();
        _character = c;
    }
    public void SetState(letterState state)
    {
        _currentState = state;
        UpdateVisuals();
    }
    private void SendLetter()
    {
        Keyboard.sendLetter(_character);
    }
    private void UpdateVisuals()
    {
        _background.color = _colors[(int)_currentState];
        _text.color = _textColors[(int)_currentState];
    }
}
