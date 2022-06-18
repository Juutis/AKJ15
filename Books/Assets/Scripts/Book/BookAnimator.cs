using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BookAnimator : MonoBehaviour
{
    private Animator anim;
    
    public bool ReadyToFlip { get; private set; } = true;

    [SerializeField]
    private UnityEvent nextRightPageRevealedEvent;
    
    [SerializeField]
    private UnityEvent previousRightPageHiddenEvent;

    [SerializeField]
    private UnityEvent nextLeftPageRevealedEvent;
    
    [SerializeField]
    private UnityEvent previousLeftPageHiddenEvent;


    private FlipDirection flipDirection = FlipDirection.NONE;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FlippingPageOnTheRight()
    {
        if (flipDirection == FlipDirection.NEXT)
        {
            nextRightPageRevealedEvent.Invoke();
        }
        if (flipDirection == FlipDirection.PREVIOUS)
        {
            previousRightPageHiddenEvent.Invoke();
            ReadyToFlip = true;
        }
    }

    void FlippingPageOnTheLeft()
    {
        if (flipDirection == FlipDirection.NEXT)
        {
            previousLeftPageHiddenEvent.Invoke();
            ReadyToFlip = true;
        }
        if (flipDirection == FlipDirection.PREVIOUS)
        {
            nextLeftPageRevealedEvent.Invoke();
        }
    }

    public void FlipToNextPage()
    {
        if (ReadyToFlip)
        {
            anim.SetTrigger("FlipRightToLeft");
            ReadyToFlip = false;
            flipDirection = FlipDirection.NEXT;
        }
    }

    public void FlipToPreviousPage() 
    {
        if (ReadyToFlip)
        {
            anim.SetTrigger("FlipLeftToRight");
            ReadyToFlip = false;
            flipDirection = FlipDirection.PREVIOUS;
        }
    }

    private enum FlipDirection {
        NONE,
        NEXT,
        PREVIOUS
    }
}
