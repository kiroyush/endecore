using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

public class DataHandler : MonoBehaviour
{
    private GameObject furniture;
    [SerializeField] private ButtonManager buttonPrefab;//hold all the info about the auto loaded buttons
    [SerializeField] private GameObject buttonContainer;// actual holder of the buttons
    [SerializeField] private List<Item> items;

    [SerializeField] private String label;
    private int current_id = 0;

    private static DataHandler instance;
    public static DataHandler Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<DataHandler>();

            }
            return instance;
        }

    }

    public object AsyncOperationHandler { get; private set; }

    private async void Start()
    {
        // LoadItems();
        await Get(label);
        CreateButtons();
        
    }
    
    //responsible for the creation of the buttons based on the items that we have
    //each button must have a unique id and a button, so to determine which particular object is currently selected.
    void CreateButtons()
    {
        foreach ( Item  i in items)
        {
            ButtonManager b = Instantiate(buttonPrefab, buttonContainer.transform);
            b.ItemId = current_id;
            b.ButtonTexture = i.itemImage;
            current_id++;
        }
    }
    public void SetFurniture (int id)
    {
        furniture = items[id].itemPrefab;
    }
    public GameObject GetFurniture()
    {
        return furniture;
    }
    public async Task Get(String label)
    {
        var locations = await Addressables.LoadResourceLocationsAsync(label).Task;
        foreach (var location in locations)
        {
            var obj =  await Addressables.LoadAssetAsync<Item>(location).Task;
            items.Add(obj);
        }
    }
}
