using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    
    public float cubeSize = 0.2f;
    public int cubesInRow = 5;
    float cubesPivotDistance;
    Vector3 cubesPivot;
    public GameObject text_perfect;
    public GameObject text_good;
    public GameObject text_miss;
    private string colour;
    
    // public GameObject lightsaber;

    public Material greenMaterial;
    public Material redMaterial;
    public Material goldMaterial;

    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;
    public int counter = 0;
    public float prev_time;
    public int miss = 0;
    // public AudioSource source;


    public  float bolt_speed = 1.0f;
    // Use this for initialization
    void Start() {
        // source = GetComponent<AudioSource>();
        //calculate pivot distance
        colour = "green";
        cubesPivotDistance = cubeSize * cubesInRow / 2;
        //use this value to create pivot vector)
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);

    }
    void Update()
    {
        Debug.Log(miss);
        gameObject.transform.Translate(0f,0f,-bolt_speed);
        if (gameObject.transform.position[2] < 30) {
            if (gameObject.transform.position[2] > 10) {
                gameObject.GetComponent<MeshRenderer>().material = goldMaterial;
                colour = "gold";
            }
            else {
                if (gameObject.transform.position[2] > 0) {
                    gameObject.GetComponent<MeshRenderer>().material = greenMaterial;
                    colour = "green";
                }
                else {
                    gameObject.GetComponent<MeshRenderer>().material = redMaterial;
                    colour = "red";
                    if (miss == 0){
                    miss = 1; 
                    Vector3 ex_pt = gameObject.transform.position;
                    Instantiate(text_miss, new Vector3(ex_pt[0], ex_pt[1], ex_pt[2]), Quaternion.identity);
                    Debug.Log(gameObject);
                }
                }
                
            }
                
                //used to be 2~40
        }


        if (gameObject.transform.position[2]<-100){
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    // void Update() {
    //     counter += 1;
    //     Debug.Log(counter);
    // }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Lightsaber_Blade") {
            Debug.Log("Sound played");
            if (colour == "gold"){
                Generate.myScore += 20;
                Vector3 ex_pt = transform.position;
                Instantiate(text_perfect, new Vector3(ex_pt[0], ex_pt[1], ex_pt[2]), Quaternion.identity);
            }
            else {
                if (colour == "green") {
                    Generate.myScore += 10;
                    Vector3 ex_pt = transform.position;
                    Instantiate(text_good, new Vector3(ex_pt[0], ex_pt[1], ex_pt[2]), Quaternion.identity);
                }
            }
            
            explode();
            prev_time = Time.time;
            
            Debug.Log(Time.time);
            // while (Time.time - prev_time < 1){
            //     Debug.Log(Time.time);
            // }
            // for (int i = 0; i < 125; i++){
            //     part = GameObject.Find("Cube");
                
            //     part.SetActive(false);
            // }
        
        }
        

    }

    public void explode() {
        //make object disappear
        gameObject.SetActive(false);

        //loop 3 times to create 5x5x5 pieces in x,y,z coordinates
        for (int x = 0; x < cubesInRow; x++) {
            for (int y = 0; y < cubesInRow; y++) {
                for (int z = 0; z < cubesInRow; z++) {
                    createPiece(x, y, z);
                }
            }
        }

        //get explosion position
        Vector3 explosionPos = transform.position;
        //get colliders in that position and radius
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        
        //add explosion force to all colliders in that overlap sphere
        foreach (Collider hit in colliders) {
            //get rigidbody from collider object
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null) {
                //add explosion force to this body with given parameters
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
                
            }
        }
        Destroy(this.gameObject);
    }

    void createPiece(int x, int y, int z) {

        //create piece
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        
        //set piece position and scale
        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        //add rigidbody and set mass
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = 0.008f;
        piece.AddComponent<Disappear>();
        // piece.AddComponent<MeshRenderer>().material = myMaterial;
    }

}