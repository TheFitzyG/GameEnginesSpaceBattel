using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint : MonoBehaviour {

     private Base myBase;


     private void Start() {
         myBase = GetComponentInParent<Base>();
          
          
          
     }


     private void OnCollisionEnter(Collision other) {

          myBase.Health--;
          
          Destroy(other.gameObject);

     }


}
