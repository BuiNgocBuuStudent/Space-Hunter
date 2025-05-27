using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int maxAmmo;
    private int currentAmmo;
    public float reloadTime;

    [SerializeField] private bool isReloading = false;
    [SerializeField] private bool wasZeroAmmo = false;
    // Start is called before the first frame update
    void Start()
    {
        maxAmmo = ObjectPooler.SharedInstance.amountToPool;
        currentAmmo = maxAmmo;
        GameManager.Instance.UpdateBulletRemaining(currentAmmo);
        reloadTime = 1.0f;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.reload);

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
        if (currentAmmo == 0 && !wasZeroAmmo)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.reload);
            wasZeroAmmo = true;
        }
        else if(currentAmmo > 0)
        {
            wasZeroAmmo = false;
        }

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
    }

    private void Shoot()
    {
        GameObject pooledProjectile = ObjectPooler.SharedInstance.GetPooledObject();
        if (pooledProjectile != null)
        {
            pooledProjectile.SetActive(true);
            currentAmmo--;
            GameManager.Instance.UpdateBulletRemaining(currentAmmo);
            pooledProjectile.transform.position = new Vector3(transform.position.x + 1.3f, transform.position.y, transform.position.z);
        }
        AudioManager.Instance.PlaySFX(AudioManager.Instance.shoot);
    }

}

