using TMPro;
using UnityEngine;

public class DemoOutputReader : MonoBehaviour
{
    public TMP_Text text;

    void OnEnable()
    {
        text.text = SimulationManager.instance.ToString();        
    }
}
