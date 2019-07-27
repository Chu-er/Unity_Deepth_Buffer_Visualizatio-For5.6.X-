using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class GzimoRect : MonoBehaviour {


    public Texture txt;
    public GameObject left;
    public GameObject right;
    Rect rt;
    public void DrawRect(Rect rt,Color  col)
    {
        
        Vector3[] lines = new Vector3[5];
        lines[1] = new Vector3(rt.x, rt.y, 0);
        lines[2] = new Vector3(rt.x+rt.width, rt.y+rt.height, 0);
        lines[3] = new Vector3(rt.x, rt.y + rt.height, 0);
        lines[4] = new Vector3(rt.x, rt.y, 0);
        Gizmos.color = Color.red;
        for (int i = 0; i < lines.Length-1; i++)
        {
            Gizmos.DrawLine(lines[i], lines[i + 1]);
        }
       
        //Gizmos.DrawGUITexture()
    }

}
