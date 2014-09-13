using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestWWW : MonoBehaviour {

    public UIAtlas atlas;
    private WWW www = null;

    private WWW www1 = null;
    private WWW www2 = null;
    private WWW www3 = null;
    private WWW www4 = null;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(load());
        //TestMesh();
	}

    private void TestMesh()
    {
        UISpriteData spData = atlas.spriteList[0];
        GameObject obj = new GameObject();
        obj.layer = 2;
        MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
        MeshRenderer meshRender = obj.AddComponent<MeshRenderer>();
        meshRender.material = atlas.spriteMaterial;
        Mesh mesh = new Mesh();

        Vector3[] vects = new Vector3[4];
        Vector2[] uvs = new Vector2[4];

        vects[0] = new Vector3(0, 0, 0);
        vects[1] = new Vector3(spData.width, 0, 0);
        vects[2] = new Vector3(0, spData.height, 0);
        vects[3] = new Vector3(spData.width, spData.height, 0);

        Rect uvSp = new Rect(spData.x, spData.y, spData.width, spData.height);
        uvSp = NGUIMath.ConvertToTexCoords(uvSp, atlas.texture.width, atlas.texture.height);

        uvs[0] = new Vector2(uvSp.xMin, uvSp.yMin);
        uvs[1] = new Vector2(uvSp.xMax, uvSp.yMin);
        uvs[2] = new Vector2(uvSp.xMin, uvSp.yMax);
        uvs[3] = new Vector2(uvSp.xMax , uvSp.yMax);

        int[] tri = new int[6];
        tri[0] = 0;
        tri[1] = 2;
        tri[2] = 1;

        tri[3] = 2;
        tri[4] = 3;
        tri[5] = 1;

        //Color32[] cols = new Color32[4];
        //cols[0] = new Color32(1, 1, 1, 0);
        //cols[1] = new Color32(1, 1, 1, 0);
        //cols[2] = new Color32(1, 1, 1, 0);
        //cols[3] = new Color32(1, 1, 1, 0);

        mesh.vertices = vects;
        mesh.uv = uvs;
        //mesh.colors32 = cols;
        mesh.triangles = tri;
        mesh.RecalculateBounds();
        meshFilter.mesh = mesh;
    }

    private IEnumerator load()
    {
        www1 = www = WWW.LoadFromCacheOrDownload("http://192.168.1.250/sanguo/server/service/res/gui_ios.res", 2);
        yield return  www;

        www2 = www = WWW.LoadFromCacheOrDownload("http://192.168.1.250/sanguo/server/service/res/effect_ios.res", 2);
        yield return www;

        www3 = www = WWW.LoadFromCacheOrDownload("http://192.168.1.250/sanguo/server/service/res/model_ios.res", 2);
        yield return www;

        www4 = www = WWW.LoadFromCacheOrDownload("http://192.168.1.250/sanguo/server/service/res/tex_ios.res", 2);
        yield return www;

        //Debug.Log(www.assetBundle.Contains("GUI_Menu"));
        //GameObject.Instantiate(www.assetBundle.Load("GUI_Menu"));
    }

	// Update is called once per frame
	void Update () 
    {
        if( www != null )
        {
            Debug.Log(www.progress + " progress " + www.isDone + " done " + www.url  + " url");
        }
	}
}
