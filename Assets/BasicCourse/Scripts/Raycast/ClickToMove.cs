using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMove : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 movePosition;
    void Start()
    {
        movePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MovePositionCalculate();
        }

        MoveObject();
    }

    private void MoveObject()
    {
        if(transform.position != movePosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePosition, 35f * Time.deltaTime);
        }
        
    }
    private void MovePositionCalculate()
    {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Cube"))
                {
                   movePosition = new Vector3(hit.point.x, transform.position.y, hit.point.z); 
                }
                
            }
    }
}
