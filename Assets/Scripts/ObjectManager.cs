using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyBPrefab;
    public GameObject enemyLPrefab;
    public GameObject enemyMPrefab;
    public GameObject enemySPrefab;
    public GameObject itemCoinPrefab;
    public GameObject itemPowerPrefab;
    public GameObject itemBoomPrefab;
    public GameObject bulletPlayerAPrefab;
    public GameObject bulletPlayerBPrefab;
    public GameObject bulletEnemyAPrefab;
    public GameObject bulletEnemyBPrefab;
    public GameObject bulletBossAPrefab;
    public GameObject bulletBossBPrefab;

    public GameObject bulletFollowerPrefab;

    GameObject[] enemyB;
    GameObject[] enemyL;
    GameObject [] enemyM;
    GameObject [] enemyS;

    GameObject [] itemCoin;
    GameObject [] itemPower;
    GameObject [] itemBoom;

    GameObject[] bulletPlayerA;
    GameObject[] bulletPlayerB;
    GameObject[] bulletEnemyA;
    GameObject[] bulletEnemyB;
    GameObject[] bulletBossA;
    GameObject[] bulletBossB;


    GameObject[] bulletFollower;

    GameObject[] targetPool;

    void Awake()
    {
        enemyB = new GameObject[1];
        enemyL = new GameObject[10];
        enemyM = new GameObject[10];
        enemyS = new GameObject[20];

        itemCoin = new GameObject[20];
        itemPower = new GameObject[10];
        itemBoom = new GameObject[10];

        bulletPlayerA = new GameObject[100];
        bulletPlayerB = new GameObject[100];
        bulletEnemyA = new GameObject[100];
        bulletEnemyB = new GameObject[100];
        bulletBossA = new GameObject[100];
        bulletBossB = new GameObject[100];

        bulletFollower = new GameObject[100];

        Generate();
    }
    void Generate()
    {
        // # 1. Enemy
        for (int ind = 0; ind < enemyB.Length; ind++)
        {
            enemyB[ind] = Instantiate(enemyBPrefab);
            enemyB[ind].SetActive(false);
        }
        for (int ind =0; ind < enemyL.Length; ind++)
        {
            enemyL[ind] = Instantiate(enemyLPrefab);
            enemyL[ind].SetActive(false);
        }
        for (int ind = 0; ind < enemyM.Length; ind++)
        {
            enemyM[ind] = Instantiate(enemyMPrefab);
            enemyM[ind].SetActive(false);
        }
        for (int ind = 0; ind < enemyS.Length; ind++)
        {
            enemyS[ind] = Instantiate(enemySPrefab);
            enemyS[ind].SetActive(false);
        }


        // # 2. Item
        for (int ind = 0; ind < itemCoin.Length; ind++)
        {
            itemCoin[ind] = Instantiate(itemCoinPrefab);
            itemCoin[ind].SetActive(false);
        }    
        for (int ind = 0; ind < itemPower.Length; ind++)
        {
            itemPower[ind] = Instantiate(itemPowerPrefab);
            itemPower[ind].SetActive(false);
        }
        for (int ind = 0; ind < itemBoom.Length; ind++)
        {
            itemBoom[ind] = Instantiate(itemBoomPrefab);
            itemBoom[ind].SetActive(false);
        }

        // # 3. Player Bullet
        for (int ind = 0; ind < bulletPlayerA.Length; ind++)
        {
            bulletPlayerA[ind] = Instantiate(bulletPlayerAPrefab);
            bulletPlayerA[ind].SetActive(false);
        }
        for (int ind = 0; ind < bulletPlayerB.Length; ind++)
        {
            bulletPlayerB[ind] = Instantiate(bulletPlayerBPrefab);
            bulletPlayerB[ind].SetActive(false);
        }

        // # 4. Enemy Bullet
        for (int ind = 0; ind < bulletEnemyA.Length; ind++)
        {
            bulletEnemyA[ind] = Instantiate(bulletEnemyAPrefab);
            bulletEnemyA[ind].SetActive(false);
        }
        for (int ind = 0; ind < bulletEnemyB.Length; ind++)
        {
            bulletEnemyB[ind] = Instantiate(bulletEnemyBPrefab);
            bulletEnemyB[ind].SetActive(false);
        }
        for (int ind = 0; ind < bulletBossA.Length; ind++)
        {
            bulletBossA[ind] = Instantiate(bulletBossAPrefab);
            bulletBossA[ind].SetActive(false);
        }
        for (int ind = 0; ind < bulletBossB.Length; ind++)
        {
            bulletBossB[ind] = Instantiate(bulletBossBPrefab);
            bulletBossB[ind].SetActive(false);
        }

        for (int ind = 0; ind < bulletFollower.Length; ind++)
        {
            bulletFollower[ind] = Instantiate(bulletFollowerPrefab);
            bulletFollower[ind].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "EnemyB":
                targetPool = enemyB;
                break;
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "ItemCoin":
                targetPool = itemCoin;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "ItemBoom":
                targetPool = itemBoom;
                break;
            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "BulletBossA":
                targetPool = bulletBossA;
                break;
            case "BulletBossB":
                targetPool = bulletBossB;
                break;
            case "BulletFollower":
                targetPool = bulletFollower;
                break;
        }
        for (int ind = 0; ind < targetPool.Length; ind++)
        {
            if (!targetPool[ind].activeSelf)
            {
                targetPool[ind].SetActive(true);
                return targetPool[ind];
            }
        }
        return null;
    }

    public GameObject[] GetPool(string type)
    {

        switch (type)
        {
            case "EnemyB":
                targetPool = enemyB;
                break;
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "ItemCoin":
                targetPool = itemCoin;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "ItemBoom":
                targetPool = itemBoom;
                break;
            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "BulletBossA":
                targetPool = bulletBossA;
                break;
            case "BulletBossB":
                targetPool = bulletBossB;
                break;
            case "BulletFollower":
                targetPool = bulletFollower;
                break;
        }
        return targetPool;
    }
}

