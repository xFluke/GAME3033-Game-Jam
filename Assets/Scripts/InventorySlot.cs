using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ColorState colorState;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().color = ColorManager.ColorStateToColor32(colorState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick() {
        FindObjectOfType<ColorManager>().ChangeCameraBGColor(colorState);
    }
    
}
