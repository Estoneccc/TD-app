using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManagerScr : MonoBehaviour
{
    public GameObject Menu;
    public static MoneyManagerScr Instance;
    public Text MoneyText;
    public Text HealthText;
    public Text WavesText;
    public Text WinText;
    public int GameMoney;
    public int Health;
    public int Waves = 9;
    public bool canSpawn = false;

    void Awake()
    {
        WinText.gameObject.SetActive(false);
        Instance = this;
    }

    void Update()
    {
        MoneyText.text = "Money: " + GameMoney.ToString();
        HealthText.text = "Health: " + Health.ToString();
        WavesText.text = "Waves: " + Waves.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
            ToMenu();
    }

    public void Play()
    {
        WinText.gameObject.SetActive(false);
        FindObjectOfType<LevelScr>().CreateLevel();
        FindObjectOfType<EnemySpawnerScr>().SpawnCount = 4;
        FindObjectOfType<EnemySpawnerScr>().timeToSpawn = 4;

        Menu.SetActive(false);
        canSpawn = true;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ToMenu()
    {
        FindObjectOfType<EnemySpawnerScr>().StopAllCoroutines();
        foreach (EnemyScript es in FindObjectsOfType<EnemyScript>())
        {
            es.StopAllCoroutines();
            Destroy(es.gameObject);
        }
        foreach (TowerScr ts in FindObjectsOfType<TowerScr>())
            Destroy(ts.gameObject);
        foreach (CellScript cs in FindObjectsOfType<CellScript>())
            Destroy(cs.gameObject);
        GameMoney = 30;
        Health = 10;
        Waves = 9;

        Menu.SetActive(true);
        canSpawn = false;
        if (FindObjectsOfType<ShopScr>().Length > 0)
            Destroy(FindObjectOfType<ShopScr>().gameObject);
    }

    public void MinusLive()
    {
        if (Health > 1)
            Health--;
        else ToMenu();
    }

    public void MinusWave()
    {
        if (Waves > 0)
            Waves--;
        else
        {
            WinText.gameObject.SetActive(true);
            ToMenu();
        }
    }
}
