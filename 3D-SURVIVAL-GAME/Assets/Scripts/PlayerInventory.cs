using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using static UnityEditor.Progress;
using System;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int inventoryCapacity = 10;
    private float index;
    private int activeItems;
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private Button[] inventoryButtons;
    private AudioSource audioSource;
    [SerializeField] private AudioClip pickupSound,healthSound;
    private GameSession session;

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

        session = FindObjectOfType<GameSession>();
        audioSource = GetComponent<AudioSource>();
        health = GetComponent<PlayerHealth>();
        if (SceneManager.GetActiveScene().buildIndex == 1 || session.inventory.Length == 0)
        {
            inventoryItems = new GameObject[inventoryCapacity];
            session.inventory = inventoryItems;
        }
        else inventoryItems = session.inventory;
        weapons[0].SetActive(false);
    }

    // Update is called once per frame
    void Update()

    {

        GetSelection();
        if (activeItem != null)
        {
            if (activeItem.name.Length >= 13 && activeItem.name.Substring(0, 13) == "Pistol_Pickup") { weapons[0].SetActive(true); }
            else weapons[0].SetActive(false);
            if (activeItem.name.Length >= 12 && activeItem.name.Substring(0,12) == "Knife_Pickup") weapons[1].SetActive(true);
            else weapons[1].SetActive(false);
        }
        else
        {
            foreach (GameObject weapon in weapons) weapon.SetActive(false);
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
                if (!health.AddHealth(20)) return;
                audioSource.PlayOneShot(healthSound);
                int index = GetItemIndex(activeItem);
                inventoryItems[index] = activeItem = null;
            }
        }

        if (activeItem!=null && Input.GetKeyDown(KeyCode.Q))
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
                session.inventory = inventoryItems;
                DontDestroyOnLoad(item);
                session.SaveItems();
                return true;
            }
        }
       return false;
    }
    void DropItem(int index)
    {
        if (index != -1)
        {
            GameObject droppedItem = Instantiate(inventoryItems[index],transform.position+transform.forward*1.1f+transform.up,transform.rotation);
            droppedItem.SetActive(true);
            droppedItem.GetComponent<Rigidbody>().AddForce(transform.forward);
            inventoryItems[index] = null;
            FillInGap(ref inventoryItems, index);
            activeItems--;
            session.inventory = inventoryItems;
            session.SaveItems();
        }
    }

    void FillInGap(ref GameObject[] array, int index)
    {
        for(int i = index+1; i< array.Length; ++i)
        {
            array[i - 1] = array[i];
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
