using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float timer = 0;
    float timer2 = 0;
    //产生敌人的速率
    public float rate;
    //敌人的预制体
    public GameObject enemeyPrefab;
    //每一波的时间
    public float timesofEachWave = 30;
    //每一波的已经产生的数量
    public int count;

    void Update()
    {

        timer2 += Time.deltaTime;
        if (timer2 < timesofEachWave && count != 10)
        {
            timer += Time.deltaTime;

            if (timer > rate)
            {
                Instantiate(enemeyPrefab, Vector3.zero, Quaternion.identity);

                count++;

                timer -= rate;
            }
        }
    }
}
