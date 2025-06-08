using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class State
{  // With abstract:you have to implement the methods in derived classes, otherwise it will not compile.
   //也就是說，如果你有一個抽象類別，裡面有抽象方法，那麼任何繼承這個抽象類別的子類別都必須實現這些抽象方法。
   // Without abstract: you can have a base implementation in the base class, and derived classes can choose to override it or not.
   //也就是說如果沒有抽象類別，你可以在基類中有一個基本實現，而派生類可以選擇覆蓋它或不覆蓋它。
   //所以只是強迫問題，避免忘記繼承後，實現某些重要方法

    // This prevents accidentally forgetting to implement critical state behaviors
    // Without abstract, you might create a new state and forget to implement 
    public abstract void OnEnter();

    public abstract void Tick(float deltaTime);
    // why us deltaTime?
    // deltaTime is used to ensure that the state behaves consistently regardless of frame rate.


    public abstract void OnExit();
   
}
