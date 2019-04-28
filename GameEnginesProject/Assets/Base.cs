using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.UI;
using UnityEngine.XR.WSA.WebCam;


public class Base : MonoBehaviour {

    public bool Eval;
    public float Health = 1000f;
    public GameObject[] Ships;
    public Transform[] SpawnPoints;
    public int Currency = 100;

     public int PriceOfShip;

     public Transform[] TargetPoints;

     public Base EnemyBase;
    // private Transform[] EnemyBaseTargetPoints;

     public Slider HealthBar;

     void Start() {

          StartCoroutine(GainCurrencyOverTime());

          Base[] bases = FindObjectsOfType<Base>();

          foreach (Base B in bases) {

               if (B.Eval != Eval) {

                    EnemyBase = B; //= FindObjectOfType<Base>();
                    return;
               }

          }

          



     }



     void Think() {

          if (Currency >= PriceOfShip) {

               SpawnShip();

               Currency -= PriceOfShip;

          }

     }


     void Update() {

          if (Currency < 0) {
               Currency = 0;
          }
          
          Think();

          HealthBar.value = Health;

     }

     void SpawnShip() {

          if (Ships.Length > 0 && SpawnPoints.Length > 0) {

               int sChoice = Random.Range(0, Ships.Length);
               int pChoice = Random.Range(0, SpawnPoints.Length);

               GameObject temp = Instantiate(Ships[sChoice]);
               temp.transform.position = SpawnPoints[pChoice].position;
               
//TODO: Get a reference to current ship and set its target. 

              Ship tempShip = temp.GetComponent<Ship>();
               tempShip.BaseTarget = EnemyBase.TargetPoints[Random.Range(0, EnemyBase.TargetPoints.Length)];
               tempShip.HomeBase = this; 



          }




     }



     IEnumerator GainCurrencyOverTime() {

          while (Health > 0f) {

               
               yield return new WaitForSeconds(1f);

               Currency++;


          }

     }



}
