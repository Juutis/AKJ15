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

    [SerializeField]
    [InspectorName("Book Line UI elements")]
    List<BookLineUI> bookLineUIs;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Initialize(BookPage page)
    {
        this.page = page;
        cam = GetComponentInChildren<Camera>();

        for (int i = 0; i < bookLineUIs.Count; i++)
        {
            BookLineUI lineUI = bookLineUIs[i];
            if (i < page.Lines.Count)
            {
                lineUI.Initialize(page.Lines[i].IsRandom, page.Lines[i].Text);
            }
        }

        var renderTexture = Instantiate(renderTextureTemplate);
        cam.targetTexture = renderTexture;
        Texture = renderTexture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
