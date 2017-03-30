using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image currentHealthbar;
    private float hitpoint = 100;
    private float maxHitpoint = 100;
    private float gameOverDelay = 5f;




    // Use this for initialization
    void Start()
    {
        UpdateHealth();

    }

    void UpdateHealth()
    {
        float ratio = hitpoint / maxHitpoint;
        //currentHealthbar.fillAmount = ratio;
        currentHealthbar.rectTransform.localScale = new Vector3(ratio, 1, 1);
       
    }

    public void TakeDamage(float damage)
    {

        hitpoint -= damage;
        if (hitpoint <= 0)
        {
            hitpoint = 0;


        }
        UpdateHealth();
    }
    public void Death()
    {




        StartCoroutine(GameOver());



    }

    private IEnumerator GameOver()
    {

        yield return new WaitForSeconds(gameOverDelay);     // Delay before resetting
                                                            //gameObject.SetActive(true);


        //PlayerShoot pShoot = gameObject.GetComponent < PlayerShoot > ();
        //pShoot.DowngradeWeapon(1);

        hitpoint = 100;
        float ratio = hitpoint / maxHitpoint;
        //currentHealthbar.fillAmount = ratio;
        currentHealthbar.rectTransform.localScale = new Vector3(ratio, 1, 1);
      
    }


}
