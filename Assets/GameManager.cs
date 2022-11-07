using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _row = 0;
    private int _letter = 0;

    private int _rowCount = 5;
    private Letter[] _activeRow;
    private Transform _textParent;
    private Keyboard _keyboard;

    [SerializeField] private string _wordToGuess;
    [SerializeField] private string _currentGuess;

    [SerializeField] Transform PopTf;
    [SerializeField] Transform WinTf;
    [SerializeField] Transform LosTf;
    void Start()
    {
        _textParent = transform.GetChild(0);
        _keyboard = transform.GetChild(1).GetComponent<Keyboard>();
        _keyboard.manager = this;

        StartGame();
    }
    public void StartGame()
    {
        _row = 0;
        _letter = 0;
        SetRow(0);
        _wordToGuess = Words.RandomWord();
    }
    public void ClearAll()
    {
        for (int r = 0; r < _rowCount; r++)
        {
            SetRow(r);
            for (int l = 0; l < _activeRow.Length; l++)
                _activeRow[l].SetLetter(' ');
        }
    }
    public void SetRow(int row)
    {
        Transform parent = _textParent.GetChild(row);
        _activeRow = new Letter[5];
        for (int i = 0; i < _activeRow.Length; i++)
            _activeRow[i] = parent.GetChild(i).GetComponent<Letter>();
        _letter = 0;
        _currentGuess = "";
    }
    public void Submit()
    {
        if (_currentGuess.Length < 5) { Debug.Log("Too Short"); return; }
        if (!Words.Contains(_currentGuess.ToLower())) { Debug.Log("Word not in library"); return; }
        Debug.Log("Valid word");
        int correct = 0;
        for (int i = 0; i < _currentGuess.Length; i++)
        {
            if (_currentGuess[i] == _wordToGuess[i])
            {
                correct++;
                _activeRow[i].SetState(letterState.Correct);
                _keyboard.SetLettersState(_currentGuess[i], letterState.Correct);
            }
            else if (_wordToGuess.Contains(_currentGuess[i]))
            {
                _activeRow[i].SetState(letterState.WrongPlace);
                _keyboard.SetLettersState(_currentGuess[i], letterState.WrongPlace);
            }
            else
            {
                _activeRow[i].SetState(letterState.Wrong);
                _keyboard.SetLettersState(_currentGuess[i], letterState.Wrong);
            }
        }
        if (correct == _currentGuess.Length) { Debug.Log("Game Won"); return; }

        if (++_row >= _rowCount) { Debug.Log("Game Lost"); return; }
        SetRow(_row);
    }
    public void SetLetter(char c)
    {
        if (_letter >= _activeRow.Length) return;
        _activeRow[_letter++].SetLetter(c);
        _currentGuess += c;
    }
    public void RemoveLetter()
    {
        if (_letter <= 0) return;
        _activeRow[--_letter].SetLetter(' ');
        _currentGuess = _currentGuess[0..^1];
    }
    public void Restart()
    {
        ClearAll();
        StartGame();
    }
    
}
