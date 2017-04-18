using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBTransferMonitor
{

    /// <summary>
    class Program
    {

        public static bool RunOnlyOne(string processName)
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            Console.WriteLine(">>{0} current.ProcessName:{1}", DateTime.Now, current.ProcessName);
            foreach (System.Diagnostics.Process process in processes) //查找相同名称的进程 
            {
                if (process.Id != current.Id)
                {
                    //确认相同进程的程序运行位置是否一样. 
                    if (System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("/", @"/") == current.MainModule.FileName)
                    {
                        return false;
                    }
                }
                else
                {
                    //忽略当前进程 
                }
            }
            return true;
        }

        static private bool GetProcessByName(string processName)
        {
            Process[] app = Process.GetProcessesByName(processName);
            if (app.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static private void KillProcess(string processName)
        {
            Process[] myproc = Process.GetProcesses();
            foreach (Process item in myproc)
            {
                if (item.ProcessName == processName)
                {
                    item.Kill();
                }
            }
        }

        static private void StartProcess(string processName)
        {
            try
            {
                System.Diagnostics.Process.Start(processName);
            }
            catch (Exception)
            {
                Console.WriteLine(">>注意！当前目录下未找到:{0}文件,\n>>请将程序放到正确目录下！", processName);
            }
        }

        static void Main(string[] args)
        {
            string strPythonProcess = "main.py";

            if (!RunOnlyOne(strPythonProcess))
            {
                MessageBox.Show("已经有一个实例在运行!", "注意!", MessageBoxButtons.OK,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }
            Console.WriteLine("*****************************************************************");
            Console.WriteLine("***********             MonitorRun              *****************");
            Console.WriteLine("*****************************************************************");
            int iflagDay = 0;
            string strFlagTime = "04:04";
            TimeSpan tsflagTime = DateTime.Parse(strFlagTime).TimeOfDay;
            Console.WriteLine(">>重启时间为每天{0}", tsflagTime);
            while (true)
            {
                DateTime dtNow = DateTime.Now;
                TimeSpan tsNowTime = dtNow.TimeOfDay;
                int iNowDay = dtNow.Day;
                if (tsflagTime < tsNowTime)
                {
                    if (iNowDay != iflagDay)
                    {
                        KillProcess("python");
                        Thread.Sleep(2000);
                        StartProcess(strPythonProcess);
                        Thread.Sleep(1000);
                        if (GetProcessByName("python"))
                        {
                            Console.WriteLine(">>{0} 成功启动{1}",dtNow, tsflagTime);
                        }
                        iflagDay = iNowDay;
                    }
                }
                Thread.Sleep(1000);
            }
        }
    }
}
