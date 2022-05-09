using UnityEngine;
using UnityEngine.Playables;

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

    [HideInInspector] public bool gameInteractable = false;

    public static GameController Instance { get; set; }

    private PlayableDirector playableDirector;

    private void Awake()
    {
        Instance = this;
        playableDirector = this.GetComponent<PlayableDirector>();
    }

    private void Start()
    {
        playableDirector.Play();
        EnemyController.Instance.ShowEffectOutline();
    }

    private void Update()
    {
        if (!gameInteractable || PlayerController.Instance.isDead)
            return;

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
            PlayerAttack();
#elif PLATFORM_ANDROID || UNITY_ANDROID
        // Solo aceptar como máximo dos (taps simultaneos) dedos a la vez
        foreach (Touch touch in Input.touches)
        {
            if (touch.fingerId == 0)
            {
                if (touch.phase == TouchPhase.Ended)
                {
                    Debug.Log("First finger left.");
                    PlayerAttack();
                }
            }
            /*
            if (touch.fingerId == 1)
            {
                if (touch.phase == TouchPhase.Ended)
                {
                    Debug.Log("Second finger left.");
                    PlayerAttack();
                }
            }
            */
        }
#endif
    }

    public void StartGame()
    {
        gameInteractable = true;
        EnemyController.Instance.StartGame();
        PlayerController.Instance.StartGame();
    }

    private void PlayerAttack() {
        if (EnemyController.Instance.isDead)
            return;

        clickCounter++;

        int dmg = PlayerController.Instance.playerStats.damage;
        EnemyController.Instance.EnemyTakingDamage(dmg);

        CalculateProbCritic(dmg);
        ShowFloatingText(dmg);
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
        obj.transform.localPosition = new Vector3(Random.Range(-.4f, .4f), Random.Range(-.4f, .2f), 0f);

        FloatingText ftxt = obj.GetComponent<FloatingText>();
        ftxt.SetInfoFloatText(_dmg, _inEnemy, _critic, _isDmg);

        Destroy(obj, 1f);
    }

    public void CameraShake()
    {
        CinemachineShake.Instance.ShakeCamera(camShake_magnitude, camShake_duration);
    }

    // Calcula los ataques criticos según la probabilidad del jugador
    private void CalculateProbCritic(int _dmg)
    {
        float probC = PlayerController.Instance.playerStats.probCritical;
        if (probC > 0.0f)
        {
            if (probC >= Random.Range(0f, 100f))
            {
                ShowFloatingText(_dmg * 2, true, true);
                EnemyController.Instance.EnemyTakingDamage(_dmg * 2, true);
            }
        }
    }
}
