﻿/*
 * Created by SharpDevelop.
 * User: Alexander Petrovskiy
 * Date: 17/02/2012
 * Time: 07:01 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace TMX
{
    using System;
    using System.Collections.Generic;
    using System.Management.Automation;
    
    /// <summary>
    /// Description of ITestResult.
    /// </summary>
    public interface ITestResult
    {
        string Name { get; set; }
        string Id { get; set; }
        List<ITestResultDetail >  Details { get; }
        string Status { get; }
        TestResultStatuses enStatus { get; set; }
        
        string ScriptName { get; }
        void SetScriptName(string scriptName);
        int LineNumber { get; }
        void SetLineNumber(int lineNumber);
        int Position { get; }
        void SetPosition(int position);
        ErrorRecord Error { get; }
        void SetError(ErrorRecord error);
        string Code { get; set; }
        
        string Description { get; set; }
        List<object> Parameters { get; }
        
        string ScenarioId { get; }
        string SuiteId { get; }
        
        
        double TimeSpent { get; }
        void SetTimeSpent(double timeSpent);
        
        System.DateTime Timestamp { get; }
        void SetNow();
        
        bool Generated { get; }
        void SetGenerated();
        
        string Screenshot { get; }
        void SetScreenshot(string path);
    }
}
