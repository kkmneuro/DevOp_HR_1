﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using WebSocket4Net;

namespace NeuroXChange.Model
{
    public class WsHelperQuotes
    {

        const int LOGON_REQUEST = 1;
        const int LOGON_RESPONSE = 2;
        const int LOGOUT_REQUEST = 3;
        const int LOGOUT_RESPONSE = 32;
        const int QUOTES_STREAM = 23;
        const int SNAPSHOT_RESPONSE = 22;

        WebSocket websocket;
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        private string UserName;
        private string Password;
        private string webSocketHostUrl;
        private static WsHelperQuotes _instance;
        private static object _lockObject;
        private static Queue _queue = new Queue();
        public Queue _syncJSONQueue = Queue.Synchronized(_queue);
        string logPath = string.Empty;
        //public Dictionary<string, Dictionary<uint, DOM>> DicDOM;
        public Dictionary<string, string> _DDMessages = new Dictionary<string, string>();
        private List<int> lstAccount = new List<int>();
        private static Queue _queueRawFeed = new Queue();
        public Queue _syncQueueRawFeed = Queue.Synchronized(_queueRawFeed);
        private System.Timers.Timer timerSocket = new System.Timers.Timer();


        public event Action<string, string> OnLogonResponse = delegate { };
        public event Action<QuotesStream> OnQuoteResponse = delegate { };
        public event Action<SnapShot> OnQuoteSnapshotResponse = delegate { };


        public WsHelperQuotes()
        {
            try
            {
                timerSocket.Elapsed += timerSocket_Elapsed;
                timerSocket.Interval = 60000;

                //DicDOM = new Dictionary<string, Dictionary<uint, DOM>>();
                Thread _logThread = new Thread(ThreadHandleQueue);
                _logThread.IsBackground = true;
                _logThread.Start();
                Thread ThreadRawFeed = new Thread(ThreadHandleRawFeed);
                ThreadRawFeed.IsBackground = true;
                ThreadRawFeed.Start();
            }
            catch (Exception ex)
            {

                _syncJSONQueue.Enqueue("ERROR >> clsTWSDataManagerJSON >> " + ex.Message);
            }

        }

        private void ThreadHandleQueue()
        {
            while (true)
            {

                if (_syncJSONQueue.Count == 0)
                {
                    Thread.Sleep(10);
                    //this will deflate the queue to size 0..
                    _syncJSONQueue.TrimToSize();
                    continue;
                }
                if (_syncJSONQueue.Count > 0)
                {
                    string msg = (string)_syncJSONQueue.Dequeue();
                    log(msg);
                }
                Thread.Sleep(0);

            }
        }

        public static object LockObject
        {
            get
            {
                return _lockObject ?? (_lockObject = new object());
            }
        }

        private void log(string msg)
        {
            lock (LockObject)  // all other threads will wait for y
            {
                System.Diagnostics.Trace.WriteLine(msg);
            }
        }

        public  void Init(string username, string pwd, string hostUrl)
        {
            try
            {
                timerSocket.Enabled = true;
                if (websocket != null && websocket.State == WebSocketState.Open)
                {
                    //Logout();
                    websocket.Close();
                    websocket = null;
                }
                UserName = username;
                Password = pwd;
                webSocketHostUrl = hostUrl;
                websocket = new WebSocket(hostUrl.Trim());
                websocket.Opened -= websocket_Opened;
                websocket.Error -= websocket_Error;
                websocket.Closed -= websocket_Closed;
                websocket.MessageReceived -= websocket_MessageReceived;
                websocket.EnableAutoSendPing = false;
                websocket.Opened += websocket_Opened;
                websocket.Error += websocket_Error;
                websocket.Closed += websocket_Closed;
                websocket.MessageReceived += websocket_MessageReceived;
                websocket.Open();
            }
            catch (Exception ex)
            {
                // _syncJSONQueue.Enqueue("ERROR >> Init >> " + ex.Message);
                throw ex;
            }

        }

        internal void send(string request)
        {
            try
            {
                if (websocket.State == WebSocketState.Open)
                {
                    websocket.Send(request);                    
                    _syncJSONQueue.Enqueue("DIRECT SEND QUOTES :" + request + Environment.NewLine);
                }
                else
                {
                    _syncJSONQueue.Enqueue("DIRECT SEND QUOTES >> websocket is Closed");
                    reconnect();
                }
            }
            catch (Exception ex)
            {
                _syncJSONQueue.Enqueue("DIRECT SEND QUOTES ERROR >> " + ex.Message);
            }

        }


        void websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                _syncQueueRawFeed.Enqueue(e);
            }
            catch (Exception)
            {
            }
        }

        void websocket_Closed(object sender, EventArgs e)
        {
            try
            {
                _syncJSONQueue.Enqueue("CONNECTION CLOSED" + Environment.NewLine);
                reconnect();
            }
            catch (Exception ex)
            {
                _syncJSONQueue.Enqueue("ERROR >> websocket_Closed >> " + ex.Message);
            }

        }

        void reconnect()
        {
            try
            {
                if (websocket != null)
                {
                    if (websocket.State == WebSocketState.Open)
                    {
                        try
                        {
                            // Logout();
                            websocket.Close();
                            Thread.Sleep(3000);
                        }
                        catch (Exception)
                        {

                        }

                    }
                    websocket = null;
                }

                websocket = new WebSocket(webSocketHostUrl);
                websocket.Opened -= websocket_Opened;
                websocket.Error -= websocket_Error;
                websocket.Closed -= websocket_Closed;
                websocket.MessageReceived -= websocket_MessageReceived;
                websocket.EnableAutoSendPing = false;
                websocket.Opened += websocket_Opened;
                websocket.Error += websocket_Error;
                websocket.Closed += websocket_Closed;
                websocket.MessageReceived += websocket_MessageReceived;
                websocket.Open();

            }
            catch (Exception ex)
            {
                _syncJSONQueue.Enqueue("ERROR >> reconnect >> " + ex.Message);
            }
        }
        void websocket_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            try
            {
                _syncJSONQueue.Enqueue("websocket_Error >> " + e.Exception + Environment.NewLine);
                //OnOrderServerConnectionEvnt("DisConnected");
                if (websocket.State != WebSocketState.Open)
                {
                    reconnect();
                }
            }
            catch (Exception ex)
            {
                _syncJSONQueue.Enqueue("ERROR >> websocket_Error >> " + ex.Message);
            }

        }
        void timerSocket_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (websocket.State == WebSocketState.Closed)
            {
                try
                {
                    reconnect();
                }
                catch (Exception)
                {

                }
            }

        }
        void websocket_Opened(object sender, EventArgs e)
        {
            try
            {
                _syncJSONQueue.Enqueue("CONNECTION OPENED" + Environment.NewLine);
                userDetails objUser = new userDetails();
                objUser.UserName = UserName;
                objUser.Password = Password;
                objUser.SenderID = "2";// Cls.ClsGlobal.BrokerGroupId.ToString();
                objUser.Version = 1.15;
                objUser.msgtype = LOGON_REQUEST;

                var json = new JavaScriptSerializer().Serialize(objUser);
                websocket.Send(json);
                _syncJSONQueue.Enqueue("TO SERVER :" + json + Environment.NewLine);
            }
            catch (Exception ex)
            {
                _syncJSONQueue.Enqueue("ERROR >> websocket_Opened >> " + ex.Message);
            }

        }

        public static WsHelperQuotes INSTANCE
        {
            get { return _instance ?? (_instance = new WsHelperQuotes()); }
        }

        private void ManageRawFeed(MessageReceivedEventArgs e)
        {
            try
            {
                _syncJSONQueue.Enqueue("FROM SERVER :" + e.Message + Environment.NewLine);
                SocketResponce _socketResponce = serializer.Deserialize<SocketResponce>(e.Message);
                if (_socketResponce.msgtype == LOGON_RESPONSE)
                {
                    logonResponce collection = serializer.Deserialize<logonResponce>(e.Message);
                    OnLogonResponse(collection.Reason, collection.BrokerName);

                }
                else if (_socketResponce.msgtype == LOGOUT_RESPONSE)
                {
                    // LoggedInSuccess = false;
                }
                else if (_socketResponce.msgtype == QUOTES_STREAM)
                {
                    QuotesStream _stream = serializer.Deserialize<QuotesStream>(e.Message);
                    //UpdatePrice(_stream);
                    _syncJSONQueue.Enqueue("FROM SERVER : QUOTES STREAM FOUND");
                    OnQuoteResponse(_stream);

                }
                else if (_socketResponce.msgtype == SNAPSHOT_RESPONSE)
                {
                    SnapShot _snapshot = serializer.Deserialize<SnapShot>(e.Message);
                    _syncJSONQueue.Enqueue("FROM SERVER : SNAPSHOT STREAM FOUND");
                    OnQuoteSnapshotResponse(_snapshot);
                  
                }
            }
            catch (Exception ex)
            {
                _syncJSONQueue.Enqueue("Error at ManageRawFeed >> " + ex.Message);
            }
        }


        private void ThreadHandleRawFeed()
        {
            while (true)
            {
                try
                {
                    if (_syncQueueRawFeed.Count == 0)
                    {
                        Thread.Sleep(10);
                        //this will deflate the queue to size 0..
                        _syncQueueRawFeed.TrimToSize();
                        continue;
                    }
                    if (_syncQueueRawFeed.Count > 0)
                    {

                        MessageReceivedEventArgs _quote = (MessageReceivedEventArgs)_syncQueueRawFeed.Dequeue();
                        ManageRawFeed(_quote);
                    }
                    Thread.Sleep(0);

                }
                catch (Exception)
                {

                }
            }
        }


        public void Logout()
        {
            try
            {
                _syncJSONQueue.Enqueue("CONNECTION OPENED" + Environment.NewLine);
                LogoutRequest objUser = new LogoutRequest();
                objUser.UserName = UserName;
                objUser.msgtype = LOGOUT_REQUEST;

                var json = new JavaScriptSerializer().Serialize(objUser);
                websocket.Send(json);
                _syncJSONQueue.Enqueue("TO SERVER :" + json + Environment.NewLine);
            }
            catch (Exception ex)
            {
                _syncJSONQueue.Enqueue("ERROR >> Logout >> " + ex.Message);
            }
        }



    }

}
