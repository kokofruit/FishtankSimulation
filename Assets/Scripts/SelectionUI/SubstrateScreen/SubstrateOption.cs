using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubstrateOption : MonoBehaviour
{
    public Toggle button;
    public JSONReader.Substrate substrate;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Image _iconImg;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private HorizontalLayoutGroup _heartContainer;
    [SerializeField] private GameObject _fishHeartPrefab;


    void Awake()
    {
        button = GetComponent<Toggle>();
        button.group = GetComponentInParent<ToggleGroup>();
    }

    public void SetSubstrate(JSONReader.Substrate newSub)
    {
        substrate = newSub;
        _nameText.text = substrate.name;
        _iconImg.sprite = Resources.Load<Sprite>("Images/Display/Substrate/" + substrate.id);
        priceText.text = "$" + substrate.price;

        CalculateLikes();
    }

    void OnEnable()
    {
        CalculateLikes();
    }

    private void CalculateLikes()
    {
        if (substrate == null) return;

        foreach (Transform child in _heartContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (JSONReader.Fish fish in SimulationManager.instance.fishInv.Keys)
        {
            if (fish.substrate.Contains(substrate.id))
            {
                GameObject fishHeart = Instantiate(_fishHeartPrefab, _heartContainer.transform);
                fishHeart.GetComponent<OptionLike>().SetFish(fish);
            }
        }
    }
}
