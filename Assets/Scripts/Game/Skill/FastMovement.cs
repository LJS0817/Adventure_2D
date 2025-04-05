using UnityEngine;

public class FastMovement : Skill
{
    protected Vector2 _targetUp;

    public FastMovement()
    {
        _power = 40f;
        _targetUp = Vector2.zero;
    }

    public override void Update()
    {
        if(_activated && _player)
        {
            if (Vector2.Distance(_player.transform.position, _targetPos) < 1.5f)
            {
                _player.GetRigibbody().linearVelocity *= 0.3f;
                _player.transform.up = _targetUp;
                _player.UseGravity(true);

                _player.SetState(PLAYER_STATE.E_IDLE);
                _player = null;
            }
        }
    }

    protected override void action()
    {
        _targetPos = _player.GetInputController().Marker.position;
        _targetUp = _player.GetInputController().GetUpVector();

        Vector2 dir = _targetPos - (Vector2)_player.transform.position;

        _player.UseGravity(false);
        _player.GetRigibbody().AddForce(dir.normalized * _power, ForceMode2D.Impulse);
    }
}
