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
    // 你就得在PlayerBaseState 的 : base(...) 呼叫中傳遞過去。   
}
