using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance;
    public static event System.Action<float> OnHeal;
    [SerializeField] private float healInterval;
    [SerializeField] private float healAmount;
    [SerializeField] private float globalHealBonus;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        healInterval = 100.0f;
        healAmount = 1.0f;
        ResetGlobalHealBonus();
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
                OnHeal?.Invoke(healAmount);
                Debug.Log("Notified all enemies to heal with amount: " + healAmount);
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
}
