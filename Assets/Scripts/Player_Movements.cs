using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_Movements : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject reset;
    [SerializeField]
    private TMP_Text live_count,countDown;
    private int lives=3;

    void OnCollisionEnter2D(Collision2D ob) { 
        if(ob.collider.tag=="Obstacles"){
            if(lives>=1){
                reset_state();
            }
            else{
                reset.SetActive(true);
                Spawn.end=true;
                ob.collider.gameObject.GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
                Destroy(this.gameObject);
                countDown.text="Game Over";
            }
        }
    }

    private void Start(){
        reset.SetActive(false);
        live_count.text=lives.ToString();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            player.transform.position = new Vector2(player.transform.position.x + 4f ,player.transform.position.y);
            if(!valid_move()){
                player.transform.position = new Vector2(player.transform.position.x - 4f ,player.transform.position.y);
            }
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow)){
            player.transform.position = new Vector2(player.transform.position.x - 4f ,player.transform.position.y);
            if(!valid_move()){
                player.transform.position = new Vector2(player.transform.position.x + 4f ,player.transform.position.y);
            }
        }
    }

    private bool valid_move(){
        if(player.transform.position.x>8 || player.transform.position.x<-8){
            return false;
        }
        return true;
    }

    private void reset_state(){
        Spawn.sped=10f;
        this.transform.gameObject.GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
        this.transform.position=new Vector2(0,-13f);
        lives-=1;
        live_count.text=lives.ToString();
        GameObject[] obs;
        obs=GameObject.FindGameObjectsWithTag("Obstacles");
        foreach(GameObject s in obs){
            Destroy(s.gameObject);
        }
    }
}
