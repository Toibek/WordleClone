using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sorting : MonoBehaviour
{
    [SerializeField] int _amountOfValues;
    [SerializeField] float delayBetweenLoops;
    [SerializeField] SortType sortType;

    List<int> values = new List<int>();
    List<RectTransform> rects = new List<RectTransform>();
    Coroutine sortRoutine;

    void Start()
    {
        rects.Add(transform.GetChild(0).GetComponent<RectTransform>());
        Setup();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) Setup();
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopCoroutine(sortRoutine);
            sortRoutine = null;
        }
    }
    void Setup()
    {
        if (sortRoutine != null) return;
        values = new();
        for (int i = rects.Count - 1; i >= 1; i--)
        {
            Destroy(rects[i].gameObject);
            rects.RemoveAt(i);
        }
        for (int i = 0; i < _amountOfValues; i++)
        {
            if (rects.Count <= i)
            {
                GameObject go = Instantiate(rects[i - 1].gameObject, transform);
                rects.Add(go.GetComponent<RectTransform>());
            }
            if (values.Count <= i)
                values.Add(i);
        }
        values = shuffleList(values);
        UpdateVisuals();
        switch (sortType)
        {
            case SortType.bubble:
                sortRoutine = StartCoroutine(BubbleSort());
                break;
            case SortType.selection:
                sortRoutine = StartCoroutine(LinearSort());
                break;
            case SortType.bogo:
                sortRoutine = StartCoroutine(Pogo());
                break;
            default:
                break;
        }
    }
    List<int> shuffleList(List<int> input)
    {
        List<int> output = new List<int>();
        while (input.Count > 0)
        {
            int i = Random.Range(0, input.Count);
            output.Add(input[i]);
            input.RemoveAt(i);
        }
        return output;
    }
    bool Sorted(List<int> input)
    {
        for (int i = 1; i < values.Count; i++)
        {
            if (values[i] < values[i - 1])
            {
                return false;
            }
        }
        return true;
    }
    void UpdateVisuals()
    {
        for (int i = 0; i < rects.Count; i++)
        {
            rects[i].localScale = new Vector2(1, (float)values[i] / (float)_amountOfValues);
        }
    }
    IEnumerator BubbleSort()
    {
        while (!Sorted(values))
        {
            for (int i = 0; i < values.Count - 1; i++)
            {
                int val1 = values[i];
                int val2 = values[i + 1];

                if (val2 >= val1) continue;

                values[i] = Mathf.Min(val1, val2);
                values[i + 1] = Mathf.Max(val1, val2);

                UpdateVisuals();
            }
            yield return new WaitForSeconds(delayBetweenLoops);
        }
        Debug.Log("Done!");
        sortRoutine = null;
    }
    IEnumerator LinearSort()
    {
        int max = _amountOfValues + 2;
        int currentCheck = 0;
        while (!Sorted(values))
        {
            int min = max;
            int minI = 0;
            for (int i = currentCheck; i < values.Count; i++)
            {
                if (values[i] < min)
                {
                    min = values[i];
                    minI = i;
                }
            }
            int cur = values[currentCheck];
            values[currentCheck] = values[minI];
            values[minI] = cur;
            currentCheck++;
            UpdateVisuals();
            yield return new WaitForSeconds(delayBetweenLoops);
        }
        Debug.Log("Done!");
        sortRoutine = null;
    }
    IEnumerator Pogo()
    {
        bool sorted = false;
        while (sorted == false)
        {
            values = shuffleList(values);
            sorted = Sorted(values);
            UpdateVisuals();
            yield return new WaitForSeconds(delayBetweenLoops);
        }
        Debug.Log("Done!");
        sortRoutine = null;
    }
    public enum SortType { bubble, selection, bogo }
}
