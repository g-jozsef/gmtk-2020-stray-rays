using DG.Tweening;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private LightSpawner _spawner;
    [SerializeField] private int _startCounter = 3;
    [SerializeField] private Counter _counter;


    private void Start()
    {
        _counter.SetCounter(4);
        DOTween.To(() => _counter.Cnt, x => _counter.Cnt = x, 0, 6f).OnComplete(() =>
         {
             _spawner.StartSpawning(0);
         });
    }


}
