using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Security.Principal;

namespace NAIHelper.Utils
{
    //public class Driver
    //{
    //    [DllImport("user32.dll")]
    //    [return: MarshalAs(UnmanagedType.Bool)]
    //    static extern bool SetForegroundWindow(IntPtr hWnd);

    //    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
    //    private static extern void MoveWindow(IntPtr hwnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

    //    [DllImport("user32.dll")]
    //    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    //    private ChromeDriverService Service { get; set; }
    //    private ChromeOptions Options { get; set; }
    //    public ChromeDriver Chrome { get; set; }
    //    public IJavaScriptExecutor JsExec { get; set; }
    //    public Actions Act { get; set; }
    //    public int Pid { get; set; }
    //    public IntPtr MainWindowHandle { get; set; }
    //    public bool OnWork { get; set; }

    //    public void Init()
    //    {
    //        Service                         = ChromeDriverService.CreateDefaultService();
    //        Service.HideCommandPromptWindow = true;
    //        Options                         = new ChromeOptions();
    //        Options.AddArgument($"user-data-dir=C:\\ChromeData");
    //        Chrome                          = new ChromeDriver(Service, Options);
    //        JsExec                          = Chrome;
    //        Act                             = new Actions(Chrome);


    //        var guid = Guid.NewGuid();
    //        Chrome.Navigate().GoToUrl("about:blank");
    //        JsExec.ExecuteScript($"document.title = '{guid}'");

    //        var counter = 0;
    //        while (counter++ < 1000)
    //        {
    //            var p = Process.GetProcessesByName("chrome").FirstOrDefault(x => x.MainWindowTitle.Contains(guid.ToString()));

    //            if (p == null) continue;

    //            Pid = p.Id;

    //            MainWindowHandle = Process.GetProcessById(Pid).MainWindowHandle;
    //            break;
    //        }

    //    }


    //    public IWebElement Wait(string xpath, int timeout = 10)
    //    {
    //        var name = "";//g.XPaths.FirstOrDefault(x => x.Value == xpath).Key;
    //        try
    //        {
    //            var wait = new WebDriverWait(Chrome, TimeSpan.FromSeconds(timeout));
    //            var el = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(xpath)));

    //            return el;
    //        }
    //        catch
    //        {
    //            return null;
    //        }
    //    }

    //    public IWebElement WaitExist(string xpath, int timeout = 10)
    //    {
    //        try
    //        {
    //            var wait = new WebDriverWait(Chrome, TimeSpan.FromSeconds(timeout));
    //            var el = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(xpath)));

    //            return el;
    //        }
    //        catch
    //        {
    //            return null;
    //        }
    //    }


    //    public bool IsBrowserClosed()
    //    {
    //        var isClosed = false;
    //        try
    //        {
    //            _ = Chrome.Title;
    //        }
    //        catch
    //        {
    //            isClosed = true;
    //        }

    //        return isClosed;
    //    }

    //    public void HideBrowser()
    //    {
    //        if (OnWork) return;
    //        if (Chrome == null) return;
    //        MoveWindow(MainWindowHandle, -32000, -32000, 1280, 720, true);
    //        SetWindowLong(MainWindowHandle, -16, 0x80000000);
    //    }

    //    public void ShowBrowser()
    //    {
    //        MoveWindow(MainWindowHandle, 0, 0, 1280, 720, true);
    //        SetWindowLong(MainWindowHandle, -16, 382664704);
    //        SetForegroundWindow(MainWindowHandle);
    //        Thread.Sleep(100);
    //        SetForegroundWindow(Process.GetCurrentProcess().MainWindowHandle);
    //    }

    //    public IEnumerable<Dictionary<string, object>> GetLogs()
    //    {
    //        return ((ReadOnlyCollection<object>)JsExec
    //                      .ExecuteScript("var performance = window.performance || window.mozPerformance || window.msPerformance || window.webkitPerformance || {}; var network = performance.getEntries() || {}; return network;"))
    //                     .Cast<Dictionary<string, object>>()
    //                     .Where(x => x.ContainsKey("initiatorType") && x["initiatorType"].ToString() == "xmlhttprequest");
    //    }
    //}
}
