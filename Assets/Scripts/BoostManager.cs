using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// use: "Random weighted options"
public class BoostManager : MonoBehaviour
{
    private static BoostManager _instance;
    public static BoostManager Instance => _instance;

    public GameObject uiBoostGameObject;
    public Bullet bullet;
    public Gun gun;
    private GameManager _gameManager;

    [SerializeField] List<BoostEntity> _boostList;
    [SerializeField] List<BoostEntity> _boostSelectedList;
    [SerializeField] List<Text> _textList;

    public List<Button> selectBtn;
    private float _totalWeight;
    private float _cloneOfTotalWeight;

    [SerializeField] GameObject _boostDmgIndicator;
    private float _boostDmgDuration;
    [SerializeField] Boost _boostPrefab;

    public float spawnRateBoost;
    public float startTimeSpawnBoost;
    private void Awake()
    {
        selectBtn[0].onClick.AddListener(delegate { selectBoost(_boostSelectedList[0]); });
        selectBtn[1].onClick.AddListener(delegate { selectBoost(_boostSelectedList[1]); });
        selectBtn[2].onClick.AddListener(delegate { selectBoost(_boostSelectedList[2]); });

        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        foreach (BoostEntity boost in _boostList)
        {
            _totalWeight += boost.weight;
        }
        InvokeRepeating("SpawnBoost", startTimeSpawnBoost, spawnRateBoost);

    }
    private void SpawnBoost()
    {
        if (!_gameManager.isGameOver && !_gameManager.isGamePause)
        {
            Boost boost = ObjectPooler.Instance.Getcomp(_boostPrefab);
            boost.Init();
            boost.gameObject.SetActive(true);
        }

    }
    public void showBoostPopup()
    {
        _boostSelectedList.Clear();
        setupLogical();
        uiBoostGameObject.SetActive(true);
        _gameManager.isGamePause = true;
    }
    private void setupLogical()
    {
        BoostEntity boostToShow;
        List<BoostEntity> cloneBoostsList = new List<BoostEntity>(_boostList);
        _cloneOfTotalWeight = _totalWeight;

        foreach (Text text in _textList)
        {
            boostToShow = getRandomBoost(cloneBoostsList);
            _boostSelectedList.Add(boostToShow);
            text.text = boostToShow.boostDescription.text;
        }
    }

    private BoostEntity getRandomBoost(List<BoostEntity> cloneBoostsList)
    {
        BoostEntity currentBoost = null;

        if (cloneBoostsList.Count == 0)
            return cloneBoostsList[0];

        float randomNumber = Random.Range(0, _cloneOfTotalWeight);

        float cumulativeWeight = 0;
        foreach (BoostEntity boost in cloneBoostsList)
        {
            cumulativeWeight += boost.weight;
            if (randomNumber <= cumulativeWeight)
            {
                _cloneOfTotalWeight -= boost.weight;
                currentBoost = boost;
                break;
            }
        }
        if (currentBoost != null)
            cloneBoostsList.Remove(currentBoost);

        return currentBoost;
    }
    private void selectBoost(BoostEntity selectedBoost)
    {
        switch (selectedBoost.name)
        {
            case "Boost 1":
                IncreaseBulletAmount(1);
                break;
            case "Boost 2":
                ReduceReloadTime(0.1f);
                break;
            case "Boost 3":
                BoostDmg(10);
                break;
            case "Boost 4":
                IncreaseBulletAmount(2);
                break;
            case "Boost 5":
                ReduceReloadTime(0.2f);
                break;
            default:
                BoostDmg(20);
                break;
        }
        _gameManager.isGamePause = false;
        Time.timeScale = 1f;
        uiBoostGameObject.SetActive(false);
        SFXManager.Instance.PlaySFX(SFXType.reload);
    }
    private void IncreaseBulletAmount(int quantity)
    {
        gun.maxAmmo += quantity;
        ObjectPooler.Instance.amountToPool += quantity;
        ObjectPooler.Instance.AddPooledObject();
    }
    private void ReduceReloadTime(float percentToReduce)
    {
        gun.reloadTime -= gun.reloadTime * percentToReduce;
    }
    private void BoostDmg(int duration)
    {
        _boostDmgDuration = duration;
        _boostDmgIndicator.SetActive(true);
        StartCoroutine(BoostDamage());
    }
    IEnumerator BoostDamage()
    {
        bullet.damage = 2;
        yield return new WaitForSeconds(_boostDmgDuration);

        bullet.damage = 1;
        _boostDmgIndicator.SetActive(false);
    }
}





