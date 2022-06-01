using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnerScr : MonoBehaviour
{
    public LevelScr LS;
    GameControllerScr gameControllerScr;

    public float timeToSpawn = 4;
    public int SpawnCount = 4;
    public int wavesCount = 0;

    public GameObject enemyPref;

    private void Start()
    {
        gameControllerScr = FindObjectOfType<GameControllerScr>();
    }

    void Update()
    {
        if (MoneyManagerScr.Instance.canSpawn)
        {
            if (timeToSpawn <= 0)
            {
                FindObjectOfType<MoneyManagerScr>().MinusWave();
                StartCoroutine(SpawnEnemy(SpawnCount));
                timeToSpawn = 12;
            }
            timeToSpawn -= Time.deltaTime;
        }
    }

    IEnumerator SpawnEnemy(int spawnCount)
    {
        if (wavesCount <= 8)
        {
            for (var i = 0; i < spawnCount; i++)
            {
                if (wavesCount == 4 || wavesCount == 8)
                {
                    DefineEnemy(2);
                    if (i == spawnCount / 4)
                        break;
                }
                else
                {
                    if (wavesCount % 2 == 0)
                        DefineEnemy(0);
                    else DefineEnemy(1);
                }

                yield return new WaitForSeconds(0.4f);
            }

            wavesCount += 1;
            SpawnCount += 2;
        }
    }

    void DefineEnemy(int type)
    {
        GameObject tmpEnemy = Instantiate(enemyPref);
        tmpEnemy.transform.SetParent(gameObject.transform, false);

        tmpEnemy.GetComponent<EnemyScript>().selfEnemy = gameControllerScr.AllEnemies[type];

        Transform startCellPos = LS.wayPoints[0].transform;
        Vector3 startPos = new Vector3(startCellPos.position.x - startCellPos.GetComponent<SpriteRenderer>().bounds.size.x,
                                       startCellPos.position.y - startCellPos.GetComponent<SpriteRenderer>().bounds.size.y / 2);
        tmpEnemy.transform.position = startPos;
    }
}
