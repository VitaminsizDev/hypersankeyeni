using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Eventler
{
    // Delegate tanımlaması
    public delegate void objeAc();

    // Delegate objesi oluşturmak
    public  static objeAc resetgame; 



    public static void resettgame()
    {
        resetgame?.Invoke();
      
    }
}
