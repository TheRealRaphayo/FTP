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
        public string _host = "192.168.1.253";
        public string _port = "21";
        public string _folder = "";
        public string _filename = "test.txt";
        public string _filePath = "\\NVRAM";
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

        //Values S+ Modifies
        public string port
        {
            get { return _port; }
            set { _port = value; }
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

        public string folder
        {
            get { return _folder; }
            set { _folder = value; }
        }

        public string filename
        {
            get { return _filename; }
            set { _filename = value; }
        }
        
        public void GetFile()
        {
            //SftpClient sftp = new SftpClient(host,user,pass);
            CrestronFileTransferClient ftp1 = new CrestronFileTransferClient();
            ftp1.SetUserName(user);
            ftp1.SetPassword(pass);
            if (debug == 1) CrestronConsole.PrintLine("Trying FTP is connected to {0}:{1}{2}/{3} with user:{4}  pass:{5}", host, port,folder, filename, user, pass);
            uget = ftp1.GetFile(string.Format("ftp://{0}:{1}{2}/{3}", host,port, folder, filename), string.Format("{0}\\{1}", filePath, filename));
            if(debug==1) CrestronConsole.PrintLine("Get File {0} ", uget);
            if (uget == 1)
            {
                if(debug == 1) CrestronConsole.PrintLine("Last Error get: {0}", ftp1.GetLastClientError());
            }
            else if (debug == 1) CrestronConsole.PrintLine("Get File {0} from {1}/ folder", filename, folder);

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
        public void GetSFile()
        {
            SftpClient sftp = new SftpClient(host,Convert.ToInt16(port),user,pass);

            try
            {
                sftp.Connect();
                
                bool connected = sftp.IsConnected;
                CrestronConsole.PrintLine("SFTP is connected :{0}", connected);
                if (connected)
                {
                    CrestronConsole.PrintLine("SFTP is connected to {0}", host);
                    sftp.ChangeDirectory(folder);
                    string dir = sftp.WorkingDirectory;
                    CrestronConsole.PrintLine("SFTP DIR is {0}", dir);
                    if (sftp.Exists(filename))
                    {
                        string read = sftp.ReadAllText(filename);
                        CrestronConsole.PrintLine("SFTP Read = {0}", read);
                    }
                    else
                    {
                        CrestronConsole.PrintLine("Cannot find File ‐ {0}{1}", folder, filename);
                    }
                    sftp.Disconnect();
                }
                else CrestronConsole.PrintLine("SFTP can't connect to {0}", host);
            }
            catch (Crestron.SimplSharp.Ssh.Common.SshException e)
            {
                CrestronConsole.PrintLine("#######SFTP exception {0}", e);
            }


        }

        public void PutFile()
        {
            //SftpClient sftp = new SftpClient(host,user,pass);
            CrestronFileTransferClient ftp1 = new CrestronFileTransferClient();
            ftp1.SetUserName(user);
            ftp1.SetPassword(pass);
            if (debug == 1) CrestronConsole.PrintLine("Trying FTP is connected to {0}:{1}{2}/{3} with user:{4}  pass:{5}", host, port, folder, filename, user, pass);
            uget = ftp1.PutFile(string.Format("ftp://{0}:{1}{2}/{3}", host, port,folder, filename), string.Format("{0}\\{1}", filePath, filename));
            if(debug == 1) CrestronConsole.PrintLine("Put File {0} ", uget);
            if (uget == 1)
            {
                //uget = ftp1.GetLastClientError();
                if(debug == 1) CrestronConsole.PrintLine("Last Error get: {0}", ftp1.GetLastClientError());
            }
            else if (debug == 1) CrestronConsole.PrintLine("Put File {0} to {1}/ folder", filename, folder);
            //else ftp1.Close();

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
        public void info()
        {
            CrestronConsole.PrintLine("Application Number : {0}", InitialParametersClass.ApplicationNumber);
            CrestronConsole.PrintLine("Controller Number : {0}", InitialParametersClass.ControllerPromptName);
            CrestronConsole.PrintLine("Firmware Number : {0}", InitialParametersClass.FirmwareVersion);
            CrestronConsole.PrintLine("Program Number : {0}", InitialParametersClass.ProgramDirectory);
            CrestronConsole.PrintLine("Program ID : {0}", InitialParametersClass.ProgramIDTag);
        }

       
    }


}
