using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BrowsableBook : MonoBehaviour
{

    public static BrowsableBook main;

    private BookAnimator bookAnim;

    private List<BrowsableBookPage> pages = new List<BrowsableBookPage>();

    [SerializeField]
    private List<Material> pageMaterials;

    [SerializeField]
    private PageRenderer pageRendererPrefab;

    [SerializeField]
    private Material coverMaterial;

    private int currentPage = 0;

    [SerializeField]
    private Transform pageContainer;

    [SerializeField]
    private GameObject bookContainer;
    
    private Book book;

    public bool IsOpen { get; private set; } = false;

    private AudioSource audioSrc;

    [SerializeField]
    private AudioClip openSound;
    [SerializeField]
    private AudioClip closeSound;
    [SerializeField]
    private AudioClip flipPageSound;

    void Awake()
    {
        main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        bookAnim = GetComponentInChildren<BookAnimator>();
        audioSrc = GetComponent<AudioSource>();
        CloseBook(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (book == null)
        {
            InitializeBook(new Book(Genre.History));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            FlipToPreviousPage();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            FlipToNextPage();
        }
    }

    public void OpenBook(Book book, Color coverColor)
    {
        InitializeBook(book);
        bookContainer.SetActive(true);
        IsOpen = true;
        coverMaterial.color = coverColor;

        audioSrc.PlayOneShot(openSound);
    }

    public void CloseBook()
    {
        CloseBook(false);
    }

    public void CloseBook(bool skipSound)
    {
        bookContainer.SetActive(false);
        IsOpen = false;

        if (!skipSound) {
            audioSrc.PlayOneShot(closeSound);
        }
    }

    public void InitializeBook(Book book)
    {
        foreach (Transform t in pageContainer)
        {
            Destroy(t.gameObject);
        }

        this.book = book;
        var pageNumber = 0;
        pages = book.Pages.Select(it => {
            var pageRenderer = Instantiate(pageRendererPrefab, pageContainer);
            pageRenderer.Initialize(it);
            pageRenderer.transform.localPosition = new Vector3(pageNumber * 10.0f, 0, 0);
            var page = new BrowsableBookPage(it);
            page.Texture = pageRenderer.Texture;
            pageNumber++;
            return page;
        }).ToList();

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
            audioSrc.PlayOneShot(flipPageSound);
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
            audioSrc.PlayOneShot(flipPageSound);
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
        public BookPage page;

        public BrowsableBookPage(BookPage page)
        {
            this.page = page;
        }
    }

}
