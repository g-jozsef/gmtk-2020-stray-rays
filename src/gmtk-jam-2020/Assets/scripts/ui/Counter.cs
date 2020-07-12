using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private GameObject _blurObj;
    [SerializeField] private TMP_Text _text;
    private int _cnt;

    public int Cnt { get { return _cnt; } set { SetCounter(value); } }

    public void SetBlur(bool val)
    {
        _blurObj.SetActive(val);
    }
    public void SetCounter(int val)
    {
        _cnt = val;
        if (val > 0)
        {
            SetBlur(true);
            if (val == 1)
            {
                _text.text = "GO";
            }
            else
            {
                _text.text = (val - 1).ToString();
            }
        }
        else
        {
            SetBlur(false);
            _text.text = "";
        }
    }
}
