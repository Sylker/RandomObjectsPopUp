// RandomPopUpObjects by Sylker Teles, 2019
// Just a little something to show up objets randomly
// inside a spherical range.

// usage: put this script on an empty Game Object and the objects to pop up
// inside this Game Object. I wanted to avoid Instantiation so this script
// will get any transform inside its hierarchy.

using System.Collections.Generic;
using System.Collections;
using UnityEngine;

/// <summary>
/// Random pop up objects inside a spherical range.
/// </summary>
public class RandomPopUpObjects : MonoBehaviour
{
    public float radius = 5; // the spherical range radius
    public float maxTime = 1; // max time between objects appearance
    List <Transform> objects = new List<Transform>(); // our list of objects
    List <Transform> pool = new List<Transform>(); // a pool to avoid objects to repeat
    
    void Awake ()
    {
        foreach (Transform child in transform)
        { 
            // populate the objects list
            objects.Add(child); 
            
            // in case objects are active, turn them off
            child.gameObject.SetActive(false); 
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // no reason to go any further if there's no objects to pop up
        if (objects.Count < 1) return;
        
        // start our routine
        StartCoroutine (PopUp());
    }
    
    // Pop up routine
    IEnumerator PopUp ()
    {
        while (true)
        {
            // when we run out of objects, rebuild the list
            // from our objects pool
            if (objects.Count < 1)
            {
                objects = new List<Transform>(pool);
                pool.Clear();
            }
            
            // calculate a random time between objects
            float t = Random.Range(0, maxTime);
            
            // wait for that time
            yield return new WaitForSeconds(t);
            
            // get a random object from objects list
            int index = Random.Range(0, objects.Count - 1);
            Transform go = objects[index];
            
            // set the object position inside a spherical range
            go.position = transform.position + Random.insideUnitSphere * radius;
            
            // activate the game object
            go.gameObject.SetActive(true);
            
            // move the object from objects list to the pool list
            // this will avoid objects to repeat themselves
            objects.Remove(go);
            pool.Add(go);
        }
    }
}
