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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
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

        if(actionInput.action.WasPressedThisFrame())
        {
            UseTool();
        }
    }

    void UseTool()
    {
        GrowBlock block = null;

        block = FindFirstObjectByType<GrowBlock>();
        block.PloughSoil();
    }
}
