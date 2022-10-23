using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int inventoryCapacity = 10;
    [SerializeField] GameObject[] weapons;
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

    public GameObject[] getInventoryItems()
    {
        return inventoryItems;
    }

    bool PickUpItem(GameObject item)
    {
        if (GetItemIndex(item) != -1) return false;
      for(int i = 0; i< inventoryItems.Length; ++i)
        {
            if (inventoryItems[i] != null)
            {
                inventoryItems[i] = item;
                return true;
            }
        }
       return false;
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
    private void OnTriggerEnter(Collider other)
    {
        if(LayerMask.LayerToName( other.gameObject.layer) == "Pickups")
        {
            PickUpItem(other.gameObject);
            

            if(other.name == "Pistol_Pickup")
            {
                weapons[0].SetActive(true);
            }



            Destroy(other.gameObject);
        }
    }

}
