using UnityEngine;
using System.Collections;



//  ROOTAdjust.cs
//  Author: Lu Zexi
//  2014-01-15



/// <summary>
/// 3D根节点适配
/// </summary>
[AddComponentMenu("Game/Effect/ROOTAdjust")]
public class ROOTAdjust : MonoBehaviour
{
    public float m_defaultWidth;
    public float m_defalutHeight;

    private float m_width;  //宽
    private float m_height; //高

	// Use this for initialization
	void Start ()
    {
        m_width = Screen.width;
        m_height = Screen.height;
        
        float aspect = m_width/ m_height;
        float defaultAspect = m_defaultWidth / m_defalutHeight;

        if (aspect < 1)
        {
            camera.fieldOfView *= (defaultAspect / aspect);
            camera.orthographicSize *= (defaultAspect / aspect);
        }
        camera.aspect = aspect;

        /*
        float defaultWHRate = Width / Height;
        float ScreenWHRate = (float)Screen.width / (float)Screen.height;

        bool isUseHResize = defaultWHRate >= ScreenWHRate ? false : true;
        if (!isUseHResize)
        {
            float rateW = Screen.width/(defaultWHRate * Screen.height);
            this.transform.localScale = new Vector3(rateW, rateW, rateW);
        }
        else
        {
            this.transform.localScale = Vector3.one;
        }*/
	}
	
	// Update is called once per frame
	void Update ()
    {
        //
	}
}
