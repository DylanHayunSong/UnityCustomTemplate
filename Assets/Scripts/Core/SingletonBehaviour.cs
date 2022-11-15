using UnityEngine;

public abstract class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
{
    public static T inst;

    protected virtual void Awake()
    {
        if (inst == null)
        {
            inst = (T)this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
        {
            Debug.LogError(string.Format("{0} is already exist.\n Destroy {0}.", name));
            Destroy(inst.gameObject);
        }
    }

    protected abstract void Init();
}
