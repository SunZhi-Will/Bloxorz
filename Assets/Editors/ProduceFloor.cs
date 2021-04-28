using UnityEngine;  
using System.Collections;  
using UnityEngine.UI;  
using UnityEngine.EventSystems; 
using System.Collections; 

public class ProduceFloor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject uigo;
    GameObject go;
    void OnMouseDown(){
        if (EventSystem.current.IsPointerOverGameObject()) { 
            Debug.Log("點擊到UGUI的UI界面，會返回true");  
        } else {  
            print(transform.position);
            Destroy(go);
            if(transform.parent.gameObject.GetComponent<RecordTerrains>().brushNow.name.IndexOf("StandBy")==-1)
                go= Instantiate(transform.parent.gameObject.GetComponent<RecordTerrains>().brushNow,transform.position,Quaternion.Euler(0, 0, 0),transform.parent.gameObject.transform);
        }  
        
    }
    void OnCollisionEnter(Collision other) {
        Debug.Log("qq");
        if(go != other.gameObject){
            Destroy(go);
            go = other.gameObject;
        }
    }
}

