using UnityEngine;
using System.Collections;


[System.Serializable]
public class Sound {

	public string name;
	public AudioClip clip;
	private AudioSource source;

	[Range(0f,1f)]
	public float volume = 0.7f;
	//[Range(0.5f,1.5f)]
	//public float pitch = 1f;

	public void SetSource (AudioSource _source)
	{
		source = _source;
		source.clip = clip;
	
	}

	public void Play()
	{
		source.volume = volume;
		//source.pitch = pitch;
		source.Play ();
	
	}


}


public class audio_manager_script : MonoBehaviour {

	public static audio_manager_script _audioM;

	[SerializeField]
	Sound[] sounds;

	void Awake()
	{
		_audioM = this;
	}

	void Start ()
	{
		for ( int i = 0; i < sounds.Length; i++)
		{
			GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
			sounds [i].SetSource (_go.AddComponent<AudioSource> ());
		}

		PlaySound ("test");
	}

	public void PlaySound (string _name)
	{
		for (int i = 0; i < sounds.Length; i++) 
		{
			if (sounds [i].name == _name) 
			{
				sounds [i].Play ();
				return;
			}
		}
	}

}