using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//using UnityEngine.Advertisements;

public class MainMenu : MonoBehaviour
{

	public Canvas optionsDialog;
	public Canvas playersCreatorDialog;
	public GameObject playerCreatorPrefab;

	public static MainMenu main;

	void Awake(){
		main = this;
	}

	// Use this for initialization
	void Start ()
	{
		AdManager.main.showBanner ();
	}

	// Update is called once per frame
	void Update ()
	{
	
	}

	public void showOptions(){
		optionsDialog.enabled = true;
	}

	public void showPvMCreator(){
		PlayerCreator pc = Instantiate (playerCreatorPrefab).GetComponent<PlayerCreator>();
		pc.setGameType (GamePlayerType.PVMOFFLINE);
		playersCreatorDialog.enabled = true;
	}

	public void showPvPCreator(){
		PlayerCreator pc = Instantiate (playerCreatorPrefab).GetComponent<PlayerCreator>();
		pc.setGameType (GamePlayerType.PVPOFFLINE);
		playersCreatorDialog.enabled = true;
	}

	public void hideOptions(){
		optionsDialog.enabled = false;
		if (PlayerCreator.main != null) {			
			PlayerCreator.main.reset ();
			PlayersManager.main.reset ();
			DestroyImmediate (PlayerCreator.main.gameObject);
		}
	}

	public void start(){
		SceneManager.LoadScene ("level1");
	}

	public void finish(){
		Application.Quit ();
	}

	public void showAd(){
//		if (Advertisement.IsReady()) {
//			Advertisement.Show ("video", new ShowOptions(){resultCallback = handleResult});
//		}
	}

//	public void handleResult(ShowResult result){
//		switch (result) {
//		case ShowResult.Finished:
//			Debug.Log ("");
//			break;
//		case ShowResult.Skipped:
//			Debug.Log ("");
//			break;
//		case ShowResult.Failed:
//			Debug.Log ("");
//			break;
//		}
//		finish ();
//	}
}

