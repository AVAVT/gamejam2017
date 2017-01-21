using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    private const float SPAWN_Y = 350;
    private const float SPAWN_RANGE_X = 520;
    private const float SPEED_TB = -1;
    private const float RANGE = 0.5f;

	public BoxCollider2D col1;
	public BoxCollider2D col2;
	public Animator horseAnim;
	public Animator enemyAnim;
	public SpriteRenderer horseSr;
	public SpriteRenderer enemySr;

    private float speed;

    public void Spawn()
    {
        gameObject.SetActive(true);
        speed = Random.Range(SPEED_TB - RANGE, SPEED_TB + RANGE);
        transform.position = new Vector2(Random.Range(-SPAWN_RANGE_X, SPAWN_RANGE_X), SPAWN_Y);
    }
    void Update(){
		if (col1.enabled) {
			transform.Translate(0, speed, 0);
		} else {
			transform.position += new Vector3(0, BackgroundScroller.Instance.scrollSpeed*Time.deltaTime, 0);
		}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Bottom"))
        {
            Debug.Log("Enemy collision Bottom");
            gameObject.SetActive(false);
        }
    }

	public void Die(int direction){
		transform.Rotate (new Vector3 (0, 0, direction * 32));
		col1.enabled = false;
		col2.enabled = false;
		horseAnim.speed = 0;
		enemyAnim.speed = 0;
		StartCoroutine (AnimateDead());
	}

	IEnumerator AnimateDead(){
		horseSr.color = Color.red;
		enemySr.color = Color.red;
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
