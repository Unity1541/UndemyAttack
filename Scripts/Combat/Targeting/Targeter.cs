using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup cinemachineTargetGroup;//因為要自動加入鎖定的目標給TargetGroup相機，所以要先取得控制權
    //要加上Rigidbody才可以有Trigger的Enter or Exit效果，→所以把Rigidbody mass = 0
    public List<Target> targets = new List<Target>();
    //這個類別是用來儲存目標的列表，並且可以在場景中使用

    public Target currentTargeter { get; private set; }
    //這個屬性用來儲存當前選中的目標，並且只能在Targeter內部修改
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        //獲取主相機，這樣就可以在其他地方使用  
    }
    private void OnTriggerEnter(Collider other)
    {

        if (!other.TryGetComponent<Target>(out Target target))
        {
            //如果other沒有Target組件，則target為null
            return; //直接返回，避免後續代碼執行
        }
        targets.Add(target);
        target.OnDestroyed += RemoveTargetOnDestroyed;

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
        RemoveTargetOnDestroyed(target);
        targets.Remove(target);

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

        if (targets.Count == 0){ return false;}
            //如果沒有目標，則返回false   
        Target closestTargeter = null;
        float closestDistance = Mathf.Infinity;
        //不可以直接使用0，因為這樣會導致計算錯誤
        //初始化最近的目標和距離      
        foreach (Target target in targets)
        {
            Vector2 viewPo = mainCamera.WorldToViewportPoint(target.transform.position);
            if (viewPo.x < 0 || viewPo.x > 1 || viewPo.y < 0 || viewPo.y > 1)
            {
                //如果目標不在相機視野內，則跳過這個目標，表示主角沒看到這個目標
                continue;
            }

            Vector2 poCenter = viewPo - new Vector2(0.5f, 0.5f);
            //計算目標在相機視野中的位置，相對於視野中心點(0.5, 0.5)
            if(poCenter.sqrMagnitude < closestDistance)
            {
                //如果目標距離視野中心點的平方距離小於目前最近的距離
                closestDistance = poCenter.sqrMagnitude;
                closestTargeter = target;
                //更新最近的目標和距離
            }
        }
            if (closestTargeter == null){ return false; }//如果沒有找到最近的目標，則返回false
            currentTargeter = closestTargeter;
            cinemachineTargetGroup.AddMember(currentTargeter.transform, 1f, 0.5f);//1表示weight，0.5表示radius
            //將第一個目標加入CinemachineTargetGroup，這樣相機就會鎖定到這個目標
            return true;
        
    }

    public void CancelTarget()
    {
        if (currentTargeter == null)
        {
            //如果沒有選中的目標，則不需要取消
            return;
        }
        cinemachineTargetGroup.RemoveMember(currentTargeter.transform);
        //取消選擇目標
        currentTargeter = null;
        //清空當前選中的目標
    }

    public void RemoveTargetOnDestroyed(Target target)
    {
       
        if (currentTargeter == target)//假設被摧毀的目標物是當前追蹤的對象時候，那麼cinemachineTargetGroup也要移除這個目標
        {
            cinemachineTargetGroup.RemoveMember(target.transform);
            currentTargeter = null; //清空當前選中的目標
        }
        target.OnDestroyed -= RemoveTargetOnDestroyed;
       
    }
}
