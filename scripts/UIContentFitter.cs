using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIContentFitter : MonoBehaviour
{
    // Start is called before the first frame update
    
    //calculating the appropriate widht of the content area
    void Start()
    {
        HorizontalLayoutGroup hg = GetComponent<HorizontalLayoutGroup>();
        int childCount = transform.childCount - 1;
        float childWidth = transform.GetChild(0).GetComponent<RectTransform>().rect.width;
        float width = hg.spacing * childCount + childWidth * childCount + hg.padding.left;

        GetComponent<RectTransform>().sizeDelta = new Vector2(width, 270);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
