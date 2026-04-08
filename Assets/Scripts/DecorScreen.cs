using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecorScreen : MonoBehaviour
{
    // selection variables
    [SerializeField] private RectTransform _scrollViewContent;
    [SerializeField] private GameObject _decorButtonPrefab;


    // display variables
    [SerializeField] private TMP_Text _displayTitle;
    [SerializeField] private Image _displayImage;
    [SerializeField] private TMP_Text _displayDesc;

    // other
    private int _remainingGallons;


    void Start()
    {

        //I blame the json for making me copy paste 6 identical foreach loops
        //if there is a better way to do this I do not cares
        //future me here   I broke down those walls and now the decor may live in harmony jus t like that one nd only cool british dude Winston Churchill im kinda just cool like thaat arent I huh  I deserve praise    and a cookie       imma go get a cookie               i do not have any cookies  damm
        foreach (JSONReader.Decoration decor in SimulationManager.instance.json.decor)
        {
            // create listing
            DecorOption button = Instantiate(_decorButtonPrefab, _scrollViewContent).GetComponent<DecorOption>();
            button.SetDecor(decor);

            // add listeners
            button.button.onClick.AddListener(()=>ToggleInteract(button, decor));
        }

        // set display off the bat
        DisplayDecor(SimulationManager.instance.json.decor[0]);

    }

    void ToggleInteract(DecorOption button, JSONReader.Decoration decor) {
        if (!button.selected) AddDecor(decor);
        else RemoveDecor(decor);
        DisplayDecor(decor);
        button.Toggle();
    }

    void AddDecor(JSONReader.Decoration decor) {
        SimulationManager.instance.decorationInventory.Add(decor);
        //update cost tally
        SimulationManager.instance.totalCost += decor.price;
    }

    void RemoveDecor(JSONReader.Decoration decor) {
        SimulationManager.instance.decorationInventory.Remove(decor);
        //update cost tally
        SimulationManager.instance.totalCost -= decor.price;
    }


    void DisplayDecor(JSONReader.Decoration decor)
    {
        _displayTitle.SetText(decor.name);
        Sprite sprite = Resources.Load<Sprite>("Images/Decor/" + decor.name);
        _displayImage.sprite = sprite;
        _displayImage.transform.localScale = new Vector3(1, sprite.rect.height / sprite.rect.width, 1);
        _displayDesc.SetText("$"+decor.price);
    }
}
