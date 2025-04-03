using UnityEngine;

public class FastMovement : Skill
{
    public override void Update()
    {
        if (_player == null) return;
        if(Vector2.Distance(_player.transform.position, _targetPos) < 1f)
        {
            _player.GetRigibbody().linearVelocity *= 0.001f;
        }
    }

    protected override void action()
    {
        _power = 50f;
        _player.GetRigibbody().AddForce(_player.GetInputController().GetMouseDir() * _power, ForceMode2D.Impulse);
        _targetPos = _player.GetInputController().Marker.position;
    }
}
