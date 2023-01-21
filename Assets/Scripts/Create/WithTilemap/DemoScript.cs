using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;

    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if (result == true)
        {
            print("added");
        }
        else
        {
            print("not added");
        }
    }
    public void GetSelectedItem()
    {
        Item recievedItem = inventoryManager.GetSelectedItem(false);
        if (recievedItem != null)
        {
            print("recieved item" + recievedItem);
        }
        else
        {
            print("not recieved");
        }
    }
    public void UseSelectedItem()
    {
        Item recievedItem = inventoryManager.GetSelectedItem(true);
        if (recievedItem != null)
        {
            print("used item" + recievedItem);
        }
        else
        {
            print("not used");
        }
    }

}
