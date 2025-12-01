using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CharacterSelectedEvent : UnityEvent<CharacterData> { }
//Unity 官方文件一直建議，如果你要在 Inspector 暴露 UnityEvent<T>：
//就自己定義一個繼承自 UnityEvent<T> 的類別，然後用這個類別當欄位型別
//public class MyIntEvent : UnityEvent<int> { }
//public MyIntEvent onSomething;，這樣比較不出錯誤
//整理
//public UnityEvent onClick;不帶有參數
//public UnityEvent<CharacterData> onClick;帶有參數，但 Inspector 會報錯
//public class CharacterDataEvent : UnityEvent<CharacterData> { }
//public CharacterDataEvent onClick; 帶有參數，Inspector 不會報錯

public class CharacterSelectionUI : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private CharacterDatabase database;

    [Header("Left List UI")]
    [SerializeField] private Transform slotParent;   // ScrollView 的 Content
    [SerializeField] private CharacterSlot slotPrefab;// Slot 預置體

    [Header("Detail Panel")]
    [SerializeField] private CharacterDetailPanel detailPanel;

    [Header("Events")]
    public CharacterSelectedEvent onCharacterSelected; 

    private CharacterData currentSelected;

    private void Start()
    {
        BuildList();
        // 場景載入時預設選第一個角色
        if (database != null && database.characters.Count > 0)
        {
            SelectCharacter(database.characters[0]);
           // GameObject characterModel = Instantiate(database.characters[0].characterModel, detailPanel.modelParentTransform);

           
        }
    }

    // 根據 Database 生成左邊所有 Slot
    private void BuildList()
    {
        if (database == null || slotParent == null || slotPrefab == null) return;

        // 先把舊有子物件清掉（保險）
        for (int i = slotParent.childCount - 1; i >= 0; i--)
        {
            Destroy(slotParent.GetChild(i).gameObject);
        }

        // 依序建立 Slot，並綁定事件
        foreach (var character in database.characters)
        {
            var slot = Instantiate(slotPrefab, slotParent);
            slot.Setup(character);

            // 點擊：正式選角
            slot.onClick.AddListener(HandleSlotClicked);

            // Hover：右側 Panel 即時預覽該角色
            slot.onHover.AddListener(HandleSlotHover);
        }
    }

    private void HandleSlotClicked(CharacterData data)
    {
        SelectCharacter(data);
    }

    // Hover 只更新右側 Panel，不改變 currentSelected（避免誤選）
    private void HandleSlotHover(CharacterData data)
    {
        if (detailPanel != null)
            detailPanel.CreateModel(data);
    }

    // 選擇角色：更新 Panel + 廣播事件
    private void SelectCharacter(CharacterData data)
    {
        currentSelected = data;

        if (detailPanel != null)
            detailPanel.CreateModel(data);

        if (onCharacterSelected != null)
            onCharacterSelected.Invoke(data);
    }

    // 可以綁在 UI「確認」按鈕 OnClick
    public void ConfirmSelection()
    {
        if (currentSelected == null) return;

        Debug.Log($"選擇角色：{currentSelected.displayName}");

        // 這裡可以：
        // 1. 把 currentSelected.id 存到 GameManager
        // 2. 切換到戰鬥場景
    }
}