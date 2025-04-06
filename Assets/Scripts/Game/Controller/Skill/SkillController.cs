using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SkillController : MonoBehaviour
{
    List<Skill> _skills;
    byte _index;

    BulletController _bulletController;
    

    public void Init(params Skill[] skils)
    {
        _skills = new List<Skill>();
        _index = 0;
        _skills = skils.ToList<Skill>();
        _bulletController = GetComponent<BulletController>();
    }

    public BulletController GetBulletController() { return _bulletController; }

    public byte GetCurrentSkillIndex() { return _index; }
    public void Activate(byte idx, Player p) { _skills[idx].Activate(p); }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < _skills.Count; i++)
        {
            _skills[i].Update();
        }
    }
}
