using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
//using System.Collections.Concurrent;
namespace GeneralUtilty
{
    public interface IMsgProcessor<T>
    {
        void OnDataReceived(T obj);
    }
    
    public class ProducerConsumerQueue<T> : IDisposable
    {
        EventWaitHandle _wh = new AutoResetEvent(false);
        Thread _worker;
        readonly object _locker = new object();
        //ConcurrentQueue<T> _tasks = new ConcurrentQueue<T>();
        Queue<T> _tasks = new Queue<T>();

        bool flagProcessor = false;
        IMsgProcessor<T> host;
        public ProducerConsumerQueue(IMsgProcessor<T> obj)
        {
            host = obj;
            _worker = new Thread(Work);
            flagProcessor = true;
            _worker.Start();
        }

        public void EnqueueTask(T task)
        {
            lock (_locker) 
            _tasks.Enqueue(task);
            _wh.Set();
        }

        public void Dispose()
        {
            flagProcessor = false;
            // _worker.Join();         // Wait for the consumer's thread to finish.
            _wh.Close();            // Release any OS resources.
        }

        void Work()
        {
            while (flagProcessor)
            {
                T task;
                //lock (_locker)
                while (_tasks.Count > 0)
                {
                    task = _tasks.Dequeue();
                    host.OnDataReceived(task);
                    //if (_tasks.TryDequeue(out task))
                    //{
                    //    host.OnDataReceived(task);
                    //}
                }
                _wh.WaitOne();         // No more tasks - wait for a signal
            }
        }
        public int getQueueCount()
        {
            int i = 0;
            //lock (_locker)
            i = _tasks.Count;
            return i;
        }

    }
}
