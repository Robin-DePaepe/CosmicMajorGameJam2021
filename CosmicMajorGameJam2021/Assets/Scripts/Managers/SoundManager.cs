
using UnityEngine;
using UnityEngine.Audio;

public enum SoundEffects
{
	click,
	email,
	bossemail,
	shutdown,
	error,
	notice,
	chime,
	foldercorruption
};

public class SoundManager : MonoBehaviour
{
	public static SoundManager main;

	[SerializeField] private AudioMixer musicMixer = null;
	[SerializeField] private AudioMixer sfxMixer = null;

	[Range(0.1f, 1f)] [SerializeField] private float defaultMusicVolume = 0.3f;
	[Range(0.1f, 1f)] [SerializeField] private float defaultSfxVolume = 0.8f;

	private float musicVolume = 0.3f;
	private float sfxVolume = 0.8f;

	[Space]
	[Header("Sounds")]
	
	public AudioClip mailNormal;
	public AudioClip mailBoss;
	public AudioClip folderCorrupt;
	public AudioClip shutDown;
	public AudioClip[] error;
	public AudioClip[] notice;
	public AudioClip[] chime;
	public AudioClip[] click;
	

	private AudioSource source;
	#region Properties
	public float MusicVolume { get => musicVolume; set => musicVolume = value; }
	public float SfxVolume { get => sfxVolume; set => sfxVolume = value; }
	public AudioMixer MusicMixer { get => musicMixer; }
	public AudioMixer SfxMixer { get => sfxMixer; }
	#endregion
	private void Awake()
	{
			main = this;		
	}

	private void Start()
	{
		source = GetComponent<AudioSource>();
		musicVolume = defaultMusicVolume;
		sfxVolume = defaultSfxVolume;

		UpdateMixerVolumes();
	}

	private void UpdateMixerVolumes()
	{
		musicMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
		sfxMixer.SetFloat("SfxVolume", Mathf.Log10(sfxVolume) * 20);
	}

	public void PlaySoundEffect(SoundEffects effect)
	{
		switch (effect)
		{
			case SoundEffects.chime:
				source.PlayOneShot(chime[Random.Range(0, chime.Length)]);break;
			case SoundEffects.click:source.PlayOneShot(click[Random.Range(0,click.Length)]);break;
			case SoundEffects.email:source.PlayOneShot(mailNormal);break;
			case SoundEffects.error:source.PlayOneShot(error[Random.Range(0,error.Length)]);break;
			case SoundEffects.foldercorruption:source.PlayOneShot(folderCorrupt);break;
			case SoundEffects.notice:source.PlayOneShot(notice[Random.Range(0,notice.Length)]); break;
			case SoundEffects.shutdown:source.PlayOneShot(shutDown); break;
			case SoundEffects.bossemail:source.PlayOneShot(mailBoss); break;
		}
	}
	
}
