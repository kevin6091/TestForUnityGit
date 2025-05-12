using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    public float MoveSpeed
    {
        get { return _moveSpeed; }
    }

    [SerializeField] float _rotateSpeed;
    public float RotateSpeed
    {
        get { return _rotateSpeed; }
    }

    void Start()
    {
        Managers.Input.MoveKeyAction -= OnMoveKey;
        Managers.Input.MoveKeyAction += OnMoveKey;
    }

    void Update()
    {
        StartCoroutine("asdf");
    }

    void OnMoveKey(bool w, bool a, bool s, bool d)
    {
        if (true == w)
        {
            transform.position += (Vector3.forward * Time.deltaTime * _moveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), Time.deltaTime * _rotateSpeed);
        }
        else if (true == s)
        {
            transform.position += (Vector3.back * Time.deltaTime * _moveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), Time.deltaTime * _rotateSpeed);
        }

        if (true == a)
        {
            transform.position += (Vector3.left * Time.deltaTime * _moveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), Time.deltaTime * _rotateSpeed);
        }
        else if (true == d)
        {
            transform.position += (Vector3.right * Time.deltaTime * _moveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), Time.deltaTime * _rotateSpeed);
        }
    }
}
