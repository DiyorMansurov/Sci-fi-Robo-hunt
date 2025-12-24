using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                var hitObject = hit.collider.gameObject;
                
                if (hitObject.CompareTag("Cube"))
                {
                    Debug.Log("Cube clicked!");
                    hitObject.GetComponent<Renderer>().material.color = Random.ColorHSV();
                } else if (hitObject.CompareTag("Sphere"))
                {
                    Debug.Log("Sphere clicked!");
                } else if (hitObject.CompareTag("Capsule"))
                {
                    Debug.Log("Capsule clicked!");
                    hitObject.GetComponent<Renderer>().material.color = Color.black;
                }
            }
        }
    }
}
