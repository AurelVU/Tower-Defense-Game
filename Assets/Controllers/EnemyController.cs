using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float speed = 1.0f;
    public float startHP = 100f;
    public float currentHP = 100f;
    public int reward = 50;
    public bool isAlive { get { return currentHP > 0.0f; } private set {} }

    public Transform target { get { return RotationController.points[currentTargetPoint]; } private set {} }

    private int currentTargetPoint;

    // Start is called before the first frame update
    void Start()
    {
        currentTargetPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.LookAt(target);
        move(direction);
        updateTarget();    
    }

    public void hit(float damage) {
        currentHP -= damage;
        if (!isAlive) {
            Debug.Log(damage);
            Debug.Log(isAlive);
            Debug.Log(currentHP);
            destroyWhendestroyed();
        }
    }

    public void move(Vector3 direction) {
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }

    void updateTarget() {
        if (Vector3.Distance(target.position, transform.position) < 0.1f) {
            currentTargetPoint += 1;
            if (currentTargetPoint == RotationController.points.Length) {
               destroyWhenFinished();
            } else {
                target = RotationController.points[currentTargetPoint];
            }
        }
    }

    public void destroyWhenFinished() {
        Destroy(gameObject);
        // ToDo: отнять у игрока жизни
    }

    public void destroyWhendestroyed() {
        Destroy(gameObject);
        // ToDo: добавить бабки
    }
}
