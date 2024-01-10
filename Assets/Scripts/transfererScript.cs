using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transfererScript : MonoBehaviour
{
    public GameObject gamemanager;
    void Awake()
    {
        DontDestroyOnLoad(gamemanager);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
