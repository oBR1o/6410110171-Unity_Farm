using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public GameObject plantItem;
    List<PlantObjectScripts> plantObjects = new List<PlantObjectScripts>();
    private void Awake() 
    {
        //Assets/Resources/Plants
        var loadPlants = Resources.LoadAll("Plants",typeof(PlantObjectScripts));
        foreach (var plant in loadPlants)
        {
            plantObjects.Add((PlantObjectScripts)plant);
        }
        plantObjects.Sort(SortByPrice);

        foreach (var plant in plantObjects)
        {
            PlantItem newPlant = Instantiate(plantItem, transform).GetComponent<PlantItem>();
            newPlant.plant = plant;
        }
    }

    int SortByPrice(PlantObjectScripts plantObject1, PlantObjectScripts plantObject2)
    {
        return plantObject1.buyPrice.CompareTo(plantObject2.buyPrice);

    }

    int SortByTime(PlantObjectScripts plantObject1, PlantObjectScripts plantObject2)
    {
        return plantObject1.timeBtwStage.CompareTo(plantObject2.timeBtwStage);

    }
}
    
