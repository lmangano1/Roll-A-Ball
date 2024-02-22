using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody rb;
    private int PickUpCount;
    private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Get the number of pick ups in our scene
        PickUpCount = GameObject.FindGameObjectsWithTag("Pick Up").Length;
        //Run the Check Picks Ups Function
        CheckPickUps();
        //Get the timer object and start the timer
        timer = FindAnyObjectByType<Timer>();
        timer.StartTimer();
    }

    
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pick Up")
        {
            //Destroy the collided object
            Destroy(other.gameObject);
            //Decrement the Pick Up count
            PickUpCount--;

            CheckPickUps();
        }
    }

    void CheckPickUps()
    {
        print("Pick Ups Left: " + PickUpCount);
        if (PickUpCount == 0)
        {
            timer.StopTimer();
            print("Yay! You Won. Your time was: " + timer.GetTime());
        }
    }
}