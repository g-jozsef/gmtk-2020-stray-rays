using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private BoolVariable _paused;
    [SerializeField] private Counter _counter;
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private Button _resetButton;

    public void PauseClick(bool p)
    {
        if (_counter.Cnt > 0)
        {
            return;
        }

        if (_paused.Value)
        {
            _pauseScreen.gameObject.SetActive(false);
            _counter.SetCounter(4);
            DOTween.To(() => _counter.Cnt, x => _counter.Cnt = x, 0, 4f).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() =>
            {
                Time.timeScale = 1;
                _resetButton.interactable = true;
                _paused.Value = false;
            });
        }
        else
        {
            if (p)
                _pauseScreen.gameObject.SetActive(true);
            _resetButton.interactable = false;
            _counter.SetBlur(true);
            Time.timeScale = 0;
            _paused.Value = true;
        }
    }

}
