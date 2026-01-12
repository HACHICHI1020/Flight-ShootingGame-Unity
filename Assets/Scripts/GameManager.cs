using UnityEngine;
using UnityEngine.UI; //UGUI »ç¿ë
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
