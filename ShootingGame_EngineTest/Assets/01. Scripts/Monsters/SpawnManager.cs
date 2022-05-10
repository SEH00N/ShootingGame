using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    [SerializeField] Transform enities;
    [SerializeField] float delay;
    [SerializeField] float maxCount;

    private List<GameObject> monsters = new List<GameObject>();

    public Transform bossEntities;
    public bool isSpawning = false;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void OnEnable()
    {
        StartMethod();
    }

    private void Update()
    {
        DelaySet();
        CountSet();
    }

    IEnumerator coroutine;
    public void StartMethod()
    {
        coroutine = SpawnMonsters();
        StartCoroutine(coroutine);
        isSpawning = true;
    }
    public void StopMethod()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        isSpawning = false;
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

    public IEnumerator SpawnGolems(int count)
    {
        for(int i = 0; i < count; i++)
        {
            GameObject golem = GameManager.Instance.BossEntityPooling.transform.GetChild(0).gameObject;
            golem.transform.SetParent(bossEntities);
            golem.SetActive(true);
        }
        yield return 0;
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
