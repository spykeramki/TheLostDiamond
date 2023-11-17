using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCtrl : MonoBehaviour
{
    public static InventoryCtrl instance;

    private Dictionary<CollectableCtrl.CollectableType, CollectableCtrl> inventoryItems;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        inventoryItems =  new Dictionary<CollectableCtrl.CollectableType, CollectableCtrl>();
    }

    public void AddItemToInventory(CollectableCtrl collectable)
    {
        if (!inventoryItems.ContainsKey(collectable.Type))
        {
            inventoryItems[collectable.Type] = collectable;
        }
    }

    public void RemoveItemFromInventory(CollectableCtrl collectable)
    {
        if (inventoryItems.ContainsKey(collectable.Type))
        {
            inventoryItems.Remove(collectable.Type);
        }
    }

    public bool IsInventoryHasItem(CollectableCtrl.CollectableType collectableType)
    {
        return inventoryItems.ContainsKey(collectableType);
    }
}
