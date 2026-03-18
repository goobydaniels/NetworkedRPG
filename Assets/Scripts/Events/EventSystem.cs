using UnityEngine;
using UnityEngine.Events;

public static class EventSystem
{
    // Battle events
    public static UnityAction OnBattleWin;
    public static void BattleWin() { OnBattleWin?.Invoke(); }
}
