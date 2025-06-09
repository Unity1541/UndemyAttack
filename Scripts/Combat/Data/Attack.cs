using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class Attack
{
    [field: SerializeField] public string attackName { get; private set; } //攻擊名稱
    [field: SerializeField] public float transitionDampTime { get; private set; } //攻擊ID
    [field: SerializeField] public int comboIndex { get; private set; } = -1; //攻擊持續時間
    [field: SerializeField] public float comboAttackTime { get; private set; } = -1; //攻擊持續時間

}
