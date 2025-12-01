using UnityEngine;
using UnityEngine.UI;
using TMPro;   // 如果你用 TextMeshPro

public class CharacterDetailPanel : MonoBehaviour
{
    public Transform modelParentTransform;    // 3D 模型的父物件
    public GameObject currentModel; // 當前顯示的模型

    // 由外部呼叫：把角色資料顯示到 UI
    public void CreateModel(CharacterData data)
    {
        if (data == null) return;
        // 先清掉舊模型
        if (currentModel != null)
        {
            Destroy(currentModel);
            currentModel = null;
        }

        // 建立新模型
        if (data.characterModel != null && modelParentTransform != null)
        {
            currentModel = Instantiate(data.characterModel, modelParentTransform);
        }
    }
}