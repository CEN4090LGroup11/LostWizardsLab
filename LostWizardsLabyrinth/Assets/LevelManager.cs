using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI strokeUI; 
    [Space(10)]
    [SerializeField] private GameObject levelCompletedUI;
    [Space(10)]
    [SerializeField] private GameObject levelFailedUI;

    [Header("Attributes")]
    [SerializeField] private int maxStrokes; 
    private int strokes; 
    [HideInInspector] public bool outOfStrokes; 
    [HideInInspector] public bool levelCompleted; 

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        UpdateStrokeUI();
    }

    public void IncreaseStroke()
    {
        strokes++;
        UpdateStrokeUI();

        if (strokes >= maxStrokes)
        {
            outOfStrokes = true;
        }
    }

    private void UpdateStrokeUI()
    {
        strokeUI.text = strokes + "/" + maxStrokes;
    }

    public void LevelComplete()
    {
        levelCompleted = true;
        levelCompletedUI.SetActive(true);
    }

    public void LevelFailed()
    {
        levelFailedUI.SetActive(true);
    }
}
