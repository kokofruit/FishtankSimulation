using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishScreen : MonoBehaviour
{
    private int _remainingGallons;
    [SerializeField] private RectTransform _scrollViewContent;
    private Dictionary<string, FishButtonTemp> _mothsFishDict;

    // I think Phil is doing a lot of stuff for this so i'm mostly doing tank related stuff
    void Start()
    {
        // set remaining gallons to maximum by default
        _remainingGallons = SimulationManager.instance.tankSize;

        foreach (JSONReader.Fish fish in SimulationManager.instance.json.fish)
        {
            
        }
    }

    private void CheckGallons()
    {
        foreach (FishButtonTemp fishButton in _mothsFishDict.Values)
        {
            fishButton.SetEnabled(fishButton.fish.gallons <= _remainingGallons);
        }
    }
}
