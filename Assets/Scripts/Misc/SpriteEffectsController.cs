using UnityEngine;

public class SpriteEffectsController : MonoBehaviour
{
    [Header("Efecto dissolve")]
    [SerializeField] Color colorDissolve;
    [SerializeField] Color colorAppear;

    Material _mat;
    Animator _anim;

    //bool isDissolving = false;
    //bool isAppear = false;
    //float fade = 1f;

    public static SpriteEffectsController Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        _mat = this.GetComponent<SpriteRenderer>().material;
        _anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        //if (isDissolving)
        //{
        //    Dissolve();
        //}

        //if (isAppear)
        //{
        //    Appear();
        //}
    }

    public void ShowEffect_Dissolve()
    {
        //isDissolving = true;
        //fade = 1f;
        //_mat.SetFloat("_Scale", 10f);
        //_mat.SetColor("_ColorBorder", colorDissolve);
        _anim.SetTrigger("isDissolve");
    }

    public void ShowEffect_Appear()
    {
        //isAppear = true;
        //fade = 0f;
        //_mat.SetFloat("_Fade", fade);
        //_mat.SetFloat("_Scale", 80f);
        //_mat.SetColor("_ColorBorder", colorAppear);
        _anim.SetTrigger("isAppear");
    }
    /*
    private void Dissolve()
    {
        fade -= Time.deltaTime;

        if (fade <= 0f)
        {
            isDissolving = false;
            fade = 0f;
        }

        _mat.SetFloat("_Fade", fade);
    }

    private void Appear()
    {
        fade += Time.deltaTime;

        if (fade >= 1f)
        {
            isAppear = false;
            fade = 1f;
        }

        _mat.SetFloat("_Fade", fade);
    }
    */
}
