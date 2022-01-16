using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonShopController : MonoBehaviour
{
    public GameObject turret;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sale() {
        Debug.Log("SALEEE!");
        GameObject shop = GameObject.Find("shop");
        ShopController shopController = shop.GetComponent<ShopController>();
        shopController.shopTurret(turret);
    }
}
