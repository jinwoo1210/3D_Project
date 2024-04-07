using UnityEngine;
using UnityEngine.UI;

public class BackPackDisplay : MonoBehaviour
{
    [SerializeField] Slider medicalSlider;
    [SerializeField] Slider foodSlider;
    [SerializeField] Slider electSlider;
    [SerializeField] Slider toolSlider;

    private void Start()
    {
        PlayerItemInventory.Inventory.AddMedicalEvent(SetMedicalSlider);
        PlayerItemInventory.Inventory.AddFoodEvent(SetFoodSlider);
        PlayerItemInventory.Inventory.AddElectEvent(SetElectSlider);
        PlayerItemInventory.Inventory.AddToolEvent(SetToolSlider);
    }

    public void SetMedicalSlider(int maxPoint, int curPoint)
    {
        medicalSlider.value = (float)curPoint / maxPoint;
    }
    public void SetFoodSlider(int maxPoint, int curPoint)
    {
        foodSlider.value = (float)curPoint / maxPoint;
    }
    public void SetElectSlider(int maxPoint, int curPoint)
    {
        electSlider.value = (float)curPoint / maxPoint;
    }
    public void SetToolSlider(int maxPoint, int curPoint)
    {
        toolSlider.value = (float)curPoint / maxPoint;
    }
}
