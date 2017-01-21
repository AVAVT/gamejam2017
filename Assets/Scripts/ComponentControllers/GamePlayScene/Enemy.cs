using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    private const float SPAWN_Y = 670;
    private const float SPAWN_RANGE_X = 900;
    private const float SPEED_TB = -1;
    private const float RANGE = 0.2f;
	private const float BASE_SPEED = 150;

	public static float arrowChance = 0;

	public BoxCollider2D col1;
	public BoxCollider2D col2;
	public Animator horseAnim;
	public Animator enemyAnim;
	public SpriteRenderer horseSr;
	public SpriteRenderer enemySr;

	public GameObject arrowPrefab;
	public bool isShooting = false;

    private float speed;

    public void Spawn()
    {
        gameObject.SetActive(true);
		speed = Random.Range(SPEED_TB - RANGE, SPEED_TB + RANGE) * BASE_SPEED;
        transform.position = new Vector2(Random.Range(-SPAWN_RANGE_X, SPAWN_RANGE_X), SPAWN_Y);
		if (isShooting && Random.Range (1, 101) < arrowChance) {
			StartCoroutine (ShootCoroutine());
		}
    }

    void Update(){
		if (col1.enabled) {
			transform.position += new Vector3(0, speed*Time.deltaTime, 0);
		} else {
			transform.position += new Vector3(0, BackgroundScroller.Instance.scrollSpeed*Time.deltaTime, 0);
		}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Bottom"))
        {
            gameObject.SetActive(false);
        }
    }

	public void Die(int direction){
		transform.Rotate (new Vector3 (0, 0, direction * 32));
		col1.enabled = false;
		col2.enabled = false;
		horseAnim.speed = 0;
		enemyAnim.speed = 0;
		StopAllCoroutines ();
		StartCoroutine (AnimateDead());
	}

	IEnumerator ShootCoroutine(){
		yield return new WaitForSeconds (Random.Range (0.8f, 2.5f));
		Color col = Color.white;
		col.a = 0.6f;
		enemySr.color = col;
		horseSr.color = col;
		yield return new WaitForSeconds (0.3f);
		enemySr.color = Color.white;
		horseSr.color = Color.white;

		TKUtils.Instantiate (arrowPrefab, transform.position + (Vector3)(Vector2.down * 40), Quaternion.identity);
	}

	IEnumerator AnimateDead(){
		horseSr.color = Color.gray;
		enemySr.color = Color.gray;
		yield return new WaitForSeconds (0.3f);

		horseAnim.speed = 1;
		enemyAnim.speed = 1;
		transform.rotation = Quaternion.identity;
		gameObject.SetActive(false);
		col1.enabled = true;
		col2.enabled = true;
		horseSr.color = Color.white;
		enemySr.color = Color.white;

	}
}
