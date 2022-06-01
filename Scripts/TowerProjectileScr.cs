using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectileScr : MonoBehaviour
{
    Transform target;
    TowerProjectTile selfProjectTile;
    public Tower selfTower;
    GameControllerScr gcs;

    private void Start()
    {
        gcs = FindObjectOfType<GameControllerScr>();

        selfProjectTile = gcs.AllProjectTiles[selfTower.type];

        GetComponent<SpriteRenderer>().sprite = selfProjectTile.Spr;
    }
    void Update()
    {
        Move();
    }

    public void SetTarget(Transform enemy)
    {
        target = enemy;
    }

    private void Move()
    {
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) < .1f)
            {
                Hit();
            }
            else
            {
                Vector2 dir = target.position - transform.position;
                transform.Translate(dir.normalized * Time.deltaTime * selfProjectTile.speed);
            }
        }
        else Destroy(gameObject);
    }

    void Hit()
    {
        switch(selfTower.type)
        {
            case (int)TowerType.First_Tower:
                //target.GetComponent<EnemyScript>().StartSlow(3, 1);
                target.GetComponent<EnemyScript>().TakeDamage(selfProjectTile.damage);
                break;
            case (int)TowerType.Second_Tower:
                target.GetComponent<EnemyScript>().AOEDamage(1, selfProjectTile.damage, 0, 0);
                break;
            case (int)TowerType.Third_Tower:
                target.GetComponent<EnemyScript>().AOEDamage(1, selfProjectTile.damage, 3, 1);
                break;
        }
        Destroy(gameObject);
    }
}
