using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecorOption : MonoBehaviour
{
    public Button button;
    public JSONReader.Decoration decor;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Image _iconImg;
    public TMP_Text priceText;
    public string decorType;
    public bool selected;


    void Awake()
    {
        button = GetComponent<Button>();
    }

    public void SetDecor(JSONReader.Decoration newDecor)
    {
        decor = newDecor;
        _nameText.text = decor.name;
        _iconImg.sprite = Resources.Load<Sprite>("Images/Decor/"+ decor.name);
        priceText.text="$"+decor.price;
        selected = false;
    }
    public void Toggle() {
        selected = !selected;
        ColorBlock newColors = button.colors;
        newColors.normalColor = selected?Color.cyan:Color.white;
        newColors.selectedColor = selected ?Color.cyan : Color.white;
        button.colors = newColors;
    }
}
