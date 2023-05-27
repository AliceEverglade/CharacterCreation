using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Stats")]
public class DNDStats : Stats
{
    public Stat Proficiency;
    public Stat ArmorClass;
    public List<Stat> Speeds;
    public List<CoreStat> CoreStats;
    public List<Skill> Skills;
    
}

[Serializable]
public class Stat
{
    public string Name;
    [TextArea(4, 4)]
    public string Description;
    public int Value;

    public enum CoreStatName
    {
        Strength,
        Dexterity,
        Constitution,
        Intelligence,
        Wisdom,
        Charisma
    }
}

[Serializable]
public class CoreStat : Stat
{
    public CoreStatName CoreName;
    public int Base;
    public int Bonus;
    public int Total;
}

[Serializable]
public class Skill : Stat
{
    public CoreStatName CoreStat;
    public Proficiency SkillProficiency;
    public int Bonus;
    public enum Proficiency
    {
        NotProficient,
        HalfProficient,
        Proficient,
        Expertise
    }

    
}
