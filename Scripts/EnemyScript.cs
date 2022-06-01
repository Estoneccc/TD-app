using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    List<GameObject> wayPoints = new List<GameObject>();

    public Enemy selfEnemy;
    int wayIndex = 0;

    private void Start()
    {
        GetWayPoints();
        GetComponent<SpriteRenderer>().sprite = selfEnemy.Spr;
    }

    void Update()
    {
        Move();
    }

    void GetWayPoints()
    {
        wayPoints = GameObject.Find("LevelGroup").GetComponent<LevelScr>().wayPoints;
    }
    private void Move()
    {
        Transform currWayPoint = wayPoints[wayIndex].transform;
        Vector3 currWayPos = new Vector3(currWayPoint.position.x + currWayPoint.GetComponent<SpriteRenderer>().bounds.size.x / 2,
                                         currWayPoint.position.y - currWayPoint.GetComponent<SpriteRenderer>().bounds.size.y / 2);
        Vector3 dir = currWayPos - transform.position;

        transform.Translate(dir.normalized * Time.deltaTime * selfEnemy.speed);

        if (Vector3.Distance(transform.position, currWayPos) < 0.01f)
        {
            if (wayIndex < wayPoints.Count - 1)
                wayIndex++;
            else
            {
                MoneyManagerScr.Instance.MinusLive();
                Destroy(gameObject);
            }
        }    
    }

    public void TakeDamage(float damage)
    {
        selfEnemy.health -= damage;

        CheckIsAlive();
    }

    void CheckIsAlive()
    {
        if (selfEnemy.health <= 0)
        {
            MoneyManagerScr.Instance.GameMoney += selfEnemy.cost;
            Destroy(gameObject);
        }
    }

    public void StartSlow(float duration, float slowValue)
    {
        StopCoroutine("GetSlow");
        selfEnemy.speed = selfEnemy.startSpeed;

        StartCoroutine(GetSlow(duration, slowValue));
    }

    IEnumerator GetSlow(float duration, float slowValue)
    {
        selfEnemy.speed -= slowValue;
        yield return new WaitForSeconds(duration);
        selfEnemy.speed = selfEnemy.startSpeed;
    }

    public void AOEDamage(float range, float damage, float duration, float slowValue)
    {
        List<EnemyScript> enemies = new List<EnemyScript>();

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (Vector2.Distance(transform.position, obj.transform.position) <= range)
                enemies.Add(obj.GetComponent<EnemyScript>());
        }

        foreach (EnemyScript enemy in enemies)
        {
            enemy.TakeDamage(damage);
            enemy.StartSlow(duration, slowValue);
        }
    }
}