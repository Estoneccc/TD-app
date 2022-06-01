using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScr : MonoBehaviour
{
    public GameObject ItemPref;
    public Transform ItemTransform;

    GameControllerScr gameControllerScr;

    public CellScript selfCell;

    void Start()
    {
        gameControllerScr = FindObjectOfType<GameControllerScr>();
        foreach (Tower tower in gameControllerScr.AllTowers)
        {
            GameObject tmpItem = Instantiate(ItemPref);
            tmpItem.transform.SetParent(ItemTransform, false);
            tmpItem.GetComponent<ShopItemScr>().SetStartData(tower, selfCell);
        }
    }

    public void CloseShop()
    {
        Destroy(gameObject);
    }
}
