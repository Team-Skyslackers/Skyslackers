using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour
{
    private float nextActionTime = 0.0f;
    public float period = 2.0f;
    public GameObject beam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime ) {
            nextActionTime += period;

            // GameObject piece;
            // piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            // //set piece position and scale
            // piece.transform.position = new Vector3(-4.8f, 4.56f, 40.0f);
            // piece.transform.localScale = new Vector3(1, 1, 5);

            //add rigidbody and set mass
            // piece.AddComponent<Rigidbody>();
            // piece.GetComponent<Rigidbody>().mass = 100000000;
            // piece.AddComponent<Explosion>();
            // piece.GetComponent<Rigidbody>().useGravity = false;
            Instantiate(beam, new Vector3(-4.8f, 4.56f, 40.0f), Quaternion.identity);
            //beam.AddComponent<Explosion>();
        }
    }
}
