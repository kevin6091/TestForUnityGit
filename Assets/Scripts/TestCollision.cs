using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    bool _bIsClickMove = false;
    Vector3 _clickMovePosition;
    Vector3 _clickMoveDirection;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 3.0f);

            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo, 100.0f))
            {
                _clickMoveDirection = hitInfo.point - transform.position;
                _clickMovePosition = hitInfo.point;
                _bIsClickMove = true;
            }
        }

        if(true == _bIsClickMove)
        {
            PlayerController playerController = gameObject.GetComponent<PlayerController>();
            transform.position += _clickMoveDirection.normalized * Time.deltaTime * playerController.MoveSpeed;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_clickMoveDirection), Time.deltaTime * playerController.RotateSpeed);

            if ((transform.position - _clickMovePosition).magnitude < 0.1f)
            {
                _bIsClickMove = false;
            }
        }
    }
}
