using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public int enemyScore;
    public float speed;
    public int health;
    public Sprite[] sprites;

    public float maxShotDelay;
    public float curShotDelay;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject itemCoin;
    public GameObject itemPower;
    public GameObject itemBoom;

    public GameObject player;
    public ObjectManager objectManager;

    
    SpriteRenderer spriteRenderer;
    Animator anim;



    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(enemyName == "B")
            anim = GetComponent<Animator>();
    }
    void OnEnable()
    {
        switch (enemyName)
        {
            case "B":
                health = 3000;
                Invoke("Stop", 2);
                break;
            case "L":
                health = 40;
                break;
            case "M":
               health = 10;
                break;
            case "S":
                health = 3;
                break;
        }
    }
    void Stop()
    {
        if (!gameObject.activeSelf)
            return;

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;
    }
    void Think()
    {

    }
    void Fire()
    {
        if (!Input.GetButton("Fire1"))
            return;
        if (curShotDelay < maxShotDelay)
            return;


        curShotDelay = 0;
    }
    private void Update()
    {
        if (enemyName == "B")
            return;

        EnemyFire();
        EnemyReload();
    }

    void EnemyReload()
    {
        curShotDelay += Time.deltaTime;
    }
    void EnemyFire()
    {
        if (curShotDelay < maxShotDelay)
            return;

        if (enemyName == "S")
        {
            GameObject bullet = objectManager.MakeObj("BulletEnemyA");
            bullet.transform.position = transform.position;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        }
        else if(enemyName == "L")
        {
            GameObject bulletR = objectManager.MakeObj("BulletEnemyA");
            bulletR.transform.position = transform.position + Vector3.right * 0.3f;
            GameObject bulletL = objectManager.MakeObj("BulletEnemyA");
            bulletL.transform.position = transform.position + Vector3.left * 0.3f;

            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);

            rigidR.AddForce(dirVecR.normalized * 4, ForceMode2D.Impulse);
            rigidL.AddForce(dirVecL.normalized * 4, ForceMode2D.Impulse);
        }
        curShotDelay = 0;
    }

    public void OnHit(int dmg)
    {
        if(health <=0)
            return;
        health -= dmg;
        if (enemyName == "B")
        {
            anim.SetTrigger("OnHit");
        }
        else
        {
            spriteRenderer.sprite = sprites[1];
            Invoke("ReturnSprite", 0.1f);
        }

        if (health <= 0)
        {
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;

            // #. Random Ratio Item Drop
            int ran = enemyName == "B" ? 0 : UnityEngine.Random.Range(0, 10);
            if( ran < 1)
            {
                // No Item

            }
            else if( ran < 3)
            {
                // Coin
                GameObject itemCoin = objectManager.MakeObj("ItemCoin");
                itemCoin.transform.position = transform.position;
            }
            else if (ran < 6)
            {
                // Power
                GameObject itemPower = objectManager.MakeObj("ItemPower");
                itemPower.transform.position = transform.position;
            }
            else if (ran < 9)
            {
                // Boom
                GameObject itemBoom = objectManager.MakeObj("ItemBoom");
                itemBoom.transform.position = transform.position;
            }
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderBullet" && enemyName != "B")
        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
        else if(collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);
            collision.gameObject.SetActive(false);
        }
    }
    private void InVoke(string v1, float v2)
    {
        throw new NotImplementedException();
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];

    }
}
