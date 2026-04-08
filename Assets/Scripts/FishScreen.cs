using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishScreen : MonoBehaviour
{
    // selection variables
    [SerializeField] private RectTransform _scrollViewContent;
    [SerializeField] private GameObject _fishButtonPrefab;
    private Dictionary<string, FishOption> _allFishOptions = new();
    private HashSet<string> _allowedFish = new();

    // display variables
    [SerializeField] private TMP_Text _displayTitle;
    [SerializeField] private Image _displayImage;
    [SerializeField] private TMP_Text _displayDesc;

    // other
    private int _remainingGallons;


    void Start()
    {
        // set remaining gallons to maximum by default
        _remainingGallons = SimulationManager.instance.tankSize;

        foreach (JSONReader.Fish fish in SimulationManager.instance.json.fish)
        {
            // create listing
            FishOption button = Instantiate(_fishButtonPrefab, _scrollViewContent).GetComponent<FishOption>();
            button.SetFish(fish);

            // add listeners
            button.button.onClick.AddListener(() => DisplayFish(fish));
            button.addBtn.onClick.AddListener(() => AddFish(fish));
            button.removeBtn.onClick.AddListener(() => RemoveFish(fish));

            // add to dict
            _allFishOptions.Add(fish.id, button);

            // add to allowed fish
            _allowedFish.Add(fish.id);
        }

        // disable bad options off the bat
        SetInteractable();
        // set display off the bat
        DisplayFish(SimulationManager.instance.json.fish[0]);
    }

    void AddFish(JSONReader.Fish fish)
    {
        // Do not allow bad fish
        if (!_allowedFish.Contains(fish.id)) return;

        // Add to inv
        SimulationManager.instance.AddToFishInv(fish);
        // set quantity
        _allFishOptions[fish.id].quantityText.SetText(SimulationManager.instance.fishInv[fish].ToString());

        // subtract gallons
        _remainingGallons -= fish.gallons;
        // Update allowed fish
        _allowedFish.IntersectWith(fish.friends);
        _allowedFish.Add(fish.id);
        // Set allowed fish toggles
        SetInteractable();

        // display fish
        DisplayFish(fish);
    }

    void RemoveFish(JSONReader.Fish fish)
    {
        // set quantity
        _allFishOptions[fish.id].quantityText.SetText("" + (SimulationManager.instance.fishInv[fish] - 1));
        // Remove fish
        SimulationManager.instance.RemoveFromFishInv(fish);

        // Recalculate allowed fish
        _allowedFish = new HashSet<string>(_allFishOptions.Keys);
        foreach (JSONReader.Fish jerry in SimulationManager.instance.fishInv.Keys)
        {
            _allowedFish.IntersectWith(jerry.friends);
            _allowedFish.Add(jerry.id);
        }
        // add gallons
        _remainingGallons += fish.gallons;
        // Set allowed fish toggles
        SetInteractable();

        // display fish
        DisplayFish(fish);
    }

    void SetInteractable()
    {
        foreach (KeyValuePair<string, FishOption> keyValue in _allFishOptions)
        {
            // allow fish to be added as long as they are compatible and there is enough room
            keyValue.Value.addBtn.interactable = _allowedFish.Contains(keyValue.Key) && keyValue.Value.fish.gallons <= _remainingGallons;
            // turn off minus button if the quantity is zero
            keyValue.Value.removeBtn.interactable = SimulationManager.instance.fishInv.ContainsKey(keyValue.Value.fish);
        }
    }

    void DisplayFish(JSONReader.Fish fish)
    {
        _displayTitle.SetText(fish.name);
        Sprite sprite = Resources.Load<Sprite>("Images/fish/" + fish.id);
        _displayImage.sprite = sprite;
        _displayImage.transform.localScale = new Vector3(1, sprite.rect.height/sprite.rect.width, 1);
        _displayDesc.SetText(fish.description);
    }
}
