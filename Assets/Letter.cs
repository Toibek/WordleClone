using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    [Tooltip("0: Default, 1: WrongPlace, 2:Correct")]
    [SerializeField] private Color[] colors;

    public enum letterState { Default, WrongPlace, Correct }
}
