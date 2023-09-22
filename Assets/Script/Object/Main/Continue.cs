using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour
{
    public void ContinueStage()
    {
        int stage = (int)GameManager.Inst.StageType;
        GameManager.Inst.ChangeStage(stage);
    }
}
