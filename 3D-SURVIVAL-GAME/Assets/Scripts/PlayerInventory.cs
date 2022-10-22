using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int inventoryCapacity = 10;
    private GameObject[] inventoryItems;
    // Start is called before the first frame update
    void Start()
    {
        inventoryItems = new GameObject[inventoryCapacity];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PickUpItem(GameObject item)
    {
        if (GetItemIndex(item) != -1) return;
      for(int i = 0; i< inventoryItems.Length; ++i)
        {
            if (inventoryItems[i] != null)
            {
                inventoryItems[i] = item;
            }
        }
    }
    void DropItem(GameObject item)
    {
        int index = GetItemIndex(item);
        if (index == -1)
        {
            inventoryItems[index] = null;
        }
    }

    int GetItemIndex(GameObject item)
    {
        for(int i = 0; i < inventoryItems.Length; ++i)
        {
            if (inventoryItems[i].Equals(item)) return i;
        }
        return -1;
    }
    
}
