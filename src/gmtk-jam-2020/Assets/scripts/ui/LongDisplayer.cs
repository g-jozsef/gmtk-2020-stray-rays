using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LongDisplayer : MonoBehaviour
{
    [SerializeField] private LongVariable _longVariable;

    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _text.text = _longVariable.Value.ToString();
    }
}
