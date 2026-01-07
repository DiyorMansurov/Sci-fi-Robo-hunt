using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{   
    private PlayerInputActions _input;
    [SerializeField] private float _timeRemaining = 120f;
    public static GameManager Instance;
    [SerializeField] private GameObject _player;
    private Player _playerScript;

    [SerializeField] private PlayableDirector _loseDirector;

    
    private bool _isEnded = false;


    private void Awake() {
        Instance = this;

        _input = new PlayerInputActions();
        _input.Player.Enable();
        
    }

    void OnEnable()
    {
        _input.Player.Restart.performed += Restart_performed;
    }

    void OnDisable()
    {
        _input.Player.Restart.performed -= Restart_performed;
    }

    private void Restart_performed(InputAction.CallbackContext context)
    {
        if (!_isEnded) return;

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Time.timeScale = 1f;
    }
    private void Start() {
        _playerScript = _player.GetComponent<Player>();
    }
    
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
                Ending("Good job bro... or sis idk", true);
            }
        }
    }

    public void Ending(string message, bool IsWin)
    {
        if (_isEnded) return;
        _isEnded = true;
        _loseDirector.Play();
        _playerScript.StopMusic();
        _playerScript.NormalizePitch();
        Time.timeScale = 0.3f;
        UIManager.Instance.EndingUI(IsWin, message);

        SpawnManager.Instance.IsEndedActivate();
        _playerScript.IsEndedActivate();

        if (IsWin)
        {
            _playerScript.PlayWin();
        } else
        {
            _playerScript.PlayLose();
        }
        

    }
}
