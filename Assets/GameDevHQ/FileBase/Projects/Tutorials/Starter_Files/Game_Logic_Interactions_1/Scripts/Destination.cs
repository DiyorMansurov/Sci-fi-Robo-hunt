using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    private int _enemiesSkipped;

    private void OnTriggerEnter(Collider other) {
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(other.gameObject);
            _enemiesSkipped += 1;
            UIManager.Instance.UpdateEnemies(_enemiesSkipped);

            if (_enemiesSkipped >= 1)
            {
                GameManager.Instance.Ending("Too much enemies went past you", false);
            }
        }
    }
}
