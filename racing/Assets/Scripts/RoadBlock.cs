using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBlock : MonoBehaviour
{
    public bool Fetch(float x) 
    {
        bool result = false;
    
        if(x > transform.position.x + 100f)
        {
            result = true; 
        }
    
        return result;
    }
    
    public void Delete() 
    {
        Destroy(gameObject); 
    }
}
