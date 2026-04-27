using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    private Button _button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _text=gameObject.GetComponentInChildren<TMP_Text>();
        _button = GetComponent<Button>();

        _button.onClick.AddListener(kill);
    }

    public void kill() { 
        gameObject.SetActive(false);
    }

    public void live(string text) {
        _text.text = text;
        gameObject.SetActive(true);
    }
}
