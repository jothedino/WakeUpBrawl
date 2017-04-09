// This script demonstrates how to use input to switch scenes. Requires 5.3+

using UnityEngine;
using System.Collections;
// Don't forget! Add the SceneManagement namespace to change scenes.
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {

    // public variables are shown in the Inspector and are usable by other scripts.
    // strings are collections of characters like "hello" or "hot dog".
    // This string is set in the Inspector and is used to set which scene to switch to.
    public string newScene;

    // This is the name of the controller's button set in the Inspector.
    // The actual name (Fire1, Jump. etc.  is set in Edit > Project Settings > Input 
    public string inputName1;
	public string inputName2;
	
	// Update is called once per frame
	void Update () {
        // Use GetButtonUp() to check for a button that is pressed and then released.
        if (Input.GetButtonUp(inputName1))
        {
            // Change to the desired scene.
            SceneManager.LoadScene(newScene);
        }
		if (Input.GetButtonUp(inputName2))
		{
			// Change to the desired scene.
			SceneManager.LoadScene(newScene);
		}
	}
}
