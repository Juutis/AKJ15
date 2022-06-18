using UnityEngine.Events;
using UnityEngine;
public class ClickableButton : ClickableObject
{

    [SerializeField]
    private UnityEvent onClickEvent;

    public override void OnClick()
    {
        onClickEvent.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
