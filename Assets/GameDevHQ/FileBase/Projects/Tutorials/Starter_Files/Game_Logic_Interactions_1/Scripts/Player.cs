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

    [SerializeField] private AudioSource _shoot_SFX;

    [SerializeField] private Camera _playerCamera;
    [SerializeField] private LayerMask _maskToHit;

    [SerializeField] private float _normalFOV = 55f;
    [SerializeField] private float _aimFOV = 40f;
    [SerializeField] private float _zoomSpeed = 10f;
       private bool isAiming;

    private int _scoreAmount = 0;
    [SerializeField] private int _ammoAmount = 10;
    void Awake()
    {
        _input = new PlayerInputActions();
        _input.Player.Enable();
        _input.Player.Shoot.performed += Shoot_performed;

        _input.Player.Zoom.started += Zoom;
        _input.Player.Zoom.canceled += Zoom;
    }

    private void Start() {
        UIManager.Instance.UpdateAmmo(_ammoAmount);

    }

    private void Zoom(InputAction.CallbackContext context)
    {
        isAiming = context.ReadValueAsButton();
        Debug.Log("called");
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

    private void Shoot_performed(InputAction.CallbackContext context)
    {
        if (_ammoAmount <= 0) return;

        _impulse.GenerateImpulse(); 
        _shoot_SFX.Play();

        _ammoAmount -= 1;
        UIManager.Instance.UpdateAmmo(_ammoAmount);

       Ray ray = _playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
       RaycastHit hit;

       if (Physics.Raycast(ray, out hit,  Mathf.Infinity, _maskToHit))
        {
                var hitObject = hit.collider.gameObject;
                Debug.Log(hitObject.name);
                if (hitObject.CompareTag("Enemy"))
                {
                    hit.collider.GetComponent<Enemy>().EnemyHit();
                    hitObject.tag = "Untagged";
                    
                    _scoreAmount += 50;
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
