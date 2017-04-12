using UnityEngine;
using System.Collections;

public class BS_Weapon_Animation_Events : MonoBehaviour {

	[Tooltip("Choose which Marker Manager should be used with this Animation Support")]
	public BS_Marker_Manager MarkerManager;
	[Tooltip("You can use various Animation Event Calls to tweak your Damage Dealing mechanisms in your attack Animations, as well as enabling and disabling shields. The Events are: DisableMarkers(), EnableMarkers(),  ClearTargets(), CancelStagger(), SetDamage<1-9>(), Stagger(), DisableShieldCollider(), EnableShieldCollider(), SetWeapon<1-9>MarkerManager() . Read about them in the Read Me File to find out what each of them does!    [It's an Info bool. It does nothing, it just stores the Tooltip]")]
	public bool InfoAndAnimationEvents;

	[Space(15)]
	[Tooltip("You can use an Animation Event to switch between which Weapon Marker we're operating (like when your character uses more than one type of weapon with a single Mecanim animator. Simply call an event SetWeapon<1-9>MarkerManager() - don't worry, the Markers of the previous Marker Manager can be automatically Disabled! [INFO]: read more in the ReadMe File!")]
	public BS_Marker_Manager Weapon1MarkerManger;
	[Tooltip("You can use an Animation Event to switch between which Weapon Marker we're operating (like when your character uses more than one type of weapon with a single Mecanim animator. Simply call an event SetWeapon<1-9>MarkerManager() - don't worry, the Markers of the previous Marker Manager can be automatically Disabled! [INFO]: read more in the ReadMe File!")]
	public BS_Marker_Manager Weapon2MarkerManger;
	[Tooltip("Should the Markers of the current Marker Manager be automatically Disabled upon switching to another Marker Manager? (It's a good thing to leade it True by default). ")]
	public bool DisableMarkersOnManagerSwitch = true;

	public void ClearTargets()
	{
		MarkerManager.ClearTargets ();
	}

	public void DisableMarkers ()
	{
		MarkerManager.DisableMarkers ();
	}
	public void  EnableMarkers()
	{
		MarkerManager.EnableMarkers ();
	}
	public void Stagger ()
	{
		MarkerManager.Stagger ();
	}
	public void  CancelStagger()
	{
		MarkerManager.CancelStagger();
	}

	public void  SetDamage1 ()
	{
		MarkerManager.SetDamage1 ();
	}
	public void  SetDamage2 ()
	{
		MarkerManager.SetDamage2 ();
	}

	public void  SetWeapon1MarkerManager ()
	{
		if (DisableMarkersOnManagerSwitch) {
			MarkerManager.DisableMarkers ();
		}
			MarkerManager = Weapon1MarkerManger;
	}
	public void  SetWeapon2MarkerManager ()
	{
		if (DisableMarkersOnManagerSwitch) {
			MarkerManager.DisableMarkers ();
		}
		MarkerManager = Weapon2MarkerManger;
	}

}
