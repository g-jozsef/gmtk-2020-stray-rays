using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private BoolVariable _paused;

    public void PauseClick()
    {
        _paused.Value = !_paused.Value;
        if(_paused.Value)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

}
