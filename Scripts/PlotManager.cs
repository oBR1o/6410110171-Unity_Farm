using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ploting : MonoBehaviour
{
    bool isPlanted = false;
    public SpriteRenderer plant;
    BoxCollider2D plantCollider;
    int plantStage = 0; 
    float timer;
    public Color availableColor = Color.green;
    public Color unavailableColor = Color.red;

    SpriteRenderer coin;
    SpriteRenderer plot;
    PlantObjectScripts selectedPlant;
    FarmManager fm;
    MouseHoldClick mhc;
    bool isDry = true;
    public Sprite drySprite;
    public Sprite normalSprite;
    public Sprite unavailableSprite;

    float speed = 1f;
    public bool isBought = true;
    bool isCoin = false;
    float coinTimer;
    void Start()
    {
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        coin = transform.GetChild(1).GetComponent<SpriteRenderer>();
        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        fm = transform.parent.GetComponent<FarmManager>();
        mhc = FindObjectOfType<MouseHoldClick>();
        plot = GetComponent<SpriteRenderer>();
        if(isBought)
        {
            plot.sprite = drySprite;
        }
        else
        {
            plot.sprite = unavailableSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlanted && !isDry)
        {
            timer -= speed*Time.deltaTime;
            if (timer < 0 && plantStage <selectedPlant.plantStages.Length -1 )
            {
                timer = selectedPlant.timeBtwStage;
                plantStage++;
                UpdatePlant();
            }
        }
        if(isCoin)
        {
            coinTimer -= Time.deltaTime;
            if(coinTimer <0 ){
                coin.gameObject.SetActive(false);
                isCoin = false;
            }
        }
    }
    private void OnMouseDown()
    {
        if (isPlanted)
        {
            if(plantStage == selectedPlant.plantStages.Length -1 && !fm.isPlanting && !fm.isSelecting)
            {
                
            }
        }
        else if(fm.isPlanting && fm.selectPlant.plant.buyPrice <= fm.money && isBought && !isCoin)
        {
            Plant(fm.selectPlant.plant);
        }
        if(fm.isSelecting)
        {
            switch(fm.selectedTool)
            {
                case 1:
                    if(isBought)
                    {
                        isDry = false;
                        plot.sprite = normalSprite;
                        if(isPlanted) UpdatePlant();
                    }
                    break;
                case 2:
                    if(fm.money >=10 && isBought)
                    {
                        fm.Transaction(-10);
                        if(speed < 2) speed +=.2f;
                    }
                    break;
                case 3:
                    if(fm.money >= 400 && !isBought)
                    {
                        fm.Transaction(-400);
                        isBought = true;
                        plot.sprite = drySprite;
                    }
                    break;
                case 4:
                    if(isBought && isPlanted)
                    {
                        if(plantStage == selectedPlant.plantStages.Length -1 && !fm.isPlanting )
                        {
                             Harvest();
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        if(isCoin && !fm.isSelecting && !fm.isPlanting){
            GetCoin();
        }

    }
    private void OnMouseOver() 
    {
        if(fm.isPlanting)
        {
            if(isPlanted || fm.selectPlant.plant.buyPrice > fm.money || !isBought || isCoin)
            {
                //can't buy
                plot.color = unavailableColor;
            }else
            {
                //can buy
                plot.color = availableColor;
                if(mhc.isMouseHold){
                    Plant(fm.selectPlant.plant);
                    plot.color = unavailableColor;
                }
            }
        }
        if(fm.isSelecting)
        {
            switch(fm.selectedTool)
            {
                case 1:
                    if(isBought)
                    {
                        plot.color = availableColor;
                        if(mhc.isMouseHold){
                            isDry = false;
                            plot.sprite = normalSprite;
                            if(isPlanted)
                            {
                                UpdatePlant();
                            }
                        }  
                    }
                    break;
                case 2:
                    if(isBought && fm.money>=(fm.selectedTool-1)*10)
                    {
                        plot.color = availableColor;
                    }
                    else
                    {
                        plot.color = unavailableColor;
                    }
                    break;
                case 3:
                    if(!isBought && fm.money >=100)
                    {
                        plot.color = availableColor;
                    }
                    else
                    {
                        plot.color = unavailableColor;
                    }
                    break;
                case 4:
                    if(isBought && isPlanted)
                    {
                        plot.color = availableColor;
                         if(mhc.isMouseHold)
                            if(plantStage == selectedPlant.plantStages.Length -1 && !fm.isPlanting)
                            {
                                Harvest();
                            }
                    }
                    else
                    {
                        plot.color = unavailableColor;
                    }
                    break;
                default:
                    plot.color = availableColor;
                    break;
            }
        }
        if(isCoin && !fm.isSelecting && !fm.isPlanting && mhc.isMouseHold){
            GetCoin();
        }

        
    }

    private void OnMouseExit() 
    {
        plot.color = Color.white;    
    }
    void Harvest()
    {
        isPlanted = false;
        plant.gameObject.SetActive(false);
        coin.gameObject.SetActive(true);
        coinTimer = 10f;
        isCoin = true;
        isDry = true;
        plot.sprite = drySprite;
        speed = 1f;
    }
    void Plant(PlantObjectScripts newPlant)
    {   
        selectedPlant = newPlant;
        isPlanted = true;

        fm.Transaction(-selectedPlant.buyPrice);
        plantStage = 0;
        UpdatePlant();
        timer = selectedPlant.timeBtwStage;
        plant.gameObject.SetActive(true);

    }
    void UpdatePlant()
    {
        if(isDry)
        {
            plant.sprite = selectedPlant.dryPlanted;
        }
        else
        {
            plant.sprite = selectedPlant.plantStages[plantStage];
        }
        plantCollider.size = plant.sprite.bounds.size;
        plantCollider.offset = new Vector2(0,plant.bounds.size.y/2);
    }

    void GetCoin()
    {
        isCoin = false;
        coin.gameObject.SetActive(false);
        fm.Transaction(+selectedPlant.sellPrice);
    }
}
