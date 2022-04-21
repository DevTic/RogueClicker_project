using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Instancia visual del daño")]
    [SerializeField] private GameObject prefabFloatingText;
    [SerializeField] private Transform posEnemy;
    [SerializeField] private Transform posPlayer;

    [Header("Contador de clicks")]
    [SerializeField] private int clickCounter = 0;

    [Header("Camera Shake")]
    [SerializeField] private float camShake_duration = .15f;
    [SerializeField] private float camShake_magnitude = .4f;

    public static GameController Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0) || Input.touchCount > 0)
        {
            clickCounter++;

            EnemyController.Instance.EnemyTakingDamage(1);
            ShowFloatingText(1);
        }
    }

    // muestra un texto flotante cada vez que recibe/hace daño, golpé critico, se regenera, etc.
    public void ShowFloatingText(int _dmg, bool _inEnemy = true, bool _critic = false, bool _isDmg = true)
    {
        GameObject obj;
        if (_inEnemy)
            obj = Instantiate(prefabFloatingText, posEnemy);
        else
            obj = Instantiate(prefabFloatingText, posPlayer);

        //obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = new Vector3(Random.Range(-.3f, .3f), 0f, 0f);
        
        FloatingText ftxt = obj.GetComponent<FloatingText>();
        ftxt.SetInfoFloatText(_dmg, _inEnemy, _critic, _isDmg);

        Destroy(obj, 1f);
    }

    public void CameraShake()
    {
        CinemachineShake.Instance.ShakeCamera(camShake_magnitude, camShake_duration);
    }
}
