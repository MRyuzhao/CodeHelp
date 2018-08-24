namespace CodeHelp.Common
{
    public interface ISettingsProvider<out T>
    {
        T GetConfig(string sectionName);
        T[] GetConfigs(string sectionName);
    }
}