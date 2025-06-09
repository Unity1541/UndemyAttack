using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    //別忘記要實作State的抽象方法
    //如果不實作的話，那就把自身變成抽象類別
    //如果一個抽象類別（abstract class）去繼承另一個抽象類別，
    //它本身不需要立刻實作（override）父類別裡所有的抽象方法。
    //只有當某個類別不再是抽象的（也就是一般的 concrete class），才必須把所有從父類別（或祖先類別）繼承來的抽象方法都實作完，否則編譯會錯誤。
    protected PlayerStateMachine stateMachine;//沒辦法直接拖曳到inspector，只能用方法傳遞參數
    public PlayerBaseState(PlayerStateMachine stateMachine)
    //this is a constructor, means it will be called when the class is instantiated
    {
        this.stateMachine = stateMachine;
    }
    //如果你要讓最上層的State也能知道 stateMachine，
<<<<<<< Updated upstream
    // 你就得在PlayerBaseState 的 : base(...) 呼叫中傳遞過去。   
=======
    // 你就得在PlayerBaseState 的 : base(...) 呼叫中傳遞過去。 

    protected void Move(float deltaTime)
    { 
        MovmentWithGravity(Vector3.zero, deltaTime);
    }
    protected void MovmentWithGravity(Vector3 movement, float deltaTime)//讓人物移動同時考慮重力
    {
        //這個方法是用來處理角色的移動和重力
        //這樣就可以在子類別中使用了
        stateMachine.characterController.Move((movement + stateMachine.forceReceiver.movmmentY) * deltaTime);
        //這裡的movement是指角色的移動速度，deltaTime是指每幀的時間
        //這樣就可以讓角色在每幀都能夠移動
    }
    protected void FaceTarget()//在鎖定目標的時候，人物只能面對目標，因此y方向上下=0.0
    {
        if (stateMachine.targeter.currentTargeter == null) { return; }
        //如果沒有目標，就不需要面對目標
        Vector3 targetPosition = stateMachine.targeter.currentTargeter.transform.position - stateMachine.transform.position;
        //取得目標的位置和主角自身位置兩人相減的向量方向
        targetPosition.y = 0.0f; //忽略y方向的高度差
        if (targetPosition.sqrMagnitude > 0.001f) //如果目標位置和主角位置的距離大於0.001f，就需要面對目標
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition);
            stateMachine.transform.rotation = targetRotation;
            //stateMachine.transform.rotation = Quaternion.Slerp(stateMachine.transform.rotation, targetRotation, stateMachine.rotationSpeed * Time.deltaTime);
            //Quaternion.Slerp是用來平滑地旋轉角色到目標方向
            //這樣就可以讓角色在鎖定目標時，平滑地面對目標
        }

    }
>>>>>>> Stashed changes
}
