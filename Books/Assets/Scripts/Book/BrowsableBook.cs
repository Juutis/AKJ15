using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BrowsableBook : MonoBehaviour
{
    private BookAnimator bookAnim;

    private List<BrowsableBookPage> pages = new List<BrowsableBookPage>();

    [SerializeField]
    private List<Material> pageMaterials;

    [SerializeField]
    private PageRenderer pageRendererPrefab;

    private int currentPage = 0;

    
    public List<Texture> testTextures;

    // Start is called before the first frame update
    void Start()
    {
        bookAnim = GetComponentInChildren<BookAnimator>();

        for (var i = 0; i < 10; i++)
        {
            var pageRenderer = Instantiate(pageRendererPrefab);
            pageRenderer.Initialize();
            pageRenderer.transform.position = transform.position + new Vector3(-2.5f + 0.5f * i, 0.0f , -5.0f);
            var page = new BrowsableBookPage();
            page.Texture = pageRenderer.Texture;
            pages.Add(page);
        }

        InitializeBook();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            FlipToPreviousPage();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            FlipToNextPage();
        }
    }

    public void InitializeBook()
    {
        currentPage = 0;
        setLeftStaticPage(0);
        setLeftFlippingPage(0);
        setRightStaticPage(1);
        setRightFlippingPage(1);
    }

    public void FlipToNextPage()
    {
        if (currentPage < pages.Count-2 && bookAnim.ReadyToFlip) {
            setRightFlippingPage(currentPage+1);
            currentPage += 2;
            bookAnim.FlipToNextPage();
            setLeftFlippingPage(currentPage);
        }
    }

    public void FlipToPreviousPage()
    {
        if (currentPage > 0 && bookAnim.ReadyToFlip) {
            setLeftFlippingPage(currentPage);
            setRightFlippingPage(currentPage);
            currentPage -= 2;
            bookAnim.FlipToPreviousPage();
            setRightFlippingPage(currentPage+1);
        }
    }

    public void NewLeftPageRevealed()
    {
        setLeftStaticPage(currentPage);
    }

    public void NewRightPageRevealed()
    {
        setRightStaticPage(currentPage+1);
    }

    public void OldLeftPageHidden()
    {
        setLeftStaticPage(currentPage);
    }

    public void OldRightPageHidden()
    {
        setRightStaticPage(currentPage+1);
    }

    private void setLeftStaticPage(int pageNumber) 
    {
        var page = pages[pageNumber];
        pageMaterials[0].mainTexture = page.Texture;
    }

    private void setRightStaticPage(int pageNumber) 
    {
        var page = pages[pageNumber];
        pageMaterials[3].mainTexture = page.Texture;
    }

    private void setLeftFlippingPage(int pageNumber) 
    {
        var page = pages[pageNumber];
        pageMaterials[2].mainTexture = page.Texture;
    }

    private void setRightFlippingPage(int pageNumber) 
    {
        var page = pages[pageNumber];
        pageMaterials[1].mainTexture = page.Texture;
    }

    private class BrowsableBookPage
    {
        public Texture Texture;
    }

}
