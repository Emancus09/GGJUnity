using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeoutOnStart : MonoBehaviour
{
    public TextMeshProUGUI text;

    float mAlpha = 2f;

    void Start()
    {
        StartCoroutine(FadeOutText(0.5f, text));
    }

    private IEnumerator FadeOutText(float timeSpeed, TextMeshProUGUI text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            mAlpha -= Time.deltaTime * timeSpeed;
            text.color = new Color(text.color.r, text.color.g, text.color.b, mAlpha);
            yield return null;
        }
    }
}
