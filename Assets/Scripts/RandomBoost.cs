using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// use: "Random weighted options"
public class RandomBoost : MonoBehaviour 
{

    public List<Boost> boosts;
    private int totalWeight;

    // Start is called before the first frame update
    void Start()
    {
        boosts.Add(new Boost("Boost 3", 20));
        boosts.Add(new Boost("Boost 1", 50));
        boosts.Add(new Boost("Boost 2", 30));

        foreach (Boost entry in boosts)
        {
            totalWeight += entry.weight;
        }
    }

    // Update is called per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Boost selectedBoost = getRandomBoost();
            
            Debug.Log(selectedBoost.boostName);
            Destroy(gameObject);
        }
    }

    private Boost getRandomBoost()
    {   
        int randomNumber = Random.Range(1, totalWeight + 1);

        int cumulativeWeight = 0;
        foreach(Boost boost in boosts)
        {
            cumulativeWeight += boost.weight;
            if(randomNumber <= cumulativeWeight)
            {
                return boost;
            }
        }
        return null;
    }
}

[System.Serializable]
public class Boost
{
    public string boostName;
    public int weight;

    public Boost(string newBoostName, int newBoostWeight)
    {
        this.boostName = newBoostName;
        this.weight = newBoostWeight;
    }
}

