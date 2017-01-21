using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPool : MonoBehaviour
{
	
	public static EnemyPool Instance { get; private set; }
	void Awake(){
		Instance = this;
	}

	public float gameLength;
	public float timeUntilEnemyFire;
	public float delayTimeStart;
	public float delayTimeMax;
	public float delayTime;
	public float arrowChanceStart;
	public float arrowChanceMax;
    public GameObject enemyPrefab;
    public int poolAmount = 2;
    List<GameObject> enemies = new List<GameObject>();

	private Coroutine fireCoroutine;
	private float timeSinceGameStart = 0;

	private bool gameEnded = false;

    void Start()
    {
		delayTime = delayTimeStart;
		fireCoroutine = StartCoroutine (EnableFireCoroutine ());

        for(int i =0;i < poolAmount; i++){
			AddEnemy ();
        }

        StartCoroutine(Spawn());
    }

	void Update(){
		if (gameEnded)
			return;
		
		delayTime = Mathf.Lerp (delayTimeStart, delayTimeMax, timeSinceGameStart / gameLength);
		Enemy.arrowChance = Mathf.Lerp (arrowChanceStart, arrowChanceMax, Mathf.Clamp ((timeSinceGameStart - timeUntilEnemyFire) / (gameLength - timeUntilEnemyFire), 0, 1));

		timeSinceGameStart += Time.deltaTime;

		if (timeSinceGameStart > gameLength) {
			gameEnded = true;
			StartCoroutine (AnimateEndGame ());
		}
	}

	IEnumerator AnimateEndGame(){
		for (int i = 0; i< 10; i++) {
			SpawnEnemy ();
			SpawnEnemy ();
			SpawnEnemy ();
			SpawnEnemy ();
			SpawnEnemy ();
			SpawnEnemy ();

			yield return new WaitForSeconds(0.15f);
		}

		GamePlayScene.Instance.Victory ();
		HUDController.Instance.ActivateSkill3 ();
	}

	IEnumerator EnableFireCoroutine(){
		yield return new WaitForSeconds (timeUntilEnemyFire);

		foreach (GameObject enemy in enemies) {
			enemy.GetComponent<Enemy>().isShooting = true;
		}
	}
	
    IEnumerator Spawn()
    {
		while (!gameEnded)
        {
            yield return new WaitForSeconds(delayTime);
			SpawnEnemy ();
        }
    }

	private void SpawnEnemy(){
		GameObject go = GetPooledObject();
		Enemy enemy = go.GetComponent<Enemy>();
		go.SetActive(true);
		enemy.Spawn();
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
