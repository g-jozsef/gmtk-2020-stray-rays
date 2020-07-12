using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{

    [SerializeField] private SceneAsset _sceneToLoad;
    [SerializeField] private CanvasGroup _fader;

    public void StartGame()
    {
        DOTween.To(() => _fader.alpha, x => _fader.alpha = x, 1, 0.4f).OnComplete(() =>
        {
            SceneManager.LoadScene(_sceneToLoad.name);
        });
    }

}

