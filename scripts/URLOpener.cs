using UnityEngine;

public class URLOpener : MonoBehaviour
{
    public string Url;

    public void Open()
    {
        Application.OpenURL(Url);
    }
   
}
