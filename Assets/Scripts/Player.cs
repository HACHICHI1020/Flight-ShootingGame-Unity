using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public enum PlayerLiveState
    {
        Live, Dead
    }
    public PlayerLiveState playerLiveState = PlayerLiveState.Live; //플레이어 생존 상태

    public enum PlayerDamageState
    {
        None, Damage
    }
    public PlayerDamageState playerDamageState = PlayerDamageState.None; //플레이어 대미지 상태

    public float speed; //이동 속도

    public Transform firePos; //발사 위치
    public GameObject rocketPrefab; //로켓 프리팹

    public List<Heart> heartList = new List<Heart>(); //하트 리스트
    private int playerHP; //플레이어HP

    public Animator playerAnim; //플레이어 애니메이터
    private float twinkleTime; //깜빡이는 시간
    public AnimationClip twinkleAnim; //깜빡임 애니메이션

    public GameObject expPrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHP = heartList.Count; //플레이어HP 초기화
    }

    // Update is called once per frame
    void Update()
    {
        switch(playerLiveState)
        {
            case PlayerLiveState.Live: //생존 중 상태
                {
                    PlayerMove(); //플레이어 이동 함수 호출
                    PlayerFire(); //플레이어 사격 함수 호출

                    switch (playerDamageState)
                    {
                        case PlayerDamageState.Damage: //대미지 입은 상태
                            {
                                twinkleTime += Time.deltaTime; //깜빡이는 시간 재생

                                //깜빡이는 시간이 설정 시간을 지나면
                                if (twinkleTime >= twinkleAnim.length)
                                {
                                    twinkleTime = 0;
                                    GetComponent<CapsuleCollider>().enabled = true; //콜라이더 실행
                                    playerAnim.SetBool("IsDamaged", false); //깜빡임 애니메이션 중지
                                    playerDamageState = PlayerDamageState.None; //생존상태로 되돌림
                                }
                                break;
                            }
                    }
                    break;
                }
            case PlayerLiveState.Dead:
                {
                    if(Input.GetKeyDown(KeyCode.Space))
                    {
                        SceneManager.LoadScene(0);
                    }
                    break;
                }
        }
    }

    //플레이어 이동 함수
    void PlayerMove()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //오른쪽으로 speed의 속도만큼 이동한다.
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //왼쪽으로 speed의 속도만큼 이동한다.
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            //위쪽으로 speed의 속도만큼 이동한다.
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            //아래쪽으로 speed의 속도만큼 이동한다.
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        //플레이어의 X축과 Y축의 이동 반경을 제한한다.
        transform.position = new Vector3
            (Mathf.Clamp(transform.position.x, -8, 8),
            Mathf.Clamp(transform.position.y, -4, 4.8f),
            transform.position.z);
    }

    //플레이어 사격 함수
    void PlayerFire()
    {
        //스페이스 바 키를 누른다면
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //로켓을 생성한다.(원본, 초기위치, 초기회전, 부모)
            GameObject rocket = Instantiate(rocketPrefab,
                firePos.position, Quaternion.identity, null);

            //5초 뒤에 로켓 삭제
            Destroy(rocket, 5.0f);
        }
    }

    //플레이어 대미지 받는 함수
    public void PlayerDamaged()
    {
        //대미지를 받더라도 0보다 크다면 생존
        if (playerHP - 1 > 0)
        {
            
            playerHP--; //현재 HP 1 감소
            heartList[playerHP].HeartOff(); //playerHP번째의 하트를 끈다.
            GetComponent<CapsuleCollider>().enabled = false; //콜라이더 끄기
            playerAnim.SetBool("IsDamaged", true); //깜빡임 애니메이션 실행
            playerDamageState = PlayerDamageState.Damage; //대미지 입은 상태로 변경
        }
        else //대미지를 받으면 0과 같거나 작을 때 -> 사망 처리
        {
            
            playerHP = 0;
            GetComponent<CapsuleCollider>().enabled = false;
            heartList[playerHP].HeartOff(); //playerHP번째의 하트를 끈다.
            playerAnim.gameObject.SetActive(false); //플레이어를 안보이게 끈다.
            playerLiveState = PlayerLiveState.Dead; //사망 상태로 변경
        }
    }
    public void CreateExplosion()
    {
        GameObject explosion = Instantiate(expPrefab, transform.position, Quaternion.identity, null);
        Destroy(explosion, 5.0f);
    }
}
