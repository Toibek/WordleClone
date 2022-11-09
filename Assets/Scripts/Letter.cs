using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Letter : MonoBehaviour
{
    private Color[] _colors;
    private Color[] _textColors;
    private letterState _currentState = letterState.Default;

    Image _background;
    TMP_Text _text;

    private void Awake()
    {
        _background = GetComponent<Image>();
        _text = GetComponentInChildren<TMP_Text>();

        _colors = GameManager.Instance.Colors;
        _textColors = GameManager.Instance.TextColors;

        UpdateVisuals();
        _text.text = "";
    }
    public void SetLetter(char letter)
    {
        _text.text = letter.ToString().ToUpper();
    }
    public void SetState(letterState state)
    {
        _currentState = state;
        UpdateVisuals();
    }
    void UpdateVisuals()
    {
        _background.color = _colors[(int)_currentState];
        _text.color = _textColors[(int)_currentState];
    }
}
