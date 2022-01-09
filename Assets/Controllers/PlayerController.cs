using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Text HPindicator;
    public int HP;
    public bool isAlive { get { return HP > 0; } private set {} }
    // Start is called before the first frame update
    void Start()
    {
        HPindicator.text = $"{HP} HP";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damagePlayer(int damage) {
        HP -= damage;
        if (HP < 0)
            HP = 0;
        HPindicator.text = $"{HP} HP";
    }
}
