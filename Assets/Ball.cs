using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;

public class Ball : MonoBehaviour
{
    public int score;
    public TMP_Text scoreText;
    public GameObject pauseText;
    private bool countdown;
    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        countdown = false;
        timer = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
        var  speed = 10f * Time.deltaTime;
        
        var horizMove = Input.GetAxisRaw("Horizontal") * speed;
        var vertMove = Input.GetAxisRaw("Vertical") * speed;
        transform.position += new Vector3(horizMove, 0, vertMove);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.timeScale == 0)
            {
                pauseText.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                pauseText.SetActive(true);
                Time.timeScale = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            countdown = true; 
        }

        if (countdown)
        {
            // timer stuff
            if (timer < 3)
            {
                timer += Time.deltaTime;
                Debug.Log(timer);
            }
            else
            {
                //Application.Quit();
                //This is how we do it in the editor, above would be for complied game
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Whoa, it triggered me!");
        float newX = Random.Range(-12, 12);
        float newZ = Random.Range(-5, 5);
        other.transform.position = new Vector3(newX, 0, newZ);
        score++;
        scoreText.text = "Score: " + score;


        other.transform.rotation = new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360),0);

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("No longer triggered");
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Still triggered");
    }
}
