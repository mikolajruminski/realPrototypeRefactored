using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    private const string SPAWN_BOMB_METHOD = "SpawnBomb";

    private enum State
    {
        ToBottom,
        ToRight,
        ToLeft,
        ToTop,
    }

    [SerializeField] private List<GameObject> spawnPoints = new List<GameObject>();
    [SerializeField] GameObject bombPrefab;
    [SerializeField] float timeFromStart;
    [SerializeField] float repeatRate;
    [SerializeField] private State state;
    [SerializeField] private float bombGravity;
    [SerializeField] private float bombForce;
    private GameObject activeSpawnPoint;

    private bool canSpawn;

    private void Awake()
    {
        activeSpawnPoint = spawnPoints[1].gameObject;
    }
    private void Start()
    {
        canSpawn = true;
        StartSpawning();
        SwitchState();
    }

    private void Update()
    {
        bombPrefab.GetComponent<Rigidbody2D>().gravityScale = bombGravity;
        bombPrefab.GetComponent<BombBehaviour>().SetForce(bombForce);
    }

    private void SwitchState()
    {
        switch (state)
        {
            case State.ToBottom:

                SetActiveSpawnPoint(1);
                if (bombPrefab.GetComponent<BombBehaviour>().GetBombState() != BombBehaviour.State.ToBottom)
                {
                    bombPrefab.GetComponent<BombBehaviour>().SetBombState(BombBehaviour.State.ToBottom);
                }
                break;
            case State.ToRight:
                SetActiveSpawnPoint(3);
                if (bombPrefab.GetComponent<BombBehaviour>().GetBombState() != BombBehaviour.State.ToRight)
                {
                    bombPrefab.GetComponent<BombBehaviour>().SetBombState(BombBehaviour.State.ToRight);
                }
                break;
            case State.ToLeft:
                SetActiveSpawnPoint(2);
                if (bombPrefab.GetComponent<BombBehaviour>().GetBombState() != BombBehaviour.State.ToLeft)
                {
                    bombPrefab.GetComponent<BombBehaviour>().SetBombState(BombBehaviour.State.ToLeft);
                }
                break;
            case State.ToTop:

                SetActiveSpawnPoint(0);
                if (bombPrefab.GetComponent<BombBehaviour>().GetBombState() != BombBehaviour.State.ToTop)
                {
                    bombPrefab.GetComponent<BombBehaviour>().SetBombState(BombBehaviour.State.ToTop);
                }
                break;
        }
    }

    private void StartSpawning()
    {
        if (canSpawn)
        {
            InvokeRepeating(SPAWN_BOMB_METHOD, timeFromStart, repeatRate);
        }
    }

    private void SpawnBomb()
    {
        Instantiate(bombPrefab, activeSpawnPoint.transform);
    }

    private void SetActiveSpawnPoint(int index)
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            spawnPoints[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            spawnPoints[index].gameObject.SetActive(true);
            activeSpawnPoint = spawnPoints[index].gameObject;
        }

    }

}
