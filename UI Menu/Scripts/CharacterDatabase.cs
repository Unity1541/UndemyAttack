using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG/Character Database")]
public class CharacterDatabase : ScriptableObject
{
    // 這裡拖進多個 CharacterData，把它當「角色圖鑑」
    public List<CharacterData> characters = new List<CharacterData>();
}