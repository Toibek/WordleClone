using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _row = 0;
    private int _letter = 0;

    private int _rowCount;
    private Letter[] _activeRow;
    private Transform _textParent;
    private Keyboard _keyboard;

    [Tooltip("0: Default, 1:Wrong , 2: WrongPlace, 3:Correct")]
    public Color[] Colors;
    [Tooltip("0: Default, 1:Wrong , 2: WrongPlace, 3:Correct")]
    public Color[] TextColors;

    public AnimationCurve AnimationCurve;
    public float AnimationTime;

    [SerializeField] private string _wordToGuess;
    [SerializeField] private string _currentGuess;

    [SerializeField] Transform PopTf;
    [SerializeField] Transform WinTf;
    [SerializeField] Transform LosTf;

    public static GameManager Instance;
    #region singleton
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
    #endregion
    void Start()
    {
        _textParent = transform.GetChild(0);
        _rowCount = _textParent.childCount;
        _keyboard = transform.GetChild(1).GetComponent<Keyboard>();
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
        WinTf.gameObject.SetActive(false);
        LosTf.gameObject.SetActive(false);
        PopTf.gameObject.SetActive(false);
        _keyboard.Clear();
        for (int r = 0; r < _rowCount; r++)
        {
            SetRow(r);
            for (int l = 0; l < _activeRow.Length; l++)
            {
                _activeRow[l].SetLetter(' ');
                _activeRow[l].SetState(letterState.Default);
            }
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
    [ContextMenu("Thing")]
    public void Submit()
    {
        if (_currentGuess.Length < _wordToGuess.Length) { StartCoroutine(Popup("Word too short")); return; }
        if (!Words.Contains(_currentGuess.ToLower())) { StartCoroutine(Popup("Word not in library")); return; }

        char[] wordArr = _currentGuess.ToCharArray();

        int correct = 0;
        for (int i = 0; i < wordArr.Length; i++)
        {
            if (wordArr[i] == _wordToGuess[i])
            {
                correct++;
                _activeRow[i].SetState(letterState.Correct);
                _keyboard.SetLettersState(_currentGuess[i], letterState.Correct);
                wordArr[i] = ' ';
            }
        }
        if (correct == _currentGuess.Length) { WinTf.gameObject.SetActive(true); return; }

        for (int i = 0; i < wordArr.Length; i++)
        {
            if (wordArr[i] == ' ') continue;
            if (_wordToGuess.Contains(wordArr[i]))
            {
                _activeRow[i].SetState(letterState.WrongPlace);
                _keyboard.SetLettersState(wordArr[i], letterState.WrongPlace);
            }
            else
            {
                _activeRow[i].SetState(letterState.Wrong);
                _keyboard.SetLettersState(wordArr[i], letterState.Wrong);
            }
        }
        if (++_row >= _rowCount)
        {
            LosTf.gameObject.SetActive(true);
            LosTf.GetChild(1).GetComponentInChildren<TMPro.TMP_Text>().text = "The word was: " + _wordToGuess;
            return;
        }
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
    public IEnumerator Popup(string message)
    {
        PopTf.GetComponentInChildren<TMPro.TMP_Text>().text = message;
        PopTf.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        PopTf.gameObject.SetActive(false);
    }
    public void Restart()
    {
        ClearAll();
        StartGame();
    }

}
