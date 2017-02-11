using System;
using System.IO;
using System.Net;
using System.Text;
using Crestron.SimplSharp; 
using Crestron.SimplSharp.Ssh;// For Basic SIMPL# Classes

namespace FTP_Test
{
    public class FTP
    {

        //Event Handlers
        //public event EventHandler<UshrtChangeEventArgs> UOutputChange;  // Event for Analog
        //public event EventHandler<StringChangeEventArgs> StringChange;  // Event for Serial

        /// <summary>
        /// SIMPL+ can only execute the default constructor. If you have variables that require initialization, please
        /// use an Initialize method
        /// </summary>
        public string _user = "anonymous";
        public string _pass = "";
        public string _host = "192.168.1.1";
        public string _filePath = "\\NVRAM\\Test\\";
        public ushort _debug = 0;
        public Int32 uget;

//Values S+ Modifies
        public string user
        {
            get { return _user; }
            set { _user = value; }
        }

        //Values S+ Modifies
        public string pass
        {
            get { return _pass; }
            set { _pass = value; }
        }
 
        //Values S+ Modifies
        public string host
        {
            get { return _host; }
            set { _host = value; }
        }

        public ushort debug                             // debug variable set from S+
        {
            get { return _debug; }
            set { _debug = value; }
        }

        public string filePath
        {
            get{ return _filePath;}
            set { _filePath = value; }
        }
            
        public void GetFile()
        {
            CrestronConsole.PrintLine("Trying FTP is connected to {0} with {1} / {2}", host,user,pass);
            //SftpClient sftp = new SftpClient(host,user,pass);
            CrestronFileTransferClient ftp1 = new CrestronFileTransferClient();
            ftp1.SetUserName(user);
            ftp1.SetPassword(pass);
            uget = ftp1.GetFile(string.Format("ftp://{0}:21/directory.cvs",host),_filePath);
            CrestronConsole.PrintLine("Get File {0} ", uget);
            if (uget == 1)
            {
                //uget = ftp1.GetLastClientError();
                CrestronConsole.PrintLine("Last Error get: {0}", ftp1.GetLastClientError());
            }

            //sftp.Connect();

            //if (sftp.IsConnected)
            //{
              //  CrestronConsole.PrintLine("SFTP is connected to {0}",host);
                //sftp.ChangeDirectory(path);
                //string dir = sftp.WorkingDirectory;
                //CrestronConsole.PrintLine("SFTP DIR is {0}", dir);
               /* if (sftp.Exists(file))
                {
                    string read = sftp.ReadAllText(file);
                    CrestronConsole.PrintLine("SFTP Read = {0}", read);
                }
                else
                {
                    CrestronConsole.PrintLine("Cannot find File ‐ {0}{1}", path, file);
                }*/
                //sftp.Disconnect();
            
        }
       
    }
}
