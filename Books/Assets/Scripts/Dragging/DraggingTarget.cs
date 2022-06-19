using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggingTarget : MonoBehaviour
{
    public Type TargetType = Type.SAVE;

    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private Sprite hoverSprite;

    private int hoverFrameCounter = 10;

    private SpriteRenderer rend;
    private AudioSource audioSrc;

    [SerializeField]
    private AudioClip openSound;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponentInChildren<SpriteRenderer>();
        audioSrc = GetComponent<AudioSource>();
        rend.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (sprite == null || hoverSprite == null)
        {
            return;
        }
        if (hoverFrameCounter < 2)
        {
            if (rend.sprite != hoverSprite)
            {
                audioSrc.PlayOneShot(openSound);
                rend.sprite = hoverSprite;
            }
        }
        else
        {
            if (rend.sprite != sprite)
            {
                audioSrc.PlayOneShot(openSound);
                rend.sprite = sprite;
            }
        }
        hoverFrameCounter++;
    }

    public void OnHover()
    {
        hoverFrameCounter = 0;
    }

    public enum Type
    {
        TABLE,
        SAVE,
        BURN,
        BAG
    }
}
