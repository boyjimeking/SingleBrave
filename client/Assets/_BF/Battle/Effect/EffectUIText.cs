using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


//  EffectUIText.cs
//  Author: Lu Zexi
//  2014-01-10



/// <summary>
/// UI文本特效
/// </summary>
[AddComponentMenu("Game/Effect/EffectUIText")]
public class EffectUIText : MonoBehaviour
{
    public UIFont m_cFont;  //字体
    private string m_strText;   //字符串

    /// <summary>
    /// 设置字符串
    /// </summary>
    /// <param name="str"></param>
    public void SetText(string str)
    {
        this.m_strText = str;
    }

    void Awake()
    {
        
    }

    void Start()
    {
        GameObject obj = this.gameObject;
        MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
        MeshRenderer meshRender = obj.AddComponent<MeshRenderer>();
        meshRender.material = this.m_cFont.material;
        Mesh mesh = new Mesh();

        BetterList<Vector3> vects = new BetterList<Vector3>();
        BetterList<Vector2> uvs = new BetterList<Vector2>();
        BetterList<Color32> colors = new BetterList<Color32>();

        this.m_cFont.Print(this.m_strText, 20, new Color32(0, 0, 0, 1), vects, uvs, colors, true, UIFont.SymbolStyle.None, TextAlignment.Center, 100, false);

        int count = vects.size;
        int index = 0;
        int[] mIndices = new int[(count >> 1) * 3];
        for (int i = 0; i < count; i += 4)
        {
            mIndices[index++] = i;
            mIndices[index++] = i + 1;
            mIndices[index++] = i + 2;

            mIndices[index++] = i + 2;
            mIndices[index++] = i + 3;
            mIndices[index++] = i;
        }

        mesh.vertices = vects.ToArray();
        mesh.uv = uvs.ToArray();
        //mesh.colors32 = colors.ToArray();
        mesh.triangles = mIndices;
        mesh.RecalculateBounds();

        meshFilter.mesh = mesh;
    }

}
