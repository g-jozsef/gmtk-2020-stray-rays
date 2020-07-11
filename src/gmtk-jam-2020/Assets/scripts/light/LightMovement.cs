using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class LightMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _radius;
    [SerializeField] private Vector2 _direction;
    [SerializeField] private float _speedIncrease = 0.5f;
    [SerializeField] private IntVariable _score;
    [SerializeField] private LayerMask _collisionMask;
    [SerializeField] private LayerMask _edgeMask;
    [SerializeField] private TrailRenderer _trail;

    public Vector3 Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    void Update()
    {
        var edgeHit = Physics2D.Raycast(this.transform.position, _direction, _speed * Time.deltaTime, _edgeMask);
        if(edgeHit.collider != null)
        {
            var coll = edgeHit.collider.GetComponent<IRaycastCollision>();
            coll?.OnCollision(this);
            DoMove();
            return;
        }

        var hit = Physics2D.Raycast(this.transform.position, _direction, _speed * Time.deltaTime, _collisionMask);
        if (hit.collider != null)
        {
            this.transform.position = hit.point + (-_direction * _radius);
            _direction = Vector2.Reflect(_direction, hit.normal).normalized;
            _speed += _speedIncrease;
            _score.Value += 1;
        }
        else
        {
            DoMove();
        }
    }

    private void DoMove()
    {
        var delta = _direction * _speed * Time.deltaTime;
        this.transform.position += new Vector3(delta.x, delta.y, 0);
    }

    public void ResetTrail()
    {
        _trail.Clear();
    }
}
