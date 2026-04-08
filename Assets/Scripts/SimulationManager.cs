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
    public HashSet<JSONReader.Decoration> decorationInventory=new();
    public JSONReader.Substrate substrateInventory;
    public float totalCost;

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
        _screens[_screenIndex].gameObject.SetActive(false);
        // decrease screen index
        _screenIndex--;
        // turn on new screen
        _screens[_screenIndex].gameObject.SetActive(true);
    }

    public void AddToFishInv(JSONReader.Fish fish)
    {
        // add to quantity if already in inventory
        if (fishInv.ContainsKey(fish)) fishInv[fish]++;
        // otherwise, add to inventory with quantity of one
        else fishInv.Add(fish, 1);
        //update cost tally
        totalCost += fish.price[0];
    }

    public void RemoveFromFishInv(JSONReader.Fish fish)
    {
        // if not in inventory, do nothing
        if (!fishInv.ContainsKey(fish)) return;
        // decrease quantity and remove if zero or less
        if (--fishInv[fish] < 1) fishInv.Remove(fish);
        //update cost tally
        totalCost -= fish.price[0];
    }

    // this works in theory
    //public void GetImage()
    //{
    //    string imagePath = "Images/fish/" + json.GetFish("betta").id;
    //    Sprite imageSprite = Resources.Load<Sprite>(imagePath);
    //    image.sprite = imageSprite;
    //    image.GetComponent<RectTransform>().sizeDelta = imageSprite.rect.size;
    //}

    override public string ToString() {
        float phLow = 0;
        float phHigh = 0;

        float tempLow = 0;
        float tempHigh = 0;

        float dkhLow = 0;
        float dkhHigh = 0;

        int numFish=fishInv.Count;


        string output = "Fish:\n";
        foreach (KeyValuePair<JSONReader.Fish,int> KVpair in fishInv) {
            output += "("+ KVpair.Value+")"+KVpair.Key.name + ": $" + KVpair.Key.price[0] + "\n";
            phLow += KVpair.Key.ph[0];
            phHigh += KVpair.Key.ph[1];

            tempLow += KVpair.Key.temp[0];
            tempHigh += KVpair.Key.temp[1];

            dkhLow += KVpair.Key.dkh[0];
            dkhHigh += KVpair.Key.dkh[1];
        }
        output += "\nDecor:\n";
        foreach (JSONReader.Decoration BigD in decorationInventory)
        {
            output += BigD.name+": $"+BigD.price+"\n";
        }
        output += "\nSubstrate: \n"+substrateInventory.name+": $"+substrateInventory.price+"\n";

        output += "\nph: " + (phLow / numFish) + "-" + (phHigh / numFish) + "\n";
        output += "\ntemp: " + (tempLow / numFish) + "-" + (tempHigh / numFish) + "\n";
        output += "\ndkh: " + (dkhLow / numFish) + "-" + (dkhHigh / numFish) + "\n";


        output += "\nTotal Cost: $" + totalCost;

        return output;
    }
}
