using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int _ammoAmount = 0;
    private int _enemiesKilled = 0;
    private int _scoreAmount = 0;
    [SerializeField] private TMP_Text  _ammoText;
    [SerializeField] private TMP_Text  _enemiesText;
    [SerializeField] private TMP_Text  _scoreText;

    [SerializeField] private TMP_Text  _timerText;

    [SerializeField] private TMP_Text  _endTitleText;
    [SerializeField] private TMP_Text  _endEnemiesText;
    [SerializeField] private TMP_Text  _endScoreText;
    [SerializeField] private TMP_Text  _endTimeText;
    [SerializeField] private TMP_Text  _reasonText;

    [SerializeField] private Image _sadImg;
    [SerializeField] private Image _happyImg;

    private int Minutes;
    private int Seconds;


    public static UIManager Instance;
    private void Awake() {
        Instance = this;
    }

    private void Update() {
        _ammoText.text = _ammoAmount.ToString();
        _enemiesText.text = _enemiesKilled.ToString();
        _scoreText.text = _scoreAmount.ToString();
    }

    public void UpdateAmmo(int amount)
    {
        _ammoAmount = amount;
    }
    public void UpdateEnemies(int amount)
    {
        _enemiesKilled = amount;
    }
    public void UpdateScore(int amount)
    {
        _scoreAmount = amount;
    }

    public void UpdateTimer(float _timeRemaining)
    {
        Minutes = Mathf.FloorToInt(_timeRemaining / 60f);
        Seconds = Mathf.FloorToInt(_timeRemaining % 60f);

        _timerText.text = $"{Minutes}:{Seconds:D2}";
    }

    public void EndingUI(bool IsWin, string Reason)
    {   
        _endEnemiesText.text = $"Enemies killed: {_enemiesKilled}";
        _endScoreText.text = $"Score: {_scoreAmount}";
        _endTimeText.text = $"Time Left: {Minutes}:{Seconds:D2}";
        _reasonText.text = Reason;
        if (IsWin)
        {
            _sadImg.gameObject.SetActive(false);
            _happyImg.gameObject.SetActive(true);

            _endTitleText.text = "You won";
            _endTitleText.color = Color.green;
            _reasonText.color = Color.green;
        }
    }
    

}
