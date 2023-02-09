using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public Item[] startItems; //collection of items at the start
    public GameObject[] prefabs;
    public TileBase[] tiles;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    int selectedSlot = -1;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        ChangeSelectedSlot(0);
        //foreach (var item in startItems)  //collection of items at the start
        //{
        //    AddItem(item);
        //}

        for (int i = 0; i < startItems.Count(); i++)
        {
            startItems[i].itemId = i;
            AddItem(startItems[i]);
        }
    }
    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < startItems.Count())
            {
                ChangeSelectedSlot(number - 1);
            }
        }
        else
        {
            return;
        }
    }
    public void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }
    public bool AddItem(Item item)
    {
        // Check if any slot has the sam eitem with count lower than max
        //print("inventory length" + inventorySlots.Length);
        //for (int i = 0; i < inventorySlots.Length; i++)
        //{

        //    InventorySlot slot = inventorySlots[i];
        //    InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        //    if (itemInSlot != null &&
        //        itemInSlot.item == item &&
        //        itemInSlot.item.stackable == true)
        //    {
        //        itemInSlot.count++;
        //        return true;
        //    }
        //}
        //Find any empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }
    public void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }
    public Item GetSelectedItem()//bool use
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;
            //if (use)
            //{
            //    if (itemInSlot.count <= 0)
            //    {
            //        Destroy(itemInSlot.gameObject);
            //    }
            //}
            return item;
        }
        return null;
    }
}