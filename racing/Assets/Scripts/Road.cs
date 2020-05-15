using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public List<GameObject> blocks; 
    public GameObject player; 
    public GameObject roadPrefab; 
    public GameObject carPrefab; 
    public GameObject carPrefab2; 
    public GameObject coinPrefab; 
    private System.Random rand = new System.Random();
    
    void Update()
    {
        float x = player.GetComponent<Moving>().rb.position.x; 
    
        var last = blocks[blocks.Count - 1]; 
        
        if(x > last.transform.position.x - 24.69f * 10f) 
        {
            var block = Instantiate(roadPrefab, new Vector3(last.transform.position.x + 24.69f, last.transform.position.y, last.transform.position.z), Quaternion.identity); 
            block.transform.SetParent(gameObject.transform);
            blocks.Add(block); 
            
            if(rand.Next(0, 100) > 80)
            {
                float side = rand.Next(1, 3) == 1 ? -1f : 1f; 
                
                var car = Instantiate(carPrefab2, new Vector3(last.transform.position.x + 24.69f, last.transform.position.y + 0.20f, last.transform.position.z + 1.30f * side), Quaternion.Euler(new Vector3(0f, 90f, 0f)));
                car.transform.SetParent(gameObject.transform); 
            }
        }
         
        int flag = 1;
        List<GameObject> toRemove = new List<GameObject>();
        foreach (GameObject block in blocks) 
        {
            bool fetched = block.GetComponent<RoadBlock>().Fetch(x); 
    
            if(fetched && flag == 0) 
            {
                toRemove.Add(block);
                block.GetComponent<RoadBlock>().Delete();
            }
            flag = 0;
        }
        foreach(var a in toRemove)
        {
            blocks.Remove(a);
        }
    }
}
