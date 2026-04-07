using UnityEngine;
using UnityEngine.UI;

public class TankScreen : MonoBehaviour
{
    [SerializeField] private Button _smTankBtn;
    [SerializeField] private Button _mdTankBtn;
    [SerializeField] private Button _lgTankBtn;
    [SerializeField] private Button _nextBtn;

    void OnEnable()
    {
        // add listeners to size selection buttons
        _smTankBtn.onClick.AddListener(() => SetTank(25));
        _mdTankBtn.onClick.AddListener(() => SetTank(25));
        _lgTankBtn.onClick.AddListener(() => SetTank(25));

        // disable the next button if the tank size is unselected
        _nextBtn.interactable = SimulationManager.instance.tankSize != 0;
        // add listener to the next button
        _nextBtn.onClick.AddListener(() => SimulationManager.instance.NextScreen());
    }

    void OnDisable()
    {
        // remove listeners
        _smTankBtn.onClick.RemoveAllListeners();
        _mdTankBtn.onClick.RemoveAllListeners();
        _lgTankBtn.onClick.RemoveAllListeners();
        _nextBtn.onClick.RemoveAllListeners();
    }


    void SetTank(int size)
    {
        // set the size variable of the sim manager
        SimulationManager.instance.tankSize = size;
        // enable the next button if the tank size set to a non-zero value
        _nextBtn.interactable = size != 0;
    }
}
