using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

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

    [SerializeField]
    TMP_InputField inputField;

    void Start()
    {
        DefineItems();
        InitialiseFullItemList();
    }

    void Update()
    {

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
    public void ClearAllItems()
    {
        inventoryItemList.Clear();
        ClearInventoryItemList();
        Debug.Log("All items have been removed from inventory.");
    }

    void ItemClicked(int index)
    {
        Debug.Log("Item Cicked: " + index + ". " + fullItemList[index].Name + " (" + fullItemList[index].Weight + ")");
        AddItemToInventory(index);
    }

    void InventoryItemClicked(int index)
    {
        Debug.Log("Inventory Item Clicked: " + index + ". " + inventoryItemList[index].Name + " (" + inventoryItemList[index].Weight + ")");
    }

    void AddItemToInventory(int index)
    {
        Item item = new Item(fullItemList[index].Name, fullItemList[index].Weight);
        inventoryItemList.Add(item);
        InitialiseInventoryItemList();
    }

    public void SortInventoryByWeight()
    {
        BubbleSortByWeight();
        InitialiseInventoryItemList();
    }

    public void SortInventoryByName()
    {
        BubbleSortByName();
        InitialiseInventoryItemList();
    }
   
   
    /// Version 1


    //linear search
    public void RemoveItemByName()
    {
        string itemName = inputField.text;
        int index = inventoryItemList.FindIndex(item => item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        if (index != -1)
        {
            inventoryItemList.RemoveAt(index);
            Debug.Log(itemName + " has been removed from inventory.");
            InitialiseInventoryItemList();
        }
        else
        {
            Debug.Log("Item not found: " + itemName);
        }
    }
    //Bubble sort

    void BubbleSortByWeight()
    {
        Stopwatch stopwatch = Stopwatch.StartNew(); // Start timing

        for (int i = 0; i < inventoryItemList.Count - 1; i++)
        {
            for (int j = 0; j < inventoryItemList.Count - i - 1; j++)
            {
                if (inventoryItemList[j].Weight > inventoryItemList[j + 1].Weight)
                {
                    // Swap the items
                    Item temp = inventoryItemList[j];
                    inventoryItemList[j] = inventoryItemList[j + 1];
                    inventoryItemList[j + 1] = temp;
                }
            }
        }

        stopwatch.Stop(); // Stop timing
        Debug.Log("Time: " + stopwatch.ElapsedMilliseconds + " ms");
    }

    void BubbleSortByName()
    {
        Stopwatch stopwatch = Stopwatch.StartNew(); // Start timing

        for (int i = 0; i < inventoryItemList.Count - 1; i++)
        {
            for (int j = 0; j < inventoryItemList.Count - i - 1; j++)
            {
                if (string.Compare(inventoryItemList[j].Name, inventoryItemList[j + 1].Name) > 0)
                {
                    // Swap the items
                    Item temp = inventoryItemList[j];
                    inventoryItemList[j] = inventoryItemList[j + 1];
                    inventoryItemList[j + 1] = temp;
                }
            }
        }

        stopwatch.Stop(); // Stop timing
        Debug.Log("Time: " + stopwatch.ElapsedMilliseconds + " ms");
    }

    //Version 2

    //Binary Search

    public void BinarySearchAndRemove()
    {
        string searchName = inputField.text;
        int index = BinarySearch(inventoryItemList, searchName);
        if (index != -1)
        {
            inventoryItemList.RemoveAt(index);
            Debug.Log(searchName + " has been removed from inventory.");
            InitialiseInventoryItemList();
        }
        else
        {
            Debug.Log("Item not found: " + searchName);
        }
    }

    private int BinarySearch(List<Item> items, string searchName)
    {
        int low = 0;
        int high = items.Count - 1;
        while (low <= high)
        {
            int mid = (low + high) / 2;
            int result = string.Compare(items[mid].Name, searchName, StringComparison.OrdinalIgnoreCase);
            if (result == 0)
            {
                return mid; // Item found
            }
            else if (result < 0)
            {
                low = mid + 1;
            }
            else
            {
                high = mid - 1;
            }
        }
        return -1; // Item not found
    }

    //Sorting

    // Merge Sort by Weight
    void MergeSortByWeight()
    {
        Stopwatch stopwatch = Stopwatch.StartNew(); // Start timing

        inventoryItemList = MergeSort(inventoryItemList, (item1, item2) => item1.Weight.CompareTo(item2.Weight));

        stopwatch.Stop(); // Stop timing
        Debug.Log("Time: " + stopwatch.ElapsedMilliseconds + " ms");
    }

    // Merge Sort by Name
    void MergeSortByName()
    {
        Stopwatch stopwatch = Stopwatch.StartNew(); // Start timing

        inventoryItemList = MergeSort(inventoryItemList, (item1, item2) => item1.Name.CompareTo(item2.Name));

        stopwatch.Stop(); // Stop timing
        Debug.Log("Time: " + stopwatch.ElapsedMilliseconds + " ms");
    }

    // Merge Sort Implementation
    private List<Item> MergeSort(List<Item> list, Comparison<Item> comparison)
    {
        if (list.Count <= 1) return list;

        int middleIndex = list.Count / 2;
        var left = MergeSort(list.GetRange(0, middleIndex), comparison);
        var right = MergeSort(list.GetRange(middleIndex, list.Count - middleIndex), comparison);

        return Merge(left, right, comparison);
    }

    private List<Item> Merge(List<Item> left, List<Item> right, Comparison<Item> comparison)
    {
        List<Item> result = new List<Item>();
        int leftIndex = 0, rightIndex = 0;
        while (leftIndex < left.Count && rightIndex < right.Count)
        {
            if (comparison(left[leftIndex], right[rightIndex]) <= 0)
            {
                result.Add(left[leftIndex++]);
            }
            else
            {
                result.Add(right[rightIndex++]);
            }
        }

        result.AddRange(left.GetRange(leftIndex, left.Count - leftIndex));
        result.AddRange(right.GetRange(rightIndex, right.Count - rightIndex));
        return result;
    }

    public void AddRandomItems()
    {
        string[] names = { "Axe", "Bandage", "Crossbow", "Dagger", "Emerald", "Fish", "Gems", "Hat", "Ingot", "Junk" };
        System.Random random = new System.Random();

        for (int i = 0; i < 10000; i++)
        {
            string randomName = names[random.Next(names.Length)];
            float randomWeight = (float)Math.Round(random.NextDouble() * 5.0 + 0.1, 1);
            inventoryItemList.Add(new Item(randomName, randomWeight));
        }

        InitialiseInventoryItemList();
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
