using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class LightSpawner : MonoBehaviour
{
    [SerializeField] private LightMovement _lightParticle;
    [SerializeField] private Vector2 _spawnRandom;
    [SerializeField] private Collider2D _bounds;

    private Timer _timer;

    private void Start()
    {
        Assert.IsTrue(_spawnRandom.x < _spawnRandom.y, "Spawn random min should be less than spawn random max");
        _timer = new Timer(0);
        _timer.Elapsed += _timer_Elapsed;
        _timer.Start();
    }

    private void _timer_Elapsed(object sender, System.EventArgs e)
    {
        var light = GameObject.Instantiate<LightMovement>(_lightParticle);
        var rx = Random.Range(_bounds.bounds.min.x, _bounds.bounds.max.x);
        var ry = Random.Range(_bounds.bounds.min.y, _bounds.bounds.max.y);
        light.transform.position = new Vector3(rx, ry, 0);
        light.Direction = Random.insideUnitCircle.normalized;
        if(Vector3.Dot(light.Direction, light.transform.position) > 0.3)
        {
            light.Direction *= -1;
        }
        _timer.RecalibrateTimer(Random.Range(_spawnRandom.x, _spawnRandom.y));
        _timer.Start();
    }

    private void Update()
    {
        _timer.Update(Time.deltaTime);
    }
}
