using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCtrl : MonoBehaviour
{
    public enum CollectableType
    {
        KEY,
        AXE,
        DIAMOND,
        TREASURE_CLOSED,
        TREASURE_OPENED,
        SHIP_WRECK,
        ANY
    }

    [SerializeField] private CollectableType type;
    public CollectableType Type { get { return type; } }

    [SerializeField] private CollectableType collectableToUnlock;
    public CollectableType CollectableToUnlock { get { return collectableToUnlock; } }

    [SerializeField] private string collectableMessage = string.Empty;

    public string CollectableMessage { get {  return collectableMessage; } }

    [SerializeField]
    private bool isRestrictedToCollect;

    public bool IsRestrictedToCollect { get {  return isRestrictedToCollect; } }

    public Transform CollectableTransform {
        get { return this.transform; }
    }

    private void Update()
    {
        if (InventoryCtrl.instance.IsInventoryHasItem(collectableToUnlock) && isRestrictedToCollect)
        {
            isRestrictedToCollect = false;
        }
    }
}
