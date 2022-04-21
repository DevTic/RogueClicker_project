using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private TextMeshPro txtDamage;     // muestra el valor del daño
    [SerializeField] private Color colorDmgNormal;      // color del texto al realizar daño base
    [SerializeField] private Color colorDmgReceived;    // color del texto al recibir daño
    [SerializeField] private Color colorDmgCritical;    // color del texto al realizar un critico
    [SerializeField] private Color colorHealing;        // color del texto al realizar una sanación

    public void SetInfoFloatText(int _amount, bool toEnemy = true, bool _critic = false, bool _isDmg = true)
    {
        // Sanaciones
        if (!_isDmg)
        {
            txtDamage.text = string.Format("+{0}", _amount);
            txtDamage.color = colorHealing;             // establecer color de la sanación
            return;
        }

        // Daño
        if (toEnemy)
        {
            // Daño al enemigo
            txtDamage.text = string.Format("{0}", _amount);

            if (_critic)
            {
                this.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                txtDamage.color = colorDmgCritical;    // establecer color del daño critico
            }
            else
                txtDamage.color = colorDmgNormal;      // establecer color del daño base
        }
        else
        {
            // Daño al jugador
            txtDamage.text = string.Format("-{0}", _amount);
            txtDamage.color = colorDmgReceived;        // establecer color del daño base
        }
    }
}
