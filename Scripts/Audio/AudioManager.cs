using GameOff2023.Scripts.Audio;
using GameOff2023.Scripts.Utils;
using Godot;
using Godot.Collections;

public partial class AudioManager : NodeSingleton<AudioManager>
{
	[Export] private AudioStreamPlayer musicPlayer;
	[Export] private AudioStream[] uiSounds; // a nice way to preview enum values would be nice

	private Dictionary<UISound, AudioStreamPlayer> _uiPlayers;
	
	private StringName _uiBusName = new StringName("UI");
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PrepareUISounds();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void PlayMusic()
	{
		musicPlayer.Play();
	}

	public void StopMusic()
	{
		musicPlayer.Stop();
	}

	public void PlayUISound(UISound sound)
	{
		if (_uiPlayers.TryGetValue(sound, out var player))
		{
			player.Play();
		}
	}

	private void PrepareUISounds()
	{
		_uiPlayers = new();
		for (var i = 0; i < uiSounds.Length; i++)
		{
			AudioStream audioStream = uiSounds[i];
			var newPlayer = new AudioStreamPlayer();
			AddChild(newPlayer);
			newPlayer.Stream = audioStream;
			newPlayer.Bus = _uiBusName;
			newPlayer.MaxPolyphony = 4;
			_uiPlayers.Add((UISound)i, newPlayer);
		}
	}
}
