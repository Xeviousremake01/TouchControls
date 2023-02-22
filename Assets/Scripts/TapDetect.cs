using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapDetect : MonoBehaviour
{
    //Declare variables for components
    public BoxCollider2D boxCollider;
    public Rigidbody2D rb;

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

            //If the touching just started AND the touch is within the collider
            if(touch.phase == TouchPhase.Began && boxCollider.OverlapPoint(touchPos))
            {
                //Tap successful. Destroying Square
                Destroy(gameObject);
            }
        }
        
    }
}
