using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayInstantiate : MonoBehaviour
{
    [SerializeField]
    private GameObject spherePrefab;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                var hitObject = hit.collider.gameObject;

                if (hitObject.CompareTag("Cube"))
                {
                    Vector3 spawnPos = hit.point + hit.normal * 0.5f;
                    Instantiate(spherePrefab, spawnPos, Quaternion.identity);
                }
            }
        }
    }
}
