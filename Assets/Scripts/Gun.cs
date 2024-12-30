using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private GameManager gameManager;
    private ObjectPooler ObjPooler;

    public int maxAmmo;
    private int currentAmmo;
    private float reloadTime = 1.5f;

    private bool isReloading = false;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        gameManager.StartGame();
        ObjPooler = GameObject.FindObjectOfType<ObjectPooler>();
        maxAmmo = ObjPooler.amountToPool;
        currentAmmo = maxAmmo;
        gameManager.UpdateBulletRemaining(currentAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentAmmo >= 1)
            Shoot();

        if (gameManager.isGameOver)
            return;

        CheckMagazine();

    }

    private void CheckMagazine()
    {
        if (isReloading || currentAmmo >= maxAmmo)
            return;
        if (currentAmmo < maxAmmo)
        {
            StartCoroutine(ReLoad());
        }
    }
    IEnumerator ReLoad()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        while (currentAmmo < maxAmmo)
        {
            if (gameManager.isGameOver)
            {
                isReloading = false;
                yield break;
            }

            yield return new WaitForSeconds(reloadTime);
            currentAmmo++;
            gameManager.UpdateBulletRemaining(currentAmmo);
        }

        isReloading = false;
        Debug.Log("Reloading finished.");
    }



    private void Shoot()
    {
        GameObject pooledProjectile = ObjectPooler.SharedInstance.GetPooledObject();
        if (pooledProjectile != null)
        {
            pooledProjectile.SetActive(true);
            currentAmmo--;
            gameManager.UpdateBulletRemaining(currentAmmo);
            pooledProjectile.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y + 0.2f, transform.position.z);
        }
    }

}

