using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldDetect : MonoBehaviour
{
    //Declare variables for the components
    public BoxCollider2D boxCollider;
    public Rigidbody2D rb;

    public bool isHolding = false;

    public float jumpForce;

    //The number of seconds I held the object
    public float holdTime;

    //The number of seconds before the event happens
    public float holdLimit;

    // Start is called before the first frame update
    void Start()
    {

        //Get the components
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Is at least one touch occuring?
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            //Where is the touch Located?
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            //IF the touching just started AND the touch is within the collider
            if (touch.phase == TouchPhase.Began && boxCollider.OverlapPoint(touchPos))
            {
                //Set isHolding to true
                isHolding = true;

                //Reset hold time
                holdTime = 0;
            }

            //If he touch is stationary add to the holdTime
            else if (touch.phase == TouchPhase.Stationary && isHolding)
            {
                holdTime += Time.deltaTime;
            }

            //Set is holding to false when the touch ends
            else if (touch.phase == TouchPhase.Ended && isHolding)
            {
                isHolding = false;
            }

            //When the holdTime reaches the limit launch the Item
            if(holdTime >= holdLimit)
            {
                holdTime = 0;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isHolding = false;
            }

        }

    }
}
