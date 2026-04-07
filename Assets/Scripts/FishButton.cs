using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishButtonTemp : MonoBehaviour
{
    public JSONReader.Fish fish;
    [SerializeField] Button _addButton;
    [SerializeField] TMP_Text _fishNameText;

    public void SetFish(JSONReader.Fish newFish)
    {
        fish = newFish;
        _fishNameText.text = fish.name;
    }

    public void SetEnabled(bool isInteractable)
    {
        _addButton.interactable = isInteractable;
    }
}
