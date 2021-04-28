using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;


public class RecordTerrains : MonoBehaviour
{
    public int maxLong=5;
    public int maxWidth=5;
    Vector3 spawnPoint;
    public GameObject standBy;
    public GameObject[] brush_NowGO;
    public GameObject brushNow;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = new Vector3(0-maxWidth,0,0-maxLong);
        for(int i=0;i<maxLong*2;i++){
            for(int j=0;j<maxWidth*2;j++){
                if(i!=maxLong || j != maxWidth){
                    Instantiate(standBy,spawnPoint+new Vector3(j,0,i),Quaternion.Euler(0, 0, 0),gameObject.transform);
                }
                
            }
        }
        //for(int i=0; i<brush_NowGO.Length; i++){
        //    brush_NowGO[i].GetComponent<ProduceFloor>().setID(i);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaceBlocks(int brush_Now){
        brushNow = brush_NowGO[brush_Now];
    }
    public void RBlocks(GameObject brush_Now){
        brushNow = brush_Now;
    }
    public void LoadGame(){
        SceneManager.LoadScene(0);
    }

}
