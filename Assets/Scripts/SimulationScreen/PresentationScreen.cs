using UnityEngine;

public class PresentationScreen : MonoBehaviour
{
    [SerializeField] GameObject _schoolManagerPrefab;

    void OnEnable()
    {
        foreach (var fishType in SimulationManager.instance.fishInv.Keys)
        {
            GameObject SMGameObject = Instantiate(_schoolManagerPrefab, transform);
            if (SMGameObject.TryGetComponent(out SchoolManager schoolManager))
            {
                schoolManager.Initialize(fishType, 10);
            }
        }
    }
}
