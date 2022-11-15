using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : ScriptableObject
{
    [SplitLine]
    [Header("Select Managers")]
    [Space(15)]
    [SerializeField]
    private bool CreateManagersAutomatically;
    public bool autoCreateManager { get { return CreateManagersAutomatically; } }

    [Space(10)]
    [SerializeField]
    [Tooltip("Add managers for create.")]
    private CreateManagerType[] createManagers;

    [SerializeField]
    [Tooltip("Add managers for instantiate.\nManager instance must be placed at \"Resources\" folder.")]
    private InstantiateManagerType[] instantiateManagers;


    public CreateManagerType[] CreateManagers { get { return createManagers; } }

    public InstantiateManagerType[] InstantiateManagers { get { return instantiateManagers; } }

    [SplitLine]
    [Header("Communicate")]
    [Space(15)]
    [SerializeField]
    private string defaultSerialPort;
    [Space(10)]
    [SerializeField]
    private int[] defaultUdpReceivePorts;
    [SerializeField]
    private int[] defaultUdpSendPorts;

    public string serialPort { get { return defaultSerialPort; } }
    public int[] udpReceivePorts { get { return defaultUdpReceivePorts; } }
    public int[] udpSendPorts { get { return defaultUdpSendPorts; } }
}
