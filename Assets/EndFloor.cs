using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndFloor : MonoBehaviour
{
    public GameObject[] m_PlayGo;
    // Start is called before the first frame update
    void Start()
    {
        m_PlayGo = GameObject.FindGameObjectsWithTag("Play");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision other) {
        print("讚"+m_PlayGo[0].transform.rotation.x + "OWO " +m_PlayGo[0].transform.rotation.z);
        if(m_PlayGo[0].transform.position.y > 1){
            SceneManager.LoadScene(0);
        }
    }
    
}
