public class LogConfig
{
    public LogConfig()//Default values
    {
        FileSizeLimitBytes = 1073741824;
        RetainedFileCountLimit = 10;
    }
    public string Port { get; set; }
    public string Server { get; set; }
    public string Path { get; set; }
    public string PathFormat { get; set; }
    public string ApplicationName { get; set; }
    public bool IsLogSessionInfo { get; set; }
    public int FileSizeLimitBytes { get; set; }
    public int RetainedFileCountLimit { get; set; }

}