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

    private int hoverFrameCounter = 0;

    private SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponentInChildren<SpriteRenderer>();
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
            rend.sprite = hoverSprite;
        }
        else
        {
            rend.sprite = sprite;
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
