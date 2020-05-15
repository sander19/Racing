using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Controls : MonoBehaviour
{
    public float speed = 10f; 
    public float maxSpeed = 30f; 
    public float minSpeed = 5f; 
    public float sideSpeed = 0f; 

    public float scores = 0f; 
    public float highScore = 0f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        using (FileStream fstream = File.OpenRead($"D:/highscore.txt"))
        {
            byte[] array = new byte[fstream.Length];
            fstream.Read(array, 0, array.Length);
            string textFromFile = System.Text.Encoding.Default.GetString(array);
            Debug.Log(textFromFile);
            highScore = float.Parse(textFromFile);
        }
        
        float moveSide = Input.GetAxis("Horizontal"); 
        float moveForward = Input.GetAxis("Vertical"); 
    
        if(moveSide != 0)
        {
            sideSpeed = moveSide * -0.1f; 
        }
        
        if(moveForward != 0)
        {
            speed += 0.1f * moveForward; 
        }
        else 
        {
            if(speed > 0)
            {
                speed -= 0.1f;
            }
            else
            {
                speed += 0.1f;
            }
        }
    
        if(speed > maxSpeed)
        {
            speed = maxSpeed; 
        }
        if(speed < minSpeed)
        {
            speed = minSpeed; 
        }
    
    }
}
