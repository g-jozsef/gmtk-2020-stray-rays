using System;
using UnityEngine;
using UnityEngine.Assertions;

public class Mirror : MonoBehaviour, IRaycastCollision
{
    [SerializeField] private EdgeCollider2D _collider;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private int _lives = 5;

    void IRaycastCollision.OnCollision(LightMovement lightMovement)
    {
        _lives -= 1;
        if (_lives == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetSize(float x)
    {
        _renderer.size = new Vector2(x, _renderer.size.y);
        _collider.points = new Vector2[] {
            new Vector2(-x / 2, 0),
            new Vector2(+x / 2, 0)
        };
        Debug.Log("Setting size: " + x);
    }
}
