using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public delegate void Hit(Transform target, EnemyController targetEnemyController);
    public event Hit? onHitEnemy;   
    public float speed = 20.0f;
    Transform enemyTarget;
    private EnemyController targetEnemyController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyTarget == null)
            Destroy(gameObject);

        Vector3 direction = enemyTarget.position - transform.position;
        transform.LookAt(enemyTarget);
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }

    void OnCollisionEnter(Collision collision)
    {
        UnityEngine.Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Enemy") {
            Destroy(gameObject);
            Debug.Log(collision.gameObject.tag);
            onHitEnemy(enemyTarget, targetEnemyController);
        }
    }

    public void Seek(Transform target, EnemyController targetEnemyController) {
        this.enemyTarget = target;
        this.targetEnemyController = targetEnemyController;
    }
}
