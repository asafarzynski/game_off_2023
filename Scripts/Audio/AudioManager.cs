using GameOff2023.Scripts.Utils;
using Godot;

public partial class AudioManager : NodeSingleton<AudioManager>
{
	[Export] private AudioStreamPlayer musicPlayer; 
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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
}
