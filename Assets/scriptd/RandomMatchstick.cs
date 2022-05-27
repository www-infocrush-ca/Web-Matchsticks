using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMatchstick : MonoBehaviour
{
    public GameObject matchstick;

    int numMatches;

    System.Random rand;

    GameObject[] matches;

    public int minMatches=11;
    public int maxMatches=35;
    // Start is called before the first frame update
    void Start()
    {
       rand = new System.Random();
       numMatches = 21;
       matches = new GameObject[numMatches];
       Debug.Log("Instantiating " + numMatches + " matches.");
       for(int i = 0; i< numMatches; i++) {
           float xPos = rand.Next (100)/400f;
           float zPos = rand.Next (100)/400f;
           matches[i] = Instantiate(matchstick, new Vector3(xPos,1,zPos), Random.rotation) as GameObject;
       }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int GetRemainingMatches() {
        return numMatches;
    }
    public void RemoveMatches(int pickMatches){
        for (int i=0; pickMatches > 0; i++){
            if (matches[i] != null) {
                Destroy(matches[i]);
                pickMatches--;
                numMatches--;
                
            }
        }
    }
}
