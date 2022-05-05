using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePos;

    [SerializeField] float fireDelay = 1f;

    private float currentTime;

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        currentTime += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && currentTime > fireDelay)
        {
            StartCoroutine(InstantiateOrPool());
            currentTime = 0;
        }
    }

    private IEnumerator InstantiateOrPool()
    {
        if (GameManager.Instance.bulletPooling.transform.childCount > 0)
        {
            Transform bullet = GameManager.Instance.bulletPooling.GetChild(0);
            bullet.SetParent(null);
            bullet.gameObject.SetActive(true);
            bullet.rotation = firePos.rotation;
            bullet.position = firePos.position;
        }
        else
            Instantiate(bullet, firePos.position, firePos.rotation);
        yield return 0;
    }
}
