using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public bool m_isFadeOut = false;
    public bool m_isFadeIn = false;

    [SerializeField] float m_fadeSpeed = 0.01f;

    float red, green, blue, alfa;

    Image m_fadeImage;

    // Start is called before the first frame update
    void Start()
    {
        m_fadeImage = GetComponent<Image>();
        red = m_fadeImage.color.r;
        green = m_fadeImage.color.g;
        blue = m_fadeImage.color.b;
        alfa = m_fadeImage.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isFadeIn)
        {
            StartFadeIn();
        }

        if (m_isFadeOut)
        {
            StartFadeOut();
        }
    }

    public void StartFadeIn()
    {
        alfa -= m_fadeSpeed;
        SetAlfa();

        if (alfa <= 0)
        {
            m_isFadeIn = false;
        }
    }

    void StartFadeOut()
    {
        alfa += m_fadeSpeed;
        SetAlfa();

        if (alfa >= 1)
        {
            m_isFadeOut = false;
        }
    }

    void SetAlfa()
    {
        m_fadeImage.color = new Color(red, green, blue, alfa);//Alfaだけ変えたいけど、それだけでは変えられない
    }
}
