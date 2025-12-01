using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "RPG/Character")]
public class CharacterData : ScriptableObject
{
    public string id;                 // 唯一識別碼（未來存檔/網路傳輸可用）
    public string displayName;        // 顯示名稱

    [TextArea]
    public string description;        // 角色介紹文字

    public Sprite portraitSmallSprite;      // 左側列表小頭像
    public Sprite portraitLargeSprite;
    public GameObject characterModel;      // 中央 Panel 大圖

    // Demo 用能力值，之後可自行擴充
    public int hp;
    public int atk;
    public int def;
}