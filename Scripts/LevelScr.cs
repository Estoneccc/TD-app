using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelScr : MonoBehaviour
{
    public int fieldWidth;
    public int fieldHeight;
    public GameObject cellPref;

    public Transform cellParent;

    public Sprite[] tileSpr = new Sprite[7];

    public List<GameObject> wayPoints = new List<GameObject>();
    GameObject[,] allCells = new GameObject[10, 24];
    int currWayX;
    int currWayY;
    GameObject firstCell;


    void Start()
    {
        CreateLevel();
    }

    public void CreateLevel()
    {
        wayPoints.Clear();
        firstCell = null;

        Vector3 worldVec = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));

        for (var i = 0; i < fieldHeight; i++)
        {
            for (var j = 0; j < fieldWidth; j++)
            {
                int sprIndex = int.Parse(LoadLevelTxt(1)[i].ToCharArray()[j].ToString());
                Sprite spr = tileSpr[sprIndex];

                bool isGround = spr == tileSpr[0] ? false : true;

                CreateCell(isGround, spr, j, i, worldVec);
            }
        }
        LoadWayPoints();
    }

    void CreateCell(bool isGround, Sprite spr, int x, int y, Vector3 wV)
    {
        GameObject tmpCell = Instantiate(cellPref);
        tmpCell.transform.SetParent(cellParent, false);

        tmpCell.GetComponent<SpriteRenderer>().sprite = spr;

        float sprSizeX = tmpCell.GetComponent<SpriteRenderer>().bounds.size.x;
        float sprSizeY = tmpCell.GetComponent<SpriteRenderer>().bounds.size.y;

        tmpCell.transform.position = new Vector3(wV.x + (sprSizeX * x), wV.y + (sprSizeY * -y));

        allCells[y, x] = tmpCell;

        if (isGround)
        {
            tmpCell.GetComponent<CellScript>().isGround = true;
            if (firstCell == null)
            {
                firstCell = tmpCell;
                currWayX = x;
                currWayY = y;
            }
        }
    }

    string[] LoadLevelTxt(int i)
    {
        TextAsset tmpTxt = Resources.Load<TextAsset>("Level" + i + "Ground");
        string tmpStr = tmpTxt.text.Replace(Environment.NewLine, string.Empty);
        return tmpStr.Split('!');
    }

    void LoadWayPoints()
    {
        GameObject currWayGo;
        wayPoints.Add(firstCell);
        while (true)
        {
            currWayGo = null;
            if (currWayX > 0 && allCells[currWayY, currWayX - 1].GetComponent<CellScript>().isGround &&
                !wayPoints.Exists(x => x == allCells[currWayY, currWayX - 1]))
            {
                currWayGo = allCells[currWayY, currWayX - 1];
                currWayX--;
            }
            else if (currWayX < (fieldWidth - 1) && allCells[currWayY, currWayX + 1].GetComponent<CellScript>().isGround &&
                !wayPoints.Exists(x => x == allCells[currWayY, currWayX + 1]))
            {
                currWayGo = allCells[currWayY, currWayX + 1];
                currWayX++;
            }
            else if (currWayY > 0 && allCells[currWayY - 1, currWayX].GetComponent<CellScript>().isGround &&
                !wayPoints.Exists(x => x == allCells[currWayY - 1, currWayX]))
            {
                currWayGo = allCells[currWayY - 1, currWayX];
                currWayY--;
            }
            else if (currWayY < (fieldHeight - 1) && allCells[currWayY + 1, currWayX].GetComponent<CellScript>().isGround &&
               !wayPoints.Exists(x => x == allCells[currWayY + 1, currWayX]))
            {
                currWayGo = allCells[currWayY + 1, currWayX];
                currWayY++;
            }
            else
                break;
            wayPoints.Add(currWayGo);
        }       
    }
}