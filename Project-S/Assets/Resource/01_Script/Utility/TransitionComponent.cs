using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public abstract class TransitionComponent : MonoBehaviour
{
    public System.Action onFinished;
    public abstract void Animate(bool isSkip);

    public abstract bool IsSkip();

    public abstract void Finish();

    public abstract bool IsPlaying();
}