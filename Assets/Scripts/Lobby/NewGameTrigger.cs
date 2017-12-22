using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NewGameTrigger : MonoBehaviour {
	public void Trigger(){
		SceneManager.LoadScene("In Game Scene");
	}
}
