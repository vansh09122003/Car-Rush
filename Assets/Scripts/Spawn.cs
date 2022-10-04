using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawn : MonoBehaviour
{
    public static int score=0;
    public static bool end=false;
    private float sTime=3f;
    public static float sped=10f;

    [SerializeField]
    private GameObject[] obstacles;
    [SerializeField]
    private TMP_Text score_count,countDown_txt;

    void Start()
    {
        end = false;
        StartCoroutine(countDown());
    }

    private void spawn(){
        GameObject obs = Instantiate(obstacles[Random.Range(0,obstacles.Length)]) as GameObject;
        obs.transform.position=new Vector2(0,32f);
        score_count.text = score.ToString();
        obs.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-sped);
        score+=10;
    }

    IEnumerator countDown(){
        countDown_txt.text="3";
        yield return new WaitForSeconds(1f);
        countDown_txt.text="2";
        yield return new WaitForSeconds(1f);
        countDown_txt.text="1";
        yield return new WaitForSeconds(1f);
        countDown_txt.text="0";
        yield return new WaitForSeconds(1f);
        countDown_txt.text="GO";
        yield return new WaitForSeconds(1f);
        countDown_txt.text=" ";
        StartCoroutine(run());
    }

    IEnumerator run(){
        while(!end){
            spawn();
            if(score%100==0){
                if(sped>20f){
                    sped+=0.2f;
                }
            }
            yield return new WaitForSeconds(sTime);
        }
    }
}
