using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private int _ammoAmount = 0;
    private int _enemiesLeft = 0;
    private int _scoreAmount = 0;
    [SerializeField] private TMP_Text  _ammoText;
    [SerializeField] private TMP_Text  _enemiesText;
    [SerializeField] private TMP_Text  _scoreText;

    [SerializeField] private TMP_Text  _timerText;

    public static UIManager Instance;
    private void Awake() {
        Instance = this;
    }

    private void Update() {
        _ammoText.text = _ammoAmount.ToString();
        _enemiesText.text = _enemiesLeft.ToString();
        _scoreText.text = _scoreAmount.ToString();
    }

    public void UpdateAmmo(int amount)
    {
        _ammoAmount = amount;
    }
    public void UpdateEnemies(int amount)
    {
        _enemiesLeft = amount;
    }
    public void UpdateScore(int amount)
    {
        _scoreAmount = amount;
    }

    public void UpdateTimer(float _timeRemaining)
    {
        int Minutes = Mathf.FloorToInt(_timeRemaining / 60f);
        int Seconds = Mathf.FloorToInt(_timeRemaining % 60f);

        _timerText.text = $"{Minutes}:{Seconds:D2}";
    }
    

}
