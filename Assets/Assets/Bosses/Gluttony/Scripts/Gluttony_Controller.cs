using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gluttony_Controller : MonoBehaviour
{
    [Header("BulletSpawns")]
    [SerializeField] private Transform[] attackOne;
    [SerializeField] private Transform[] attackTwo;
    [SerializeField] private Transform[] attackThree;

    [Header("Prefabs")]
    [SerializeField] private GameObject shootPlayerBullet;
    [SerializeField] private GameObject shootBullet;
    [SerializeField] private GameObject winScreen;


    [Header("Boss Settings")]
    [SerializeField] private GameObject bossHeart;
    [SerializeField] private Transform bossHeartSpawn;
    [SerializeField] private float attackSpeed = .01f;
    public int phase = 0;
    [SerializeField] private bool attacking = false;
    public int attackAmount = 0;

    private int[] phaseOnePattern;
    private int[] phaseTwoPattern;
    private int[] phaseThreePattern;

    private int attackIndex;

    void Start()
    {
        winScreen.gameObject.SetActive(false);

        phaseOnePattern = new int[3];
        phaseTwoPattern = new int[3];
        phaseThreePattern = new int[3];

        for (int i = 0; i < 3; i++)
        {
            phaseOnePattern[i] = Random.Range(1, 4);
        }
        for (int i = 0; i < 3; i++)
        {
            phaseTwoPattern[i] = Random.Range(1, 4);
        }
        for (int i = 0; i < 3; i++)
        {
            phaseThreePattern[i] = Random.Range(1, 4);
        }
        Vulnerable();
    }

    private void Update()
    {
        if (phase == 1)
        {
            if (attacking == false && attackAmount < 3)
            {
                switch (phaseOnePattern[attackAmount])
                {
                    case 1:
                        AttackOne();
                        break;
                    case 2:
                        AttackTwo();
                        break;
                    case 3:
                        AttackThree();
                        break;
                }
                attackAmount++;
                attacking = true;
            }
            else if (attacking == false && attackAmount == 3)
            {
                Vulnerable();
                attackAmount++;
            }
        }

        if (phase == 2)
        {
            if (attacking == false && attackAmount < 3)
            {
                switch (phaseTwoPattern[attackAmount])
                {
                    case 1:
                        AttackOne();
                        break;
                    case 2:
                        AttackTwo();
                        break;
                    case 3:
                        AttackThree();
                        break;
                }
                attackAmount++;
                attacking = true;
            }
            else if (attacking == false && attackAmount == 3)
            {
                Vulnerable();
                attackAmount++;
            }
        }

        if (phase == 3)
        {
            if (attacking == false && attackAmount < 3)
            {
                switch (phaseThreePattern[attackAmount])
                {
                    case 1:
                        AttackOne();
                        break;
                    case 2:
                        AttackTwo();
                        break;
                    case 3:
                        AttackThree();
                        break;
                }
                attackAmount++;
                attacking = true;
            }
            else if (attacking == false && attackAmount == 3)
            {
                Vulnerable();
                attackAmount++;
            }
        }
        if (attacking == false && phase == 4)
        {
            Debug.Log("YOU WIN");
            winScreen.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void Vulnerable()
    {
        Instantiate(bossHeart, bossHeartSpawn.position, transform.rotation);
    }
    
    private void AttackOne()
    {
        attacking = true;
        attackIndex = 1;
        attackSpeed = .5f;
        StartCoroutine(SummonBullet());
    }

    private void AttackTwo()
    {
        attacking = true;
        attackIndex = 2;
        attackSpeed = 1f;
        StartCoroutine(SummonBullet());
    }

    private void AttackThree()
    {
        attacking = true;
        attackIndex = 3;
        attackSpeed = .1f;
        StartCoroutine(SummonBullet());
    }

    IEnumerator SummonBullet()
    {
        switch (attackIndex)
        {
            case 1:
                for (int i = 0; i < attackOne.Length; i++)
                {
                    yield return new WaitForSeconds(attackSpeed);
                    Instantiate(shootPlayerBullet, attackOne[i].position, transform.rotation);
                }
                yield return new WaitForSeconds(3.0f);
                attacking = false;
                break;
            case 2:
                for (int i = 0; i < attackTwo.Length; i++)
                {
                    yield return new WaitForSeconds(attackSpeed);
                    Instantiate(shootBullet, attackTwo[i].position, Quaternion.AngleAxis(180, new Vector3 (0,0,1)));
                    i++;
                    Instantiate(shootBullet, attackTwo[i].position, transform.rotation);
                }
                yield return new WaitForSeconds(3.0f);
                attacking = false;
                break;
            case 3:
                for (int i = 0; i < attackThree.Length * 2; i++)
                {
                    for (int p = 1; p != 10; p++)
                    {
                        yield return new WaitForSeconds(.1f);
                        Instantiate(shootBullet, attackThree[0].position, Quaternion.AngleAxis(180, new Vector3(0, 0, 1)));
                    }
                    for (int p = 1; p != 10; p++)
                    {
                        yield return new WaitForSeconds(.1f);
                        Instantiate(shootBullet, attackThree[1].position, Quaternion.AngleAxis(0, new Vector3(0, 0, 1)));
                    }
                }
                yield return new WaitForSeconds(3.0f);
                attacking = false;
                break;
        }
    }
}
