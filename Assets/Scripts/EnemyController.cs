using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static event System.Action<float> OnHeal;
    // Start is called before the first frame update
    [SerializeField] private float healInterval;
    [SerializeField] private float healAmount;
    void Start()
    {
        healInterval = 120.0f;
        healAmount = 1.0f;
        StartCoroutine(IncreaseHeal());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator IncreaseHeal()
    {
        while (true)
        {
            yield return new WaitForSeconds(healInterval);
            OnHeal?.Invoke(healAmount);
            Debug.Log("All enemies have been notified to heal!");
        }
    }
}
