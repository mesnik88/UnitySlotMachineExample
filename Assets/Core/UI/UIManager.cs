using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : PersistentSingleton<UIManager>
{
    [SerializeField] private Button FillBtn;
    [SerializeField] private Text ScoreText;
    private int Score;
    private void Start()
    {
        FillBtn.onClick.AddListener(GameController.Instance.Spin);
        GameController.Instance._OnEndMath += UpdateScore;
        UpdateScore(0);
    }
    public void EnableFillButton(bool act)
    {
        FillBtn.interactable = act;
    }
    private void UpdateScore(int score)
    {
        Score += score;
        ScoreText.text = "Score : " + Score;
    }
}
