using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public GameObject standartTurretPrefab;
    public GameObject doubleTurretPrefab;
    public GameObject machingunTurretPrefab;
    private GameObject turretToBuild;
    public GameObject TurretToBuild { get { return turretToBuild; } private set {} }

    void Awake()
    {
        if (instance != null) {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
        turretToBuild = standartTurretPrefab;
    }
    public void selectBaseTurret() {
        Debug.Log("Selected base turret");
        turretToBuild = standartTurretPrefab;
    }

    public void selectDoubleTurret() {
        Debug.Log("Selected double turret");
        turretToBuild = doubleTurretPrefab;
    }

    public void selectMachinganTurret() {
        Debug.Log("Selected double turret");
        turretToBuild = machingunTurretPrefab;
    }
}
