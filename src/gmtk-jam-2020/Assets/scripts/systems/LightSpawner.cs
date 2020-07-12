using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class LightSpawner : MonoBehaviour
{
    private List<LightMovement> _pool;

    [SerializeField] private LightMovement _lightParticle;
    [SerializeField] private Vector2 _spawnRandom;
    [SerializeField] private Collider2D _bounds;
    [SerializeField] private long _spawnCount = 0;

    private Timer _timer;

    public List<LightMovement> Pool { get { return _pool; } }

    private void Awake()
    {
        Assert.IsTrue(_spawnRandom.x < _spawnRandom.y, "Spawn random min should be less than spawn random max");
        _pool = new List<LightMovement>();
        _timer = new Timer(0);
        _timer.Elapsed += _timer_Elapsed;
        if (_spawnCount > 0)
        {
            StartSpawning(_spawnCount);
        }
    }

    public void IncSpawnCount(long i)
    {
        if (_spawnCount == 0)
        {
            StartSpawning(i);
        }
        else
        {
            _spawnCount += i;
        }
        
    }
    public void StartSpawning(long count)
    {
        _spawnCount = count;
        _timer.Start();
    }

    private void _timer_Elapsed(object sender, System.EventArgs e)
    {
        _spawnCount--;
        var existingLight = _pool.FirstOrDefault(x => !x.gameObject.activeSelf);
        var light = (existingLight != null) ? existingLight : GameObject.Instantiate<LightMovement>(_lightParticle);
        if (existingLight == null)
        {
            _pool.Add(light);
        }
        light.gameObject.SetActive(true);
        var rx = Random.Range(_bounds.bounds.min.x, _bounds.bounds.max.x);
        var ry = Random.Range(_bounds.bounds.min.y, _bounds.bounds.max.y);
        light.transform.position = new Vector3(rx, ry, 0);
        light.Direction = Random.insideUnitCircle.normalized;
        if (Vector3.Dot(light.Direction, light.transform.position) > 0.3)
        {
            light.Direction *= -1;
        }
        light.ResetStats();

        if (_spawnCount > 0)
        {
            _timer.RecalibrateTimer(Random.Range(_spawnRandom.x, _spawnRandom.y));
            _timer.Start();
        }
    }

    private void Update()
    {
        if (_pool.TrueForAll(x => !x.gameObject.activeSelf))
        {
            _timer.Stop();
        }
        else
        {
            _timer.Update(Time.deltaTime);
        }
    }
}
