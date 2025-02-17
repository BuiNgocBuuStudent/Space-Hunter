using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private ObjectPooler ObjPooler;

    public int maxAmmo;
    private int currentAmmo;
    public float reloadTime;

    private bool isReloading = false;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.StartGame();
        ObjPooler = FindObjectOfType<ObjectPooler>();
        maxAmmo = ObjPooler.amountToPool;
        currentAmmo = maxAmmo;
        GameManager.Instance.UpdateBulletRemaining(currentAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameOver || GameManager.Instance.isGamePause)
            return;

        if (Input.GetKeyDown(KeyCode.Space) && currentAmmo >= 1)
            Shoot();

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
            if (GameManager.Instance.isGameOver || GameManager.Instance.isGamePause)
            {
                isReloading = false;
                yield break;
            }

            yield return new WaitForSeconds(reloadTime);
            currentAmmo++;
            GameManager.Instance.UpdateBulletRemaining(currentAmmo);
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
            GameManager.Instance.UpdateBulletRemaining(currentAmmo);
            pooledProjectile.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y + 0.2f, transform.position.z);
        }
    }

}

