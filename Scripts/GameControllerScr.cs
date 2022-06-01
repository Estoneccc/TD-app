using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Tower
{
    public string Name;
    public int Price;
    public int type;
    public float range;
    public float Cooldown;
    public float CurrCooldown;
    public Sprite Spr;

    public Tower(string Name, int Price, int type, float range, float cd, string path)
    {
        this.Name = Name;
        this.Price = Price;
        this.type = type;
        this.range = range;
        Cooldown = cd;
        CurrCooldown = 0;
        Spr = Resources.Load<Sprite>(path);
    }
}

public struct TowerProjectTile
{
    public float speed;
    public int damage;
    public Sprite Spr;

    public TowerProjectTile(float speed, int dmg, string path)
    {
        this.speed = speed;
        damage = dmg;
        Spr = Resources.Load<Sprite>(path);
    }
}

public struct Enemy
{
    public float health;
    public float speed;
    public float startSpeed;
    public int cost;
    public Sprite Spr;

    public Enemy(int cost, float health, float speed, string path)
    {
        this.health = health;
        startSpeed = this.speed = speed;
        Spr = Resources.Load<Sprite>(path);
        this.cost = cost;
    }
}

public enum TowerType
{
    First_Tower, Second_Tower, Third_Tower
}

public class GameControllerScr : MonoBehaviour
{
    public List<Tower> AllTowers = new List<Tower>();
    public List<TowerProjectTile> AllProjectTiles = new List<TowerProjectTile>();
    public List<Enemy> AllEnemies = new List<Enemy>();

    private void Awake()
    {
        AllTowers.Add(new Tower("FastShooting Tower", 15, 0, 2.5f, 0.5f, "TowerSprites/DefaultTower"));
        AllTowers.Add(new Tower("LargeRadius, AreaDamage Tower", 20, 1, 5, 2, "TowerSprites/SplashTower"));
        AllTowers.Add(new Tower("SmallRadius, Slow Tower", 40, 2, 1.5f, 1, "TowerSprites/FrozeTower"));

        AllProjectTiles.Add(new TowerProjectTile(6, 10, "ProjecttilesSprites/DefaultTile"));
        AllProjectTiles.Add(new TowerProjectTile(5, 20, "ProjecttilesSprites/SplashTile"));
        AllProjectTiles.Add(new TowerProjectTile(6, 5, "ProjecttilesSprites/FrozeTile"));

        AllEnemies.Add(new Enemy(4, 40, 2, "EnemySprites/SlowMonster"));
        AllEnemies.Add(new Enemy(4, 30, 3, "EnemySprites/FastMonster"));
        AllEnemies.Add(new Enemy(6, 150, 1.5f, "EnemySprites/BigMonster"));
    }
}