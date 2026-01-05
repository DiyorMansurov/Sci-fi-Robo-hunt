using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputActions _input;
    [SerializeField] private Camera _playerCamera;

    private int _scoreAmount = 0;
    private int _ammoAmount = 25;
    void Awake()
    {
        _input = new PlayerInputActions();
        _input.Player.Enable();
        _input.Player.Shoot.performed += Shoot_performed;

        UIManager.Instance.UpdateAmmo(_ammoAmount);

    }

    private void Shoot_performed(InputAction.CallbackContext context)
    {
        if (_ammoAmount <= 0) return;

        _ammoAmount -= 1;
        UIManager.Instance.UpdateAmmo(_ammoAmount);

       Ray ray = _playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
       RaycastHit hit;

       if (Physics.Raycast(ray, out hit,  Mathf.Infinity))
        {
                var hitObject = hit.collider.gameObject;
                Debug.Log(hitObject.name);
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    hit.collider.GetComponent<Enemy>().EnemyHit();
                    
                    _scoreAmount += 50;
                    UIManager.Instance.UpdateScore(_scoreAmount);
                }
        }                 

    }

    
    void Update()
    {

    }
}
