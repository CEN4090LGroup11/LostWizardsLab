using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{

    public string sceneName = "MiniGame1";

    //will press F to interact with game marker and start minigame
    public KeyCode activationKey = KeyCode.F;

    //see if close enough to game marker to allow trigger to start game
    private bool playerInRange = false;

    public Transform playerTransform;


    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void SwitchtoMiniGame(Transform playerTransform)
    {
        if(MainSceneManager.instance != null)
        {
            MainSceneManager.instance.SavePlayerState(playerTransform);

        }


        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {

        if(playerInRange && Input.GetKeyDown(activationKey))
        {
            SwitchtoMiniGame(playerTransform);
        }

        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {

            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = false;
        }
        
    }



}
