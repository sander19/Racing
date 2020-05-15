using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Moving : MonoBehaviour
{

	public Rigidbody rb;
	public GameObject car; 

	public GameObject brokenPrefab; 
	public GameObject modelHolder;

	public Controls control; 

	private float speed = 0.1f; 

	private float maxSpeed; 
	private float minSpeed; 

	private bool isAlive = true; 
	private bool isKilled = false; 

	public List<GameObject> wheels; 

    private System.Random rand = new System.Random(); 

	void Start() 
	{
		rb = GetComponent<Rigidbody>();
		maxSpeed = control.maxSpeed;
        minSpeed = control.minSpeed;
	}

	void Update()
	{
		if(isAlive)
		{
			float newSpeed = speed; 
			float sideSpeed = 0f; 
			if(control != null) 
			{
				newSpeed += control.speed; 
				sideSpeed = control.sideSpeed; 

				control.scores += (newSpeed - minSpeed) / 100; 
                if(newSpeed > maxSpeed)
                {
                    newSpeed = maxSpeed; 
                }

                if(newSpeed < minSpeed)
                {
                    newSpeed = minSpeed; 
                }

				rb.velocity = new Vector3(newSpeed, 0, newSpeed * sideSpeed);

			} else {

		        newSpeed = 0.1f;
				transform.position = new Vector3(transform.position.x + newSpeed, transform.position.y, transform.position.z);
	            }

			if(control != null)
			{
				control.sideSpeed = 0f; 
			}

			if(wheels.Count > 0) 
			{
				foreach (var wheel in wheels)
				{
					wheel.transform.Rotate(-3f, 0f, 0f);
				}
			}

			if(tag == "Car") 
			{
				if(transform.position.y < -50f)
				{
                    Destroy(gameObject.GetComponent<BoxCollider>());
					Destroy(gameObject);
				}
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
	    if(other.tag == "Car" || other.tag == "Wall") 
		{
			isAlive = false; 

			if(car != null) 
			{
				if(!isKilled) 
				{
                    rb.velocity = new Vector3(0, 0, 0);
					Destroy(car);

					var broken = Instantiate(brokenPrefab, transform.position, Quaternion.Euler(new Vector3(0f, -270f, 0f)));
					broken.transform.SetParent(modelHolder.transform);

					isKilled = true; 
					StartCoroutine("Die"); 
				}
				

			}
		}

	}

	IEnumerator Die() 
	{
		
		if(Math.Floor(control.highScore) < Math.Floor(control.scores))
		{
            string text =  Math.Floor(control.scores).ToString();
            using (FileStream fstream = new FileStream($"D:/highscore.txt", FileMode.OpenOrCreate))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(text);
                fstream.Write(array, 0, array.Length);
            }
		}
		
		yield return new WaitForSeconds(4f);
	    SceneManager.LoadScene("Menu"); 
	}
}