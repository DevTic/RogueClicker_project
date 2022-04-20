using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStats_SO playerStats;

    [SerializeField] private float hpCurrent;
    [SerializeField] private float hpMax;

    public static PlayerController Instance { get; set; }

    private void Awake()
    {
        Instance = this;

        hpCurrent = hpMax = playerStats.hpMax;        
    }

    // Jugador recibe daño
    public void PlayerTakingDamage(float damage)
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
    public void PlayerHealing(float amount)
    {
        hpCurrent += amount;
        if (hpCurrent >= hpMax)
            hpCurrent = hpMax;
    }
}
