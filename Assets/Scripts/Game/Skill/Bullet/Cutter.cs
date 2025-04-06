using UnityEngine;

public class Cutter : Skill
{
    GameObject _bullet;

    public Cutter()
    {
        _bullet = null;
        _force = 40f;
    }

    public override void Update()
    {

    }

    protected override void action()
    {
        if (_bullet == null) _bullet = _player.GetBulletController().GetBulletObj();
        GameObject b = GameObject.Instantiate(_bullet);
        b.GetComponent<Rigidbody2D>().AddForce(_force * _player.GetInputController().GetUpVector(), ForceMode2D.Impulse);
    }
}
