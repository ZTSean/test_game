using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradationTimer
{
    private int id;
    private DateTime? upgradationStartTime;

    public void StartUpgradation()
    {
        upgradationStartTime = DateTime.Now;
    }

    public Boolean IsUpgradationEnd()
    {
        return upgradationStartTime.HasValue && DateTime.Now > upgradationStartTime.Value.AddMinutes(Constant.UPGRADATION_DURATION_IN_MINS);
    }
}
