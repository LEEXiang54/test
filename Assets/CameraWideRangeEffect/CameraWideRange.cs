using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class CameraWideRange : MonoBehaviour
{
    Material cm;
    [Range(0,4)]
    public float tilling=1;
    public float offsetX;
    public float offsetY;
    public Texture bgTex;
    
    public Vector2 pan;
    // Start is called before the first frame update
    void Start()
    {
        cm = new Material(Shader.Find("Zgame/PostProcessing/CameraWideRangeEffect"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (cm == null)
            Graphics.Blit(source,destination);
        else
        {
            if (bgTex != null)
                cm.SetTexture("_BgTex", bgTex);
            cm.SetFloat("_Tilling", tilling);
            cm.SetFloat("_OffsetX", offsetX);
            cm.SetFloat("_OffsetY", offsetY);
            Graphics.Blit(source, destination, cm);
        }
    }
}
