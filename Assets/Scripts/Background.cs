using UnityEngine;

public class Background : MonoBehaviour
{
    private Renderer renderer; //렌더러
    private float offset; //오프셋 값
    public float speed; //배경 속도

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<Renderer>(); //렌더러 초기화
    }

    // Update is called once per frame
    void Update()
    {
        offset = Time.time * speed; //오프셋 값에 배경 속도를 곱한다.
        //렌더러의 _BaseMap의 오프셋값을 offset으로 설정한다.
        renderer.material.SetTextureOffset("_BaseMap", new Vector2(offset, 0));
    }
}
