using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MiniGame
{
    public void Play();
    public void Exit();

    public void gameStarted();

    public int MiniGameNumber();
}
