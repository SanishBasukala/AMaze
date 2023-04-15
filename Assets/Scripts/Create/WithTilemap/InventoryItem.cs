using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour/*, IBeginDragHandler, IDragHandler, IEndDragHandler*/
{
	[Header("UI")]
	public Image image;

	[HideInInspector] public Item item;
	[HideInInspector] public int count;
	[HideInInspector] public Transform parentAfterDrag;


	public void InitialiseItem(Item newItem)
	{
		item = newItem;
		image.sprite = newItem.image;
	}

}