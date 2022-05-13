using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    [SerializeField] GameObject melee;
    [SerializeField] GameObject axe;
    [SerializeField] float attackTime;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        melee.SetActive(false);
        StartCoroutine(MeleeAtack());
    }
    

    private IEnumerator MeleeAtack()
    {
        while (true)
        {
            if(Input.GetKeyDown(KeyCode.Mouse1) && PlayerMove.Instance.isGround())
            {
                Player.Instance.state = Player.State.Melee;
                animator.SetTrigger("Melee");
                melee.SetActive(true);
                axe.SetActive(false);
                yield return new WaitForSeconds(attackTime);
                Player.Instance.state = Player.State.Idle;
                melee.SetActive(false);
                axe.SetActive(true);
            }
            yield return 0;
        }
    }
}
