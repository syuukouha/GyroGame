using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float waveTimer = 0;
    float rateTimer = 0;
    //产生敌人的速率
    public float Rate;
    //敌人的预制体
    public GameObject EnemeyPrefab;
    //每一波的时间
    public float TimesofEachWave = 30;
    //每一波的已经产生的数量
    public int Count;

    void Update()
    {

        waveTimer += Time.deltaTime;
        if (waveTimer < TimesofEachWave && Count != 10)
        {
            rateTimer += Time.deltaTime;
            if (rateTimer > Rate)
            {
                Instantiate(EnemeyPrefab, Vector3.zero, Quaternion.identity);
                Count++;
                rateTimer -= Rate;
            }
        }
    }
}
