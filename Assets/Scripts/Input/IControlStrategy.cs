using System;
using UnityEngine;

public interface IControlStrategy
{
    bool Left { get; }
    bool Right { get; }
    bool Up { get; }
    bool Down { get; }
    public void ManageInput(); 
}