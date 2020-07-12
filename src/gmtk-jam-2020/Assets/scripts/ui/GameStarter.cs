using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private BoolVariable _paused;
    [SerializeField] private string _sceneToLoad;
    [SerializeField] private CanvasGroup _fader;

    public void StartGame()
    {
        DOTween.To(() => _fader.alpha, x => _fader.alpha = x, 1, 0.4f).SetUpdate(true).OnComplete(() =>
        {
            _paused.Value = false;
            Time.timeScale = 1;
            SceneManager.LoadScene(_sceneToLoad);
        });
    }

}

