using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject bulletHolePrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {   
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 15f))
            {
                Debug.Log("Hit something");
                var hitObject = hit.collider.gameObject;

                Instantiate(bulletHolePrefab, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 forward = Camera.main.transform.forward * 15;
        Gizmos.DrawRay(Camera.main.transform.position, forward);
    }
}
