using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Letter : MonoBehaviour
{
    [Tooltip("0: Default, 1:Wrong , 2: WrongPlace, 3:Correct")]
    [SerializeField] private Color[] _colors;
    [SerializeField] private Color[] _textColors;
    [SerializeField] private letterState _currentState;
    [SerializeField] private bool _colorTest;

    Image _background;
    TMP_Text _text;
    private void Start()
    {
        _background = GetComponent<Image>();
        _text = GetComponentInChildren<TMP_Text>();

        _background.color = _colors[(int)_currentState];
        _text.color = _textColors[(int)_currentState];
    }
    private void Update()
    {
        if (_colorTest)
        {
            _colorTest = false;
            _background.color = _colors[(int)_currentState];
            _text.color = _textColors[(int)_currentState];
        }
    }

    public enum letterState { Default = 0, Wrong = 1, WrongPlace = 2, Correct = 3 }
}
