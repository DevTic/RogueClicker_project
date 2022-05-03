using UnityEngine;

public class SpriteEffectsController : MonoBehaviour
{
    Material _mat;
    Animator _anim;

    private Color _matTintColor;
    private float _tintFadeSpeed;
    private bool _useTint = false;

    private int useDissolve = 0;

    private void Awake()
    {
        _mat = this.GetComponent<SpriteRenderer>().material;
        _anim = this.GetComponent<Animator>();

        // establecer color de daño al sprite
        _matTintColor = new Color(1, 0, 0, 0);
        _tintFadeSpeed = 6f;
    }

    private void Start()
    {
        _mat.SetInt("_UseTint", 0); // false
    }

    void Update()
    {
        if (_useTint)
        {
            if (_matTintColor.a > 0)
            {
                _matTintColor.a = Mathf.Clamp01(_matTintColor.a - _tintFadeSpeed * Time.deltaTime);
                SetTintColor(_matTintColor);
            }
            else
            {
                _useTint = false;
                _mat.SetInt("_UseTint", 0); // false
                _mat.SetInt("_UseDissolve", useDissolve);
            }
        }
    }

    public void ShowEffect_Dissolve()
    {
        _mat.SetInt("_UseDissolve", 1);
        _mat.SetInt("_UseTint", 0);
        _anim.SetTrigger("isDissolve");
    }

    public void ShowEffect_Appear()
    {
        _mat.SetInt("_UseDissolve", 1);
        _mat.SetInt("_UseTint", 0);
        _anim.SetTrigger("isAppear");
    }

    public void ShowEffect_TintSprite(Color _color, float _speed = 6f)
    {
        _useTint = true;
        useDissolve = _mat.GetInt("_UseDissolve");
        _mat.SetInt("_UseDissolve", 1);
        _mat.SetInt("_UseTint", 1);
        SetTintFadeSpeed(_speed);
        SetTintColor(_color);
    }
    
    private void SetTintColor(Color _color)
    {
        _matTintColor = _color;
        _mat.SetColor("_ColorTint", _matTintColor);
    }

    private void SetTintFadeSpeed(float _speed)
    {
        this._tintFadeSpeed = _speed;
    }
}
