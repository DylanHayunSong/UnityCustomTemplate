using System;
using System.Collections.Generic;
using UnityEngine;
using _4by4.Communicate;

public class SerialCommManager : SingletonBehaviour<SerialCommManager>
{
    [ReadOnly]
    [SerializeField]
    private string serialNum;

    public Action<string> OnDataReceived = null;

    private SerialCommunicator serialComm = null;
    private Queue<string> dataQueue = new Queue<string>();

    protected override void Init ()
    {
        
    }

    public void SetManager (string portNum)
    {
        serialNum = portNum;
        serialComm = new SerialCommunicator(portNum);
        serialComm.OnDataReceived += (data) => { dataQueue.Enqueue(data); };
        serialComm.Listen();
    }

    public void Send (string data)
    {
        serialComm.Write(data);
    }

    private void Update ()
    {
        QueueCheck();
    }
    private void QueueCheck ()
    {
        if(dataQueue.Count > 0)
        {
            if(OnDataReceived != null)
            {
                OnDataReceived.Invoke(dataQueue.Dequeue());
            }
        }
    }

    private void OnApplicationQuit ()
    {
        serialComm.Close();
    }
}