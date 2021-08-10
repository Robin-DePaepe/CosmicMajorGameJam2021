
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
	public static SoundManager main;

	[SerializeField] private AudioMixer musicMixer = null;
	[SerializeField] private AudioMixer sfxMixer = null;

	[Range(0.1f, 1f)] [SerializeField] private float defaultMusicVolume = 0.3f;
	[Range(0.1f, 1f)] [SerializeField] private float defaultSfxVolume = 0.8f;

	private float musicVolume = 0.3f;
	private float sfxVolume = 0.8f;

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
		musicVolume = defaultMusicVolume;
		sfxVolume = defaultSfxVolume;

		UpdateMixerVolumes();
	}

	private void UpdateMixerVolumes()
	{
		musicMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
		sfxMixer.SetFloat("SfxVolume", Mathf.Log10(sfxVolume) * 20);
	}
}
