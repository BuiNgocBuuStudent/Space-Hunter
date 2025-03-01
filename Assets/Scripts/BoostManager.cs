using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// use: "Random weighted options"
public class BoostManager : MonoBehaviour
{
    public GameObject uiBoostGameObject;
    public Bullet bullet;
    public Gun gun;


    [SerializeField]
    private List<Boost> boostList;

    public List<Boost> boostSelectedList;

    [SerializeField]
    private List<Text> textList;

    public List<Button> selectBtn;
    private float totalWeight;
    private float cloneOfTotalWeight;

    private float countdownTime;
    private bool isBoosting;

    private void Awake()
    {
        selectBtn[0].onClick.AddListener(delegate { selectBoost(boostSelectedList[0]); });
        selectBtn[1].onClick.AddListener(delegate { selectBoost(boostSelectedList[1]); });
        selectBtn[2].onClick.AddListener(delegate { selectBoost(boostSelectedList[2]); });
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (Boost boost in boostList)
        {
            totalWeight += boost.weight;
        }
        boostSelectedList = new List<Boost>();
    }

    // Update is called per frame
    public void showBoostPopup()
    {
        boostSelectedList.Clear();//xóa tất cả các boost trong lần hiển thị trước đó
        setupLogical();
        uiBoostGameObject.SetActive(true);
        GameManager.Instance.isGamePause = true;
    }
    private void setupLogical()
    {
        Boost boostToShow;
        List<Boost> cloneBoostsList = new List<Boost>(boostList);
        cloneOfTotalWeight = totalWeight;

        foreach (Text text in textList)
        {
            boostToShow = getRandomBoost(cloneBoostsList);
            boostSelectedList.Add(boostToShow);
            text.text = boostToShow.boostDescription.text;
            Debug.Log(boostToShow.name + " is displaying");
        }
    }

    private Boost getRandomBoost(List<Boost> cloneBoostsList)
    {
        Boost currentBoost = null;

        if (cloneBoostsList.Count == 0)
            return cloneBoostsList[0];

        float randomNumber = Random.Range(0, cloneOfTotalWeight);

        float cumulativeWeight = 0;
        foreach (Boost boost in cloneBoostsList)
        {
            cumulativeWeight += boost.weight;
            if (randomNumber <= cumulativeWeight)
            {
                cloneOfTotalWeight -= boost.weight;
                currentBoost = boost;
                break;
            }
        }
        if (currentBoost != null)
            cloneBoostsList.Remove(currentBoost);

        return currentBoost;
    }
    private void selectBoost(Boost selectedBoost)
    {

        switch (selectedBoost.name)
        {
            case "Boost 1":
                Debug.Log("Boost 1 is selected");
                gun.maxAmmo += 1;
                break;
            case "Boost 2":
                Debug.Log("Boost 2 is selected");
                gun.reloadTime -= gun.reloadTime * 0.2f;
                break;
            case "Boost 3":
                Debug.Log("Boost 3 is selected");
                countdownTime = 5;
                StartCoroutine(boostDamage());
                break;
            case "Boost 4":
                Debug.Log("Boost 4 is selected");
                gun.maxAmmo += 2;
                break;
            case "Boost 5":
                Debug.Log("Boost 5 is selected");
                gun.reloadTime -= gun.reloadTime * 0.4f;
                break;
            default:
                Debug.Log("Boost 6 is selected");
                countdownTime = 10;
                StartCoroutine(boostDamage());
                break;
        }
        uiBoostGameObject.SetActive(false);
        GameManager.Instance.isGamePause = false;
    }
    IEnumerator boostDamage()
    {
        isBoosting = true;
        Debug.Log("Boosting");

        bullet.damage = 2;
        Debug.Log("Current damage: " + bullet.damage);
        yield return new WaitForSeconds(countdownTime);

        bullet.damage = 1;
        Debug.Log("Current damage: " + bullet.damage);

        isBoosting = false;
        Debug.Log("Boosting finish");
    }
}





