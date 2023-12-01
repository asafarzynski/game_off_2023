using System;
using GameOff2023.Scripts.Characters;
using Godot;

public partial class VFX : Node3D
{
    [Export] private GpuParticles3D[] allParticles;
    // Called when the node enters the scene tree for the first time.

    public event Action<VFX> OnReachedTarget;
    public event Action<VFX> OnEnded;

    private Vector3 _targetPosition;
    private CharacterVisuals targetCharacterVisuals;

    private bool _reachedTarget;
    private int _particleEmittersNumber;
    private int _particleEmittersFinished;

    private const float SPELL_MOVING_SPEED = 5f;

    public void Init(Vector3 from, Vector3 to, CharacterVisuals targetCharacterVisuals)
    {
        GlobalPosition = from;
        _targetPosition = to;
        this.targetCharacterVisuals = targetCharacterVisuals;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (!_reachedTarget)
        {
            GlobalPosition += (_targetPosition - GlobalPosition).Normalized() * (float)(delta * SPELL_MOVING_SPEED);
            if (GlobalPosition.DistanceTo(_targetPosition) < 1f)
            {
                _reachedTarget = true;
                OnReachedTarget?.Invoke(this);
                
                if (allParticles == null)
                {
                    OnEnded?.Invoke(this);
                }
                else
                {
                    foreach (var gpuParticles3D in allParticles)
                    {
                        if (gpuParticles3D == null)
                            continue;

                        _particleEmittersNumber++;
                        gpuParticles3D.Emitting = false;
                        gpuParticles3D.Finished += GpuParticles3D_Finished;
                    }
                }
            }
        }
    }

    private void GpuParticles3D_Finished()
    {
        _particleEmittersFinished++;

        if (_particleEmittersFinished == _particleEmittersNumber)
        {
            OnEnded?.Invoke(this);
        }
    }
}
