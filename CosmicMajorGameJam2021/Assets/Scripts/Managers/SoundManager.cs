
using System;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public enum SoundEffects
{
	click,
	email,
	bossemail,
	shutdown,
	error,
	notice,
	chime,
	foldercorruption,
	daystart,
	daycomplete,
	donewithgame
};

public class SoundManager : MonoBehaviour
{
	public static SoundManager main;

	[SerializeField] private AudioMixer musicMixer = null;
	[SerializeField] private AudioMixer sfxMixer = null;
	
	public float musicVolume;
	public float sfxVolume;
	
	[Space] [Header("Music")] 
	public AudioClip MainMenuMusic;
	public AudioClip[] MainGameMusic;
	[SerializeField] private bool isInMainMenuScene;
	[Space] [Header("Sounds")] 
	public AudioClip mailNormal;
	public AudioClip mailBoss;
	public AudioClip folderCorrupt;
	public AudioClip shutDown;
	public AudioClip[] error;
	public AudioClip[] notice;
	public AudioClip[] chime;
	public AudioClip[] click;
	public AudioClip dayStart;
	public AudioClip dayComplete;
	public AudioClip doneWithGame;


	public AudioSource musicSource;
	public AudioSource sfxSource;

	#region Properties

	public float MusicVolume
	{
		get => musicVolume;
		set => musicVolume = value;
	}

	public float SfxVolume
	{
		get => sfxVolume;
		set => sfxVolume = value;
	}

	public AudioMixer MusicMixer
	{
		get => musicMixer;
	}

	public AudioMixer SfxMixer
	{
		get => sfxMixer;
	}

	#endregion

	private void Awake()
	{
		main = this;
		getMixerVolumes();
	}

	private void Start()
	{
		if(isInMainMenuScene)PlayMainMenuMusic();
		else PlayMainGameMusic();
		
	}

	private void getMixerVolumes()
	{
		musicMixer.GetFloat("MusicVolume", out musicVolume);
		sfxMixer.GetFloat("SfxVolume", out sfxVolume);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0)) PlaySoundEffect(SoundEffects.click);
	}

	public void PlaySoundEffect(SoundEffects effect)
	{
		
		switch (effect)
		{
			case SoundEffects.chime:
				sfxSource.PlayOneShot(chime[Random.Range(0, chime.Length)]);
				break;
			case SoundEffects.click:
				sfxSource.PlayOneShot(click[Random.Range(0, click.Length)]);
				break;
			case SoundEffects.email:
				sfxSource.PlayOneShot(mailNormal);
				break;
			case SoundEffects.error:
				sfxSource.PlayOneShot(error[Random.Range(0, error.Length)]);
				break;
			case SoundEffects.foldercorruption:
				sfxSource.PlayOneShot(folderCorrupt);
				break;
			case SoundEffects.notice:
				sfxSource.PlayOneShot(notice[Random.Range(0, notice.Length)]);
				break;
			case SoundEffects.shutdown:
				sfxSource.PlayOneShot(shutDown);
				break;
			case SoundEffects.bossemail:
				sfxSource.PlayOneShot(mailBoss);
				break;
			case SoundEffects.daystart:
				sfxSource.PlayOneShot(dayStart);
				break;
			case SoundEffects.daycomplete:
				sfxSource.PlayOneShot(dayComplete);
				break;
			case SoundEffects.donewithgame:
				sfxSource.PlayOneShot(doneWithGame);
				break;
			default: break;
		}
	}

	public void PlayMainMenuMusic()
	{
		musicSource.clip = MainMenuMusic;
		musicSource.Play();
		Invoke(nameof(PlayMainMenuMusic),MainMenuMusic.length);//using this here because loop will be turned off for consistency across scenes with the prefab
	}
	public void PlayMainGameMusic()
	{
		musicSource.clip = MainGameMusic[Random.Range(0,3)];
		musicSource.Play();
		Invoke(nameof(PlayMainGameMusic),musicSource.clip.length);//doing this instead of loop because we may want to switch between different main game music
	}

}
