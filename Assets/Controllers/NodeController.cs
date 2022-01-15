using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeController : MonoBehaviour
{
    private GameObject turret;
    public Color hoverColor;
    Renderer renderer;
    Color startColor;
    BuildManager buildManager;
    bool isSettedTurret;
    public Button saleButton;

    void Start() {
        renderer = GetComponent<Renderer>();
        startColor = renderer.material.color;
        buildManager = BuildManager.instance;
        isSettedTurret = false;
        saleButton.gameObject.SetActive(false);
    }
    void OnMouseDown() {
        if (!isSettedTurret)
        {
            if (buildManager.ConstructionAllowed)
                {
                    turret = Instantiate(buildManager.TurretToBuild, transform.position + new Vector3(0, 0.4f, 0), transform.rotation);
                    isSettedTurret = true;
                }
        } else 
        {
            saleButton.gameObject.SetActive(true);
            
        }
    }

    public void sale() {
        TurretController turretController = turret.GetComponent<TurretController>();
        turretController.sale();
        Destroy(turret);
        isSettedTurret = false;
        saleButton.gameObject.SetActive(false);
    }

    void OnMouseEnter() {
        renderer.material.color = hoverColor;
    }

    void OnMouseExit() {
        renderer.material.color = startColor;
    }
}
