using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDetect : MonoBehaviour
{
    //Declare variables for the components
    public BoxCollider2D boxCollider;
    public Rigidbody2D rb;

    public bool isDragging = false;

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
                //Set iss Dragging to True
                isDragging = true;
                //Make it kinematic
                rb.isKinematic = true;
                //Stop the Velocity
                rb.velocity = Vector2.zero;
            }

            //If he touch is moving, Move the object
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                gameObject.transform.position = touchPos;
            }

            //When you release the touch, Make it Not Kenematic again
            else if (touch.phase == TouchPhase.Ended && isDragging)
            {
                //Set isDragging to False
                isDragging = false;
                ///Make it not kenematic
                rb.isKinematic = false;
            }
        }

    }
}
