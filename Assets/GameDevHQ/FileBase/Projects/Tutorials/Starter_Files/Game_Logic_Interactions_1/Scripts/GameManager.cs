using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public float _timeRemaining = 120f;
    
    private void Update() {
        TimerCount();
    }
    private void TimerCount()
    {
        if (_timeRemaining <= 0f) return;
        
        if (_timeRemaining > 0f)
        {
            _timeRemaining -= Time.deltaTime;
            _timeRemaining = Mathf.Max(0f, _timeRemaining);

            UIManager.Instance.UpdateTimer(_timeRemaining);

            if (_timeRemaining <=0f)
            {
                TimeIsUp();
            }
        }
    }

    private void TimeIsUp()
    {
        Debug.Log("Lose");
    }
}
