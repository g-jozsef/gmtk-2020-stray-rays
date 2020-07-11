using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _direction;
    public Vector3 Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    void Update()
    {
        this.transform.position += _direction * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("AAAAAAAAAAA");
        var tr = collision.transform.parent.transform;
        _direction = Vector3.Reflect(_direction, tr.up).normalized;
    }
}
