using UnityEngine;
using System.Collections;

public class OffsetScroller : MonoBehaviour {

    public static OffsetScroller S;

    public Material normal_bg;
    public Material inv_bg;
    public float scrollSpeed;
    float scrollMult = 1;
    private Vector2 savedOffset;
    private Renderer renderer;

    void Awake()
    {
        S = this;
    }

    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
        savedOffset = renderer.sharedMaterial.GetTextureOffset("_MainTex");
    }

    void Update()
    {
        
        float y = Mathf.Repeat(Time.time * scrollSpeed * scrollMult, 1);
        Vector2 offset = new Vector2(savedOffset.x, y);
        renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

    void OnDisable()
    {
        renderer.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
    }

    public void EnterInv()
    {
        renderer.material = inv_bg;
        
    }

    public void ExitInv()
    {
        renderer.material = normal_bg;
        
    }
}
