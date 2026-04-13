using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishOption : MonoBehaviour
{
    public Button button;
    public JSONReader.Fish fish;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Image _iconImg;
    public TMP_Text quantityText;
    public Button removeBtn;
    public Button addBtn;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    public void SetFish(JSONReader.Fish newFish)
    {
        fish = newFish;
        _nameText.text = fish.name;
        _iconImg.sprite=Resources.Load<Sprite>("Images/fish/" + fish.id);
    }
}
