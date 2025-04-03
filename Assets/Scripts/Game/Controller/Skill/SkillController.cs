using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SkillController : MonoBehaviour
{
    List<Skill> _skills;
    byte _index;

    public void Init(params Skill[] skils)
    {
        _skills = new List<Skill>();
        _index = 0;
        _skills = skils.ToList<Skill>();

        Debug.Log(_skills);
    }

    public byte GetCurrentSkillIndex() { return _index; }
    public void Activate(byte idx, Player p) { _skills[idx].Activate(p); }

    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
