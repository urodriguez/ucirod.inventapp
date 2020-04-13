﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Compilation;
using Infrastructure.Crosscutting.AppSettings;
using RestSharp;

namespace Infrastructure.Crosscutting.Logging
{
    public class LogService : ILogService
    {
        private readonly IRestClient _restClient;
        private readonly IAppSettingsService _appSettingsService;

        private readonly string _application;
        private readonly string _projectName;
        private Guid _correlationId;

        public LogService(IAppSettingsService appSettingsService)
        {
            _application = "InventApp";
            _projectName = BuildManager.GetGlobalAsaxType().BaseType.Assembly.FullName.Split(',').First();

            _appSettingsService = appSettingsService;
            _restClient = new RestClient(appSettingsService.LoggingApiUrl);
        }

        public Guid GetCorrelationId() => _correlationId;

        //Very detailed logs, which may include high-volume information such as protocol payloads. This log level is typically only enabled during development
        public void LogTraceMessage(string messageToLog)
        {
            LogMessage(messageToLog, LogType.Trace);
        }

        //Information messages, which are normally enabled in production environment
        public void LogInfoMessage(string messageToLog)
        {
            LogMessage(messageToLog, LogType.Info);
        }

        //Error messages - most of the time these are Exceptions
        public void LogErrorMessage(string messageToLog)
        {
            LogMessage(messageToLog, LogType.Error);
        }

        private void LogMessage(string messageToLog, LogType logType)
        {
            GenerateCorrelationId();

            var task = new Task(() =>
            {
                try
                {
                    var request = new RestRequest
                    {
                        Resource = "logs",
                        Method = Method.POST
                    };
                    request.AddJsonBody(new Log(
                        _appSettingsService.InfrastructureCredential, 
                        _application, 
                        _projectName,
                        _correlationId,
                        messageToLog, 
                        logType, 
                        _appSettingsService.Environment.Name)
                    );

                    var logResponse =  _restClient.Post(request);

                    if (!logResponse.IsSuccessful)
                        throw new Exception(
                            $"logResponse.IsSuccessful=false - logResponse.StatusCode={logResponse.StatusCode} - logResponse.Content={logResponse.Content}"
                        );
                }
                catch (Exception e) 
                {
                    //Do not call LogService to log this exception in order to avoid infinite loop
                    FileSystemLog($"{e}");

                    //queue 'log' data

                    //do not throw the exception in order to avoid finish the main request
                }
            });

            task.Start();
        }

        private void GenerateCorrelationId()
        {
            _correlationId = _correlationId == Guid.Empty ? Guid.NewGuid() : _correlationId;
        }

        private void FileSystemLog(string messageToLog)
        {
            var fileSystemLogsDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}FileSystemLogs";
            Directory.CreateDirectory(fileSystemLogsDirectory);

            var logFileName = $"FSL,{_correlationId}";
            var logFilePath = $"{fileSystemLogsDirectory}\\{logFileName}.txt";

            using (StreamWriter sw = File.AppendText(logFilePath))
            {
                sw.WriteLine($"{messageToLog}{Environment.NewLine}----------------******----------------{Environment.NewLine}");
            }
        }
    }
}
