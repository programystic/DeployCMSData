﻿using System;
using DeployCmsData.Constants;
using DeployCmsData.Interfaces;
using DeployCmsData.Models;
using Umbraco.Core;

[assembly: CLSCompliant(true)]
namespace DeployCmsData.Services
{
    public sealed class UpgradeScriptManager : IDisposable
    {
        private readonly IUpgradeLogRepository _logDatastore;

        public UpgradeScriptManager()
        {
            _logDatastore = new UpgradeLogRepository();
        }

        public UpgradeScriptManager(IUpgradeLogRepository logDataStore)
        {
            _logDatastore = logDataStore;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _logDatastore.DisposeIfDisposable();
            }
        }

        public UpgradeLog RunScript(IUpgradeScript upgradeScript)
        {
            if (upgradeScript == null)
                return new UpgradeLog() { Exception = ExceptionMessages.UpgradeScriptIsNull };

            if (ScriptAlreadyRun(upgradeScript))
                return null;

            return RunScriptAgain(upgradeScript);
        }

        public UpgradeLog RunScriptAgain(IUpgradeScript upgradeScript)
        {
            if (upgradeScript == null)
                return new UpgradeLog() { Exception = ExceptionMessages.UpgradeScriptIsNull };

            var upgradeLog = new UpgradeLog
            {
                UpgradeScriptName = GetScriptName(upgradeScript),
                Id = Guid.NewGuid(),
                Timestamp = DateTime.Now
            };

            var start = DateTime.Now;
            try
            {
                upgradeLog.Success = upgradeScript.RunScript(_logDatastore);
            }
            catch (Exception e)
            {
                upgradeLog.Success = false;
                upgradeLog.Exception = e.Message;
                Console.WriteLine(e);
            }

            upgradeLog.RuntTimeMilliseconds = (DateTime.Now - start).Milliseconds;
            _logDatastore.SaveLog(upgradeLog);
            return upgradeLog;
        }

        private bool ScriptAlreadyRun(IUpgradeScript upgradeScript)
        {
            if (upgradeScript == null) return false;

            var scriptName = GetScriptName(upgradeScript);
            var result = _logDatastore.GetLog(scriptName);

            return result?.UpgradeScriptName == scriptName;
        }

        public string GetScriptName(IUpgradeScript upgradeScript)
        {            
            return upgradeScript.GetType().FullName;
        }
    }
}
