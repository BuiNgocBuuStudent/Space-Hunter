using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public static EnemyController Instance;
    public static event Action<float, float> OnHeal;
    public float timeInterval;
    public float healAmount;
    public float globalHealBonus;
    public float speedAmount;
    public float globalSpeedBonus;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        ResetGlobalHealBonus();
        ResetGlobalSpeedBonus();
        StartCoroutine(IncreaseHealthAndSpeed());

    }
    IEnumerator IncreaseHealthAndSpeed()
    {
        while (!GameManager.Instance.isGameOver)
        {
            while (GameManager.Instance.isGamePause)
            {
                yield return null;
            }

            float elapsedTime = 0f;
            while (elapsedTime < timeInterval)
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
                globalSpeedBonus += speedAmount;
                OnHeal?.Invoke(healAmount, speedAmount);
                Debug.Log("Notified all health and speed of enemies increase!!!");
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
        globalHealBonus = 0f;
    }
}
