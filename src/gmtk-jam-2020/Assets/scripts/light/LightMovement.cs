using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class LightMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _radius;
    [SerializeField] private Vector2 _direction;
    [SerializeField] private float _speedIncrease = 1.1f;

    public Vector3 Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    void Update()
    {

        var hit = Physics2D.Raycast(this.transform.position, _direction, _speed * Time.deltaTime);
        if (hit.collider != null && hit.distance < _radius)
        {
            _direction = Vector2.Reflect(_direction, hit.normal).normalized;
            _speed *= _speedIncrease;
        }
        else if(hit.collider != null)
        {
            this.transform.position = hit.point + (-_direction * _radius);
        }
        else
        {
            var delta = _direction * _speed * Time.deltaTime;
            this.transform.position += new Vector3(delta.x, delta.y, 0);
        }
    }
}
