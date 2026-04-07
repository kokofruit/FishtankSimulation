using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishButton : MonoBehaviour
{
    public JSONReader.Fish fish;
    [SerializeField] Toggle _addToggle;
    [SerializeField] Text _fishNameText;

    public void SetFish(JSONReader.Fish newFish)
    {
        fish = newFish;
        _fishNameText.text = fish.name;
    }

    public void SetEnabled(bool isInteractable)
    {
        _addToggle.interactable = isInteractable;
    }
}
