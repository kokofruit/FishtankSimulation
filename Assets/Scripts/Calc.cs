using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Calc : MonoBehaviour
{
    HashSet<string> _goodFish;
    Dictionary<string,Fish> _allFish;

    public GameObject buttonPrefab;

    class Fish
    {
        public HashSet<string> friends;
        public GameObject button;

        private Vector3 stupid_temp = new Vector3(0,0,0);
    
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //load fish from json here into all fish and good fish because all fish are good fish if you think about it really  im not trying to get like emotional or anything here its just the fish deserve better and i feel you guys take them for granted  fish are important for the ecosystem yknow    i got off track   fish are good      for now
        //also make the buttons here
        foreach (var fish in _allFish)
        { 
            
        }
        
    }


    public void addFish(string bob) {
        //bad  should not be possible
        if (!_goodFish.Contains(bob))
            return;

        _goodFish.Intersect(_allFish[bob].friends);
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
