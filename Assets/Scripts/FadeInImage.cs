using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInImage : MonoBehaviour
{
    public Image fadeImage;

    private void Update()
    {
        if (fadeImage.color.a == 0)
        {
            gameObject.SetActive(false);
        }
    }

}
