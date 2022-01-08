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
    }

    void buyChangeMoney(int cost) {
        Balance -= cost;
        balanceText.text = $"{Balance}$";
    }
    public void shopBaseTurret ()
    {
        if (Balance - BaseTurretCost > 0) {
            buyChangeMoney(BaseTurretCost);
            buildManager.selectBaseTurret();
        }
    }
    public void shopDoubleTurret ()
    {
        if (Balance - DoubleTurretCost > 0) {
            buyChangeMoney(DoubleTurretCost);
            buildManager.selectDoubleTurret();
        }
    }

    public void shopMachinganTurret ()
    {
        if (Balance - MachinganTurretCost > 0) {
            buyChangeMoney(MachinganTurretCost);
            buildManager.selectMachinganTurret();
        }
    }
}
