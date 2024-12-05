using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{

    public static MainSceneManager instance;
    public Vector3 playerSpawnPosition;
    public Quaternion playerSpawnRotation;



    private void Awake()
    {
       if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void SavePlayerState(Transform playerTransform)
    {
        // maybe add instance.playerSpawnPosition
        playerSpawnPosition = playerTransform.position;
        playerSpawnRotation = playerTransform.rotation;
    }

    public void RestorePlayerState(Transform playerTransform)
    {
        playerTransform.position = playerSpawnPosition;
        playerTransform.rotation = playerSpawnRotation;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
