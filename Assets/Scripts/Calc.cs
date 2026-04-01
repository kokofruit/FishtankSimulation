using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Calc : MonoBehaviour
{
    HashSet<string> _goodFish;
    Dictionary<string,FishButton> _allFish;

    public static GameObject buttonPrefab;

    public static Vector3 stupid_temp = new Vector3(0, 0, 0);

    class FishButton
    {
        public JSONReader.Fish fish;
        public GameObject button;

        public FishButton(JSONReader.Fish f) { 
            fish = f;
            button = Instantiate(buttonPrefab, stupid_temp, Quaternion.identity);
            stupid_temp.y++;
        }

    
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //load fish from json here into all fish and good fish because all fish are good fish if you think about it really  im not trying to get like emotional or anything here its just the fish deserve better and i feel you guys take them for granted  fish are important for the ecosystem yknow    i got off track   fish are good      for now
        //also make the buttons here
        _allFish = new Dictionary<string, FishButton>();
        _goodFish = new HashSet<string>();
        foreach (var fish in SimulationManager.instance.fishInventory)
        {
            _allFish.Add(fish.name,new FishButton(fish));
            _goodFish.Add(fish.name);
        }        
    }


    public void addFish(string bob) {
        //bad  should not be possible
        if (!_goodFish.Contains(bob))
            return;

        _goodFish.Intersect(_allFish[bob].fish.friends);
    }

    public void removeFish(string bob)
    {
        _allFish.Remove(bob);

        _goodFish.Clear();

        foreach (var fish in _allFish)
        {
            if (!_goodFish.Contains(fish.Key))
            {
                fish.Value.button.SetActive(false);
            }
            else
            {
                //pro
                fish.Value.button.SetActive(true);
            }
        }
    }

    public void theTing() {
        foreach (var fish in _allFish) {
            if (!_goodFish.Contains(fish.Key))
            {
                fish.Value.button.SetActive(false);
            }
            else {
                //pro
                fish.Value.button.SetActive(true);
            }
        }
    }
}
