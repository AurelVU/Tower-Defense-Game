using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public delegate void Death(string name);
    public event Death? onDeath;
    public delegate void Finish(int damage);
    public event Finish? onFinish;
    public string name;
    public float speed = 1.0f;
    public float startHP = 100f;
    public float currentHP = 100f;
    public int reward = 50;
    public int damage = 10;
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
        if (onFinish != null)
            onFinish(damage);
    }

    public void destroyWhendestroyed() {
        Destroy(gameObject);
        if (onDeath != null)
            onDeath(name);
    }
}
