using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    private GameObject turret;
    public Color hoverColor;
    Renderer renderer;
    Color startColor;
    BuildManager buildManager;
    bool isSettedTurret;

    void Start() {
        renderer = GetComponent<Renderer>();
        startColor = renderer.material.color;
        buildManager = BuildManager.instance;
        isSettedTurret = false;
    }
    void OnMouseDown() {
        if (!isSettedTurret)
        {
            if (buildManager.ConstructionAllowed)
                {
                    turret = Instantiate(buildManager.TurretToBuild, transform.position + new Vector3(0, 3.0f, 0), transform.rotation);
                    isSettedTurret = true;
                }
        } else 
        {
            TurretController turretController = turret.GetComponent<TurretController>();
            turretController.sale();
            Destroy(turret);
            isSettedTurret = false;
        }
    }

    void OnMouseEnter() {
        renderer.material.color = hoverColor;
    }

    void OnMouseExit() {
        renderer.material.color = startColor;
    }
}
