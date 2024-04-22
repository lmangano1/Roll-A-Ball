using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;


public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody rb;
    private int PickUpCount;
    private Timer timer;
    private bool gameOver = false;
    bool grounded = true;

    public GameObject boosterCamera;


    [Header("UI")]
    public TMP_Text PickUpText;
    public TMP_Text timerText;
    public TMP_Text WinTimeText;
    public GameObject WinPanel;
    public GameObject InGamePanel;

    // Start is called before the first frame update
    void Start()
    {
        //Turn on our In Game Panel
        InGamePanel.SetActive(true);
        //Turn off our Win Panel
        WinPanel.SetActive(false);

        rb = GetComponent<Rigidbody>();
        //Get the number of pick ups in our scene
        PickUpCount = GameObject.FindGameObjectsWithTag("Pick Up").Length;
        //Run the Check Picks Ups Function
        CheckPickUps();
        //Get the timer object and start the timer
        timer = FindAnyObjectByType<Timer>();
        timer.StartTimer();
    }

    private void Update()
    {
        timerText.text = "Time: " + timer.GetTime().ToString("F2");
    }

    void FixedUpdate()
    {

        if (gameOver == true)
            return;
        if (grounded)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
            rb.AddForce(movement * speed);
        }  
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ground")) 
        {
            boosterCamera.SetActive(false);
            grounded = true;

        }
           
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            boosterCamera.SetActive(true);
            grounded = true;
        }
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
        PickUpText.text = "Pick Ups Left: " + PickUpCount;
        if (PickUpCount == 0)
        {
            WinGame();
        }
    }

    void WinGame ()
    {
        //Set our game over to true
        gameOver = true;
        //Turn off our In Game Panel
        InGamePanel.SetActive(false);
        //Turn on our Win Panel
        WinPanel.SetActive(true);
        PickUpText.color = Color.green;
        PickUpText.fontSize = 60;
        //Stop the timer
        timer.StopTimer();
        // Display our time to the win time text
        WinTimeText.text = "Your time was: " + timer.GetTime().ToString("F2");

        //Stop the ball from moving
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    //Temporary - Remove when doing A2 modules
    public void RestartGame()
    {
    UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}