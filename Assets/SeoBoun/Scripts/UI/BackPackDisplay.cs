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
        PlayerItemInventory.Inventory.FieldInventory.AddMedicalEvent(SetMedicalSlider);
        PlayerItemInventory.Inventory.FieldInventory.AddFoodEvent(SetFoodSlider);
        PlayerItemInventory.Inventory.FieldInventory.AddElectEvent(SetElectSlider);
        PlayerItemInventory.Inventory.FieldInventory.AddToolEvent(SetToolSlider);
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
