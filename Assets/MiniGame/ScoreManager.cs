using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    [SerializeField] private int score;
    [SerializeField] private int combo;
    [SerializeField] private int pointAdd;

    [SerializeField] private TextMeshPro textScore;

    [SerializeField] private TextMeshPro textCombo;

    private int HighestCombo;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        textScore.text = score.ToString();
        textCombo.text = combo.ToString();
    }
    public void addScore()
    {
        combo++;
        score = pointAdd * combo;
        
    }
    public void resetCombo()
    {
        HighestCombo = combo;
        combo = 1;
    }
    public int getScore()
    {
        return score;
    }
    public int getCombo()
    {
        return HighestCombo;
    }

}
