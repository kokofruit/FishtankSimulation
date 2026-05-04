using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    [SerializeField] private List<string> _infoStrips;

    private int _nextIdx = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _text.enabled = false;
        GetComponent<Button>().onClick.AddListener(nextStrip);
    }
    private void OnEnable()
    {
        foreach (KeyValuePair<JSONReader.Fish, int> KVpair in SimulationManager.instance.fishInv)
        {
            _infoStrips.Add(KVpair.Key.info);
        }
        if (_infoStrips.Count > 0)
            _text.enabled = true;
        nextStrip();
    }

    private void nextStrip()
    {
        //switch the text displayed
        if (_nextIdx >= _infoStrips.Count)
            _nextIdx = 0;

        _text.text = _infoStrips[_nextIdx];
        _nextIdx++;
    }

    private void OnDisable()
    {
        _text.enabled = false;
    }
}
