using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum EEnemyState
{
    PathMove,
    PlayerTrack,
}
public class EnemyController : MonoBehaviour
{
    public EEnemyState enemyState = EEnemyState.PathMove;

    private bool isPlayerTrack = false;
    private InterpolationPath interpolationPath;
    private GameObject player;
    private void Awake()
    {
        interpolationPath = GetComponent<InterpolationPath>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemyState();
    }

    void ChangeEnemyState(EEnemyState enemyState)
    {
        this.enemyState = enemyState;
    }

    void UpdateEnemyState()
    {
        switch (enemyState)
        {
            case EEnemyState.PathMove:
                interpolationPath.IsPathMove = true;
                isPlayerTrack = false;
                break;
            case EEnemyState.PlayerTrack:
                interpolationPath.IsPathMove = false;
                isPlayerTrack = true;
                transform.position += (player.transform.position - transform.position) * Time.deltaTime;
                transform.LookAt(player.transform);
                break;
            default:
                break;
        }
    }
}
