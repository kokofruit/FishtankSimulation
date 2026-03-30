using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    // the singleton instance
    public static SimulationManager instance;

    // the json file
    public TextAsset jsonFile;

    // inventory
    public List<string> fishInventory;

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
        JSONObject jSONObject = JsonUtility.FromJson<JSONObject>(jsonFile.text);
        print(jSONObject);
    }


    public void NextScreen()
    {
        // early return if there is not a next screen
        if (_screenIndex + 1 >= _screens.Length) return;

        // turn off current screen
        _screens[_screenIndex].blocksRaycasts = false;
        _screens[_screenIndex].enabled = false;
        // increase screen index
        _screenIndex++;
        // turn on new screen
        _screens[_screenIndex].blocksRaycasts = true;
        _screens[_screenIndex].enabled = true;
    }

    public void PreviousScreen()
    {
        // early return if there is not a previous screen
        if (_screenIndex - 1 < 0) return;

        // turn off current screen
        _screens[_screenIndex].blocksRaycasts = false;
        _screens[_screenIndex].enabled = false;
        // decrease screen index
        _screenIndex++;
        // turn on new screen
        _screens[_screenIndex].blocksRaycasts = true;
        _screens[_screenIndex].enabled = true;
    }
}
