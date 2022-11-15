using _4by4.Communicate;

public class IniCommManager : SingletonBehaviour<IniCommManager>
{
    private IniCommunicator iniComm = null;

    protected override void Init ()
    {
        iniComm = new IniCommunicator();
    }

    public string ReadData (string filePath, string section, string key)
    {
        return iniComm.ReadIni(filePath, section, key);
    }

    public void WriteData (string filepath, string section, string key, string value)
    {
        iniComm.WriteIni(filepath, section, key, value);
    }
}