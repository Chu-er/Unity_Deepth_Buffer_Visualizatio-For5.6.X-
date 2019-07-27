using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
public class CaptureTarget : MonoBehaviour {

    public RectTransform Left;
    public RectTransform Right;
    public Image ReceiveTexture;

   
    void Start () {
        Vector2 sPos = Camera.main.WorldToScreenPoint(Left.transform.position);
        Vector2 rSpos = Camera.main.WorldToScreenPoint(Right.transform.position);






        float width = (rSpos.x - sPos.x);
       float height = rSpos.y - sPos.y;
        //print(width);
        //print(height);
        //.sizeDelta= Vector2.zero;
        //  Vector2 sPos = RectTransformUtility.WorldToScreenPoint(Camera.main, Left.transform.position);
       
        print(sPos+"哈哈");
         Rect rt = new Rect(sPos.x, sPos.y, width, height);
       // Rect rt = new Rect(1, 1,Screen. width-2, Screen.height-2);
        //   Texture2D scteenShoot = CaptureScreen(null, rt);
        #region 不会用


        /*
            if (scteenShoot != null)
            {
              // ReceiveTexture.sprite = Sprite.Create(scteenShoot,
             // new Rect(lSpos.x, lSpos.y, scteenShoot.width, scteenShoot.height), Vector2.zero);
                ReceiveTexture.sprite = Sprite.Create(scteenShoot,
           new Rect(0, 0, scteenShoot.width, scteenShoot.height), Vector2.zero);
            }
            else Debug.LogWarning("不存在得" + scteenShoot.GetType());
          */
        #endregion

        print(rt.width + "dwd"+ rt.height);
      
        StartCoroutine(SavePhoto(rt, null));
        print(rt.size+"Size");
    }

    public   Texture2D CaptureScreen(Camera came, Rect r)
    {
        if (came == null)
        {
            came = Camera.main;
        }
        RenderTexture rt = new RenderTexture((int)r.width, (int)r.height, 0);

        came.targetTexture = rt;
        came.Render();

        RenderTexture.active = rt;
        Texture2D screenShot = new Texture2D((int)r.width, (int)r.height, TextureFormat.RGB24, false);

        screenShot.ReadPixels(r, 0, 0,false);
        screenShot.Apply();

        came.targetTexture = null;
        RenderTexture.active = null;
        GameObject.Destroy(rt);

        byte[] bytes = screenShot.EncodeToPNG();
        string filename = Application.dataPath + "ScreenShoot";
        if (!Directory.Exists(filename))
        {
            Directory.CreateDirectory(filename);
        }
        string neaName = Application.dataPath + "ScreenShoot/image.png";
        File.WriteAllBytes(neaName, bytes);
       
        return screenShot;
     
    }
    IEnumerator SavePhoto(Rect rect, string name)
    {
        yield return new WaitForEndOfFrame();
        Texture2D t = new Texture2D((int)rect.width, (int)rect.height,TextureFormat.RGB24,false);
       // t.ReadPixels(new Rect(0, 0, rect.width, rect.height), 0, 0, false);

        //距X左的距离        距Y屏上的距离  
       t.ReadPixels(rect, 0, 0, false);  
        t.Apply();
        byte[] byts = t.EncodeToJPG(100);

        string neaName = Application.dataPath + "/CaptureTarget/image1.png";
        File.WriteAllBytes(neaName, byts);
        yield return new WaitForEndOfFrame ();
        AssetDatabase.Refresh();
        yield return byts;
        Destroy(t); ;
        print("截完后大小" + t.width + "Height" + t.height);
    }
    public void OnDrawGizmos()
    {
     
        Vector3[] lines = new Vector3[5];
        lines[0] = new Vector3(Left.transform.position.x, Left.transform.position.y, 0);
        lines[1] = new Vector3(Right.transform.position.x, Left.transform.position.y, 0);
        lines[2] = new Vector3(Right.transform.position.x, Right.transform.position.y, 0);
        lines[3] = new Vector3(Left.transform.position.x, Right.transform.position.y, 0);
        lines[4] = lines[0];
        Gizmos.color = Color.red;
        for (int i = 0; i < lines.Length-1; i++)
        {
         //   if(i<= 5)
           // {
                Gizmos.DrawLine(lines[i], lines[i + 1]);
           // }
         
        }
           
            

        //Gizmos.DrawGUITexture()
    }


    Rect temp = new Rect(0,0,0,0);
    private void Update()
    {
        if (temp.x != Left.rect.x || temp.y != Left.rect.y)
        {
            print(Left.rect);
            temp = Left.rect;
        }
      //  print(Input.mousePosition);
    }
}
