using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Eventler
{
    // Delegate tan�mlamas�
    public delegate void objeAc();

    // Delegate objesi olu�turmak
    public  static objeAc resetgame; 



    public static void resettgame()
    {
        resetgame?.Invoke();
      
    }
}
