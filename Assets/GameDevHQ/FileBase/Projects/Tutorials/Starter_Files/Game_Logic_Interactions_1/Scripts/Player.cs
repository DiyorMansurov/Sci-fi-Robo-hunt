using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class Player : MonoBehaviour
{
    private PlayerInputActions _input;
    [SerializeField] private CinemachineVirtualCamera  _playerVirtualCamera;
    [SerializeField] private CinemachineImpulseSource _impulse;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _backMusic;
    [SerializeField] private AudioClip _shoot_SFX;
    [SerializeField] private AudioClip _lose_SFX;
    [SerializeField] private AudioClip _win_SFX;

    [SerializeField] private Camera _playerCamera;
    [SerializeField] private LayerMask _maskToHit;

    [SerializeField] private float _normalFOV = 55f;
    [SerializeField] private float _aimFOV = 40f;
    [SerializeField] private float _zoomSpeed = 10f;
    private bool IsEnded = false;

  
    private bool isAiming;

    private int _scoreAmount = 0;
    [SerializeField] private int _ammoAmount = 10;
    void Awake()
    {
        _input = new PlayerInputActions();
        _input.Player.Enable();

    }

    void OnEnable()
    {
        _input.Player.Shoot.performed += Shoot_performed;
        _input.Player.Zoom.started += Zoom;
        _input.Player.Zoom.canceled += Zoom;
    }

    void OnDisable()
    {
        _input.Player.Shoot.performed -= Shoot_performed;
        _input.Player.Zoom.started -= Zoom;
        _input.Player.Zoom.canceled -= Zoom;
    }

    private void Start() {
        UIManager.Instance.UpdateAmmo(_ammoAmount);

    }


    public void StopMusic() => _backMusic.Stop();
    public void NormalizePitch() => _audioSource.pitch = 1;
    public void PlayShoot() => _audioSource.PlayOneShot(_shoot_SFX);
    public void PlayLose() => _audioSource.PlayOneShot(_lose_SFX);
    public void PlayWin() => _audioSource.PlayOneShot(_win_SFX);

    private void Zoom(InputAction.CallbackContext context)
    {
        isAiming = context.ReadValueAsButton();
 
    }

    private void ZoomAim()
    {
        float targetFOV;

        if (isAiming)
        {
            _impulse.m_ImpulseDefinition.m_AmplitudeGain = 0.1f;
            targetFOV = _aimFOV;
        }
        else
        {
            _impulse.m_ImpulseDefinition.m_AmplitudeGain = 0.3f;
            targetFOV = _normalFOV;
        }

        _playerVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(
            _playerVirtualCamera.m_Lens.FieldOfView,
            targetFOV,
            Time.deltaTime * _zoomSpeed
        );

    }

    private void AddAmmo()
    {
        _ammoAmount += 1;
    }

    public void IsEndedActivate()
    {
        IsEnded = true;
    }

    public void UpdateEnemyStats()
    {
        _scoreAmount += 50;
        UIManager.Instance.UpdateScore(_scoreAmount);
        UIManager.Instance.UpdateEnemiesKilled();
    }

    private void Shoot_performed(InputAction.CallbackContext context)
    {
        if (IsEnded) return;
        if (Time.timeScale == 0f) return;

        if (_ammoAmount <= 0)
        {
            GameManager.Instance.Ending("Shoot more precisely, you are out of ammo", false);
        };

        _impulse.GenerateImpulse(); 
        PlayShoot();

        _ammoAmount -= 1;

        UIManager.Instance.UpdateAmmo(_ammoAmount);

       Ray ray = _playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
       RaycastHit hit;

       if (Physics.Raycast(ray, out hit,  Mathf.Infinity, _maskToHit))
        {
                var hitObject = hit.collider.gameObject;

                if (hitObject.CompareTag("Enemy"))
                {
                    hit.collider.GetComponent<Enemy>().EnemyHit();
                    hitObject.tag = "Untagged";

                    AddAmmo();
                } else if (hitObject.CompareTag("Barrier"))
                {
                    hit.collider.GetComponent<Barrier>().TakeDamage();
                    AddAmmo();
                    _scoreAmount += 5;
                    UIManager.Instance.UpdateScore(_scoreAmount);
                } else if (hitObject.CompareTag("Barrel"))
                {
                    hit.collider.GetComponent<ExplosiveBarrel>().TakeDamage();
                    _scoreAmount += 25;
                    UIManager.Instance.UpdateScore(_scoreAmount);
                    AddAmmo();
                }
        }                 

    }

    
    void Update()
    {
        ZoomAim();
    }
}
