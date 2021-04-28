using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMove : MonoBehaviour
{
    
    bool test=true;
    public int seep=5;
    public GameObject Point;
    Vector3 go;

    Vector3 tp;
    Quaternion tr;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
     void Update() {
        if(test && (transform.position.y > transform.localScale.y/2f || transform.position.y > transform.localScale.x/2f || transform.position.y > transform.localScale.z/2f) ){
            if (Input.GetKeyDown(KeyCode.UpArrow)) {

                go = Point.GetComponent<SpawnPoint>().UpPoint();
                StartCoroutine(Rotate(Vector3.right));
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow)){
                go = Point.GetComponent<SpawnPoint>().DownPoint();
                StartCoroutine(Rotate(Vector3.left));
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                go = Point.GetComponent<SpawnPoint>().LeftPoint();
                StartCoroutine(Rotate(Vector3.forward));
            } 
            else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                go = Point.GetComponent<SpawnPoint>().RightPoint();
                StartCoroutine(Rotate(Vector3.back));
            } 
            
        }
     }
    
     private IEnumerator Rotate(Vector3 axis) {
        //GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        tp=transform.position;
        tr=transform.rotation;
        test=false;
        float amount = 0;
        while (amount < 90) {
            yield return new WaitForEndOfFrame();
            var increase = Time.deltaTime * seep * 89;
            amount += increase;
            transform.RotateAround(go, axis, increase);
        }
        
        transform.position=tp;
        transform.rotation=tr;
        transform.RotateAround(go, axis, 89);
        
        transform.position = new Vector3((float)(Mathf.Round(transform.position.x * 10)) / 10,transform.position.y,(float)(Mathf.Round(transform.position.z * 10)) / 10);
        Point.GetComponent<SpawnPoint>().JudgingTheDirection();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        //GetComponent<Rigidbody>().isKinematic = false;
        test=true;
     }
     /*private void OnCollisionEnter(Collision other) {
         print("讚A");
        if(other.gameObject.tag.IndexOf("other")>-1){
            print("讚");
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.centerOfMass = other.gameObject.transform.position;
        }
    }*/
}
