using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishScreen : MonoBehaviour
{
    private int _remainingGallons;
    [SerializeField] private RectTransform _scrollViewContent;
    [SerializeField] private GameObject _fishButtonPrefab;

    [SerializeField] private TMP_Text _displayName;
    [SerializeField] private Image _displayImage;
    [SerializeField] private TMP_Text _displayDescription;

    private Dictionary<string, FishButton> _allFishButtons = new();
    private HashSet<string> _allowedFish = new();


    void Start()
    {
        // set remaining gallons to maximum by default
        _remainingGallons = SimulationManager.instance.tankSize;

        foreach (JSONReader.Fish fish in SimulationManager.instance.json.fish)
        {
            // create listing
            FishButton button = Instantiate(_fishButtonPrefab, _scrollViewContent).GetComponent<FishButton>();
            button.SetFish(fish);

            // add listener
            button.GetComponent<Toggle>().onValueChanged.AddListener((bool value) => HandleInput(value, fish));

            // add to dict
            _allFishButtons.Add(fish.id, button);

            // add to allowed fish
            _allowedFish.Add(fish.id);
        }
    }

    void HandleInput(bool state, JSONReader.Fish fish)
    {
        if (state) AddFish(fish);
        else RemoveFish(fish);
    }

    void AddFish(JSONReader.Fish fish)
    {
        // Do not allow bad fish
        if (!_allowedFish.Contains(fish.id)) return;

        // Add to inv
        SimulationManager.instance.fishInventory.Add(fish);

        // Update allowed fish
        _allowedFish.IntersectWith(fish.friends);
        _allowedFish.Add(fish.id);

        // Set allowed fish toggles
        SetInteractable();

        //Edit info panel
        SetDisplay(fish);
    }

    void RemoveFish(JSONReader.Fish fish)
    {
        // Remove fish
        SimulationManager.instance.fishInventory.Remove(fish);

        // Recalculate allowed fish
        _allowedFish = new HashSet<string>(_allFishButtons.Keys);


        foreach (JSONReader.Fish jerry in SimulationManager.instance.fishInventory)
        {
            _allowedFish.IntersectWith(jerry.friends);
            _allowedFish.Add(jerry.id);
        }

        // Set allowed fish toggles
        SetInteractable();
    }

    void SetDisplay(JSONReader.Fish fish) {
        _displayName.text = fish.name;

        //TODO: json still needs this
        //_displayDescription.text=fish.description;

        _displayImage.sprite = Resources.Load<Sprite>("Images/fish/" + fish.id);
    }

    void SetInteractable()
    {
        foreach (KeyValuePair<string, FishButton> keyValue in _allFishButtons)
        {
            keyValue.Value.SetEnabled(_allowedFish.Contains(keyValue.Key));
        }
    }

    // private void CheckGallons()
    // {
    //     foreach (FishButton fishButton in _mothsFishDict.Values)
    //     {
    //         fishButton.SetEnabled(fishButton.fish.gallons <= _remainingGallons);
    //     }
    // }
}
