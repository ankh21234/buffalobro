using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using Buffalo.Kernel;

namespace UpdateHelper
{
    public class CopyPath
    {
        private string _sourcePath;
        /// <summary>
        /// 源目录
        /// </summary>
        public string SourcePath
        {
            get { return _sourcePath; }
            set { _sourcePath = value; }
        }

        private string _targetPath;
        /// <summary>
        /// 目标目录
        /// </summary>
        public string TargetPath
        {
            get { return _targetPath; }
            set { _targetPath = value; }
        }

        private static string[] _allowFile ={".dll",".xml"};

        /// <summary>
        /// 拷贝
        /// </summary>
        /// <returns></returns>
        public int DoCopy() 
        {
            string sPath=_basePath+"\\"+SourcePath;
            string tPath = _basePath + "\\" + TargetPath;
            DirectoryInfo tinfo = new DirectoryInfo(tPath.TrimEnd('\\'));
            DirectoryInfo sinfo = new DirectoryInfo(sPath.TrimEnd('\\'));
            
            int totle = CopyDirectory(sinfo.FullName, tinfo.FullName);
            return totle;
        }

        /// <summary>
        /// 递归拷贝文件夹
        /// </summary>
        /// <param name="sPath">源路径</param>
        /// <param name="tPath">目标</param>
        /// <returns></returns>
        private int CopyDirectory(string sPath, string tPath) 
        {
            
            int totle=CopyFiles(sPath, tPath);
            string[] childPaths = Directory.GetDirectories(sPath);
            foreach (string child in childPaths) 
            {
                string childPart = child.Replace(sPath, "").Trim('\\');
                string csPath = sPath + "\\" + childPart;
                string tsPath = tPath + "\\" + childPart;
                totle += CopyDirectory(csPath, tsPath);
            }
            return totle;
        }
        /// <summary>
        /// 拷贝
        /// </summary>
        /// <returns></returns>
        private int CopyFiles(string sPath, string tPath) 
        {
            if (!Directory.Exists(tPath))
            {
                Directory.CreateDirectory(tPath);
            }
            sPath = sPath.TrimEnd('\\');
            tPath = tPath.TrimEnd('\\');
            string[] files= Directory.GetFiles(sPath);
            int totle = 0;
            foreach (string file in files)
            {
                FileInfo fInfo = new FileInfo(file);
                if (!fInfo.Exists)
                {
                    continue;
                }

                string extension = fInfo.Extension;
                bool isAllow = false;
                foreach (string allow in _allowFile)
                {
                    if (extension.Equals(allow, StringComparison.CurrentCultureIgnoreCase))
                    {
                        isAllow = true;
                        break;
                    }

                }

                if (!isAllow)
                {
                    continue;
                }
                try
                {
                    CommonMethods.CopyNewer(file, tPath + "\\" + fInfo.Name);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                totle++;
            }
            return totle;
        }

        static string _basePath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');

        /// <summary>
        /// 获取需要拷贝的路径
        /// </summary>
        /// <returns></returns>
        public static List<CopyPath> GetPath() 
        {
            string infoFile = _basePath + "\\UpdateConfig.xml";
            if (!File.Exists(infoFile))
            {
                return null;
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(infoFile);
            XmlNodeList addinNodes = doc.GetElementsByTagName("Path");
            List<CopyPath> lstAddIn = new List<CopyPath>();
            foreach (XmlNode node in addinNodes)
            {
                CopyPath info = new CopyPath();
                XmlAttribute att = node.Attributes["source"];
                if (att != null)
                {
                    info._sourcePath = att.InnerText;
                }
                att = node.Attributes["target"];
                if (att != null)
                {
                    info._targetPath = att.InnerText;
                }
                lstAddIn.Add(info);
            }
            return lstAddIn;
        }
    }
}
