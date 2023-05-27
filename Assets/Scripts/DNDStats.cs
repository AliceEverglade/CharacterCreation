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

    public int GetCoreStatBonus(Stat.CoreStatName name)
    {
        foreach (CoreStat stat in CoreStats) 
        {
            if(stat.CoreName == name)
            {
                return stat.Value;
            }
        }
        return 0;
    }


    public void OnValidate()
    {
        foreach(CoreStat stat in CoreStats)
        {
            stat.SetTotal(this);
            stat.SetValue(this);
            stat.CalculateValue(this);
        }
        foreach(Skill skill in Skills)
        {
            skill.SetValue(this);
            skill.CalculateTotal(this);
        }
    }
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
    public int GetValue()
    {
        return Value;
    }
}

[Serializable]
public class CoreStat : Stat
{
    public CoreStatName CoreName;
    public int Base;
    public int Bonus;
    public int Total;

    public void SetTotal(DNDStats self)
    {
        Total =  Base + Bonus;
    }
    public void SetValue(DNDStats self)
    {
        Value = CalculateValue(self);
    }
    public int CalculateValue(DNDStats self)
    {
        switch (Total)
        {
            case 0:
            case 1:
                return -5;
            case 2:
            case 3:
                return -4;
            case 4:
            case 5:
                return -3;
            case 6:
            case 7:
                return -2;
            case 8:
            case 9:
                return -1;
            case 10:
            case 11:
                return 0;
            case 12:
            case 13:
                return 1;
            case 14:
            case 15:
                return 2;
            case 16:
            case 17:
                return 3;
            case 18:
            case 19:
                return 4;
            case 20:
                return 5;
            default:
                return 0;
        }
    }
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

    public int SetValue(DNDStats self)
    {
        return self.GetCoreStatBonus(CoreStat);
    }
    public int GetProfBonus(DNDStats self)
    {
        switch (SkillProficiency)
        {
            case Proficiency.NotProficient: return 0;
            case Proficiency.HalfProficient:return Convert.ToInt32(MathF.Round(Convert.ToSingle(self.Proficiency.Value) / 2));
            case Proficiency.Proficient:    return self.Proficiency.Value;
            case Proficiency.Expertise:     return self.Proficiency.Value * 2;
            default: return 0;
        }
    }
    public void CalculateTotal(DNDStats self)
    {
        Value = self.GetCoreStatBonus(CoreStat) + GetProfBonus(self) + Bonus;
    }
}
