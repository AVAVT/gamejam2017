using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPool : MonoBehaviour
{
	
	public static EnemyPool Instance { get; private set; }
	void Awake(){
		Instance = this;
	}

	private static float TIME_UNTIL_ENEMY_FIRE = 30;

	public float delayTime;
    public GameObject enemyPrefab;
    public int poolAmount = 2;
    List<GameObject> enemies = new List<GameObject>();

	private Coroutine fireCoroutine;

    void Start()
    {
		fireCoroutine = StartCoroutine (EnableFireCoroutine ());

        for(int i =0;i < poolAmount; i++){
			AddEnemy ();
        }

        StartCoroutine(Spawn());
    }

	IEnumerator EnableFireCoroutine(){
		yield return new WaitForSeconds (TIME_UNTIL_ENEMY_FIRE);
		foreach (GameObject enemy in enemies) {
			enemy.GetComponent<Enemy>().isShooting = true;
		}
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
		if (fireCoroutine == null) {
			Debug.Log ("aaaa");
			obj.GetComponent<Enemy> ().isShooting = true;
		}
		return obj;
	}
}
