using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using UnityEngine.UI; 
//using Newtonsoft.Json;

public class ReadWriteFiles : MonoBehaviour
{
    public List<string> mList = new List<string>();
    public GameObject floorEnd;
    public GameObject[] floorID;
    public int g_MapID;

    public string versions = "2.0";
    public string verTimes = "200714";

    
    void Start()
    {
        DirSearch(Application.dataPath + "/world/");
    }
    


    public void Save(Text enterText)
    {
        String str;
        if(enterText.text ==""){
            str="save";
        }else{
            str=enterText.text;
        }
        GameObject[] _TentativeFloor = GameObject.FindGameObjectsWithTag("OrdinaryFloor");

        //JSON檔版本設定
        List<string> floodId = new List<string>();
        List<Vector3> floodPos = new List<Vector3>();
        List<Color32> floodMatCube = new List<Color32>();

        //載入儲存資料
        foreach (GameObject s in _TentativeFloor)
        {
            floodId.Add(s.name);
            floodPos.Add(s.transform.position);
            Color32 floodColcor = new Color32((byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), 255);
            floodMatCube.Add(floodColcor);
        }
        GameObject[] _EndFloor = GameObject.FindGameObjectsWithTag("End");
        floodId.Add(_EndFloor[0].name);
        floodPos.Add(_EndFloor[0].transform.position);
        Color32 _floodColcor = new Color32((byte)0, (byte)255, (byte)0, 255);
        floodMatCube.Add(_floodColcor);



        //建立物件，塞資料
        Dates root = new Dates()
            {
                version = versions,
                times = verTimes,
                floodId = floodId,
                floodPos = floodPos,
                floodMatCube = floodMatCube
            };
        
        //物件序列化
        string strJson = JsonUtility.ToJson(root,true); 
        Byte[] b = Encoding.UTF8.GetBytes(strJson);
        File.WriteAllBytes(Application.dataPath + "/world/"+str+".json", b);

        Debug.Log("寫入完成");


        ///////////////////////////////////////////////////////////
/*
        StringBuilder sb = new StringBuilder();//宣告一個可變字串
        sb.Append("存檔版本:1.0" + '|');
        foreach (GameObject s in _TentativeFloor)
        {
            sb.Append(s.transform.position.ToString() +"@ID:"+ s.name + '|');
        }
        sb.Append(GameObject.FindWithTag("End").transform.position.ToString());
        String str;
        if(enterText.text ==""){
            str="save";
        }else{
            str=enterText.text;
        }

        //寫檔案 檔名為save.text
        //這裡的FileMode.create是建立這個檔案,如果檔名存在則覆蓋重新建立
        FileStream fs = new FileStream(Application.dataPath + "/world/"+str+".txt", FileMode.Create);
        //儲存時時二進位制,所以這裡需要把我們的字串轉成二進位制
        byte[] bytes = new UTF8Encoding().GetBytes(sb.ToString());
        
        fs.Write(bytes, 0, bytes.Length);
        //每次讀取檔案後都要記得關閉檔案
        fs.Close();*/
        
    }

    //讀取
    public void Load(Text enterText)
    {
        DirSearch(Application.dataPath + "/world/");
        Byte[] LoadData;
        Dates MyData;
        GameObject DyedObject;

        
        FileStream fs;

        String str;
        

        if(enterText==null){
            if(g_MapID == -1){
                g_MapID = UnityEngine.Random.Range( 0, mList.Count);
            }
            str=mList[g_MapID];
            //FileMode.Open開啟路徑下的save.text檔案
        }else{
            foreach (var item in GameObject.FindGameObjectsWithTag("OrdinaryFloor"))
            {
                Destroy(item);
            }
            foreach (var item in GameObject.FindGameObjectsWithTag("End"))
            {
                Destroy(item);
            }
            if(enterText.text ==""){
                str=Application.dataPath + "/world/" + "save";
            }else{
                str=Application.dataPath + "/world/" +enterText.text;
            }
        }

        try{
            //讀取指定路徑的Json檔案並轉成字串(路徑同上一篇)
            
            if(enterText==null){
                LoadData = File.ReadAllBytes(str);
            }else{
                LoadData = File.ReadAllBytes(str+".json");
            }
            //將讀取到的二進位制轉換成字串
            string s = new UTF8Encoding().GetString(LoadData);

            //把字串轉換成Data物件
            MyData = JsonUtility.FromJson<Dates>(s);

            if(MyData.version.IndexOf("2.0")>-1){
                for(int i=0;i<MyData.floodId.Count;i++){
                    DyedObject = Instantiate(ObjectConversion(MyData.floodId[i]),MyData.floodPos[i],Quaternion.Euler(0, 0, 0),gameObject.transform);
                    DyedObject.GetComponent<Renderer>().material.color = MyData.floodMatCube[i];
                }
            }

            Debug.Log("最新讀取");
        }catch(Exception e){
            if(enterText==null){
                fs = new FileStream(str, FileMode.Open);
            }else{
                fs = new FileStream(str, FileMode.Open);
            }
            
            byte[] bytes = new byte[fs.Length]; 
            fs.Read(bytes, 0, bytes.Length);
            //將讀取到的二進位制轉換成字串
            string s = new UTF8Encoding().GetString(bytes);
            //將字串按照'|'進行分割得到字串陣列
            string[] itemIds = s.Split('|');
            if(itemIds[0].IndexOf("存檔版本:1.0")>-1){
                for (int i = 1; i < itemIds.Length-1; i++)
                {
                    Instantiate(ObjectConversion(itemIds[i].Split(':')[1]),StringToVector3(itemIds[i].Split('@')[0]),Quaternion.Euler(0, 0, 0),gameObject.transform);
                    //Debug.Log(itemIds[i]);
                }
                Instantiate(floorEnd,StringToVector3(itemIds[itemIds.Length-1]),Quaternion.Euler(0, 0, 0),gameObject.transform);
            }else{
                for (int i = 0; i < itemIds.Length-1; i++)
                {
                    Instantiate(floorID[0],StringToVector3(itemIds[i]),Quaternion.Euler(0, 0, 0),gameObject.transform);
                    //Debug.Log(itemIds[i]);
                }
                Instantiate(floorEnd,StringToVector3(itemIds[itemIds.Length-1]),Quaternion.Euler(0, 0, 0),gameObject.transform);
            }
        }



        ////////////////////////////////////////
        /*DirSearch(Application.dataPath + "/world/");
        FileStream fs;
        if(enterText==null){
            if(g_MapID == -1){
                g_MapID = UnityEngine.Random.Range( 0, mList.Count);
            }
            //FileMode.Open開啟路徑下的save.text檔案
            fs = new FileStream(mList[g_MapID], FileMode.Open);
        }else{
            fs = new FileStream(Application.dataPath + "/world/"+enterText.text+".txt", FileMode.Open);
        }

        byte[] bytes = new byte[fs.Length]; 
        fs.Read(bytes, 0, bytes.Length);
        //將讀取到的二進位制轉換成字串
        string s = new UTF8Encoding().GetString(bytes);
        //將字串按照'|'進行分割得到字串陣列
        string[] itemIds = s.Split('|');
        if(itemIds[0].IndexOf("存檔版本:1.0")>-1){
            for (int i = 1; i < itemIds.Length-1; i++)
            {
                Instantiate(ObjectConversion(itemIds[i].Split(':')[1]),StringToVector3(itemIds[i].Split('@')[0]),Quaternion.Euler(0, 0, 0),gameObject.transform);
                //Debug.Log(itemIds[i]);
            }
            Instantiate(floorEnd,StringToVector3(itemIds[itemIds.Length-1]),Quaternion.Euler(0, 0, 0),gameObject.transform);
        }else{
            for (int i = 0; i < itemIds.Length-1; i++)
            {
                Instantiate(floorID[0],StringToVector3(itemIds[i]),Quaternion.Euler(0, 0, 0),gameObject.transform);
                //Debug.Log(itemIds[i]);
            }
            Instantiate(floorEnd,StringToVector3(itemIds[itemIds.Length-1]),Quaternion.Euler(0, 0, 0),gameObject.transform);
        }*/
    }


    public static Vector3 StringToVector3(string sVector)
     {
         // Remove the parentheses
         if (sVector.StartsWith ("(") && sVector.EndsWith (")")) {
             sVector = sVector.Substring(1, sVector.Length-2);
         }
 
         // split the items
         string[] sArray = sVector.Split(',');
 
         // store as a Vector3
         Vector3 result = new Vector3(
             float.Parse(sArray[0]),
             float.Parse(sArray[1]),
             float.Parse(sArray[2]));
 
         return result;
     }
     //查詢資料夾目錄下所有檔案名稱
    public void DirSearch(string sDir){
        try{
            mList.Clear();
            print(sDir);
            //foreach (string d in Directory.GetDirectories(sDir)){
                //print(d);
                //先針對目前目路的檔案做處理
                foreach (string f in Directory.GetFiles(sDir)){
                    
                    if(f.IndexOf(".meta")==-1){
                        print(f);
                        mList.Add(f);
                    }
                }//此目錄處理完再針對每個子目錄做處理
                
            //}
        }
        catch (System.Exception excpt){
            print(excpt.Message);
        }
    }
    public GameObject ObjectConversion(string str){
        foreach (var item in floorID)
        {
            if(str.IndexOf(item.name)>-1){
                return item;
            }
        }
        return null;
        
    }
}
