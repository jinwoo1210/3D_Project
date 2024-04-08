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
        if (PlayerStatManager.Inventory == null)
            return;

        PlayerStatManager.Inventory.FieldInventory.AddMedicalEvent(SetMedicalSlider);
        PlayerStatManager.Inventory.FieldInventory.AddFoodEvent(SetFoodSlider);
        PlayerStatManager.Inventory.FieldInventory.AddElectEvent(SetElectSlider);
        PlayerStatManager.Inventory.FieldInventory.AddToolEvent(SetToolSlider);
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
