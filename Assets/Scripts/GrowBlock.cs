using UnityEngine;
using UnityEngine.InputSystem;

public class GrowBlock : MonoBehaviour
{
    public enum GrowthStage
    {
        barren,
        ploughed,
        planted,
        growing1,
        growing2,
        ripe
    }
    public GrowthStage currentStage;
    public SpriteRenderer theSR;
    public Sprite soilTilled, soilWatered;

    public SpriteRenderer cropSR;
    public Sprite cropPlanted, cropGrowing1, cropGrowing2, cropRipe;

    public bool isWatered;

    public bool preventUse;

    private Vector2Int gridPosition;

    public CropController.CropType cropType;
    public float growthFailChance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_EDITOR
        if(Keyboard.current.qKey.wasPressedThisFrame)
        {
            AdvanceCrop();
        }
        #endif
    }

    public void AdvanceStage()
    {
        currentStage+=1;
        if(currentStage >= GrowthStage.ripe+1)
        {
            currentStage = GrowthStage.barren;
        }
    }

    public void SetSoilSprite()
    {
        if(currentStage == GrowthStage.barren)
        {
            theSR.sprite = null;
        }
        else 
        {
            if(isWatered == true)
            {
                theSR.sprite = soilWatered;
            }
            else
            {
                theSR.sprite = soilTilled;
            }
        }
        UpdateGridInfo();
    }

    public void PloughSoil()
    {
        if(currentStage == GrowthStage.barren && preventUse == false)
        {
            currentStage = GrowthStage.ploughed;
            SetSoilSprite();
        }
    }

    public void WaterSoil()
    {
        if(preventUse == false)
        {
            isWatered = true;
            SetSoilSprite();
        } 
    }

    public void PlantCrop(CropController.CropType cropToPlant)
    {
        if(currentStage == GrowthStage.ploughed && isWatered && preventUse == false)
        {
            currentStage = GrowthStage.planted;
            cropType = cropToPlant;
            growthFailChance = CropController.instance.GetCropInfo(cropType).growthFailChance;

            CropController.instance.UseSeed(cropToPlant);

            UpdateCropSprite();
        }
    }

    public void UpdateCropSprite()
    {
        CropInfo activeCrop = CropController.instance.GetCropInfo(cropType);
        switch(currentStage)
        {
            case GrowthStage.planted:
                cropSR.sprite = activeCrop.planted;
                break;
            case GrowthStage.growing1:
                cropSR.sprite = activeCrop.growStage1;
                break;
            case GrowthStage.growing2:
                cropSR.sprite = activeCrop.growStage2;
                break;
            case GrowthStage.ripe:
                cropSR.sprite = activeCrop.ripe;
                break;
        }
        UpdateGridInfo();
    }

    public void AdvanceCrop()
    {
        if(isWatered && preventUse == false)
        {
            if(currentStage == GrowthStage.planted || currentStage == GrowthStage.growing1 || currentStage == GrowthStage.growing2)
            {
                currentStage++;
                isWatered = false;
                SetSoilSprite();
                UpdateCropSprite();
            }
        }
    }

    public void HarvestCrop()
    {
        if(currentStage == GrowthStage.ripe && preventUse == false)
        {
            currentStage = GrowthStage.ploughed;
            SetSoilSprite();
            cropSR.sprite = null;

            CropController.instance.AddCrop(cropType);
        }
    }

    public void SetGridPosition(int x, int y)
    {
        gridPosition = new Vector2Int(x, y);
    }

    void UpdateGridInfo()
    {
        GridInfo.instance.UpdateInfo(this, gridPosition.x, gridPosition.y);
    }
}
