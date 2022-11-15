using System.Runtime.InteropServices;
using System.Text;


namespace _4by4.Communicate
{
    public class IniCommunicator
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString (
            string Section, string Key, string Value, string FilePath
        );
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString (
            string Section, string Key, string Defalt, StringBuilder RetVal, int Size, string FilePath
        );

        public string ReadIni (string filePath, string section, string key)
        {
            var value = new StringBuilder(255);
            GetPrivateProfileString(section, key, "Error", value, 255, filePath);
            return value.ToString();
        }

        public void WriteIni (string filePath, string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, filePath);
        }
    }
}