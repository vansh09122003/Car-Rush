using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D ob) { 
        if(ob.collider.tag=="Obstacles" || ob.collider.tag=="Part"){
            Destroy(ob.collider.gameObject);
        }
    }
}
