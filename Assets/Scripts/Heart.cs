using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Heart : MonoBehaviour
{
    public List<Image> heartSprite = new List<Image>(); //하트 스프라이트

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HeartOff()
    {
        heartSprite[1].enabled = false; //1번 배열의 하트를 끈다.
    }
}
