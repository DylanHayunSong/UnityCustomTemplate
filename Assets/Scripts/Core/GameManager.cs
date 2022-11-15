using System;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    private PageController pageController = null;
    public PageController PageController { get { return pageController; } }

    public Action<PageType> OnPageChangedAction;

    public void SetPageController(PageController controller)
    {
        pageController = controller;
    }

    private void BindDelegates()
    {

    }

    #region Initiallize

    private GameSetting gameSetting;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void CreateGameManagerGameObject()
    {
        GameObject gameManagerObj = new GameObject("GameManager");
        gameManagerObj.AddComponent<GameManager>();
    }

    protected override void Init()
    {
        InstantiateManager<ResourceCacheManager>();

        GetGameSetting();
        CreateManagers();
        AddComponents();

        BindDelegates();
    }

    private void GetGameSetting()
    {
        gameSetting = ResourceCacheManager.inst.GameSetting;
    }

    private void CreateManagers()
    {
        if (gameSetting.autoCreateManager)
        {
            foreach (var managerType in gameSetting.CreateManagers)
            {
                CreateManager(Type.GetType(managerType.ToString()));

                if (managerType == CreateManagerType.UDPCommManager)
                {
                    UDPCommManager.inst.SetManager(gameSetting.udpSendPorts, gameSetting.udpReceivePorts);
                }

                if (managerType == CreateManagerType.SerialCommManager)
                {
                    SerialCommManager.inst.SetManager(gameSetting.serialPort);
                }
            }

            foreach (var managerType in gameSetting.InstantiateManagers)
            {
                InstantiateManager(Type.GetType(managerType.ToString()));
            }
        }
        else
        {

        }
    }

    private void InstantiateManager<T>() where T : SingletonBehaviour<T>
    {
        GameObject gameObj = Instantiate(Resources.Load(typeof(T).ToString())) as GameObject;
        if (gameObj != null)
        {
            gameObj.transform.parent = inst.transform;
        }
    }

    private void InstantiateManager(Type type)
    {
        GameObject gameobj = Instantiate(Resources.Load(type.ToString())) as GameObject;
        if (gameobj != null)
        {
            gameobj.transform.parent = inst.transform;
        }
    }

    private void CreateManager(Type type)
    {
        if (type != null)
        {
            GameObject manager = new GameObject(type.ToString());
            manager.AddComponent(type);
            manager.transform.parent = inst.transform;
        }
    }

    private void AddComponents()
    {
    }

    #endregion
}
