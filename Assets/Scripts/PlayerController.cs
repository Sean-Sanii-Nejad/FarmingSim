using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D theRB;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]    
    private InputActionReference moveInput, actionInput;

    [SerializeField]
    private Animator anim;

    public enum ToolType 
    {
        plough,
        wateringCan,
        seeds,
        basket
    }

    public ToolType currentTool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        UIController.instance.SwitchTool((int) currentTool);
    }

    // Update is called once per frame
    void Update()
    {
        theRB.linearVelocity = moveInput.action.ReadValue<Vector2>().normalized * moveSpeed;
        anim.SetFloat("speed", theRB.linearVelocity.magnitude);

        if(theRB.linearVelocity.x < 0f) // Moving Left
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if(theRB.linearVelocityX > 0f) // Moving Right
        {
            transform.localScale = Vector3.one;
        }

        bool hasSwitchedTool = false;
        if(Keyboard.current.tabKey.wasPressedThisFrame)
        {
            currentTool++;
            if((int) currentTool >= 4)
            {
                currentTool = ToolType.plough;
            }

            hasSwitchedTool = true;
        }

        if(Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            currentTool = ToolType.plough;
            hasSwitchedTool = true;
        }

        if(Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            currentTool = ToolType.wateringCan;
            hasSwitchedTool = true;
        }

        if(Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            currentTool = ToolType.seeds;
            hasSwitchedTool = true;
        }

        if(Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            currentTool = ToolType.basket;
            hasSwitchedTool = true;
        }

        if(actionInput.action.WasPressedThisFrame())
        {
            UseTool();
        }

        if(hasSwitchedTool)
        {
            UIController.instance.SwitchTool((int) currentTool);
        }
        hasSwitchedTool = false;
    }

    void UseTool()
    {
        GrowBlock block = null;

        block = FindFirstObjectByType<GrowBlock>();
        
        if(block != null)
        {
            switch(currentTool)
            {
                case ToolType.plough: 
                        block.PloughSoil();
                    break;

                case ToolType.wateringCan: 

                    break;

                case ToolType.seeds: 

                    break;

                case ToolType.basket: 

                    break;
            }
        }
    }
}
