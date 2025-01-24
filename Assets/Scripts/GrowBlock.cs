using UnityEngine;
using UnityEngine.InputSystem;

public enum GrowthStage
{
    barren,
    ploughed,
    planted,
    growing1,
    growing2,
    ripe
}

public class GrowBlock : MonoBehaviour
{
    public GrowthStage currentStage;
    public SpriteRenderer theSR;
    public Sprite soilTilled;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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
        else if (currentStage == GrowthStage.ploughed)
        {
            theSR.sprite = soilTilled;
        }
    }

    public void PloughSoil()
    {
        if(currentStage == GrowthStage.barren)
        {
            currentStage = GrowthStage.ploughed;

            SetSoilSprite();
        }
    }
}
