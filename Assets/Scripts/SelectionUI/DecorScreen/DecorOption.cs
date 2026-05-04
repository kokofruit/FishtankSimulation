using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecorOption : MonoBehaviour
{
    public Button button;
    public JSONReader.Decoration decor;
    public TMP_Text priceText;
    public bool selected;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Image _iconImg;
    [SerializeField] private HorizontalLayoutGroup _heartContainer;
    [SerializeField] private GameObject _fishHeartPrefab;


    void Awake()
    {
        button = GetComponent<Button>();
    }

    public void SetDecor(JSONReader.Decoration newDecor)
    {
        decor = newDecor;
        _nameText.text = decor.name;
        _iconImg.sprite = Resources.Load<Sprite>("Images/Shop/Decor/" + decor.name);
        priceText.text = "$" + decor.price;
        selected = false;

        CalculateLikes();
    }

    void OnEnable()
    {
        CalculateLikes();
    }

    private void CalculateLikes()
    {
        if (decor == null) return;

        foreach (Transform child in _heartContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (JSONReader.Fish fish in SimulationManager.instance.fishInv.Keys)
        {
            if (fish.decor.Contains(decor.name))
            {
                GameObject fishHeart = Instantiate(_fishHeartPrefab, _heartContainer.transform);
                fishHeart.GetComponent<OptionLike>().SetFish(fish);
            }
        }
    }

    public void Toggle()
    {
        selected = !selected;
        ColorBlock newColors = button.colors;
        newColors.normalColor = selected ? Color.cyan : Color.white;
        newColors.selectedColor = selected ? Color.cyan : Color.white;
        button.colors = newColors;
    }
}
