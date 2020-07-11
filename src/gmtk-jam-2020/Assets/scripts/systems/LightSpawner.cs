using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class LightSpawner : MonoBehaviour
{
    [SerializeField] private LightMovement _lightParticle;
    [SerializeField] private Vector2 _spawnRandom;

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
        light.Direction = Random.insideUnitCircle.normalized;
        _timer.RecalibrateTimer(Random.Range(_spawnRandom.x, _spawnRandom.y));
        _timer.Start();
    }

    private void Update()
    {
        _timer.Update(Time.deltaTime);
    }
}
