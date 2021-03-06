using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerStats_SO playerStats;
    public SpriteEffectsController spriteEffectsPlayer;
    public Animator animPlayer;

    [SerializeField] private int hpCurrent;
    [SerializeField] private int hpMax;

    [HideInInspector] public bool isDead;

    public static PlayerController Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        isDead = false;
        animPlayer.runtimeAnimatorController = playerStats.animatorController;

        hpCurrent = hpMax = playerStats.hpMax;        
    }

    private void Start()
    {
        UI_GameController.Instance.ShowHealthBarPlayer(playerStats);
    }

    public void StartGame()
    {
        PlayerRegeneration();
    }

    // Jugador recibe daño
    public void PlayerTakingDamage(int damage)
    {
        if (isDead)
            return;

        hpCurrent -= damage;

        // Da efecto a la camara al recibir un golpe
        GameController.Instance.CameraShake();
        if (hpCurrent <= 0)
        {
            hpCurrent = 0;
            Dead();
        }
        else
            spriteEffectsPlayer.ShowEffect_TintSprite(new Color(1f, .1f, .1f, 1f));

        UI_GameController.Instance.UpdateHealthBarPlayer(hpCurrent, playerStats);
    }

    // Jugador atacando
    public void PlayerAttack(bool _critic)
    {
        if (_critic)
        {
            // animación de ataque critico
            animPlayer.SetTrigger("atk3");
        }
        else
        {
            // animación de ataque aleatorio normal
            int rAtk = Random.Range(1, 3);
            animPlayer.SetTrigger(string.Format("atk{0}", rAtk));
        }
    }

    // Jugador sanando
    public void PlayerHealing(int amount)
    {
        if ((playerStats.regenAmount > 0) && (hpCurrent < hpMax))
        {
            hpCurrent += amount;

            GameController.Instance.ShowFloatingText(amount, false, false, false);

            if (hpCurrent >= hpMax)
                hpCurrent = hpMax;

            UI_GameController.Instance.UpdateHealthBarPlayer(hpCurrent, playerStats);
        }
    }

    // Jugador regenerando salud
    public void PlayerRegeneration()
    {
        StartCoroutine(DelayRegeneration());
    }

    IEnumerator DelayRegeneration()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(playerStats.regenIntervalTime);

            if (GameController.Instance.gameInteractable && !isDead)
                PlayerHealing(playerStats.regenAmount);
        }
    }

    public void Dead()
    {
        isDead = true;
        spriteEffectsPlayer.HideEffect_Outline();
        spriteEffectsPlayer.ShowEffect_Dissolve();
        UI_GameController.Instance.PlayerDead();
    }
}
