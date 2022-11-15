using _4by4.Communicate;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UDPCommManager : SingletonBehaviour<UDPCommManager>
{
    [ReadOnly]
    [SerializeField]
    private int[] sendPorts;
    [ReadOnly]
    [SerializeField]
    private int[] receivePorts;

    public Action<string> OnMessageReceived = null;

    private UDPCommunicator udpComm;
    private Queue<string> messageQueue = new Queue<string>();

    protected override void Init ()
    {

    }

    public void SetManager (int[] sPorts, int[] rPorts)
    {
        sendPorts = sPorts;
        receivePorts = rPorts;

        udpComm = new UDPCommunicator(sendPorts, receivePorts);
        udpComm.OnMessageReceived += (message) => { messageQueue.Enqueue(message); };
    }

    private void Update ()
    {
        QueueCheck();
    }

    public void Send (int portNum, string msg)
    {
        udpComm.Send(portNum, msg);
    }

    private void QueueCheck ()
    {
        if(messageQueue.Count > 0)
        {
            if(OnMessageReceived != null)
            {
                OnMessageReceived.Invoke(messageQueue.Dequeue());
            }
        }
    }

    private void OnApplicationQuit ()
    {
        udpComm.ShutDown();
    }

}