using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using UnityEngine.SceneManagement;

public class TerrainDecoding : MonoBehaviour
{
    // Start is called before the first frame update

    


    void Start()
    {
        GetComponent<ReadWriteFiles>().Load(null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadEdFlood(){
        SceneManager.LoadScene(1);
    }
    
    

}
