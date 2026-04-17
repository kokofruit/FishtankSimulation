using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubstrateScreen : MonoBehaviour
{
    // selection variables
    [SerializeField] private RectTransform _scrollViewContent;
    [SerializeField] private GameObject _substrateButtonPrefab;


    // display variables
    [SerializeField] private TMP_Text _displayTitle;
    [SerializeField] private Image _displayImage;
    [SerializeField] private TMP_Text _displayDesc;



    void Start()
    {

        //I blame the json for making me copy paste 6 identical foreach loops
        //if there is a better way to do this I do not cares
        //future me here   I broke down those walls and now the decor may live in harmony jus t like that one nd only cool british dude Winston Churchill im kinda just cool like thaat arent I huh  I deserve praise    and a cookie       imma go get a cookie               i do not have any cookies  damm
        foreach (JSONReader.Substrate substrate in SimulationManager.instance.json.substrate)
        {
            // create listing
            SubstrateOption button = Instantiate(_substrateButtonPrefab, _scrollViewContent).GetComponent<SubstrateOption>();
            button.SetSubstrate(substrate);

            // add listeners
            button.button.onValueChanged.AddListener((bool b) => ToggleInteract(b, substrate));
        }

        // set display off the bat
        DisplaySubstrate(SimulationManager.instance.json.substrate[0]);

        // set default substrate
        AddSubstrate(SimulationManager.instance.json.substrate[0]);
    }

    void ToggleInteract(bool b, JSONReader.Substrate substrate)
    {
        if (b) AddSubstrate(substrate);
        DisplaySubstrate(substrate);
    }

    void AddSubstrate(JSONReader.Substrate substrate)
    {
        //remove previous substrte cost
        if (SimulationManager.instance.substrateInventory != null)
            SimulationManager.instance.totalCost -= SimulationManager.instance.substrateInventory.price;

        SimulationManager.instance.substrateInventory = substrate;
        //update cost tally
        SimulationManager.instance.totalCost += substrate.price;
    }



    void DisplaySubstrate(JSONReader.Substrate substrate)
    {
        _displayTitle.SetText(substrate.name);
        Sprite sprite = Resources.Load<Sprite>("Images/Substrate/" + substrate.name);
        _displayImage.sprite = sprite;
        _displayImage.transform.localScale = new Vector3(1, sprite.rect.height / sprite.rect.width, 1);
        _displayDesc.SetText("$" + substrate.price);
    }
}
