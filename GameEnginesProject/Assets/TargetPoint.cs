using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint : MonoBehaviour {

     private Base myBase;


     private void Start() {
         myBase = GetComponentInParent<Base>();

          if (myBase == null) {

               Debug.LogError("No Base found!");
               
          }

     }


     private void OnTriggerEnter(Collider other) {

        //  Debug.Log("Hit");
          if (other.CompareTag("Bullet")) {


               myBase.Health--;

               Destroy(other.gameObject);
          }

     }


}
