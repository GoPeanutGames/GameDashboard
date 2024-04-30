using System;
using System.Security;

[Serializable]
public class SessionStartData
{
    public string signature;
    public string address;
    public string mode;
    public string timezone;
    public string startTime;
    public string gameName;
}

[Serializable]
public class SessionUpdateData
{
    public string signature;
    public string address;
    public string sessionId;
    public int score;
}

[Serializable]
public class SessionEndData
{
    public string signature;
    public string address;
    public string sessionId;
    public int score;
    public string status;
    public string endTime;
}

[Serializable]
public class SessionResponseData
{
    public string sessionId;
}