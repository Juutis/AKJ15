using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageRenderer : MonoBehaviour
{
    [SerializeField]
    private RenderTexture renderTextureTemplate;

    public Texture Texture { get; private set; }

    private Camera cam;

    private BookPage page;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Initialize(BookPage page)
    {
        this.page = page;
        cam = GetComponentInChildren<Camera>();

        var renderTexture = Instantiate(renderTextureTemplate);
        cam.targetTexture = renderTexture;
        Texture = renderTexture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
