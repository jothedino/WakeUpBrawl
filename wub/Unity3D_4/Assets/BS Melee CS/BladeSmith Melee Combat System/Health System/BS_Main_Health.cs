using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BS_Main_Health : MonoBehaviour {

    public Image healthBar;
    private float maxHitpoint = 100;
    new Animator animation;

    public string WinningPlayer;

    [Tooltip("How much health should this object have? When it reaches 0, Death functions will be triggered on")]
	public int  _health;
	[Tooltip("You can set a prefab to be instantiated upon being hit - like blood or flying robot parts! Simply put your Blood prefab here. The same function can be called from the BS_Marker_Manager if you wish.")]
	public GameObject Blood;

	[Space(10)]
	[Header("Sound")]
	[Tooltip("If we're using any sounds, put your AudioSource here!")]
	public AudioSource SoundSource;
	[Range(0,5)]
	[Tooltip("How many Hurt Sounds Can We randomise from?")]
	public int _numberOfHurtSounds;
	public AudioClip HurtSound1;
	public AudioClip HurtSound2;

	[Space(5)]
	[Range(0,5)]
	[Tooltip("How many Death sounds can we randomise from?")]
	public int _numberOfDeathSounds;
	public AudioClip DeathSound1;
	public AudioClip DeathSound2;

	[Space (10)]
	[Header("Death Feautres")]

	[Tooltip("Should something be instantiated on Death (like an explosion of special blood splat)? Put your prefab here if yes")]
	public GameObject _SpawnOnDeath;


	GameObject _Blood_Instance;
	int _HurtAnim_Randomisation;
	int _DeathAnim_Randomisation;
	int _HurtSound_Randomisation;
 

    void Start()
    {
        UpdateHealth();
    }

    void UpdateHealth()
    {
        float ratio = _health/ maxHitpoint;
        healthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        animation = GetComponent<Animator>();
        animation.SetTrigger("Hurt");

    }

	public void Bloodflood(Vector3 _prevMarkerPos, Vector3 _hitPos)   //Instantiate blood in the direction of the marker which hit this object.
	{
		if (Blood != null && _health >0) 
		{
			_Blood_Instance =  Instantiate(Blood, _hitPos, transform.rotation) as GameObject;
			_Blood_Instance.transform.LookAt(2* _Blood_Instance.transform.position - _prevMarkerPos);

		}

	}



	public void ApplyDamage(int _dmg)   //Let's apply some damage on hit, shall we?
	{
		_health -= _dmg;
       

        if (_health< 0) 
		{
			_health = 0;
		}

		if (_health > 0) 
		{
			if(SoundSource != null)
			{
				if(_numberOfHurtSounds >0)
				{
					_HurtSound_Randomisation = Random.Range (1, _numberOfHurtSounds+1);
					
						if(_HurtSound_Randomisation == 1)
						{
							SoundSource.clip = HurtSound1;
							SoundSource.Play();
						}
						if(_HurtSound_Randomisation == 2)
						{
							SoundSource.clip = HurtSound2;
							SoundSource.Play();
						}

				}
			}
            UpdateHealth();
		
		}

		if (_health <= 0) //DEATH
		{
			UpdateHealth ();
            animation.SetTrigger("Death");

            if ( _SpawnOnDeath != null)
			{
				Instantiate( _SpawnOnDeath, transform.position, transform.rotation);
			}

			if(SoundSource != null)
			{
				if(_numberOfDeathSounds >0)
				{
					_HurtSound_Randomisation = Random.Range (1, _numberOfDeathSounds+1);
					
						if(_HurtSound_Randomisation == 1)
						{
							SoundSource.clip = DeathSound1;
							SoundSource.Play();
						}
						if(_HurtSound_Randomisation == 2)
						{
							SoundSource.clip = DeathSound2;
							SoundSource.Play();
						}

				}
			}
            
            StartCoroutine(Delay(2f));
            //SceneManager.LoadScene(WinningPlayer);
           
        }
	}

    IEnumerator Delay(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(WinningPlayer);
    }
}