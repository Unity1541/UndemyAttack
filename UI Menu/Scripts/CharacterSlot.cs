using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;   // 用來做 Hover 閃動動畫（請先安裝 DOTween）

[System.Serializable]
public class CharacterDataEvent : UnityEvent<CharacterData> { }
//本體就是 UnityEvent<CharacterData>
//給後面的onClick和onHover用的
//Invoke(character) 丟什麼物件，HandleSlotClicked(CharacterData data) 裡的 data 就會收到那個同一個物件實例。
//這樣就可以把「是哪個角色被點擊/被 Hover」的資訊傳遞出去
//這樣 CharacterSelectionUI 就可以知道使用者點擊或 Hover 到哪個角色了

public class CharacterSlot : MonoBehaviour,
    IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI")]
    [SerializeField] private Image portraitImage;    // 頭像 Image
    [SerializeField] private Image highlightFrame;   // 高亮外框（可選）

    [Header("Hover 顏色閃動 (DOTween)")]
    [SerializeField] private Color normalColor = new Color(1f, 1f, 1f, 0.3f);   // 原本淡色
    [SerializeField] private Color flashColor  = new Color(1f, 0.9f, 0.3f, 1f); // 閃動亮色
    [SerializeField] private float flashDuration = 0.15f; // 單次變色時間

    [Header("Data")]
    [SerializeField] private CharacterData character;//當前的角色資料，由CharacterSelectionUI.Setup()塞入

    [Header("Events")]
    public CharacterDataEvent onClick = new CharacterDataEvent();
    public CharacterDataEvent onHover = new CharacterDataEvent();

    // 由外部（Manager）呼叫，塞入對應角色資料
    public void Setup(CharacterData data)
    {
        character = data;

        if (portraitImage != null)
            portraitImage.sprite = data.portraitSmallSprite;

        if (highlightFrame != null)
        {
            highlightFrame.enabled = false;
            highlightFrame.color   = normalColor;   // 初始化成原本顏色
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (character == null) return;
        onClick.Invoke(character);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (character == null) return;

        if (highlightFrame != null)
        {
            highlightFrame.enabled = true;
            PlayHoverFlashColor();   // 🔸 改成顏色閃動
        }

        onHover.Invoke(character);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (highlightFrame != null)
        {
            highlightFrame.DOKill();               // 停止顏色 Tween
            highlightFrame.color   = normalColor;  // 還原顏色
            highlightFrame.enabled = false;
        }
    }

    // 🔸 邊框「顏色閃動」：在 normalColor 和 flashColor 之間來回
    private void PlayHoverFlashColor()
    {
        if (highlightFrame == null) return;

        highlightFrame.DOKill();
        highlightFrame.color = normalColor;

        // 顏色在 normal ↔ flash 之間 Yoyo 兩次
        highlightFrame
            .DOColor(flashColor, flashDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }
}