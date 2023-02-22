using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetect : MonoBehaviour
{
    //This script will apply force to the rigidbody
    //In the direction that you swiped.

    public BoxCollider2D boxCollider;
    public Rigidbody2D rb;

    public bool isSwiping = false;

    public Vector2 startPos;    //Position where the swipe starts
    public Vector2 endPos;      //Position where the swipe ends
    public Vector2 swipeVector; //Vector from startPos to endPos

    public float swipeMin;      //the minimum magnitude to be a swipe

    public float moveForce;     //how much force to move when swiped

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //If at least one touch is occuring
        if (Input.touchCount > 0)
        {
            //Get the first touch
            Touch touch = Input.GetTouch(0);
            //Get the position of the touch
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            //If this is the start of the touch...
            if (touch.phase == TouchPhase.Began && boxCollider.OverlapPoint(touchPos))
            {
                isSwiping = true;
                startPos = touchPos;
            }
            //if this is the END of the touch
            else if (touch.phase == TouchPhase.Ended && isSwiping)
            {
                isSwiping = false;
                endPos = touchPos;
                Swipe();
            }
        }
    }

    public void Swipe()
    {
        //Find the Swipe Vector
        swipeVector = endPos - startPos;

        //Only swipe if the magnitude is more than the min.
        if (swipeVector.magnitude >= swipeMin)
        {
            //Determine if the swipe was horizontal or vertical
            if (Mathf.Abs(swipeVector.x) > Mathf.Abs(swipeVector.y))
            {
                //This means I am swiping left or right

                //if x is positive, I swiped right
                if (swipeVector.x > 0)
                {
                    //Add right force
                    rb.AddForce(Vector2.right * moveForce, ForceMode2D.Impulse);
                }
                else
                {
                    //add left force
                    rb.AddForce(Vector2.left * moveForce, ForceMode2D.Impulse);
                }
            }
            else
            {
                //This means I am swiping up or down

                // if y is positive, I am swiping up
                if (swipeVector.y > 0)
                {
                    //Add up force
                    rb.AddForce(Vector2.up * moveForce, ForceMode2D.Impulse);
                }
                else
                {
                    //add down force
                    rb.AddForce(Vector2.down * moveForce, ForceMode2D.Impulse);
                }
            }
            //extra comment
            //one more extra comment
        }
    }
}
