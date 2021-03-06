using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public string[] enemyObjs;
    public Transform[] spawnPoints;

    public float nextSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;
    public Text scroeText;
    public Image[] lifeImage;
    public Image[] boomImage;

    public GameObject gameOverSet;
    public ObjectManager objectManager;



    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    void Awake()
    {
        spawnList = new List<Spawn>();
        enemyObjs = new string[] { "EnemyS", "EnemyM", "EnemyL", "EnemyB" };
        ReadSpawnFile();
    }

    void ReadSpawnFile()
    {
        // # 1. 변수 초기화
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        // # 2. 리스폰 파일 읽기
        TextAsset textFile = Resources.Load("Stage 0") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);
        
        while(stringReader != null)
        {
            string line = stringReader.ReadLine();
            
            Debug.Log(line);

            if (line == null)
                break;

            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }

        // # 텍스트 파일 닫기
        stringReader.Close();

        // #. 첫번째 스폰 딜레이 적용
        nextSpawnDelay = spawnList[0].delay;
    }

    void Update()
    {
        curSpawnDelay += Time.deltaTime;
        
        if(curSpawnDelay > nextSpawnDelay &&  !spawnEnd)
        {
            SpawnEnemy();
            curSpawnDelay = 0;
        }

        // # UI Score Update
        Player playerLogic = player.GetComponent<Player>();
        scroeText.text = string.Format("{0:n0}", playerLogic.score); 
    }

    void SpawnEnemy()
    {
        int enemyIndex = 0;
        switch (spawnList[spawnIndex].type)
        {
            case "S":
                enemyIndex = 0;
                break;
            case "M":
                enemyIndex = 1;
                break;
            case "L":
                enemyIndex = 2;
                break;
            case "B":
                enemyIndex = 3;
                break;
        }

        int enemyPoint = spawnList[spawnIndex].point;

        GameObject enemy = objectManager.MakeObj(enemyObjs[enemyIndex]);
        enemy.transform.position = spawnPoints[enemyPoint].position;
        
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        enemyLogic.objectManager = objectManager;

        if (enemyPoint == 7 || enemyPoint == 8) //Right Spawn
        {
            enemy.transform.Rotate(Vector3.back * 45);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
        }
        else if(enemyPoint == 5 || enemyPoint == 6) //Left Spawn
        {
            enemy.transform.Rotate(Vector3.forward * 45);
            rigid.velocity = new Vector2(enemyLogic.speed , -1);

        }
        else // Front Spawn
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }
        // # 리스폰 인덱스 증가
        spawnIndex++;
        if(spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }
        // # 다음 리스폰 딜레이 갱신
        nextSpawnDelay = spawnList[spawnIndex].delay;
    }
    public void UpdateLifeIcon(int life)
    {
        // # UI Life Init Disable
        for (int idx = 0; idx < 3; idx++)
        {
            lifeImage[idx].color = new Color(1, 1, 1, 0);
        }

        // # UI Life Active
        for (int idx =0; idx <life; idx++)
        {
            lifeImage[idx].color = new Color(1, 1, 1, 1);
        }
    }
    public void UpdateboomIcon(int boom)
    {
        // # UI Boom Init Disable
        for (int idx = 0; idx < 3; idx++)
        {
            boomImage[idx].color = new Color(1, 1, 1, 0);
        }

        // # UI Boom Active
        for (int idx = 0; idx < boom; idx++)
        {
            boomImage[idx].color = new Color(1, 1, 1, 1);
        }
    }

    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f);

    }
    void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down * 3.5f;
        player.SetActive(true);
        
        Player playerLogic = player.GetComponent<Player>();
        playerLogic.isHit = false;
    }

    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }
    public void GameRetry ()
    {
        SceneManager.LoadScene(0);
    }
}
