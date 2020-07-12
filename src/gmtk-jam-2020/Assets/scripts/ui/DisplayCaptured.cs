using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayCaptured : MonoBehaviour
{
    [SerializeField] private LongVariable _captureCurrent;
    [SerializeField] private LongVariable _captureMax;

    [SerializeField] private TMP_Text _text;

    private void Update()
    {
        _text.text = $"{_captureCurrent.Value}/{_captureMax.Value}";
    }

}
