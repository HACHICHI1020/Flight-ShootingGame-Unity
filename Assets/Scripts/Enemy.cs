using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform propeller; //프로펠러
    public float rotSpeed; //프로펠러 회전 속도
    public float moveSpeed; //이동 속도

    public enum LaunchState
    {
        None, Launch
    }
    public LaunchState launchState = LaunchState.None; //발사 상태
    private float launchTime; //발사 시간
    private float launchTimeOffset; //발사 시간 오프셋

    public Transform firePos; //발사 위치
    public GameObject rocketPrefab; //로켓 프리팹


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //2초에서 4초 사이로 발사 시간 설정
        launchTimeOffset = Random.Range(1.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
        EnemyLaunch();
    }

    void EnemyMove()
    {
        //프로펠러 회전
        propeller.Rotate(Vector3.back *  rotSpeed  * Time.deltaTime);

        //왼쪽으로 moveSpeed의 속도로 이동
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    //발사 함수
    void EnemyLaunch()
    {
        switch(launchState)
        {
            case LaunchState.None: //발사 전 상태
                {
                    launchTime += Time.deltaTime; //발사 시간 재생
                    if (launchTime >= launchTimeOffset)//발사 시간이 설정 시간에 도달할 경우
                    {
                        CreateRocket(); //로켓 생성 함수 호출
                        launchState = LaunchState.Launch; //발사한 상태로 변경
                    }
                    break;
                }
        }    
    }
     //로켓 생성 함수
    void CreateRocket()
    {
        //로켓을 생성한다. (원본, 위치, 회전, 부모)
        GameObject rocket = Instantiate(rocketPrefab, 
            firePos.position, firePos.rotation, null);
    }

   
    //트리거 충돌 시작 함수
    private void OnTriggerEnter(Collider other)
    {
        //충돌 대상의 태그가 Player라면
        if(other.transform.CompareTag("Player"))
        {
            //플레이어 대미지 함수 호출
            other.transform.GetComponent<Player>().PlayerDamaged();
            other.transform.GetComponent<Player>().CreateExplosion();
            Destroy(gameObject); //적 제거
        }
    }
}
