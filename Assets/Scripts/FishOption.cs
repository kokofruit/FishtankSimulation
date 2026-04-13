using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
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

    public static event UnityAction Spyke;

    void Awake()
    {
        button = GetComponent<Button>();
        Spyke += ResetQuantity;
    }

    public static void Reset() {
        Spyke?.Invoke();
    }
    public void ResetQuantity() {
        quantityText.SetText("0");
    }

    public void SetFish(JSONReader.Fish newFish)
    {
        fish = newFish;
        _nameText.text = fish.name;
        _iconImg.sprite=Resources.Load<Sprite>("Images/fish/" + fish.id);
    }
}
