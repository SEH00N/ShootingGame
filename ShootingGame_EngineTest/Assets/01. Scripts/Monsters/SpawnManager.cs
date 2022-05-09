using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform enities;
    private List<GameObject> monsters = new List<GameObject>();
    [SerializeField] float delay;
    [SerializeField] float maxCount;

    private void OnEnable()
    {
        StartCoroutine(SpawnMonsters());
    }

    private void Update()
    {
        DelaySet();
        CountSet();
    }

    private IEnumerator SpawnMonsters()
    {
        while(true)
        {
            if(transform.childCount > 0 && enities.childCount <= maxCount)
            {
                int randChild = Random.Range(0, transform.childCount);
                Debug.Log(randChild);
                monsters.Add(transform.GetChild(randChild).gameObject);
                monsters[0].transform.SetParent(enities);
                monsters[0].SetActive(true);
                monsters.RemoveAt(0);
            }
            yield return new WaitForSeconds(delay);
        }
    }

    private void DelaySet()
    {
        delay = 5 - (GameManager.Instance.elapsedTime / 720);
    }

    private void CountSet()
    {
        maxCount = Mathf.Floor(GameManager.Instance.elapsedTime / 36);
    }
}
