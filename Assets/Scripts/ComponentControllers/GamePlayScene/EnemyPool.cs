using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPool : MonoBehaviour
{

    public GameObject enemyPrefab;
    public int poolAmount = 20;
    List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        for(int i =0;i < poolAmount; i++){
            GameObject obj = Instantiate(enemyPrefab);
            obj.SetActive(false);
            enemies.Add(obj);
        }

        StartCoroutine(Spawn(1));
    }

    IEnumerator Spawn(float delayTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(delayTime);
            GameObject go = GetPooledObject();
            if (go != null)
            {
                Enemy enemy = go.GetComponent<Enemy>();
                go.SetActive(true);
                enemy.Spawn();
            }
        }

    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].activeInHierarchy)
            {
                return enemies[i];
            }
        }
        return null;
    }
}
