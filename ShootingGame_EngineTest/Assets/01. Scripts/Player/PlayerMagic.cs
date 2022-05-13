using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePos;
    [SerializeField] float fireDelay = 1f;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(Fire());
    }

    private IEnumerator Fire()
    {
        while(true)
        {
            if (Input.GetKey(KeyCode.Mouse0) && Player.Instance.state != Player.State.Melee)
            {
                Player.Instance.state = Player.State.Magic;
                animator.SetTrigger("Magic");
                StartCoroutine(InstantiateOrPool());
            }
            yield return new WaitForSeconds(fireDelay);
            Player.Instance.state = Player.State.Idle;
        }
    }

    private IEnumerator InstantiateOrPool()
    {
        if (GameManager.Instance.BulletPooling.transform.childCount > 0)
        {
            Transform bullet = GameManager.Instance.BulletPooling.GetChild(0);
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
