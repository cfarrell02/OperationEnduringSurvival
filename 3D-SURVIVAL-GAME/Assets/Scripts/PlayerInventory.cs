using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEditor.Progress;
using System;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int inventoryCapacity = 10;
    private float index;
    private int activeItems;
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private Button[] inventoryButtons;
    private AudioSource audioSource;
    [SerializeField] private AudioClip pickupSound;
    private GameObject activeItem;
    private PlayerHealth health;
    private readonly KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };

    private GameObject[] inventoryItems;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        health = GetComponent<PlayerHealth>();
        inventoryItems = new GameObject[inventoryCapacity];
        weapons[0].SetActive(false);
    }

    // Update is called once per frame
    void Update()

    {


        GetSelection();
        if (activeItem != null)
        {
            if (activeItem.name == "Pistol_Pickup") weapons[0].SetActive(true);
            else weapons[0].SetActive(false);
            if (activeItem.name == "Knife_Pickup") weapons[1].SetActive(true);
            else weapons[1].SetActive(false);
        }

        for (int i = 0; i < inventoryItems.Length; ++i)
        {
            if (inventoryItems[i] == null)
            {
                inventoryButtons[i].gameObject.SetActive(false);
                continue;
            }
            inventoryButtons[i].gameObject.SetActive(true);
            inventoryButtons[i].GetComponentInChildren<TextMeshProUGUI>().SetText(inventoryItems[i].name.Substring(0, 4) + "...");
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (activeItem != null && activeItem.CompareTag("Health"))
            {
                if (!health.addHealth(20)) return;
                int index = GetItemIndex(activeItem);
                inventoryItems[index] = activeItem = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            print("Dropped " + activeItem.name);
            DropItem(GetItemIndex(activeItem));
            activeItem = null;
        }
    }

    public GameObject[] getInventoryItems()
    {
        return inventoryItems;
    }

    bool PickUpItem(GameObject item)
    {
        audioSource.PlayOneShot(pickupSound);
        if (GetItemIndex(item) != -1) return false;
        item.SetActive(false);

      for(int i = 0; i< inventoryItems.Length; ++i)
        {
            if (inventoryItems[i] == null)
            {
                inventoryItems[i] = item;
                activeItems++;
                return true;
            }
        }
       return false;
    }
    void DropItem(int index)
    {
        if (index == -1)
        {
           // Instantiate(inventoryItems[index]);
            inventoryItems[index] = null;
            activeItems--;
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

    private void GetSelection()
    {

        if (Input.mouseScrollDelta.y == 0)
        {
            for (int i = 0; i < keyCodes.Length; i++)
            {
                if (Input.GetKeyDown(keyCodes[i]))
                {
                    index = i;
                }
            }
        }
        else
            index += Input.mouseScrollDelta.y*10;
        for(int i = 0; i < activeItems; ++i)
        {
            if (i == Math.Abs((int)(index % activeItems)))
            {
                inventoryButtons[i].GetComponent<Image>().color = new Color(0, 0, 155, 1);
                activeItem = inventoryItems[i];
            }
            else
                inventoryButtons[i].GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }
        
    }

}
