using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int maxAmmo;
    private int _currentAmmo;
    public float reloadTime;

    [SerializeField] bool _isReloading;
    [SerializeField] bool _wasZeroAmmo;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        maxAmmo = ObjectPooler.Instance.amountToPool;
        _currentAmmo = maxAmmo;
        gameManager.UpdateBulletRemaining(_currentAmmo);
        reloadTime = 1.0f;
        SFXManager.Instance.PlaySFX(SFXType.reload);

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameOver || gameManager.isGamePause)
            return;

        if (Input.GetKeyDown(KeyCode.Space) && _currentAmmo >= 1)
            Shoot();

        CheckMagazine();
    }

    private void CheckMagazine()
    {
        if (_isReloading || _currentAmmo >= maxAmmo)
            return;

        if (_currentAmmo == 0 && !_wasZeroAmmo)
        {
            SFXManager.Instance.PlaySFX(SFXType.reload);
            _wasZeroAmmo = true;
        }
        else if (_currentAmmo > 0)
            _wasZeroAmmo = false;

        if (_currentAmmo < maxAmmo)
            StartCoroutine(ReLoad());
    }
    IEnumerator ReLoad()
    {
        _isReloading = true;

        while (_currentAmmo < maxAmmo)
        {
            if (gameManager.isGameOver || gameManager.isGamePause)
                _isReloading = false;

            yield return new WaitForSeconds(reloadTime);
            _currentAmmo++;
            gameManager.UpdateBulletRemaining(_currentAmmo);
        }

        _isReloading = false;
    }

    private void Shoot()
    {
        GameObject pooledProjectile = ObjectPooler.Instance.GetPooledObject();
        if (pooledProjectile != null)
        {
            pooledProjectile.SetActive(true);
            _currentAmmo--;
            gameManager.UpdateBulletRemaining(_currentAmmo);
            pooledProjectile.transform.position = new Vector3(transform.position.x + 1.3f, transform.position.y, transform.position.z);
        }
        SFXManager.Instance.PlaySFX(SFXType.shoot);
    }

}

