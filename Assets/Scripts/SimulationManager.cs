using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulationManager : MonoBehaviour
{
    // the singleton instance
    public static SimulationManager instance;

    // the json file
    [SerializeField] private string _jsonPath;
    public JSONReader.JSONClass json;

    // inventory
    public int tankSize = 0;
    // public List<JSONReader.Fish> fishInventory;
    public Dictionary<JSONReader.Fish, int> fishInv = new();
    public List<JSONReader.Decoration> decorationInventory;
    public JSONReader.Substrate substrateInventory;

    // the array of UI
    [SerializeField] private CanvasGroup[] _screens = new CanvasGroup[5];
    // the screen index
    private int _screenIndex = 0;


    void Awake()
    {
        // Set the singleton instance
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }

        // load the json
        var jsonTextAsset = Resources.Load<TextAsset>(_jsonPath);
        json = JsonUtility.FromJson<JSONReader.JSONClass>(jsonTextAsset.text);
    }


    public void NextScreen()
    {
        // early return if there is not a next screen
        if (_screenIndex + 1 >= _screens.Length) return;

        // turn off current screen
        _screens[_screenIndex].gameObject.SetActive(false);
        // increase screen index
        _screenIndex++;
        // turn on new screen
        _screens[_screenIndex].gameObject.SetActive(true);
    }

    public void PreviousScreen()
    {
        // early return if there is not a previous screen
        if (_screenIndex - 1 < 0) return;

        // turn off current screen
        _screens[_screenIndex].alpha = 0;
        _screens[_screenIndex].blocksRaycasts = false;
        _screens[_screenIndex].enabled = false;
        // decrease screen index
        _screenIndex++;
        // turn on new screen
        _screens[_screenIndex].alpha = 1;
        _screens[_screenIndex].blocksRaycasts = true;
        _screens[_screenIndex].enabled = true;
    }

    public void AddToFishInv(JSONReader.Fish fish)
    {
        // add to quantity if already in inventory
        if (fishInv.ContainsKey(fish)) fishInv[fish]++;
        // otherwise, add to inventory with quantity of one
        else fishInv.Add(fish, 1);
    }

    public void RemoveFromFishInv(JSONReader.Fish fish)
    {
        // if not in inventory, do nothing
        if (!fishInv.ContainsKey(fish)) return;
        // decrease quantity and remove if zero or less
        if (--fishInv[fish] < 1) fishInv.Remove(fish);
    }

    // this works in theory
    //public void GetImage()
    //{
    //    string imagePath = "Images/fish/" + json.GetFish("betta").id;
    //    Sprite imageSprite = Resources.Load<Sprite>(imagePath);
    //    image.sprite = imageSprite;
    //    image.GetComponent<RectTransform>().sizeDelta = imageSprite.rect.size;
    //}
}
