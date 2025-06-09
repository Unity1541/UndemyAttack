using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    //Because StateMachine is a monoBehaviour, 
    //therefore this class is also a monoBehaviour with inheritance.
    //PlayerStateMachine只是負責轉換更新場景的腳色狀態

    PlayerFreeLookState playerFreeLookState;
    [field:SerializeField]public InputReader inputReader { get; private set; }
    //因為SerializeField是Unity的特性（attribute），
    //所以這個變數會在Inspector中顯示，並且可以被序列化，但是他不能序列property，因此要加上field:，這樣才能在Inspector中顯示
    //這個變數是用來存儲InputReader的實例，這樣可以在其他地方使用
    //任何人可以外部取得InputReader的實例，但不能修改它,只能在InputReader自身修改他
    //因為已經產生C#的InputSystem，因此這邊就不需要再加上PlayerInput元件了
    //InputReader是用來讀取玩家輸入的類別，這裡使用了InputSystem的Controls類別來處理輸入
    // 使用 屬性（property）或欄位共享引用（reference） 的主要好處之一 —— 
    // 你可以讓其他類別直接存取 InputReader 實例，不用每一個Class要用到InputReader都要GetCompoent要用到InputReader都要GetCompoent
    [field:SerializeField]public CharacterController characterController { get; private set; }
    [field:SerializeField]public Animator animator { get; private set; }
    [field: SerializeField] public float freeMoveSpeed { get; private set; }

    [field: SerializeField] public Targeter targeter { get; private set; }
<<<<<<< Updated upstream
=======
    [field: SerializeField] public ForceReceiver forceReceiver{ get; private set; }
    [field: SerializeField] public float movementWithTargetSpeed { get; private set; }
    [field: SerializeField] public Attack[] attacks { get; private set; }

>>>>>>> Stashed changes
    public Transform mainCameraTransform { get; private set; }
    void Start()
    {
        mainCameraTransform = Camera.main.transform;
        playerFreeLookState = new PlayerFreeLookState(this);
        SwitchState(playerFreeLookState);
    }
}
