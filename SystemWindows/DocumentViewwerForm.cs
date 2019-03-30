using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemWindows
{
    public partial class DocumentViewwerForm : Form
    {
        public DocumentViewwerForm(string filePath)
        {
            InitializeComponent();
            this.FormClosed += DocumentViewwerForm_FormClosed;
            wbDocView.NavigateComplete2 += WbDocView_NavigateComplete2;
            FIX_REGIST();
            FileOpen(filePath);
        }
        private void FIX_REGIST()
        {
            //[HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Word.Document.8] "BrowserFlags"=dword:80000024 
            //[HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Word.RTF.8] "BrowserFlags"=dword:80000024 
            //[HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Word.Document.12] "BrowserFlags"=dword:80000024 
            //[HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Word.DocumentMacroEnabled.12] "BrowserFlags"=dword:80000024
            //[HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Excel.Sheet.8] "BrowserFlags"=dword:80000A00
            //[HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Excel.Sheet.12] "BrowserFlags"=dword:80000A00
            //[HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Excel.SheetMacroEnabled.12] "BrowserFlags"=dword:80000A00
            //[HKEY_LOCAL_MACHINE\SOFTWARE\Classes\Excel.SheetBinaryMacroEnabled.12] "BrowserFlags"=dword:80000A00
            //[HKEY_LOCAL_MACHINE\SOFTWARE\Classes\PowerPoint.Show.8] "BrowserFlags"=dword:800000A0
            //[HKEY_LOCAL_MACHINE\SOFTWARE\Classes\PowerPoint.Show.12] "BrowserFlags"=dword:800000A0
            //[HKEY_LOCAL_MACHINE\SOFTWARE\Classes\PowerPoint.ShowMacroEnabled.12] "BrowserFlags"=dword:800000A0
            //[HKEY_LOCAL_MACHINE\SOFTWARE\Classes\PowerPoint.SlideShow.8] "BrowserFlags"=dword:800000A0
            //[HKEY_LOCAL_MACHINE\SOFTWARE\Classes\PowerPoint.SlideShow.12] "BrowserFlags"=dword:800000A0
            //[HKEY_LOCAL_MACHINE\SOFTWARE\Classes\PowerPoint.SlideShowMacroEnabled.12] "BrowserFlags"=dword:800000A0
            //RegistryKey hklm = Registry.LocalMachine;
            //RegistryKey hkSoftware = hklm.OpenSubKey("SOFTWARE\\Classes\\Word.Document.8");
            //if(hkSoftware.GetValue("BrowserFlags").ToString()!= "80000024")
            //{
            //    Process.Start("FIX_REGIST.reg");
            //}

            //hkSoftware.SetValue("BrowserFlags", 0x80000024);
        }
        //dsoframer
        private void DocumentViewwerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            wbDocView.Navigate2(ref sBlankPage, ref refmissing, ref refmissing, ref refmissing, ref refmissing);
        }

        Object refmissing = Type.Missing;///System.Reflection.Missing.Value,oDocument = null;
        
        Object sBlankPage = "about:blank";
        private void DocumentViewwerForm_Load(object sender, EventArgs e)
        {
            //wbDocView.Navigate(@"E:\项目资料\报表系统\ProjectCode\文档管理系统.docx");
           
            
            
        }
        public void FileOpen(string filePath)
        {
            Object sFilePath = filePath;
            wbDocView.Navigate2(ref sFilePath, ref refmissing, ref refmissing, ref refmissing, ref refmissing);
        }
        private void WbDocView_NavigateComplete2(object sender, AxSHDocVw.DWebBrowserEvents2_NavigateComplete2Event e)
        {
            Microsoft.Office.Interop.Word.Application wordApp = e.pDisp.GetType().InvokeMember("Application", System.Reflection.BindingFlags.GetProperty, null, e.pDisp, null) as Microsoft.Office.Interop.Word.Application;
            Microsoft.Office.Interop.Word.Document doc = e.pDisp.GetType().InvokeMember("Document", System.Reflection.BindingFlags.GetProperty, null, e.pDisp, null) as Microsoft.Office.Interop.Word.Document;

        }
    }
}
