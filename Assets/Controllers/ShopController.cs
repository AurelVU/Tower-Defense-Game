using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class ShopController : MonoBehaviour
{
    public GameObject prefabButton;
    public GameObject shopPanel;
    public Text balanceText;
    public int Balance;
    public int BaseTurretCost;
    public int DoubleTurretCost;
    public int MachinganTurretCost;
    public BuildManager buildManager;


    void Start()
    {
        buildManager = BuildManager.instance;
        balanceText.text = $"{Balance}$";

        GameObject   panel = shopPanel;
        Debug.Log(panel);
        foreach (var turret in buildManager.turrets) {
            GameObject newButton = (GameObject)Instantiate(prefabButton);
            newButton.GetComponent<Image>().sprite = turret.GetComponent<TurretController>().icon;
            newButton.GetComponent<ButtonShopController>().turret = turret;
            newButton.transform.SetParent(panel.transform, false);
        }
        
        GameObject enemyFactory = GameObject.Find("EnemyFactory");
        EnemyFactoryController enemyFactoryController = enemyFactory.GetComponent<EnemyFactoryController>();
        enemyFactoryController.onWaveFinished += onWaveFinished;
    }

    void onWaveFinished(int indexWave) {
        balanceReward((indexWave + 1) * 100);
    }

    public void balanceReward(int cost) {
        Balance += cost;
        balanceText.text = $"{Balance}$";
    }

    void buyChangeMoney(int cost) {
        Balance -= cost;
        balanceText.text = $"{Balance}$";
    }
    public void shopTurret(GameObject turret)
    {
        Debug.Log(turret);
        TurretController controller = turret.GetComponent<TurretController>();
        if (!buildManager.ConstructionAllowed && 
        Balance - controller.cost > 0) {
            buyChangeMoney(controller.cost);
            buildManager.selectTurret(turret);
        }
    }
    public void shopBaseTurret ()
    {
        Debug.Log("gdfgfddfgdfgdf");
        // if (!buildManager.ConstructionAllowed && 
        // Balance - BaseTurretCost > 0) {
        //     buyChangeMoney(BaseTurretCost);
        //     buildManager.selectBaseTurret();
        // }
    }
    // public void shopDoubleTurret ()
    // {
    //     if (!buildManager.ConstructionAllowed && Balance - DoubleTurretCost > 0) {
    //         buyChangeMoney(DoubleTurretCost);
    //         buildManager.selectDoubleTurret();
    //     }
    // }

    // public void shopMachinganTurret ()
    // {
    //     if (!buildManager.ConstructionAllowed && Balance - MachinganTurretCost > 0) {
    //         buyChangeMoney(MachinganTurretCost);
    //         buildManager.selectMachinganTurret();
    //     }
    // }

    public void towerSale(string name) {
        int reward = 0;
        Debug.Log($"name {name}");
        if (name.Equals("Turret"))
            reward = BaseTurretCost;
        if (name.Equals("Turret2")) 
            reward = DoubleTurretCost;
        if (name.Equals("Turret3"))
            reward = MachinganTurretCost;
        Debug.Log($"reward {reward}");
        balanceReward(reward / 2);
    }
}
