using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIimits : MonoBehaviour
{
    public GameObject go;
    
    // Start is called before the first frame update
    void Start()
    {
        
        RaycastHitOut(transform.right);
        RaycastHitOut(-transform.right);
        RaycastHitOut(transform.forward);
        RaycastHitOut(-transform.forward);
        /*RayCastHit hit;  //儲存物件
        //down (0,-1,0)  forwaed (0,0,1)   其他詳細Vector3
        Ray myRay=new Ray(transform.position,Vector3.right); //射線方向

        //射線由myRay射出長度為DownHeight 碰到的物件為HIT
        if(!(Physics.Raycast (myRay, out hit ,1)) ){
            Instantiate(go,transform.position+Vector3.right+new Vector3(0,1,0),Quaternion.Euler(0, 0, 0),gameObject.transform);
        }*/
    }
    void RaycastHitOut(Vector3 _TraPos){
        Ray ray = new Ray(transform.position, _TraPos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 1)){
            print("QQ");
            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }else{
            Instantiate(go,transform.position+_TraPos,Quaternion.Euler(0, 0, 0), transform.parent);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
