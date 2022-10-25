using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int inventoryCapacity = 10;
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private Button[] inventoryButtons;

    private GameObject[] inventoryItems;
    // Start is called before the first frame update
    void Start()
    {
        inventoryItems = new GameObject[inventoryCapacity];
        weapons[0].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetItemByName("Pistol_PickUp") == null) weapons[0].SetActive(false);
        else weapons[0].SetActive(true);
        for (int i = 0; i < inventoryItems.Length; ++i)
        {
            if (inventoryItems[i] == null) {
                inventoryButtons[i].gameObject.SetActive( false);
                continue;
            }
            inventoryButtons[i].gameObject.SetActive(true);
            inventoryButtons[i].GetComponentInChildren<TextMeshProUGUI>().SetText(inventoryItems[i].name.Substring(0,4)+"...");
        }
    }

    public GameObject[] getInventoryItems()
    {
        return inventoryItems;
    }

    bool PickUpItem(GameObject item)
    {
        if (GetItemIndex(item) != -1) return false;
        item.SetActive(false);

      for(int i = 0; i< inventoryItems.Length; ++i)
        {
            if (inventoryItems[i] == null)
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
            Instantiate(inventoryItems[index]);
            inventoryItems[index] = null;
        }
    }

    int GetItemIndex(GameObject item)
    {
        for(int i = 0; i < inventoryItems.Length; ++i)
        {
            if (inventoryItems[i]!=null && inventoryItems[i].Equals(item)) return i;
        }
        return -1;
    }

    GameObject GetItemByName(string name)
    {
        for (int i = 0; i < inventoryItems.Length; ++i)
        {
            if (inventoryItems[i] != null && inventoryItems[i].name == name) return inventoryItems[i];
        }
        return null;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (LayerMask.LayerToName(other.gameObject.layer) == "Pickups")
        {

            PickUpItem(other.gameObject);
          
        }
    }

}
