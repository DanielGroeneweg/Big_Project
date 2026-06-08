using UnityEngine;
public class InventoryManager : MonoBehaviour
{
    [SerializeField] InventoryPresenter inventoryPresenter;
    Inventory inventory;
    private void Start()
    {
        inventory = new Inventory();
    }
}