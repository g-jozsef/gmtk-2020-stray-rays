using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private LightSpawner _spawner;
    [SerializeField] private int _startCounter = 3;
    [SerializeField] private Counter _counter;
    [SerializeField] private LongVariable _capturedCurrent;
    [SerializeField] private LongVariable _escapedCount;
    [SerializeField] private LongVariable _capturedMax;
    [SerializeField] private TMP_Text _stageTxt;

    private int _currentStage = 0;

    private void Start()
    {
        StartSettingNextStage(1);
    }

    public void Escape()
    {
        _capturedMax.Value += 1;
        _spawner.IncSpawnCount(2);
    }

    private void StartSettingNextStage(int stage)
    {
        SetStage(stage);
        _capturedCurrent.Value = 0;
        _capturedMax.Value = stage;
        _escapedCount.Value = 0;
        _counter.SetText(_stageTxt.text);
        _counter.SetCounter(2);
        DOTween.To(() => _counter.Cnt, x => _counter.Cnt = x, 0, 3f).OnComplete(() =>
        {
            _spawner.StartSpawning(_capturedMax.Value);
        });

    }

    public void Update()
    {
        if (_capturedCurrent.Value == _capturedMax.Value)
        {
            Debug.Log("Stage cleared, moving to next stage!");
            StartSettingNextStage(_currentStage + 1);
        }
    }

    private void SetStage(int stage)
    {
        _currentStage = stage;
        _stageTxt.text = $"STAGE {ToRoman(stage)}";
    }

    public static string ToRoman(int number)
    {
        if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
        if (number < 1) return string.Empty;
        if (number >= 1000) return "M" + ToRoman(number - 1000);
        if (number >= 900) return "CM" + ToRoman(number - 900);
        if (number >= 500) return "D" + ToRoman(number - 500);
        if (number >= 400) return "CD" + ToRoman(number - 400);
        if (number >= 100) return "C" + ToRoman(number - 100);
        if (number >= 90) return "XC" + ToRoman(number - 90);
        if (number >= 50) return "L" + ToRoman(number - 50);
        if (number >= 40) return "XL" + ToRoman(number - 40);
        if (number >= 10) return "X" + ToRoman(number - 10);
        if (number >= 9) return "IX" + ToRoman(number - 9);
        if (number >= 5) return "V" + ToRoman(number - 5);
        if (number >= 4) return "IV" + ToRoman(number - 4);
        if (number >= 1) return "I" + ToRoman(number - 1);
        throw new ArgumentOutOfRangeException("something bad happened");
    }
}
