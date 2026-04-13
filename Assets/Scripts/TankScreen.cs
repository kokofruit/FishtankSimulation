using UnityEngine;
using UnityEngine.UI;

public class TankScreen : MonoBehaviour
{
    [SerializeField] private Toggle _smTankTog;
    [SerializeField] private Toggle _mdTankTog;
    [SerializeField] private Toggle _lgTankTog;
    [SerializeField] private Button _nextBtn;

    void Start()
    {
        // set default tank size
        SetTank(10);

        // add listeners to size selection buttons
        _smTankTog.onValueChanged.AddListener((bool value) => SetTank(10));
        _mdTankTog.onValueChanged.AddListener((bool value) => SetTank(25));
        _lgTankTog.onValueChanged.AddListener((bool value) => SetTank(50));

        // add listener to the next button
        _nextBtn.onClick.AddListener(() => SimulationManager.instance.NextScreen());
    }

    void SetTank(int size)
    {
        // set the size variable of the sim manager
        SimulationManager.instance.tankSize = size;
    }
}
