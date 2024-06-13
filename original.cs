using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ItemManagementSystem : MonoBehaviour
{
    [SerializeField]
    List<Item> fullItemList = new List<Item>();
    [SerializeField]
    List<Item> inventoryItemList = new List<Item>();

    [SerializeField]
    GameObject itemPrefab;

    [SerializeField]
    Transform inventoryTransform;
    GameObject searchBar;

    void Start()
    {
        DefineItems();
        InitialiseFullItemList();
    }



    void DefineItems()
    {
        fullItemList.Add(new Item("Axe", 3.0f));
        fullItemList.Add(new Item("Bandage", 0.4f));
        fullItemList.Add(new Item("Crossbow", 4.0f));
        fullItemList.Add(new Item("Dagger", 0.8f));
        fullItemList.Add(new Item("Emerald", 0.2f));
        fullItemList.Add(new Item("Fish", 2.0f));
        fullItemList.Add(new Item("Gems", 0.3f));
        fullItemList.Add(new Item("Hat", 0.6f));
        fullItemList.Add(new Item("Ingot", 5.0f));
        fullItemList.Add(new Item("Junk", 1.2f));
    }

    void InitialiseFullItemList()
    {
        GameObject gameObject;

        for (int i = 0; i < fullItemList.Count; i++)
        {
            gameObject = Instantiate(itemPrefab, transform);
            gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = fullItemList[i].Name;
            gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = fullItemList[i].Weight.ToString();
            gameObject.GetComponent<Button>().AddEventListener(i, ItemClicked);
        }
    }

    void InitialiseInventoryItemList()
    {
        ClearInventoryItemList();
        GameObject gameObject;

        for (int i = 0; i < inventoryItemList.Count; i++)
        {
            gameObject = Instantiate(itemPrefab, inventoryTransform);
            gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = inventoryItemList[i].Name;
            gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = inventoryItemList[i].Weight.ToString();
            gameObject.GetComponent<Button>().AddEventListener(i, InventoryItemClicked);
        }
    }


    void ClearInventoryItemList()
    {
        foreach (Transform child in inventoryTransform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    void ItemClicked(int index)
    {
        Debug.Log("Item Cicked: " + index + ". " + fullItemList[index].Name + " (" + fullItemList[index].Weight + ")");
        AddItemToInventory(index);

    }

    void InventoryItemClicked(int index)
    {
        Debug.Log("Item Cicked: " + index + ". " + inventoryItemList[index].Name + " (" + inventoryItemList[index].Weight + ")");
    }

    void AddItemToInventory(int index)
    {
        Item item = new Item(fullItemList[index].Name, fullItemList[index].Weight);
        inventoryItemList.Add(item);

        InitialiseInventoryItemList();
    }
    //***Searching***

    //LinearSearching



    public static bool LinearSearch(List<Item> inventory, string value)
    {
        Debug.Log("Linear Search on :" + value);

        for (int i = 0; i < inventory.Count; i++)
        {
            if (string.Compare(inventory[i].Name, value) == 0)
            {
                Debug.Log("Linear Search returns : true");
                return true;
            }
        }

        Debug.Log("Linear Search returns : false");
        return false;
    }

    /*    public void LinearButton()
        {
            LinearSearch(inventoryItemList, searchBar.text);

            InitialiseInventoryItemList();

        }*/

    /*Binary Search*/
    public static bool BinarySearch(list<Item> inventory, string value)
    {
        int left = 0, right = args.Length, middle = 0;

        while left < right)
        {
            middle = (left + right) / 2;
            if (targetVal == args[middle])
            {
                debug.Log("Target Found:" + targetVal;
            }
            else if (targetVal > args[middle])
            {
                left = middle + 1;
            }
            else
            {
                right = middle - 1;
            }
        }
        Debug.log("Target not found in inventory
    }

    //Sorting

    //Bubble Weight

    List<Item> BubbleWeight(List<Item> list)
    {
        int n = list.Count;
        Item temp;

        bool swapped = true;

        while (swapped)
        {
            swapped = false;
            for (int i = 0; i < n - 1; i++)
            {
                if (list[i].Weight > list[i + 1].Weight)
                {

                    temp = list[i];
                    list[i] = list[i + 1];
                    list[i + 1] = temp;
                    swapped = true;
                }
            }
        }
        return list;
    }

    public void SortBubbleWeightButton()
    {
        Debug.Log("Start: " + Time.time * 1000);
        inventoryItemList = BubbleWeight(inventoryItemList);
        Debug.Log("End: " + Time.time * 1000);

        InitialiseInventoryItemList();
    }
    //     BUBBLE NAME
    List<Item> BubbleName(List<Item> list)
    {
        int n = list.Count;
        Item temp;

        bool swapped = true;

        while (swapped)
        {
            swapped = false;
            for (int i = 0; i < n - 1; i++)
            {
                if (string.Compare(list[i + 1].Name, list[i + 1].Name) > 0)
                {
                    temp = list[i];
                    list[i] = list[i + 1];
                    list[i + 1] = temp;
                    swapped = true;
                }
            }
        }
        return list;

    }
    public void SortBubbleButton()
    {
        Debug.Log("Start: " + Time.time * 1000);
        inventoryItemList = BubbleWeight(inventoryItemList);
        Debug.Log("End: " + Time.time * 1000);

        Debug.Log("Start: " + Time.time * 1000);
        inventoryItemList = BubbleName(inventoryItemList);
        Debug.Log("End: " + Time.time * 1000);

        InitialiseInventoryItemList();
    }
    //Merge sort 
    public void MergeSort(List<Item> list)
    {
        int[] left;
        int[] right;
        int[] results = new int[list.Length];

        if (list.length <= 1)
            return list;
        int midPoint = list.length / 2;
        left = new int[midPoint];

        if (list.Length % 2 == 0)
            right = new int[midPoint];

        else
            right = new int[midPoint + 1];

        for (int i = 0; i < midPoint; i++)
            left[i] = list[i];

        int x = 0;

        for (int i = midPoint; i < array.Length; i++)
        {
            right[x] = array[i];
            x++;
        }
        left = MergeSort(left);

        right = MergeSort(right);

        results = MergeSort(left, right);
        return results;
    }
}




public static class ButtonExtension
{
    public static void AddEventListener<T>(this Button button, T param, Action<T> OnClick)
    {
        button.onClick.AddListener(delegate () { OnClick(param); });
    }
}

public class Item
{
    public string Name { get; set; }
    public float Weight { get; set; }

    public Item(string name, float weight)
    {
        Name = name;
        Weight = weight;
    }
}