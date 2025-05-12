using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HJ_TestCollision : MonoBehaviour
{
	void Start()
    {
        
    }

	void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float dist = 100f;

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100f))
            {                
                Debug.Log($"Raycast Hit ! : {hit.collider.gameObject.name}");
                dist = hit.distance;
            }

            Debug.DrawRay(ray.origin, ray.direction * dist, Color.red, 2f);
        }
    }
}
