using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggingTarget : MonoBehaviour
{
    public Type TargetType = Type.SAVE;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHover()
    {

    }

    public enum Type
    {
        TABLE,
        SAVE,
        BURN,
        BAG
    }
}
