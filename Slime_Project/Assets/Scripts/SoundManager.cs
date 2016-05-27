using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioSource efxSource;
	public AudioSource[] BGMList;
	public static SoundManager instance = null;

	public float lowPitchRange = 0.95f;
	public float highPitchRange = 1.05f;

	void Awake () {
		if (instance == null){
			instance = this;
		}
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
	}

	public void PlaySingle (AudioClip clip){
		efxSource.clip = clip;
		efxSource.Play ();
	}

	public void StopPlay (AudioClip clip){
		efxSource.clip = clip;
		efxSource.Stop ();
	}

	public void RandomizeSfx (params AudioClip [] clips){
		int randomIndex = Random.Range (0, clips.Length);
		float randomPitch = Random.Range (lowPitchRange, highPitchRange);

		efxSource.pitch = randomPitch;
		efxSource.clip = clips [randomIndex];
		efxSource.Play ();
	}

	public void PlayNextBGM (int level){
		if (level == 0) {
			BGMList [level].Play ();
		} else {
			BGMList[level-1].Stop ();
			BGMList[level].Play ();
		}
	}

	public void PlayDeathBGM(int level){
		BGMList[level].Stop ();
		BGMList [7].Play ();
	}

	public void Restart(int level){
		BGMList [7].Stop ();
		BGMList[level].Play ();
	}
}
