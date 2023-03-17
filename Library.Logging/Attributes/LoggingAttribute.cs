
using Library.Logging.Entities;
using System;
using Serilog;
using System.Diagnostics;
using MethodBoundaryAspect.Fody.Attributes;
using System.Collections.Generic;
using Libary.Logging.Extensions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Libary.Logging.Containers;
using Libary.Logging.Enums;
using Libary.Logging.Utils;
using System.Reflection;
using System.Linq;
using System.Net;
using Libary.Common.Utils;
using Library.Logging.Consts;
using System.Reflection.Metadata;

public sealed class LoggedAttribute : OnMethodBoundaryAspect
{
    #region Contrructure
    public LoggedAttribute()
    {
        this.aspectType = AspectType.All;
    }
    public LoggedAttribute(AspectType aspectType = AspectType.All)
    {
        this.aspectType = aspectType;

    }
    public LoggedAttribute(AspectType aspectType = AspectType.All, FlowBehavior flowBehavior = FlowBehavior.Default, object returnType = null)
    {
        this.aspectType = aspectType;
        FlowBehavior = flowBehavior;
        this.returnType = returnType;
    }
    #endregion

    #region Fields
    object returnType;
    FlowBehavior FlowBehavior;
    AspectType aspectType;
    static Dictionary<string, int> errorCount = new Dictionary<string, int>();
    static LoggedAttribute()
    {
        errorCount.Add("Hata", 0);
        errorCount.Add("Uyarı", 0);
        errorCount.Add("Bilgilendirme", 0);
    }
    Type instanceType;
    #endregion

    #region AspectMethods
    public override void OnEntry(MethodExecutionArgs args)
    {
        args.FlowBehavior = FlowBehavior;
        AppSettings.CheckIsRegistered();
        var assambly = Assembly.GetEntryAssembly();
        string sourceContext = $"{assambly.GetName().Name}";

        instanceType = args.Instance?.GetType();
        if (aspectType == AspectType.All || aspectType == AspectType.OnEntry)
            try
            {
                LogContainer log = buildLogContainer(args, AspectEvent.OnEntry, MessgageType.Information);
                LoadHttpContextInfo(args, log);
                var logMessage = PrepareLogMessage(log, "Bilgilendirme");
                logMessage.Message = $"{log.ClassName}.{log.MethodName} Metodu #{log.ParameterValues}# parametreleri ile çalışmaya başladı.";
                WriteLog(args, logMessage, LogLevel.Information);
            }
            catch (Exception ex)
            {
                Console.Write(ex.GetAllMessages());
            }

    }
    private static void WriteLog(MethodExecutionArgs args, LogContainer logMessage, LogLevel logLevel = LogLevel.Information)
    {
        var assambly = Assembly.GetEntryAssembly(); ;
        var sourceContext = assambly.GetName().Name;
        var log = Serilog.Log.ForContext("SourceContext", sourceContext)
               .ForContext("MethodName", args.Method.Name)
               .ForContext("LoggingTime", logMessage.LogTime)
               .ForContext("MessageType", logMessage.MessageType)
               .ForContext("MessageNo", logMessage.MessageNo)
               .ForContext("AspectEvent", logMessage.AspectEvent)
               .ForContext("AssamblyName", logMessage.AssamblyName)
               .ForContext("ClassName", logMessage.ClassName)
               .ForContext("ParameterValues", logMessage.ParameterValues)
               .ForContext("UserInfo", logMessage.ContextInfo.UserInfo.ToJson())
               .ForContext("SessionInfo", logMessage.ContextInfo.SessionInfo)
               .ForContext("Host", logMessage.ContextInfo.Host)
               .ForContext("StatusCode", logMessage.ContextInfo.StatusCode)
               .ForContext("ReturnValueJson", logMessage.ReturnValueJson)
               .ForContext("ClientIP", logMessage.ContextInfo.ClientIp)
               .ForContext("RequestPath", logMessage.ContextInfo.RequestPath);
        switch (logLevel)
        {
            case LogLevel.Error:
                log.Error(logMessage.Message);
                break;
            case LogLevel.Warning:
                log.Warning(logMessage.Message);
                break;
            case LogLevel.Information:
                log.Information(logMessage.Message);
                break;
            default:
                break;
        }

    }
    static List<int> errorCodes = new List<int> { 400, 404, 405 };
    static List<int> succedCodes = new List<int> { 200 };
    static List<int> warningCodes = new List<int> { 401 };
    public override void OnExit(MethodExecutionArgs args)
    {
        AppSettings.CheckIsRegistered();
        if (aspectType == AspectType.All || aspectType == AspectType.OnExit)
            try
            {

                LogContainer log = buildLogContainer(args, AspectEvent.OnSuccess, MessgageType.Information);
                var logMessage = PrepareLogMessage(log, "Bilgilendirme");
                log.ReturnValueJson = args.ReturnValue.ToJson();
                logMessage.Message = $"{log.ClassName}.{log.MethodName} Metodu #{log.ParameterValues}# parametreleri ile {args.ReturnValue.ToJson()}'sonucunu dönderip çalışmasını başarı ile bitirdi.";
                LoadHttpContextInfo(args, log);
                var httpcontextLog = LoadHttpContextInfo(args, log);

                if (succedCodes.Contains(httpcontextLog.ContextInfo.StatusCode))
                {
                    WriteLog(args, logMessage, LogLevel.Information);
                }
                else if (warningCodes.Contains(httpcontextLog.ContextInfo.StatusCode))
                {
                    WriteLog(args, logMessage, LogLevel.Warning);
                }
                else if (errorCodes.Contains(httpcontextLog.ContextInfo.StatusCode))
                {
                    WriteLog(args, logMessage, LogLevel.Error);
                }
                else
                {
                    WriteLog(args, logMessage, LogLevel.Information);
                }
                var methodExecutionBase = ((MethodExecutionBase)args.ReturnValue);
                methodExecutionBase.Status = true;
                methodExecutionBase.StatusMessage = "Başarılı";
            }
            catch (Exception ex)
            {
                Console.Write(ex.GetAllMessages());
            }
    }
    public override void OnException(MethodExecutionArgs args)
    {
        AppSettings.CheckIsRegistered();
        if (aspectType == AspectType.All || aspectType == AspectType.OnException)
        {
            try
            {
                LogContainer log = buildLogContainer(args, AspectEvent.OnException, MessgageType.Error);
                LoadHttpContextInfo(args, log);
                var logMessage = PrepareLogMessage(log, "Bilgilendirme");
                WriteLog(args, logMessage, LogLevel.Error);
            }
            catch (Exception ex)
            {
                Console.Write(ex.GetAllMessages());
            }
        }
        if (FlowBehavior == FlowBehavior.Continue)
        {
            args.ReturnValue = new MethodExecutionBase() { Status = false, StatusMessage = "Hata oluştu" };
        }
    }

    #endregion

    #region Helpers

    private LogContainer buildLogContainer(MethodExecutionArgs args, AspectEvent aspectEvent, MessgageType messgageType = MessgageType.Information)
    {
        return new LogContainer
        {
            AssamblyName = instanceType?.Assembly.FullName,
            ClassName = instanceType?.Name,
            MethodName = args.Method.Name,
            ParameterValues = args.Arguments.ToJson(),
            LogTime = DateTime.Now,
            Message = args.Exception.GetAllMessages(),
            MessageType = messgageType.GetDescription(),
            AspectEvent = aspectEvent.GetDescription(),
        };
    }

    private LogContainer LoadHttpContextInfo(MethodExecutionArgs args, LogContainer log)
    {
        HttpContextInfo httpContextInfo = new HttpContextInfo();
        try
        {
            dynamic result = args.ReturnValue;
            httpContextInfo.StatusCode = result.Result.StatusCode;
        }
        catch (Exception ex)
        {
            Console.Write(ex.GetAllMessages());
        }
        if (args.Instance is PageModel)
        {

            PageModel pageModel = (PageModel)args.Instance;

            httpContextInfo.RequestPath = pageModel.Request.Path;
            if (AppSettings.IsLogSessionInfo == true)
            {
                try
                {
                    string session = "";
                    foreach (var item in pageModel.HttpContext.Session.Keys)
                    {
                        session += string.Concat("Key:", item, "  Value:", pageModel.HttpContext.Session.GetString(item)) + Environment.NewLine;
                    }
                    httpContextInfo.SessionInfo = session;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.GetAllMessages());
                    WriteSessionConfigurationLog(ex);
                }
            }
            httpContextInfo.ClientIp = NetworkUtils.GetClientIPAddress(pageModel.HttpContext);
            httpContextInfo.Host = pageModel.Request.Host.ToJson();
            httpContextInfo.UserInfo = pageModel.User?.Identity?.Name;
        }
        else if (args.Instance is Controller || args.Instance is ControllerBase)
        {

            ControllerBase controller = (ControllerBase)args.Instance;
            httpContextInfo.RequestPath = controller.Request.Path;
            if (AppSettings.IsLogSessionInfo == true)
            {
                try
                {
                    string session = "";
                    foreach (var item in controller.HttpContext.Session.Keys)
                    {
                        session += string.Concat("Key:", item, "  Value:", controller.HttpContext.Session.GetString(item)) + Environment.NewLine;
                    }
                    httpContextInfo.SessionInfo = session;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.GetAllMessages());
                    WriteSessionConfigurationLog(ex);
                }
            }
            httpContextInfo.ClientIp = NetworkUtils.GetClientIPAddress(controller.HttpContext);
            httpContextInfo.Host = controller.Request.Host.ToJson();
            httpContextInfo.UserInfo = controller.User?.Identity?.Name;
        }
        log.ContextInfo = httpContextInfo;
        return log;
    }

    private static void WriteSessionConfigurationLog(Exception ex)
    {
        Console.Write(@"**************************************
Lütfen aşağıdaki ayarlamaları yapın.
  services.AddSession(options => {options.IdleTimeout = TimeSpan.FromMinutes(1);//isteğe bağlı zaman ayarlanabilir});
  app.UseSession();


Bu ayar yapılmadığından kaynaklı hata mesajı:  
  " + ex.GetAllMessages() + @"
====================================================

");
    }

    private static LogContainer PrepareLogMessage(LogContainer log, string MessageType)
    {
        errorCount[MessageType]++;
        log.MessageNo = errorCount[MessageType];
        return log;
    }

    #endregion
}
public class MethodExecutionBase
{

    public object Data { get; set; }
    public string StatusMessage { get; set; }
    public bool Status { get; set; }
}
