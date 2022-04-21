using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStats_SO playerStats;

    [SerializeField] private int hpCurrent;
    [SerializeField] private int hpMax;

    private bool isDead;

    public static PlayerController Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        isDead = false;

        hpCurrent = hpMax = playerStats.hpMax;        
    }

    private void Start()
    {
        PlayerRegeneration();
    }

    // Jugador recibe daño
    public void PlayerTakingDamage(int damage)
    {
        hpCurrent -= damage;

        // Da efecto a la camara al recibir un golpe
        GameController.Instance.CameraShake();
        if (hpCurrent <= 0)
        {
            // Dead
        }
    }

    // Jugador sanando
    public void PlayerHealing(int amount)
    {
        hpCurrent += amount;

        GameController.Instance.ShowFloatingText(amount, false, false, false);

        if (hpCurrent >= hpMax)
            hpCurrent = hpMax;
    }

    public void PlayerRegeneration()
    {
        StartCoroutine(DelayRegeneration());
    }

    IEnumerator DelayRegeneration()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(playerStats.regenIntervalTime);

            if ((playerStats.regenAmount > 0) && (hpCurrent < hpMax))
                PlayerHealing(playerStats.regenAmount);
        }
    }
}
