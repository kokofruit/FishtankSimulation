using UnityEngine;
using UnityEngine.UI;

public class OptionLike : MonoBehaviour
{
    [SerializeField] Image fishImage;

    public void SetFish(JSONReader.Fish fish)
    {
        fishImage.sprite = Resources.Load<Sprite>("Images/Shop/Fish/" + fish.id);
    }
}
