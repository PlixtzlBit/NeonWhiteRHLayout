using MelonLoader;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Globalization;
using System.Reflection;
using System.Collections;

[assembly: MelonInfo(typeof(RHLayout), "RHLayout", "1.0.0", "PlixtzlBit")]

public class RHLayout : MelonMod
{
    private bool Set;

    public override void OnSceneWasLoaded(int BuildIndex, string SceneName)
    {
        if (SceneName == "Menu" && !Set){SetPositions();}
    }

    private void SetPositions()
    {
        try
        {
            string BaseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string DataDirectory = Path.Combine(BaseDirectory, "plixtzlbit-RHLayout");
            string ConfigPath = Path.Combine(DataDirectory, "Positions.config");
            if (!File.Exists(ConfigPath)){return;}

            string[] Lines = File.ReadAllLines(ConfigPath);
            if (Lines.Length < 3){return;}

            Vector3 Pos1, Pos2, Pos3;
            if (!ParsePos(Lines[0], out Pos1)){return;}
            if (!ParsePos(Lines[1], out Pos2)){return;}
            if (!ParsePos(Lines[2], out Pos3)){return;}

            GameObject A = GameObject.Find("Main Menu/Canvas/Ingame Menu/Menu Holder/Results Panel/Results Buttons");
            GameObject B = GameObject.Find("Main Menu/Canvas/Ingame Menu/Menu Holder/Results Panel/Leaderboards And LevelInfo");
            GameObject C = GameObject.Find("Main Menu/Canvas/Ingame Menu/Menu Holder/Results Panel/Leaderboards And LevelInfo/Leaderboards");
            if (A == null || B == null || C == null){return;}

            A.transform.localPosition = Pos1;
            B.transform.localPosition = Pos2;
            C.transform.localPosition = Pos3;
            Set = true;
        }
        catch{}
    }

    private bool ParsePos(string Line, out Vector3 Pos)
    {
        Pos = Vector3.zero;
        string[] P = Line.Split(',');
        if (P.Length != 3){return false;}

        float X, Y, Z;
        if (!float.TryParse(P[0], NumberStyles.Float, CultureInfo.InvariantCulture, out X)){return false;}
        if (!float.TryParse(P[1], NumberStyles.Float, CultureInfo.InvariantCulture, out Y)){return false;}
        if (!float.TryParse(P[2], NumberStyles.Float, CultureInfo.InvariantCulture, out Z)){return false;}
        Pos = new Vector3(X, Y, Z);
        return true;
    }
}
