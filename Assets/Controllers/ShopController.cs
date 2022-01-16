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

    public void towerSale(GameObject turret) {
        int reward = turret.GetComponent<TurretController>().cost;
        Debug.Log($"reward {reward}");
        balanceReward(reward / 2);
    }
}
