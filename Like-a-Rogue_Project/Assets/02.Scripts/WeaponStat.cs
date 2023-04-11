using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data_Enum;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "WeaponStat", menuName = "Scriptable Object Asset/WeaponStat")]
public class WeaponStat : ScriptableObject
{
    [Header("Type")]
    public TypeOfWorld m_worldType = TypeOfWorld.NormalWorld;
    public TypeOfGrade m_weaponGrade = TypeOfGrade.Normal;
    public TypeOfWeapon m_weaponType = TypeOfWeapon.Sword;
    
    [Header("Attack Variable")]
    public float m_attackDamage = 0f;
    public float m_attackDelay = 0f;

    [Tooltip("석궁에 사용될 추가 변수. 석궁이 아니면 정의하지 말것.")] [Header("Crossbow Variable")]
    public int m_ammo;
    public int m_reloadDelay;
    
    [Header("Special Attack Variable")]
    public UnityEvent m_specialAttackMethod;
    public float m_specialAttackDamage = 0f;
    public float m_specialAttackDelay = 0f;
}

namespace Data_Enum
{
    public enum TypeOfWeapon
    {
        Sword = 0,
        GreatSword,
        Spear,
        Axe,
        ThrowingAxe,
        BattleAxe,
        Sickle,
        Bow,
        Crossbow,
        MagicStick,
        MagicBall,
        MagicBook
    }

    public enum TypeOfGrade
    {
        Normal = 0,
        Common,
        Rare,
        Epic,
        Legend
    }

    public enum TypeOfWorld
    {
        NormalWorld = 0,
        ReverseWorld
    }
}