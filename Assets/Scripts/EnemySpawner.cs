using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    //적 생성 상태
    public enum EnemySpawnState
    {
        None, Spawn
    }
    public EnemySpawnState enemySpawnState = EnemySpawnState.None;

    private float spawnTime; //소환시간
    private float spawnTimeOffset; //소환 주기

    public GameObject enemyPrefab; //적 프리팹
    public List<Transform> spawnPos = new List<Transform>(); //소환 위치



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //1초에서 4초사이로 주기를 무작위 지정
        spawnTimeOffset = Random.Range(1.0f, 4.0f); 
    }

    // Update is called once per frame
    void Update()
    {
        switch(enemySpawnState)
        {
            case EnemySpawnState.Spawn:
                {
                    spawnTime += Time.deltaTime; //소환시간 시작

                    if(spawnTime >= spawnTimeOffset)
                    {
                        SpawnEnemy(); //적 소환 함수 호출
                        spawnTime = 0;
                    }
                    break;
                }
        }
    }

    //적 소환 함수
    void SpawnEnemy()
    {
        //소환 위치의 Y축 최솟값과 최댓값 사이에 무작위로 float형 값 생성
        float posY = Random.Range(spawnPos[0].position.y, spawnPos[1].position.y);

        //적을 생성한다. (원본, 초기위치, 초기회전, 부모)
        GameObject enemy = Instantiate(enemyPrefab, 
            new Vector3(transform.position.x, posY, transform.position.z), 
            Quaternion.identity, null);

        //10초 뒤에 적 삭제
        Destroy(enemy, 10.0f);

        //1초에서 4초사이로 주기를 무작위 지정
        spawnTimeOffset = Random.Range(1.0f, 4.0f);
    }
}
