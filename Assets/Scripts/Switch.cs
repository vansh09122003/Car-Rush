using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    private void Start()
    {
        player.GetComponent<PlayerAgent>().enabled = false;
    }

    public void onSwitch() {
        if (player.GetComponent<Player_Movements>().enabled == true)
        {
            player.GetComponent<PlayerAgent>().enabled = true;
            player.GetComponent<Player_Movements>().enabled = false;
        }
        else
        {
            player.GetComponent<PlayerAgent>().enabled = false;
            player.GetComponent<Player_Movements>().enabled = true;
        }
        
    }

    public void play()
    {
        EditorSceneManager.LoadScene("Main Scene");
    }

    public void onRestart() {
        EditorSceneManager.LoadScene("Main Scene");
    }

}