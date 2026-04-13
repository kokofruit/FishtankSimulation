using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubstrateOption : MonoBehaviour
{
    public Toggle button;
    public JSONReader.Substrate substrate;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Image _iconImg;
    public TMP_Text priceText;
    void Awake()
    {
        button = GetComponent<Toggle>();
    }

    public void SetSubstrate(JSONReader.Substrate newSub)
    {
        substrate = newSub;
        _nameText.text = substrate.name;
        _iconImg.sprite = Resources.Load<Sprite>("Images/Substrate/" + substrate.name);
        priceText.text="$"+substrate.price;
    }
}
