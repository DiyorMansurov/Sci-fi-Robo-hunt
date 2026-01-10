using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] private int _barrierHP = 5;
    private Renderer _barrierRenderer;
    private Collider _barrierCollider;
    private Coroutine _rechargeRoutine;

    private Color32 Green  = new Color32(95, 255, 0, 255);
    private Color32 Lime   = new Color32(165, 255, 0, 255);
    private Color32 Yellow = new Color32(254, 255, 0, 255);
    private Color32 Orange = new Color32(255, 157, 0, 255);
    private Color32 Red    = new Color32(255, 20, 0, 255);

    
    private void Start() {
        _barrierRenderer = this.gameObject.GetComponent<Renderer>();
        _barrierCollider = this.gameObject.GetComponent<Collider>();
    }

    private void Update() {
  
    }

    public void TakeDamage()
    {
        _barrierHP -= 1;
        ColorChange();
        TryStartRecharge();
    }

    private void BarrierColor(Color _color)
    {
        _barrierRenderer.material.SetColor("_Color", _color);
    }

    private void TurnOff()
    {
        _barrierRenderer.enabled = false;
        _barrierCollider.enabled = false;
    }

    private void TurnOn()
    {
        _barrierRenderer.enabled = true;
        _barrierCollider.enabled = true;
    }

    private void ColorChange()
    {
        switch (_barrierHP)
        {
            case 5: BarrierColor(Green);break;
            case 4: BarrierColor(Lime);break;
            case 3: BarrierColor(Yellow);break;
            case 2: BarrierColor(Orange);break;
            case 1: BarrierColor(Red);break; 
            case 0: TurnOff(); break; 
            
            default: break;
        }
    }

    private void TryStartRecharge()
    {
        if (_barrierHP < 5 && _rechargeRoutine == null)
            _rechargeRoutine = StartCoroutine(Recharge());
    }

    private IEnumerator Recharge()
    {
        while (_barrierHP < 5)
        {
            yield return new WaitForSeconds(6f);
            _barrierHP++;
            if (_barrierHP > 1)
            {
                TurnOn();
            }
            
            ColorChange();
        }

        _rechargeRoutine = null;
        
    }
}
