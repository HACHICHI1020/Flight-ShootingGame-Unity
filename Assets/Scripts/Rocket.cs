using UnityEngine;

public class Rocket : MonoBehaviour
{
    public enum RocketType
    {
        PlayerRocket, EnemyRocket
    }
    public RocketType rocketType; //로켓 타입

    public Transform rocketMesh; //로켓 메쉬
    public float rotSpeed; //회전 속도
    public float moveSpeed; //이동 속도
    public GameObject expPrefab; //폭발 프리팹
    // Update is called once per frame
    void Update()
    {
        RocketAct();
    }

    void RocketAct()
    {
        //로켓 메쉬를 X축 방향으로 rotSpeed의 속도로 회전시킨다.
        rocketMesh.Rotate(Vector3.up * rotSpeed * Time.deltaTime);

        //오른쪽으로 moveSpeed의 속도로 이동한다.
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    //트리거 충돌 시작 함수
    private void OnTriggerEnter(Collider other)
    {
        switch(rocketType)
        {
            case RocketType.PlayerRocket://플레이어 로켓
                {
                    //충돌한 대상의 Tag가 Enemy라면
                    if (other.transform.CompareTag("Enemy"))
                    {
                        CreateExplosion();
                        Score.instance.GetScore();
                        Destroy(other.transform.gameObject); //충돌 대상 제거
                        Destroy(gameObject); //자신(로켓)도 제거
                    }
                    break;
                }
            case RocketType.EnemyRocket:// 적 로켓
                {
                    //충돌한 대상의 Tag가 Player라면
                    if (other.transform.CompareTag("Player"))
                    {
                        CreateExplosion();
                        //플레이어 대미지 함수 호출
                        other.transform.GetComponent<Player>().PlayerDamaged();
                        Destroy(gameObject); //자신(로켓)도 제거
                    }
                    break;
                }
        }


    }
    public void CreateExplosion()
    {
        GameObject explosion = Instantiate(expPrefab, transform.position, Quaternion.identity, null);
        Destroy(explosion, 5.0f);
    }

}
