using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private BoolVariable _paused;
    [SerializeField] private Counter _counter;

    public void PauseClick()
    {
        if (_counter.Cnt > 0)
        {
            return;
        }

        if (_paused.Value)
        {
            _counter.SetCounter(4);
            DOTween.To(() => _counter.Cnt, x => _counter.Cnt = x, 0, 4f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                Time.timeScale = 1;
                _paused.Value = false;
            });
        }
        else
        {
            _counter.SetBlur(true);
            Time.timeScale = 0;
            _paused.Value = true;
        }
    }

}
