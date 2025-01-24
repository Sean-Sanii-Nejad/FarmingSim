using UnityEngine;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AdvanceStage();
    }

    public void AdvanceStage()
    {
        currentStage+=1;

        if(currentStage >= GrowthStage.ripe+1)
        {
            currentStage = GrowthStage.barren;
        }
    }
}
