using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamControl : MonoBehaviour {
    
    public float cubeSize = 0.2f;
    public int cubesInRow = 5;
    float cubesPivotDistance;
    Vector3 cubesPivot;
    public GameObject text_perfect;
    public GameObject text_good;
    public GameObject text_miss;
    private string colour;
    
    // public GameObject lightsaber;

    public int player;

    public Material greenMaterial;
    public Material redMaterial;
    public Material goldMaterial;
    public Material cubeMaterial;

    GameObject bladeObject;
    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;
    public int counter = 0;
    public float prev_time;
    public int miss = 0;


    static public float perfect_range = 20;

    public float spawn_position;


    void Start() {
        // source = GetComponent<AudioSource>();
        //calculate pivot distance
        colour = "green";
        cubesPivotDistance = cubeSize * cubesInRow / 2;
        //use this value to create pivot vector)
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
        // Physics.IgnoreLayerCollision(7, 8, true);
        // Debug.Log(Physics.GetIgnoreLayerCollision(7, 8));
        bladeObject = GameObject.Find("Lightsaber_Blade");

    }
    void Update()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position[0], gameObject.transform.position[1],
            spawn_position - (Generate.music_current_time + SettingsController.music_delay) * SettingsController.bolt_speed);
        // Debug.Log("moved");

        //if (gameObject.transform.position[2] < Generate.bolt_z_offset - 2 * perfect_range)
        if (gameObject.transform.position[2] < 0)
        {
            gameObject.GetComponent<MeshRenderer>().material = redMaterial;
            colour = "red";
            if (miss == 0)
            {
                miss = 1;
                if (player == 1)
                    Generate.myCombo1 = 0;
                else 
                    Generate.myCombo2 = 0;
                Vector3 ex_pt = gameObject.transform.position;
                Instantiate(text_miss, new Vector3(ex_pt[0], ex_pt[1], ex_pt[2]), Quaternion.identity);
                if (player == 1)
                    GameSummary.missed1++;
                else
                    GameSummary.missed2++;
            }
        }
        else if (gameObject.transform.position[2] <= perfect_range + Generate.bolt_z_offset &&
          gameObject.transform.position[2] > -perfect_range + Generate.bolt_z_offset)
        {
            gameObject.GetComponent<MeshRenderer>().material = goldMaterial;
            colour = "gold";
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = greenMaterial;
            colour = "green";
        }

        if (gameObject.transform.position[2] < -100)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.name == "Lightsaber_Blade" &&
            ((WS_Client.blade_av1 > 10 && player == 1) || (WS_Client.blade_av2 > 10 && player == 2))&&
            gameObject.transform.position[2] < 3 * perfect_range + Generate.bolt_z_offset &&
            colour != "red") {
            if (colour == "gold"){
                if (player == 1) {
                    GameSummary.perfect1++;
                    if (Generate.myCombo1 > 0) {
                        Generate.myScore1 += 40;
                    }
                    else Generate.myScore1 += 20;
                    Generate.myCombo1 += 1;
                }
                else {
                    GameSummary.perfect2++;
                    if (Generate.myCombo2 > 0) {
                        Generate.myScore2 += 40;
                    }
                    else Generate.myScore2 += 20;
                    Generate.myCombo2 += 1;
                }
                // Debug.Log(Generate.myCombo);
                Vector3 ex_pt = transform.position;
                Instantiate(text_perfect, new Vector3(ex_pt[0], ex_pt[1], ex_pt[2]), Quaternion.identity);
            }
            else if (colour == "green") {
                if (player == 1) {
                    GameSummary.good1++;
                    Generate.myScore1 += 10;
                    Generate.myCombo1 = 0;
                }
                else {
                    GameSummary.good2++;
                    Generate.myScore2 += 10;
                    Generate.myCombo2 = 0;
                }
                
                Vector3 ex_pt = transform.position;
                Instantiate(text_good, new Vector3(ex_pt[0], ex_pt[1], ex_pt[2]), Quaternion.identity);
            }
            
            explode();
            prev_time = Time.time;
            
            Debug.Log(gameObject.transform.position[2].ToString("N"));
        }
        

    }

    public void explode() {
        //make object disappear
        gameObject.SetActive(false);

        // play explosion sound
        bladeObject.GetComponent<ExplosionSound>().PlayExplosionSound(transform.position);

        ////loop 3 times to create 5x5x5 pieces in x,y,z coordinates
        //for (int x = 0; x < cubesInRow; x++) {
        //    for (int y = 0; y < cubesInRow; y++) {
        //        for (int z = 0; z < cubesInRow; z++) {
        //            createPiece(x, y, z);
        //        }
        //    }
        //}

        ////get explosion position
        //Vector3 explosionPos = transform.position;
        ////get colliders in that position and radius
        //Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        
        ////add explosion force to all colliders in that overlap sphere
        //foreach (Collider hit in colliders) {
        //    //get rigidbody from collider object
        //    Rigidbody rb = hit.GetComponent<Rigidbody>();
        //    if (rb != null) {
        //        //add explosion force to this body with given parameters
        //        rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
                
        //    }
        //}
        Destroy(this.gameObject);
    }

    void createPiece(int x, int y, int z) {

        //create piece
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        
        //set piece position and scale
        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
        piece.GetComponent<MeshRenderer>().material = cubeMaterial;

        //add rigidbody and set mass
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = 0.008f;
        piece.AddComponent<Disappear>();
        
    }

}