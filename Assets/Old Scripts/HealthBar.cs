using System.Collections;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    // an SO instance containing hp
    public IntVariable playerHP;

    public void RefillHealth(int amount)
    {
        //StartCoroutine(HealthIncrease(amount));
    }

   /* IEnumerator HealthIncrease(float amount)
    {
        for (int x = 1; x <= amount && playerHP.Value <= playerHP.maxValue; x++)
        {
            playerHP.Value += x;
            //GameManager gameManager = GameObject.FindGameObjectWithTag("gm").GetComponent<GameManager>();
           // GameManager.UpdateUI(); // calls for update
            yield return new WaitForSeconds(0.2f);
        }
    }*/
}