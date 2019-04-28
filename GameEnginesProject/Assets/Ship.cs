using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Ship : MonoBehaviour {

    public Transform BaseTarget;
    public Transform ShipTarget;

    private Transform currentTarget;

    private Arrive AR;
    private Pursue PR;

    public Base HomeBase;

    public int Health;
    
    [Header("Shooting")]
    public int shotCount;

    public float ShotsPerSecond;

    public float ReloadTime;
    
    private bool isShooting;

    public float RangeToShoot = 25f;
    
    
    public Transform Barrel;
    public GameObject Bullet;
    
    List<Transform> Enemies = new List<Transform>();

    private bool pursuing;
    
    // Start is called before the first frame update
    void Start() {

        AR = GetComponent<Arrive>();
        PR = GetComponent<Pursue>();

        currentTarget = BaseTarget;
        
        AR.targetGameObject = BaseTarget;
        AR.enabled = true;
        PR.enabled = false;


    }

    void SwitchState() {

        AR.enabled = !PR.enabled;

    }

    

   IEnumerator Shoot() {

       isShooting = true;
       

       for (int i = 0; i < shotCount; i++) {

           GameObject temp = Instantiate(Bullet);
           temp.transform.position = Barrel.position;
           temp.transform.rotation = Barrel.rotation;

           Destroy(temp, 5f);

           yield return new WaitForSeconds(1/ShotsPerSecond);


       }

       
       yield return new WaitForSeconds(ReloadTime);

       isShooting = false;


   }


    // Update is called once per frame
    void Update()
    {

        if (Health <= 0) {

            //TODO: EXPLOSION VFX

            HomeBase.EnemyBase.Currency += 5;
            
            Destroy(gameObject);


        }

        if (pursuing) {



            if (currentTarget == null || Enemies.Count < 1) {

                pursuing      = false;
                AR.enabled    = true;
                PR.enabled    = false;
                currentTarget = BaseTarget;



            }

            // if (Vector3.Distance()



        }

        if (Vector3.Distance(transform.position, currentTarget.position) < RangeToShoot) {


            if (!isShooting) {

                StartCoroutine(Shoot());

            }


        }


        if (!pursuing && Enemies.Count > 0 ) {

            pursuing = true;
            AR.enabled = false;
            PR.target = Enemies[0].GetComponent<Boid>();
            PR.enabled = true;
            currentTarget = PR.target.transform;
            GetComponent<Boid>().velocity.x = 10f;



        }

       




    }

    private void OnTriggerEnter(Collider other) {


        if (other.GetComponent<Ship>()) {

            if (!Enemies.Contains(other.transform)){
                Enemies.Add(other.transform);
            }

        }

    }

    void OnCollisionEnter(Collision other) {

        if (other.gameObject.CompareTag("Bullet")) {

            Health--;
            Destroy(other.gameObject);

//TODO: VFX
            
        }

    }

    private void OnTriggerExit(Collider other) {

        if (other.GetComponent<Ship>()) {
            if (Enemies.Contains(other.transform)) {
                Enemies.Remove(other.transform);
            }


        }
    }


}
