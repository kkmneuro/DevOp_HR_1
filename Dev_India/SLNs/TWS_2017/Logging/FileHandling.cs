#define USE_LOCAL_TIME
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GeneralUtilty;
using System.Xml;
using System.Xml.Serialization;
//using Contract;

namespace Logging
{
    public class FileHandling
    {
        static Logger logger = null;
        static bool flag = false;

        static FileHandling()
        {
            logger = new Logger();
        }

        public void SetBool(bool val)
        {
            flag = val;
        }

        #region "     COMMENTED CODE    "
        //public static void WriteLog(string msg)
        //{
        //    //if (flag)
        //    //{
        //        logger.WriteLog(msg);
        //    //}
        //}
        //public static void WriteErrorLog(string msg)
        //{
        //    logger.WriteErrorLog(msg);
        //}
        //public static void WriteLogIn(string msg)
        //{
        //    //if (flag)
        //    //{
        //        logger.WriteLogIn(msg);
        //    //}
        //}
        //public static void WriteLogOut(string msg)
        //{
        //    //if (flag)
        //    //{
        //        logger.WriteLogOut(msg);
        //    //}
        //}

        #endregion "     COMMENTED CODE    "

        public static void Dispose()
        {
            logger.Dispose();
            GC.SuppressFinalize(logger);
            logger = null;
        }

        public static void WriteInOutLog(string msg)
        {
            logger.WriteLogInOut(msg);
        }

        public static void WriteAllLog(string msg)
        {
            if (msg == null)
                return;
            logger.WriteAllLog(msg);
        }

    }
    class Logger : IDisposable,IMsgProcessor<Message>
    {
        ProducerConsumerQueue<Message> queue = null;

        FileStream fsInput, fsOutput, fsError, fsInfo, fsInOut, fsAllLog, fsSetting;
        StreamWriter swInput, swOutput, swError, swInfo, swInOut, swAllLog;
        string strInput, strOutput, strError, strInfo,strInOut,strLog, strSetting, workingDir, strCounter;
        int cntInput, cntOutput, cntError, cntInfo, cntInOut,cntLog;

        LogSetting setting = null;
        FileSystemWatcher watcher;
        object lckSetting = new object();

        public Logger()
        {
            //try
            {
                queue = new ProducerConsumerQueue<Message>(this);

                watcher = new FileSystemWatcher();
                watcher.Path = System.Environment.CurrentDirectory;
                watcher.Filter = "*.xml";
                watcher.NotifyFilter = NotifyFilters.LastWrite;
                watcher.Changed += watcher_Changed;
                watcher.EnableRaisingEvents = true;
#if USE_LOCAL_TIME
                workingDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/BOAdminUI/Logs" + "/Log_" + DateTime.Now.ToString("dd_MM_yyyy");
#else
                workingDir = "./Log_" + DateTime.UtcNow.ToString("dd_MM_yyyy");
#endif

                strCounter =Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/BOAdminUI/Logs/Counter.txt";
                lock (lckSetting)
                    loadSetting();

                if (!File.Exists(strCounter))    //this if case by vijay
                {
                    Directory.CreateDirectory(workingDir);
                    cntInfo = 1;
                    cntError = 1;
                    cntInput = 1;
                    cntOutput = 1;
                    cntInOut = 1;
                    cntLog = 1;
                    writeCounter();
                }
                else
                {
                    if (Directory.Exists(workingDir) == true)
                    {
                        readCounter();
                    }
                    else
                    {
                        Directory.CreateDirectory(workingDir);
                        cntInfo = 1;
                        cntError = 1;
                        cntInput = 1;
                        cntOutput = 1;
                        cntInOut = 1;
                        cntLog = 1;
                        writeCounter();
                    }
                }
                //initErrorSetting();
                //initInfoSetting();
               // initInputSetting();
                //initOutputSetting();
                //initInOutSetting();
                initAllLogSetting();
                writeCounter();
            }
            //catch (Exception ex)
            {

            }
        }

        public void OnDataReceived(Message msg)
        {
            if (msg == null)
                return;
            //lock (lckSetting)
                switch (msg.type)
                {
                    //case LogType.INFO:
                    //        swInfo.WriteLine(msg.ToString());
                    //        Monitoring(fsInfo.Position, LogType.INFO);
                    //    break;
                    //case LogType.ERROR:
                    //        swError.WriteLine(msg.ToString());
                    //        Monitoring(fsError.Position, LogType.ERROR);
                    //    break;
                    //case LogType.IN:
                    //        swInput.WriteLine(msg.ToString());
                    //        Monitoring(fsInput.Position, LogType.IN);
                    //    break;
                    //case LogType.OUT:
                    //        swOutput.WriteLine(msg.ToString());
                    //        Monitoring(fsOutput.Position, LogType.OUT);
                    //        break;
                    case LogType.IN_OUT:
                        if (swInOut != null)
                        {
                            swInOut.WriteLine(msg.ToString());
                            Monitoring(fsInOut.Position, LogType.IN_OUT);
                        }
                            break;
                    case LogType.LOG:
                            swAllLog.AutoFlush = true;
                            swAllLog.WriteLine(msg.ToString());
                            Monitoring(fsAllLog.Position, LogType.LOG);
                        break;
                    default:
                        break;
                }
        }

        internal void WriteLog(string msg)
        {
            lock (lckSetting)
                if (setting.FlagInfo == true)
            {
#if USE_LOCAL_TIME
                Message message = new Message(LogType.INFO, DateTime.Now, msg);
#else
               Message message = new Message(LogType.INFO, DateTime.UtcNow, msg);
#endif
                queue.EnqueueTask(message);
            }
        }
        internal void WriteErrorLog(string msg)
        {
            lock (lckSetting)
                if (setting.FlagError == true)
            {
#if USE_LOCAL_TIME
                Message message = new Message(LogType.ERROR, DateTime.Now, msg);
#else
               Message message = new Message(LogType.ERROR, DateTime.UtcNow, msg);
#endif
                queue.EnqueueTask(message);
            }
        }
        internal void WriteLogIn(string msg)
        {
            lock (lckSetting)
                if (setting.FlagIn == true)
            {
#if USE_LOCAL_TIME
                Message message = new Message(LogType.IN, DateTime.Now, msg);
#else
               Message message = new Message(LogType.IN, DateTime.UtcNow, msg);
#endif
                queue.EnqueueTask(message);
            }
        }
        internal void WriteLogOut(string msg)
        {
            lock (lckSetting)
                if (setting.FlagOut == true)
            {
#if USE_LOCAL_TIME
                Message message = new Message(LogType.OUT, DateTime.Now, msg);
#else
               Message message = new Message(LogType.OUT, DateTime.UtcNow, msg);
#endif
                queue.EnqueueTask(message);
            }
        }


        internal void WriteLogInOut(string msg)
        {
            lock (lckSetting)
            {
                if (setting.FlagInOut== true)
                {
#if USE_LOCAL_TIME
                    Message message = new Message(LogType.IN_OUT, DateTime.Now, msg);
#else
               Message message = new Message(LogType.IN_OUT, DateTime.UtcNow, msg);
#endif
                    queue.EnqueueTask(message);
                }
            }
        }

        internal void WriteAllLog(string msg)
        {
            lock (lckSetting)
            {
                if (setting.FlagLog == true)
                {
#if USE_LOCAL_TIME
                    Message message = new Message(LogType.LOG, DateTime.Now, msg);
#else
               Message message = new Message(LogType.IN_OUT, DateTime.UtcNow, msg);
#endif
                    queue.EnqueueTask(message);
                }
            }
        }

        internal void StopWriting()
        {
            queue.Dispose();
        }

        private void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            lock(lckSetting)
                loadSetting();
        }
        private void loadSetting()
        {
            XmlSerializer deserialize = new XmlSerializer(typeof(LogSetting));
            initConfigSetting();
            
            fsSetting.Lock(0, fsSetting.Length);
            XmlTextReader reader2 = new XmlTextReader(fsSetting);
            if (deserialize.CanDeserialize(reader2) == true)
                setting = (LogSetting)deserialize.Deserialize(reader2);
            else
                throw new Exception("Unable to serialize xml file");
            fsSetting.Unlock(0, fsSetting.Length);
            reader2.Close();
        }
        private void initConfigSetting()
        {
            strSetting = "LogSetting.xml";
            fsSetting = new FileStream(strSetting, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }
        private void initInputSetting()
        {
            strInput = workingDir + "/Input_" + cntInput + ".txt";
            fsInput = new FileStream(strInput, FileMode.OpenOrCreate, FileAccess.Write);
            swInput = new StreamWriter(fsInput);
            swInput.AutoFlush = true;
        }
        private void initOutputSetting()
        {
            strOutput = workingDir + "/Output_" + cntOutput + ".txt";
            fsOutput = new FileStream(strOutput, FileMode.OpenOrCreate, FileAccess.Write);
            swOutput = new StreamWriter(fsOutput);
            swOutput.AutoFlush = true;
        }
        private void initErrorSetting()
        {
            strError = workingDir + "/Error_" + cntError + ".txt";
            fsError = new FileStream(strError, FileMode.OpenOrCreate, FileAccess.Write);
            swError = new StreamWriter(fsError);
            swError.AutoFlush = true;
        }
        private void initInfoSetting()
        {
            strInfo = workingDir + "/Info_" + cntInfo + ".txt";
            fsInfo = new FileStream(strInfo, FileMode.OpenOrCreate, FileAccess.Write);
            swInfo = new StreamWriter(fsInfo);
            swInfo.AutoFlush = true;
        }

        private void initInOutSetting()
        {
            strInOut = workingDir + "/InOut_" + cntInOut + ".txt";
            if (File.Exists(strInOut))
            {
                fsInOut = new FileStream(strInOut, FileMode.Append, FileAccess.Write,FileShare.ReadWrite);
            }
            else
            {
                fsInOut = new FileStream(strInOut, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
            }
            swInOut = new StreamWriter(fsInOut);
            swInOut.AutoFlush = true;
        }

        private void initAllLogSetting()
        {
            strLog = workingDir + "/Log_" + cntLog + ".txt";
            if (File.Exists(strLog))
            {
                fsAllLog = new FileStream(strLog, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            }
            else
            {
                fsAllLog = new FileStream(strLog, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
            }
            swAllLog = new StreamWriter(fsAllLog);
            swAllLog.AutoFlush = true;
        }
        
        /// <summary>
        /// 
        /// </summary>
        private void writeCounter()
        {
            FileStream fs = new FileStream(strCounter, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            //sw.WriteLine("cntInfo=" + cntInfo);
            //sw.WriteLine("cntError=" + cntError);
            //sw.WriteLine("cntInput=" + cntInput);
            //sw.WriteLine("cntOutput=" + cntOutput);
            sw.WriteLine("cntInOut=" + cntInOut);
            sw.WriteLine("cntLog=" + cntLog);
            sw.AutoFlush = true;
            sw.Close();
            fs.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        private void readCounter()
        {
            FileStream fs = new FileStream(strCounter, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            while (sr.EndOfStream == false)
            {
                string temp = sr.ReadLine();
                //if (temp.StartsWith("cntInfo="))
                //{
                //    int index = temp.IndexOf('=');
                //    cntInfo = Convert.ToInt32(temp.Substring(index + 1, temp.Length - index - 1))+1;
                //}
                //else if (temp.StartsWith("cntError="))
                //{
                //    int index = temp.IndexOf('=');
                //    cntError = Convert.ToInt32(temp.Substring(index + 1, temp.Length - index - 1))+1;
                //}
                //else if (temp.StartsWith("cntInput="))
                //{
                //    int index = temp.IndexOf('=');
                //    cntInput = Convert.ToInt32(temp.Substring(index + 1, temp.Length - index - 1))+1;
                //}
                //else if (temp.StartsWith("cntOutput="))
                //{
                //    int index = temp.IndexOf('=');
                //    cntOutput = Convert.ToInt32(temp.Substring(index + 1, temp.Length - index - 1))+1;
                //}
                if (temp.StartsWith("cntInOut="))
                {
                    int index = temp.IndexOf('=');
                    cntInOut = Convert.ToInt32(temp.Substring(index + 1, temp.Length - index - 1));//+ 1;
                }
                else if (temp.StartsWith("cntLog="))
                {
                    int index = temp.IndexOf('=');
                    cntLog = Convert.ToInt32(temp.Substring(index + 1, temp.Length - index - 1));//+ 1;
                }
            }
            sr.Close();
            fs.Close();
        }
        private void Monitoring(long position, LogType logType)
        {
#if USE_LOCAL_TIME
            string temp = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/BOAdminUI/Logs" + "/Log_" + DateTime.Now.ToString("dd_MM_yyyy");
#else
             string temp = "./Log_" + DateTime.UtcNow.ToString("dd_MM_yyyy");
#endif
            if (workingDir.Equals(temp) == false)
            {
                workingDir = temp;
                Directory.CreateDirectory(workingDir);
                cntInfo = 1;
                cntError = 1;
                //lock (lck)
                {
                    loadSetting();
                    //initErrorSetting();
                    //initInfoSetting();
                    //initOutputSetting();
                    //initInOutSetting();
                    initAllLogSetting();
                }
            }
            else
            {
                double size = (double)position / (1024 * 1024);
                double maxSize = setting.MAXSizeMb;
                //lock (lck)
                maxSize = setting.MAXSizeMb;
                if (size > maxSize)
                {
                    switch (logType)
                    {
                        case LogType.INFO:
                            cntInfo++;
                            //lock (lck)
                            //initInfoSetting();
                            break;
                        case LogType.ERROR:
                            cntError++;
                            //lock (lck)
                            //initErrorSetting();
                            break;
                        case LogType.IN:
                            cntInput++;
                            //lock (lck)
                           // initInputSetting();
                            break;
                        case LogType.OUT:
                            cntOutput++;
                            //lock (lck)
                            //initOutputSetting();
                            break;
                        case LogType.IN_OUT:
                            cntInOut++;
                            //lock (lck)
                            initInOutSetting();
                            break;
                        case LogType.LOG:
                            cntLog++;
                            //lock (lck)
                            initAllLogSetting();
                            break;
                        default:
                            break;
                    }


                }
            }
        }

        public void Dispose()
        {
            //try
            {
                queue.Dispose();
            }
            //catch (Exception ex)
            //{

            //}
        }
    }
}
