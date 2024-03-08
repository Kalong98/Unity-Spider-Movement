using UnityEngine;

public abstract class SpiderBaseState
{
    public abstract void EnterState(SpiderStateManager spider);

    public abstract void UpdateState(SpiderStateManager spider);
}
