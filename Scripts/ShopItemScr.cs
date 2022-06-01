using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ShopItemScr : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    Tower selfTower;
    CellScript selfCell;
    public Image TowerLogo;
    public Text TowerName;
    public Text TowerPrice;

    public Color BaseColor;
    public Color CurrColor;


    public void SetStartData(Tower tower, CellScript cell)
    {
        selfTower = tower;
        TowerLogo.sprite = tower.Spr;
        TowerName.text = tower.Name;
        TowerPrice.text = tower.Price.ToString();
        selfCell = cell;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (MoneyManagerScr.Instance.GameMoney >= selfTower.Price)
        {
            selfCell.BuildTower(selfTower);
            MoneyManagerScr.Instance.GameMoney -= selfTower.Price;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = BaseColor;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = CurrColor;
    }
}
