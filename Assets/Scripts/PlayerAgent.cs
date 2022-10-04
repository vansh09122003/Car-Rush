using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using TMPro;

public class PlayerAgent : Agent{

    private GameObject player;
    [SerializeField]
    private TMP_Text live_count,countDown_txt;
    [SerializeField]
    private TMP_Text score_count;
    [SerializeField]
    private GameObject reset;
    private int lives = 3;

    GameObject GetClosestReward(GameObject[] rd)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in rd)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    void OnCollisionEnter2D(Collision2D ob){ 
        if(ob.collider.tag=="Obstacles"){
            if(lives>=1){
                lives -= 1;
                live_count.text = lives.ToString();
                Out();
            }
            else{
                reset.SetActive(true);
                Spawn.score=0;
                score_count.text = "0";
                AddReward(-1f);
                lives = 3;
                live_count.text = lives.ToString();
                Out();
            }
            Destroy(ob.collider.gameObject); 
        }
    }

    private void Out() {
        Spawn.sped = 10f;
        AddReward(-1f);
        this.transform.position = new Vector2(0, -13f);
        EndEpisode();
    }

    public override void Initialize(){
        player=this.transform.gameObject;
        lives=3;
        reset.SetActive(false);
        player.transform.position=new Vector2(0,-13f);
        live_count.text=lives.ToString();
    }

    public override void CollectObservations(VectorSensor sensor){
        float player_x;
        player_x=this.transform.position.x;
        sensor.AddObservation(this.transform.position.x);
        GameObject[] spwans;
        spwans=GameObject.FindGameObjectsWithTag("Reward");
        GameObject redw = GetClosestReward(spwans);
        sensor.AddObservation(redw);
    }

    void Update(){
        AddReward(0.001f);
        if(desicionRequester()){
            RequestDecision();
        }        
    }
    
    bool desicionRequester(){
        GameObject[] reward=GameObject.FindGameObjectsWithTag("Reward");
        GameObject redw = GetClosestReward(reward);
        if (redw != null)
        {
            if (this.transform.position.x == redw.transform.position.x)
            {
                return false;
            }
        }
        return true;
    }

    public override void OnActionReceived(ActionBuffers pos){
        if(pos.DiscreteActions[0]==1){
            right();
        }
        else if(pos.DiscreteActions[0]==0){
            left();
        }
    }

    void right(){
        player.transform.position = new Vector2(player.transform.position.x + 4f ,player.transform.position.y);
        if(!valid_move()){
            player.transform.position = new Vector2(player.transform.position.x - 4f ,player.transform.position.y);
        }
    }
    void left(){
        player.transform.position = new Vector2(player.transform.position.x - 4f ,player.transform.position.y);
        if(!valid_move()){
            player.transform.position = new Vector2(player.transform.position.x + 4f ,player.transform.position.y);
        }
    }
    private bool valid_move(){
        if(player.transform.position.x>8 || player.transform.position.x<-8){
            AddReward(-0.01f);
            return false;
        }
        return true;
    }

    void OnTriggerEnter2D(Collider2D ob){
        if(ob.tag=="Reward"){
            AddReward(+1f);  
            EndEpisode();
        }
    }
}
