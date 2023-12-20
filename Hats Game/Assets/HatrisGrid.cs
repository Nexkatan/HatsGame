using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatrisGrid : HexGrid
{
    public SaveLoadMenu SaveLoadMenu;


        
    private void Awake()
    {
        //cellCell = true;
        String path = "C:/Users/Gabe/AppData/LocalLow/DefaultCompany/Hats Game\\Hatris Map.map";
        SaveLoadMenu.Load(path);
    }
    
}
