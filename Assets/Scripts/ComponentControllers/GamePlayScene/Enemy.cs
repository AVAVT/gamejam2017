using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    private const float SPAWN_Y = 350;
    private const float SPAWN_RANGE_X = 520;
    private const float SPEED_TB = -1;
    private const float RANGE = 0.5f;

    private float speed;

    public void Spawn()
    {
        gameObject.SetActive(true);
        speed = Random.Range(SPEED_TB - RANGE, SPEED_TB + RANGE);
        transform.position = new Vector2(Random.Range(-SPAWN_RANGE_X, SPAWN_RANGE_X), SPAWN_Y);
    }
    void Update(){
        transform.Translate(0, speed, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Bottom"))
        {
            Debug.Log("Enemy collision Bottom");
            gameObject.SetActive(false);
        }
    }
}
