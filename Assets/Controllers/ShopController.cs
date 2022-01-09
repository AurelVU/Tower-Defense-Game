using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public Text balanceText;
    public int Balance;
    public int BaseTurretCost;
    public int DoubleTurretCost;
    public int MachinganTurretCost;
    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
        balanceText.text = $"{Balance}$";
        
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
    public void shopBaseTurret ()
    {
        if (!buildManager.ConstructionAllowed && 
        Balance - BaseTurretCost > 0) {
            buyChangeMoney(BaseTurretCost);
            buildManager.selectBaseTurret();
        }
    }
    public void shopDoubleTurret ()
    {
        if (!buildManager.ConstructionAllowed && Balance - DoubleTurretCost > 0) {
            buyChangeMoney(DoubleTurretCost);
            buildManager.selectDoubleTurret();
        }
    }

    public void shopMachinganTurret ()
    {
        if (!buildManager.ConstructionAllowed && Balance - MachinganTurretCost > 0) {
            buyChangeMoney(MachinganTurretCost);
            buildManager.selectMachinganTurret();
        }
    }
}
