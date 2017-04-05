using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BS_Marker_Manager : MonoBehaviour {
	

	//Manager should be close to the weapon's model - ideally in the center.


	[Tooltip("To start, simply assign weapon's Markers (from a Prefab Folder) around the damage-dealing zone (like a blade) as children of any object in the Weapon's hierarchy (preferably as children of this Weapon Manager object) and fill out the variables!     [INFO]: It's an info bool. It does nothing, it just stores the instruction Tooltip")]
	public bool Instructions;

	[Header ("Basic Features")]
	[Tooltip("Choose which Object stores all the weapon's Makers. If it's the same Object as the one with this script attached, then you can leave it blank.")]
	public Transform _MarkersParent; 
	[Tooltip("What is the Tag of objects which can be hit? (Like enemies, exploding barrels, enemy players, etc.) [INFO]: You can set the target Layers on each individual Marker too. Everything what is not a Target or a Shield is considered a wall.")]
	public string _targetTag = "Enemy";

	[Tooltip("How much damage should this weapon deal with each hit? It can be changed with an Animation Event or a function call SetDamage<number>(), below.")]
	public int _damage = 1; 
	[Tooltip("You can change the Damage value of this weapon in-game with an Animation Event or a function call. Set this damage value and call SetDamage1() function at the start or in a middle of your animation to set it. [INFO] Useful if you want to have combo attacks - just call a different  SetDamage function at the start of each swing animation with different damage value. Just remember to set it at the start of each swing, so it's always the right damage type!")]
	public int _damageType1;
     [Tooltip("You can change the Damage value of this weapon in-game with an Animation Event or a function call. Set this damage value and call SetDamage2() function at the start or in a middle of your animation to set it. [INFO] Useful if you want to have combo attacks - just call a different  SetDamage function at the start of each swing animation with different damage value. Just remember to set it at the start of each swing, so it's always the right damage type!")]
	 public int _damageType2;

	 [Tooltip("A weapon can work in two ways: Manual and Continuous. Manual bases on an idea that each attack (like a sword swing) can hit each target only once - it then needs to be manualy reloaded through a function call or an Animation Event (for example by ClearTargets() - explained in the Info Bool, at the bottom of this Inspector window). This method is very precise, as you can be certain that each Target will get damage and  Hurt animation (on BS_Main_Health script) triggered only once with each swing. Manual should be used in most situations, mostly for animation-driven combat (like Dark Souls or God of War). The Continuous mode deals damage constantly, in a given time interval, giving you a constant damage-dealing weapon. It's better for VR and games with free-moving weapons (like when the Player can swing his sword by moving his mouse in any desired direction) - the downside of Continuous damage is that it takes away the precise control of the damage dealt (as each Target may be hit more than once depending on how fast the weapon is being driven around). Continuous should be used with blades which can move freely, independent from animations. [INFO:] Continuous damage is not fit for working with shields (BS_Shield script objects).")]
	public bool ContinuousDamage;
	[Tooltip("If you choose the Continuous damage, what should the interval of damage dealing be? (In seconds)")]
	public float ContinuousDamageInterval = 0.2f;
	float _ContinuousDamage_Timer;
	[Tooltip("You can spawn Blood upon hit both from the Weapon side and from the BS_Main_Health side. If you want to spawn it from Weapon side, put your Blood prefab here or leave empty if there's no blood (or sparks or anything equivalent) upon hit!")]
	public GameObject Blood;
	bool HitFlesh;
	bool HitWall;

	BS_Marker [] _markers; 
	List<Transform> _Targets_Raw_Hit = new List<Transform>(); //Targets initialy hit by the blade (pre-check
	List<Transform> _Used_Targets = new List<Transform>(); //Targets which were excluded from being hit or were already hit in that frame
	List<Vector3> _Blade_Direction = new List<Vector3>();   
	List<Vector3> _Blade_Startpoint = new List<Vector3>();
	List<Vector3> _wallHitPositions = new List<Vector3>();
	bool _markersAreEnabled;
	GameObject _missSparks; //it's an inside variable. For true miss sparks look for WallHitSparks.

	BS_Main_Health _Raw_Target_Instance; //A single target which was hit.

	[Tooltip("Should the Markers be active upon the Start of this weapon?")]
	public bool _startActivated = true;

	[Space(15)]
	[Tooltip("If we're going to use any Target or Wall hit sounds, put the AudioSource here!")]
	 public AudioSource SoundSource;

	[Range(0,5)]
	[Tooltip("How many Wall Sounds can we randomize from? (these sounds are played upon hitting something which is not a Target or a Shield (so mostly environment hits, like hitting a wall)")]
	public int _numberOfWallHitSounds;
	public AudioClip WallHitSound1;
	public AudioClip WallHitSound2;

	[Range(0,5)]
	[Tooltip("How many Target Sounds can we randomize from? (these sounds are played upon hitting an Object with a Target Tag")]
	public int _numberOfTargetHitSounds;
	public AudioClip TargetHitSound1;
	public AudioClip TargetHitSound2;

	[Header ("Stagger and Block Features")]
	[Space(15)]
	[Tooltip("If we want to use Stagger Animations (like when you hit a wall or a shield), choose if you want to use Legacy or Mecanim animator. If Mecanim, put your Animator here! [INFO]: When staggered, the Animator will play a state called Stagger, so set your Animation States accordingly!")]
	public Animator _staggerAnimator;
	[Tooltip("If we want to use Stagger Animations (like when you hit a wall or a shield), choose if you want to use Legacy or Mecanim animator. If Legacy, put your Animation component here! [INFO]: When staggered, an animation called Stagger will be played!")]
	public Animation _staggerLegacyAnimation;
	[Tooltip("Should we play a Stagger animation (like attack interruption) upon hitting a wall (object with a different Tag then Target Tag or Shield Tag?")]
	public bool StaggerOnWallHit;
	[Tooltip("Should we spawn some sparks or equivalent object upon hitting a wall or a shield?")]
	public GameObject _wallHitSparks;
	[Tooltip("[ADVANCED] You can also send a message to call a function on another object when we get staggered. If yes, assign the reciever here - a function called Stagger() will be called upon it!")]
	public GameObject _sendStaggerMessage;
	[Tooltip("Should we disable markers automatically upon being staggered?")]
	public bool _disableMarkersOnBlock;

	[Space(10)]
	[Tooltip("Should the markers be disabled when this object gets destroyed or disabled? It's good to keep this function on, so there are no hit detection glitches on enabling and disabling weapons")]
	public bool DisableMarkersOnObjectDisable = true;


	private int sRoll; //This is a dummy variable, used in various Random Range rolls (like rolling for sound randomisation).


	
	void Start() 
	{
		if (_MarkersParent == null) {
			_MarkersParent = transform;
		}
		_markers = new BS_Marker[_MarkersParent.childCount];

		
		for(int i = 0; i < _markers.Length; i++) 
		{
			_markers[i] = _MarkersParent.GetChild(i).gameObject.GetComponent<BS_Marker>();
		}

		if (_startActivated) 
		{
			EnableMarkers();
		}

	}
	
	
	public void EnableMarkers()
	{
		_markersAreEnabled = true;
		_Targets_Raw_Hit.Clear ();
		_Blade_Startpoint.Clear ();
		_Blade_Direction.Clear();
		_Used_Targets.Clear ();
		_wallHitPositions.Clear ();
		for (int i2 = 0; i2 < _markers.Length; i2++) {
			_markers [i2]._tempPos = _markers [i2].transform.position; 
			if (i2 > _markers.Length) {
				i2 = 0;
			}
		}
	}
	
	public void DisableMarkers()
	{
		_markersAreEnabled = false;
		for (int i2 = 0; i2 < _markers.Length; i2++) {
			_markers [i2]._tempPos = _markers [i2].transform.position; 
			if (i2 > _markers.Length) {
				i2 = 0;
			}
		}
		_Targets_Raw_Hit.Clear ();
		_Blade_Startpoint.Clear ();
		_Blade_Direction.Clear();
		_Used_Targets.Clear ();
		_wallHitPositions.Clear ();
		for (int i2 = 0; i2 < _markers.Length; i2++) {
			_markers [i2]._tempPos = _markers [i2].transform.position; 
			if (i2 > _markers.Length) {
				i2 = 0;
			}
		}
	}

	public void OnDisable()
	{
		if (DisableMarkersOnObjectDisable) {
			DisableMarkers();
		}
	}

	public void ClearTargets()
	{
		_Targets_Raw_Hit.Clear ();
		_Blade_Startpoint.Clear ();
		_Blade_Direction.Clear();
		_Used_Targets.Clear ();
		_wallHitPositions.Clear ();
		for (int i2 = 0; i2 < _markers.Length; i2++) {
			_markers [i2]._tempPos = _markers [i2].transform.position; 
			if (i2 > _markers.Length) {
				i2 = 0;
			}
		}
	}

	public void CancelStagger()
	{
		_staggerAnimator.SetBool ("Stagger", false);

	}
	
	void Update() 
	{
	if (_markersAreEnabled) {
		
			int i; 
			for (i = 0; i < _markers.Length; i++) { //Let's check what each marker hits, shall we?
			
			if (_markers [i].HitCheck () != null) {

					if (_markers [i]._target.tag == _targetTag && _Targets_Raw_Hit.Contains(_markers [i]._target) == false && _Used_Targets.Contains(_markers [i]._target) == false) 
					{
					
					_Blade_Direction.Add(_markers [i]._tempPos);
					_Blade_Startpoint.Add(_markers [i]._hit.point);
					HitFlesh = true;
						if(_markers[i]._target.GetComponent<BS_Main_Health>() != null)
						{
							_Raw_Target_Instance = _markers[i]._target.GetComponent<BS_Main_Health>();
						}
						if(_markers[i]._target.GetComponent<BS_Limb_Hitbox>() != null)
						{
							_Raw_Target_Instance = _markers[i]._target.GetComponent<BS_Limb_Hitbox>().MainHealth;
							_Used_Targets.Add(_markers[i]._target.transform);
						}
						if(_Raw_Target_Instance != null)
						{
						_Targets_Raw_Hit.Add(_Raw_Target_Instance.transform);
						}
				}

			}
			
			if (i > _markers.Length) {
				i = 0;
			}
			
		}

			// dealing damage and staggering
			if (HitWall) { 
				if(HitWall)
				{
					PlayWallHitSound();
				}
				for (int i3 = 0; i3 < _wallHitPositions.Count; i3++)
				{
					
					if(_wallHitSparks != null){
						_missSparks = Instantiate(_wallHitSparks, _wallHitPositions[i3], Quaternion.identity) as GameObject;
						_missSparks.transform.LookAt(_MarkersParent);
					}
				}
				
				
				if(StaggerOnWallHit && HitWall)
				{
					Stagger ();
				}
			}
			if (HitFlesh) { 
				
				for (int i2 = 0; i2 < _Targets_Raw_Hit.Count; i2++) 
				{
					if( _Targets_Raw_Hit[i2]!= null && _Targets_Raw_Hit[i2].GetComponent<BS_Main_Health> () != null && _Used_Targets.Contains(_Targets_Raw_Hit[i2]) == false)
					{
						_Targets_Raw_Hit[i2].GetComponent<BS_Main_Health> ().Bloodflood (_Blade_Direction[i2], _Blade_Startpoint[i2]);
						_Targets_Raw_Hit[i2].GetComponent<BS_Main_Health> ().ApplyDamage (_damage);
						PlayTargetHitSound();
						if(Blood != null)
						{
							GameObject b =  Instantiate(Blood, _Blade_Startpoint[i2], Quaternion.identity) as GameObject;
							b.transform.LookAt(_MarkersParent);
						}
						_Used_Targets.Add(_Targets_Raw_Hit[i2]);
					}
					if( _Targets_Raw_Hit[i2]!= null && _Targets_Raw_Hit[i2].GetComponent<BS_Limb_Hitbox> () != null && _Used_Targets.Contains(_Targets_Raw_Hit[i2]) == false)
					{
						_Targets_Raw_Hit[i2].GetComponent<BS_Limb_Hitbox> ().MainHealth.Bloodflood (_Blade_Direction[i2], _Blade_Startpoint[i2]);
						_Targets_Raw_Hit[i2].GetComponent<BS_Limb_Hitbox> ().MainHealth.ApplyDamage (_damage);
						PlayTargetHitSound();
						if(Blood != null)
						{
							GameObject b =  Instantiate(Blood, _Blade_Startpoint[i2], Quaternion.identity) as GameObject;
							b.transform.LookAt(_MarkersParent);
						}
						_Used_Targets.Add(_Targets_Raw_Hit[i2].GetComponent<BS_Limb_Hitbox> ().MainHealth.transform);
					}
				}
				
			}
			
			
		}
			
		_Blade_Direction.Clear();
		_Blade_Startpoint.Clear();
		_Targets_Raw_Hit.Clear();
		_wallHitPositions.Clear ();
		
		HitWall = false;
		HitFlesh = false;
		
		if(ContinuousDamage)
		{
			_ContinuousDamage_Timer += Time.deltaTime;
			if(_ContinuousDamage_Timer >= ContinuousDamageInterval)
			{
				ClearTargets();
				_ContinuousDamage_Timer = 0;
			}
		}


	}

	public void Stagger()
	{
		if(_staggerAnimator != null)
		{
			_staggerAnimator.Play("Stagger");
		}
		
		if(_staggerLegacyAnimation != null)
		{
			_staggerLegacyAnimation.Play("Stagger");
		}
		
		if(_sendStaggerMessage != null)
		{
			_sendStaggerMessage.SendMessage("Stagger", SendMessageOptions.DontRequireReceiver);
		}
		
		if(_disableMarkersOnBlock)
		{
			DisableMarkers();
		}
	}

	public void SetDamage1()
	{
		_damage = _damageType1;
	}
	public void SetDamage2()
	{
		_damage = _damageType2;
	}

	void PlayTargetHitSound()
	{
		if(SoundSource != null)
		{
			if(_numberOfTargetHitSounds >0)
			{
				sRoll = Random.Range (1, _numberOfTargetHitSounds+1);
				
				if(sRoll == 1)
				{
					SoundSource.PlayOneShot(TargetHitSound1);
					
				}
				if(sRoll == 2)
				{
					SoundSource.PlayOneShot(TargetHitSound2);
					
				}
				
			}
		}
		
	}

	void PlayWallHitSound()
	{
		if (SoundSource != null) {
			if (_numberOfWallHitSounds > 0) {
				sRoll = Random.Range (1, _numberOfWallHitSounds + 1);
			
				if (sRoll == 1) {
					SoundSource.PlayOneShot(WallHitSound1);
					
				}
				if (sRoll == 2) {
													SoundSource.PlayOneShot(WallHitSound2);
					
				}
			
			}
		}
	}
}




