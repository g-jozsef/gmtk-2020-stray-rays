using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntDisplayer : MonoBehaviour
{
    [SerializeField] private IntVariable _intVariable;

    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _text.text = _intVariable.Value.ToString();
    }
}
