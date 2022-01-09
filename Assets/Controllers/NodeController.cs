using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    public Transform turret;
    public Color hoverColor;
    Renderer renderer;
    Color startColor;
    BuildManager buildManager;

    void Start() {
        renderer = GetComponent<Renderer>();
        startColor = renderer.material.color;
        buildManager = BuildManager.instance;
    }
    void OnMouseDown() {
        if (buildManager.ConstructionAllowed)
            Instantiate(buildManager.TurretToBuild, transform.position + new Vector3(0, 3.0f, 0), transform.rotation);
    }

    void OnMouseEnter() {
        renderer.material.color = hoverColor;
    }

    void OnMouseExit() {
        renderer.material.color = startColor;
    }
}
