using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    //要加上Rigidbody才可以有Trigger的Enter or Exit效果，→所以把Rigidbody mass = 0
    public List<Target> targets = new List<Target>();
    //這個類別是用來儲存目標的列表，並且可以在場景中使用

    public Target currentTargeter { get; private set; }
    //這個屬性用來儲存當前選中的目標，並且只能在Targeter內部修改
    private void OnTriggerEnter(Collider other)
    {

        if (!other.TryGetComponent<Target>(out Target target))
        {
            //如果other沒有Target組件，則target為null
            return; //直接返回，避免後續代碼執行
        }
        targets.Add(target);

        //原先寫法
        // Target target = other.GetComponent<Target>();
        // if (target != null && !targets.Contains(target))
        // {
        //     targets.Add(target);    
        //     Debug.Log($"Target added: {target.name}");
        // }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target))
        {
            //如果other沒有Target組件，則target為null
            return; //直接返回，避免後續代碼執行
        }
        targets.Remove(target);
        Debug.Log($"Target removed: {target.name}");
        //使用TryGetComponent來獲取Target組件，這樣可以避免在沒有Target組件時出現錯誤，傳回來bool值
        //如果other有Target組件，則target不為null
        //如果other沒有Target組件，則target為null

        //原先寫法
        // Target target = other.GetComponent<Target>();
        // if (target != null && targets.Contains(target))
        // {
        //     targets.Remove(target);
        //     Debug.Log($"Target removed: {target.name}");
        // }
    }

    public bool SelectTarget()
    {
        if (targets.Count == 0)
        {
            //如果沒有目標，則返回false
            return false;
        }
        else
        {
            currentTargeter = targets[0];
            return true;
        }
    }

    public void CancelTarget()
    {
        //取消選擇目標
        currentTargeter = null;
        //清空當前選中的目標
    }
}
