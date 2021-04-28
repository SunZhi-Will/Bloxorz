using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject protagonist;
    float pointX=0,pointY=0,pointZ=0;
    public GameObject[] movingPoint;
    // Start is called before the first frame update
    void Start()
    {

        pointX=protagonist.transform.localScale.x/2f;
        pointY=protagonist.transform.localScale.y/2f;
        pointZ=protagonist.transform.localScale.z/2f;


        JudgingTheDirection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    float go=0;
    public Vector3 UpPoint(){
        go=pointY;
        pointY=pointZ;
        pointZ=go;

        return movingPoint[0].transform.position;
    }
    public Vector3 DownPoint(){
        go=pointY;
        pointY=pointZ;
        pointZ=go;
        return movingPoint[1].transform.position;
    }
    public Vector3 LeftPoint(){
        go=pointY;
        pointY=pointX;
        pointX=go;
        return movingPoint[2].transform.position;
    }
    public Vector3 RightPoint(){
        go=pointY;
        pointY=pointX;
        pointX=go;
        
        return movingPoint[3].transform.position;
    }

    public void JudgingTheDirection(){
        movingPoint[3].transform.position = new Vector3(protagonist.transform.position.x+pointX,0,protagonist.transform.position.z);
        movingPoint[2].transform.position = new Vector3(protagonist.transform.position.x-pointX,0,protagonist.transform.position.z);
        movingPoint[1].transform.position = new Vector3(protagonist.transform.position.x,0,protagonist.transform.position.z-pointZ);
        movingPoint[0].transform.position = new Vector3(protagonist.transform.position.x,0,protagonist.transform.position.z+pointZ);
    }
    
}
