using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// use: "Random weighted options"
public class BoostManager : MonoBehaviour
{
    public GameObject uiBoostGameObject;

    [SerializeField]
    private List<Boost> boostList;


    [SerializeField]
    private List<Text> textList;


    private float totalWeight;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Boost boost in boostList)
        {
            totalWeight += boost.weight;
        }
    }

    // Update is called per frame
    public void showBoostPopup()
    {
        setupLogical();
        uiBoostGameObject.SetActive(true);

    }
    private void setupLogical()
    {
        Boost boostToShow;
        List<Boost> cloneBoostsList = new List<Boost>(boostList);

        foreach (Text text in textList)
        {
            boostToShow = getRandomBoost(cloneBoostsList);
            text.text = boostToShow.boostName.text;
            Debug.Log(boostToShow.name + " is displaying");
        }
    }

    private Boost getRandomBoost(List<Boost> cloneBoostsList)
    {
        Boost currentBoost = null;

        if (cloneBoostsList.Count == 0)
            return null;

        float randomNumber = Random.Range(0, totalWeight);

        float cumulativeWeight = 0;
        foreach (Boost boost in cloneBoostsList)
        {
            cumulativeWeight += boost.weight;
            if (randomNumber <= cumulativeWeight)
            {
                currentBoost = boost;
                break;
            }
        }
        if (currentBoost != null)
            cloneBoostsList.Remove(currentBoost);

        return currentBoost;
    }
}





