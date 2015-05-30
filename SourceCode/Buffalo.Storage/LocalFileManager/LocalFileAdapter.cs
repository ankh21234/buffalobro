using Buffalo.Kernel.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Collections;
using Buffalo.Kernel;

namespace Buffalo.Storage.LocalFileManager
{
    /// <summary>
    /// �����ļ�������
    /// </summary>
    public class LocalFileAdapter : IFileStorage
    {
        /// <summary>
        /// �洢��Ŀ¼
        /// </summary>
        private string _fileRoot;
        /// <summary>
        /// �û���
        /// </summary>
        private string _userName;

        /// <summary>
        /// ����
        /// </summary>
        private string _password;

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="fileName"></param>
        ///// <param name="userName"></param>
        ///// <param name="password"></param>
        //public LocalFileAdapter(string fileName, string userName, string password) 
        //{
        //    _fileRoot = fileName;
        //    _userName = userName;
        //    _password = password;
        //}

        public LocalFileAdapter(string connectString) 
        {

            Hashtable hs=ConnStrFilter.GetConnectInfo(connectString);
            _fileRoot = hs["root"] as string;
            if (!string.IsNullOrEmpty(_fileRoot)) 
            {
                _fileRoot = GetRealRoot(_fileRoot);
                if(!_fileRoot.EndsWith("\\"))
                {
                    _fileRoot=_fileRoot+"\\";
                }
            }
            _userName = hs["user"] as string;
            _password = hs["pwd"] as string;
        }

        /// <summary>
        /// ��ȡ����·��
        /// </summary>
        /// <param name="path">Ŀ¼</param>
        /// <returns></returns>
        private string GetLocal(string path) 
        {
            return _fileRoot + path;
        }

        /// <summary>
        /// ��ȡ��ʵ·��
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private string GetRealRoot(string root) 
        {
            char start=root[0];
            if (start=='.' || start=='\\' || start=='/') 
            {
                string mroot = CommonMethods.GetBaseRoot()+"\\" + root;
                DirectoryInfo dir = new DirectoryInfo(mroot);
                return dir.FullName;
            }
            return root;
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public void Open() 
        {
            if (string.IsNullOrEmpty(_userName)) 
            {
                return;
            }
            uint resault=FileAPI.WNetAddConnection(_userName, _password, _fileRoot, null);
            
        }

        /// <summary>
        /// �ر�����
        /// </summary>
        public void Close() 
        {
            if (string.IsNullOrEmpty(_userName))
            {
                return;
            }
            uint resault = FileAPI.WNetCancelConnection(_fileRoot, 1, true); //ȡ��ӳ��
            
        }

        /// <summary>
        /// ��ȡ�ļ���
        /// </summary>
        /// <param name="path">·��</param>
        /// <param name="postion"></param>
        /// <returns></returns>
        public Stream GetFile(string path, long postion) 
        {
            FileStream fs = new FileStream(GetLocal(path), FileMode.Open);
            if (postion > 0) 
            {
                fs.Position = postion;
            }
            return fs;
        }
        /// <summary>
        /// ��ȡ�ļ���
        /// </summary>
        /// <param name="path">·��</param>
        /// <param name="postion"></param>
        /// <returns></returns>
        public Stream GetFile(string path)
        {
            return GetFile(path,0);
        }

        /// <summary>
        /// ����ļ���
        /// </summary>
        /// <param name="path"></param>
        private void CheckDirectory(string path) 
        {
            //string curPath = GetLocal(path);
            FileInfo finf = new FileInfo(path);
            DirectoryInfo info = new DirectoryInfo(finf.DirectoryName);
            if (!info.Exists) 
            {
                info.Create();
            }
        }

        /// <summary>
        /// ׷���ļ�
        /// </summary>
        /// <param name="path">·��</param>
        /// <param name="content">�ļ�����</param>
        /// <param name="postion">д����ʼλ��</param>
        /// <returns></returns>
        public bool AppendFile(string path, byte[] content, long postion) 
        {
            string curpath=GetLocal(path);
            CheckDirectory(curpath);
            using (FileStream file = new FileStream(curpath, FileMode.Open, FileAccess.Write))
            {
                //file.Seek(postion, SeekOrigin.End);
                file.Position = postion;
                file.Write(content, 0, content.Length);
            }
            return true;
        }
        /// <summary>
        /// ׷�ӵ��ļ�ĩβ
        /// </summary>
        /// <param name="path">·��</param>
        /// <param name="content">�ļ�����</param>
        /// <returns></returns>
        public bool AppendFile(string path, byte[] content)
        {
            string curpath = GetLocal(path);
            CheckDirectory(curpath);
            using (FileStream file = new FileStream(curpath, FileMode.Append, FileAccess.Write))
            {
                //file.Seek(postion, SeekOrigin.End);
                
                file.Write(content, 0, content.Length);
            }
            return true;
        }
        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <param name="path">·��</param>
        /// <param name="content">�ļ�����</param>
        /// <returns></returns>
        public bool SaveFile(string path, byte[] content)
        {
            string curpath = GetLocal(path);
            CheckDirectory(curpath);
            using (FileStream file = new FileStream(curpath, FileMode.Create, FileAccess.Write))
            {
                //file.Seek(postion, SeekOrigin.End);
                file.Write(content, 0, content.Length);
            }
            return true;
        }

        /// <summary>
        /// ɾ���ļ�
        /// </summary>
        /// <param name="path">·��</param>
        public void RemoveFile(string path) 
        {
            File.Delete(GetLocal(path));
        }

        /// <summary>
        /// �������ļ�
        /// </summary>
        /// <param name="source">ԭ�ļ�·��</param>
        /// <param name="target">Ŀ���ļ�·��</param>
        public void ReNameFile(string source, string target) 
        {
            string tpath = GetLocal(target);
            CheckDirectory(tpath);
            File.Move(GetLocal(source), tpath);
        }

        /// <summary>
        /// ��ȡ�����ļ�
        /// </summary>
        /// <param name="path">�ļ���</param>
        /// <param name="searchOption">����ѡ��</param>
        /// <returns></returns>
        public List<string> GetFiles(string path, SearchOption searchOption) 
        {
            string sfilePath = GetLocal(path);

            string[] files = Directory.GetFiles(sfilePath, "*.*", searchOption);
            List<string> ret = new List<string>(files.Length);
            foreach (string spath in files) 
            {
                string curPath = spath.Substring(_fileRoot.Length);
                if (curPath[0] != '\\') 
                {
                    curPath = '\\' + curPath;
                }
                ret.Add(curPath);
            }
            return ret;
        }
        /// <summary>
        /// ��ȡ�����ļ���
        /// </summary>
        /// <param name="path">�ļ���</param>
        /// <param name="searchOption">����ѡ��</param>
        /// <returns></returns>
        public List<string> GetDirectories(string path, SearchOption searchOption)
        {
            string sfilePath = GetLocal(path);
            string[] files = Directory.GetDirectories(sfilePath, "*", searchOption);
            List<string> ret = new List<string>(files.Length);
            foreach (string spath in files)
            {
                string curPath = spath.Substring(_fileRoot.Length);
                if (curPath[0] != '\\')
                {
                    curPath = '\\' + curPath;
                }
                ret.Add(curPath);
            }
            return ret;
        }


        #region IDisposable ��Ա

        public void Dispose()
        {
            Close();
        }

        #endregion
    }
}
