using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Plant", menuName = "Plant")]
public class PlantObjectScripts : ScriptableObject
{
    public string plantName;
    public Sprite[] plantStages;
    public float timeBtwStage;
    public int buyPrice;
    public int sellPrice;
    public Sprite icon;
    public Sprite dryPlanted;

}
