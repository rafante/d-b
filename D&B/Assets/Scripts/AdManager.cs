using UnityEngine;
using System.Collections;
using admob;

public class AdManager : MonoBehaviour
{

	public static AdManager main;
	public string bannerId = "ca-app-pub-1343647000894788/1318114357";
	public string videoId = "ca-app-pub-1343647000894788/7225047156";

	// Use this for initialization
	void Awake ()
	{
		main = this;
		DontDestroyOnLoad (gameObject);

		Admob.Instance ().initAdmob (bannerId, videoId);
		Admob.Instance ().loadInterstitial ();
		Admob.Instance ().setTesting (true);
	}

	IEnumerator loadAds(){
		while (!Admob.Instance ().isInterstitialReady()) {
			yield return new WaitForSeconds(1);
		}
		Admob.Instance ().showInterstitial ();
	}

	public void showBanner(){
		Admob.Instance ().showBannerRelative (AdSize.Banner, AdPosition.BOTTOM_CENTER, 7);
	}

	public void showVideo(){
		StartCoroutine (loadAds());
	}

	// Update is called once per frame
	void Update ()
	{
	
	}
}

