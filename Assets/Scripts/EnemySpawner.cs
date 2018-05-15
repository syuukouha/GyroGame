using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EnemySpawner : MonoBehaviour
{
    [BoxGroup("产生敌人的速率", centerLabel: true)]
    //产生敌人的速率
    public float Rate;
    [BoxGroup("敌人的预制体", centerLabel: true)]
    [SuffixLabel("Prefab")]
    //敌人的预制体
    public GameObject EnemeyPrefab;
    [BoxGroup("每一波的时间", centerLabel: true)]
    //每一波的时间
    public float TimesOfEachWave = 30;
    [BoxGroup("每一波的已经产生的数量", centerLabel: true)]
    //每一波的已经产生的数量
    public int Count;

    float waveTimer = 0;
    float rateTimer = 0;

    void Update()
    {

        waveTimer += Time.deltaTime;
        if (waveTimer < TimesOfEachWave && Count != 10)
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
