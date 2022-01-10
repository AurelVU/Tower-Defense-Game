using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
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
        if (Vector3.Distance(target.position, transform.position) < 0.3f) {
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
        GameObject player = GameObject.Find("player");
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.damagePlayer(damage);

        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

        if (!playerController.isAlive) {
            GameObject game = GameObject.Find("gameController");
            GameController gameController = game.GetComponent<GameController>();
            gameController.loseGame();
        }
        if (enemys.Length == 1) {      
            GameObject game = GameObject.Find("gameController");
            GameController gameController = player.GetComponent<GameController>();
            gameController.winGame();    
        }
    }

    public void destroyWhendestroyed() {
        Destroy(gameObject);
        GameObject shop = GameObject.Find("shop");
        ShopController shopController = shop.GetComponent<ShopController>();
        shopController.balanceReward(reward);

        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject enemyFactory = GameObject.Find("EnemyFactory");
        EnemyFactoryController enemyFactoryController = enemyFactory.GetComponent<EnemyFactoryController>();
        if (enemys.Length == 1) {
            GameObject game = GameObject.Find("gameController");
            GameController gameController = game.GetComponent<GameController>();

            gameController.winGame();
        }
    }
}
