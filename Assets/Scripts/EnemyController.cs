using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance;
    public static event System.Action<float, float> OnHeal;
    [SerializeField] private float healInterval;
    [SerializeField] private float healAmount;
    [SerializeField] private float globalHealBonus;
    [SerializeField] private float speedAmout;
    [SerializeField] private float globalSpeedBonus;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        healInterval = 90.0f;
        healAmount = speedAmout = 1.0f;
        ResetGlobalHealBonus();
        ResetGlobalSpeedBonus();
        StartCoroutine(IncreaseHeal());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator IncreaseHeal()
    {

        while (!GameManager.Instance.isGameOver)
        {
            while (GameManager.Instance.isGamePause)
            {
                yield return null; 
            }

            float elapsedTime = 0f;
            while (elapsedTime < healInterval)
            {
                if (GameManager.Instance.isGamePause || GameManager.Instance.isGameOver)
                {
                    yield return null; 
                }
                else
                {
                    elapsedTime += Time.deltaTime;
                }
                yield return null;
            }

            if (!GameManager.Instance.isGameOver)
            {
                globalHealBonus += healAmount;
                globalSpeedBonus += speedAmout;
                OnHeal?.Invoke(healAmount, speedAmout);
                Debug.Log("Notified all enemies to heal and speed with amount: " + healAmount);
            }
        }

    }
    public float GetGlobalHealBonus()
    {
        return globalHealBonus;
    }
    public void ResetGlobalHealBonus()
    {
        globalHealBonus = 0f;
    }

    public float GetGlobalSpeedBonus()
    {
        return globalSpeedBonus;
    }

    public void ResetGlobalSpeedBonus()
    {
        globalSpeedBonus = 0f;
    }
}
