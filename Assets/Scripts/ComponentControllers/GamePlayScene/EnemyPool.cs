using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPool : MonoBehaviour
{
	public static EnemyPool Instance { get; private set; }
	void Awake(){
		Instance = this;
	}

	public float delayTime;
    public GameObject enemyPrefab;
    public int poolAmount = 2;
    List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        for(int i =0;i < poolAmount; i++){
			AddEnemy ();
        }

        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
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

	public void CheckNetBo(){
		foreach(GameObject enemy in enemies){
			if(enemy.activeInHierarchy && Vector2.Distance(enemy.transform.position, PlayerController.Instance.transform.position) < PlayerController.Instance.netBoAoeRange){
				enemy.GetComponent<Enemy>().Die(Random.Range(0,100) < 50 ? -1 : 1);
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
		return AddEnemy();
    }

	private GameObject AddEnemy(){
		GameObject obj = Instantiate(enemyPrefab);
		obj.SetActive(false);
		enemies.Add(obj);
		return obj;
	}
}
