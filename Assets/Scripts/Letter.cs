using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Letter : MonoBehaviour
{
    [Tooltip("0: Default, 1:Wrong , 2: WrongPlace, 3:Correct")]
    [SerializeField] private Color[] _colors;
    [Tooltip("0: Default, 1:Wrong , 2: WrongPlace, 3:Correct")]
    [SerializeField] private Color[] _textColors;
    [SerializeField] private letterState _currentState = letterState.Default;

    Image _background;
    TMP_Text _text;

    private void Start()
    {
        _background = GetComponent<Image>();
        _text = GetComponentInChildren<TMP_Text>();
        UpdateVisuals();
        _text.text = "";
    }
    public void SetLetter(string letter)
    {
        _text.text = letter;
    }
    public void SetState(letterState state)
    {
        _currentState = state;
    }
    void UpdateVisuals()
    {
        _background.color = _colors[(int)_currentState];
        _text.color = _textColors[(int)_currentState];
    }
}
