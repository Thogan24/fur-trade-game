using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDontDestroyScript : MonoBehaviour
{

    public static MusicDontDestroyScript instance;


    private void Awake()
    {
        CreateSingleton();
    }

    void CreateSingleton()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;

        }
        else
        {
            Debug.Log("Destroyed");
            Destroy(gameObject);
        }


    }

}
