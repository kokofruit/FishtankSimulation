using System;
using UnityEngine;

public class JSONReader : MonoBehaviour
{
    [Serializable]
    public class FishDecor
    {
        public string[] plants;
        public string[] driftwood;
        public string[] coral;
        public string[] caves;
        public string[] buildings;
        public string[] misc;
    }

    [Serializable]
    public class Fish
    {
        public string id;
        public string name;
        public int gallons;
        public string[] friends;
        public FishDecor decor;
        public string[] substrate;
        public string[] diet;
        public float[] ph;
        public int[] temp;
        public int[] dkh;
        public float[] salinity;
        public float[] price;
        public string description;
    };

    [Serializable]
    public class Decor
    {
        public string[] plants;
        public string[] driftwood;
        public string[] coral;
        public string[] caves;
        public string[] buildings;
        public string[] misc;
    }

    [Serializable]
    public class JSONClass
    {
        public Fish[] fish;
        public Decor decor;

        public void FishRead()
        {
            // foreach (Fish item in fish)
            // {
            //     print(item.decor.plants.Length);
            // }
            print(decor.plants.Length);
        }
    }

}
