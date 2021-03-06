﻿/*
 * Created by SharpDevelop.
 * User: Alexander Petrovskiy
 * Date: 8/16/2012
 * Time: 6:02 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace PSTestLib
{
    using System;
    using System.Management.Automation;
    using System.Management.Automation.Runspaces;
    using System.Collections;
    using System.Collections.ObjectModel;
    
    using System.Windows.Automation;
    
    /// <summary>
    /// Description of PSCmdletBase.
    /// </summary>
    public abstract class PSCmdletBase : PSCmdlet //, ICommonCmdletBase
    {
        public PSCmdletBase()
        {
        }
        
        #region Parameters
            #region Actions
        [Parameter(Mandatory = false)]
        public ScriptBlock[] OnSuccessAction { get; set; }
        [Parameter(Mandatory = false)]
        public ScriptBlock[] OnErrorAction { get; set; }
        [Parameter(Mandatory = false)]
        public virtual SwitchParameter OnErrorScreenShot { get; set; }
        [Parameter(Mandatory = false)]
        internal ScriptBlock[] EventAction { get; set; }
        
        [Parameter(Mandatory = false)]
        public ScriptBlock[] OnSleepAction { get; set; }
            #endregion Actions
            #region Testing
        [Parameter(Mandatory = false)]
        public virtual string TestResultName { get; set; }
        [Parameter(Mandatory = false)]
        public virtual string TestResultId { get; set; }
        [Parameter(Mandatory = false)]
        public SwitchParameter TestPassed { get; set; }
        [Parameter(Mandatory = false)]
        public SwitchParameter KnownIssue { get; set; }
        [Parameter(Mandatory = false)]
        public SwitchParameter TestLog { get; set; }
            #endregion Testing
//        // 20120208
//        [Parameter(Mandatory = false)]
//        public SwitchParameter Highlight { get; set; }
//        [Parameter(Mandatory = false)]
//        public SwitchParameter HighlightParent { get; set; }
////        [Parameter(Mandatory = false)]
////        public SwitchParameter HighlightFirstChild { get; set; }
        #endregion Parameters
        
        #region Properties
        protected AutomationElement EventSource { get; set; }
        protected AutomationEventArgs EventArgs { get; set; }
        
        //public static System.Collections.Generic.List<object> UnitTestOutput { get; set; }
        //public static UnitTestOutputClass UnitTestOutput { get; set; }
        #endregion Properties
        
        public static bool UnitTestMode { get; set; }
        
        private static bool CmdletParametersCheckingOn { get; set; }
        public static void SetCmdletParametersCheckingOn(bool check)
        {
            CmdletParametersCheckingOn = check;
        }
        protected void CheckCmdletParameters()
        {
            if (CmdletParametersCheckingOn) {
                Exception eExit =
                    new Exception("Parameters checked");
                throw eExit;
            }
        }
        
        protected override void BeginProcessing()
        {
            this.CheckCmdletParameters();
        }
        
        protected abstract void WriteLog(string logRecord);
        
        public static bool EnableTrace { get; set; }
        
//        protected abstract bool WriteObjectMethod010CheckOutputObject(object outputObject);
//        protected abstract void WriteObjectMethod020Highlight(PSCmdletBase cmdlet, object outputObject);
//        protected abstract void WriteObjectMethod030RunScriptBlocks(PSCmdletBase cmdlet, object outputObject);
//        protected abstract void WriteObjectMethod040SetTestResult(PSCmdletBase cmdlet, object outputObject);
//        protected abstract void WriteObjectMethod045OnSuccessScreenshot(PSCmdletBase cmdlet, object outputObject);
//        protected abstract void WriteObjectMethod050OnSuccessDelay(PSCmdletBase cmdlet, object outputObject);
//        protected abstract void WriteObjectMethod060OutputResult(PSCmdletBase cmdlet, object outputObject);
//        protected abstract void WriteObjectMethod070Report(PSCmdletBase cmdlet, object outputObject);
//        protected abstract void WriteObjectMethod080ReportFailure();
        
        protected abstract bool CheckSingleObject(PSCmdletBase cmdlet, object outputObject);
        protected abstract void BeforeWriteSingleObject(PSCmdletBase cmdlet, object outputObject);
        protected abstract void WriteSingleObject(PSCmdletBase cmdlet, object outputObject);
        protected abstract void AfterWriteSingleObject(PSCmdletBase cmdlet, object outputObject);
        protected abstract void BeforeWriteCollection(PSCmdletBase cmdlet, object[] outputObjectCollection);
        protected abstract void BeforeWriteCollection(PSCmdletBase cmdlet, System.Collections.Generic.List<object> outputObjectCollection);
        protected abstract void BeforeWriteCollection(PSCmdletBase cmdlet, ArrayList outputObjectCollection);
        protected abstract void BeforeWriteCollection(PSCmdletBase cmdlet, IList outputObjectCollection);
        protected abstract void BeforeWriteCollection(PSCmdletBase cmdlet, IEnumerable outputObjectCollection);
        protected abstract void BeforeWriteCollection(PSCmdletBase cmdlet, ICollection outputObjectCollection);
        protected abstract void BeforeWriteCollection(PSCmdletBase cmdlet, Hashtable outputObjectCollection);
        protected abstract void AfterWriteCollection(PSCmdletBase cmdlet, object[] outputObjectCollection);
        protected abstract void AfterWriteCollection(PSCmdletBase cmdlet, System.Collections.Generic.List<object> outputObjectCollection);
        protected abstract void AfterWriteCollection(PSCmdletBase cmdlet, ArrayList outputObjectCollection);
        protected abstract void AfterWriteCollection(PSCmdletBase cmdlet, IList outputObjectCollection);
        protected abstract void AfterWriteCollection(PSCmdletBase cmdlet, IEnumerable outputObjectCollection);
        protected abstract void AfterWriteCollection(PSCmdletBase cmdlet, ICollection outputObjectCollection);
        protected abstract void AfterWriteCollection(PSCmdletBase cmdlet, Hashtable outputObjectCollection);
        
        public void WriteObject(PSCmdletBase cmdlet, object outputObject)
        {
            if (PSCmdletBase.UnitTestMode) {
                
                UnitTestOutput.CheckInitialized();

                UnitTestOutput.StartAddingOutput();

            }

            this.writeSingleObject(cmdlet, outputObject);

        }
        
        public virtual void WriteObject(PSCmdletBase cmdlet, object[] outputObjectCollection)
        {
            BeforeWriteCollection(cmdlet, outputObjectCollection);
            
            if (PSCmdletBase.UnitTestMode) {

                UnitTestOutput.CheckInitialized();

                UnitTestOutput.StartAddingOutput();

            }

            for (int i = 0; i < outputObjectCollection.Length; i++) {

                this.writeSingleObject(cmdlet, outputObjectCollection[i]);

            }
            
            AfterWriteCollection(cmdlet, outputObjectCollection);
        }
        
        public virtual void WriteObject(PSCmdletBase cmdlet, System.Collections.Generic.List<object> outputObjectCollection)
        {
            BeforeWriteCollection(cmdlet, outputObjectCollection);
            
            if (PSCmdletBase.UnitTestMode) {
                
                UnitTestOutput.CheckInitialized();

                UnitTestOutput.StartAddingOutput();
            }
            // 20121117
            //for (int i = 0; i < outputObjectCollection.Count; i++) {
            foreach (var item in outputObjectCollection) {
                this.writeSingleObject(cmdlet, item);
            }
            //}
            
            AfterWriteCollection(cmdlet, outputObjectCollection);
        }
       
        public virtual void WriteObject(PSCmdletBase cmdlet, ArrayList outputObjectCollection)
        {
            BeforeWriteCollection(cmdlet, outputObjectCollection);
            
            if (PSCmdletBase.UnitTestMode) {
                
                UnitTestOutput.CheckInitialized();

                //UnitTestOutput.Add(outputObjectCollection);
                
                UnitTestOutput.StartAddingOutput();
            }

            for (int i = 0; i < outputObjectCollection.Count; i++) {
                this.WriteObject(cmdlet, outputObjectCollection[i]);
            }
            
            AfterWriteCollection(cmdlet, outputObjectCollection);
        }
        
        // 20121112
        public virtual void WriteObject(PSCmdletBase cmdlet, IList outputObjectCollection)
        {
            BeforeWriteCollection(cmdlet, outputObjectCollection);
            
            if (PSCmdletBase.UnitTestMode) {

                UnitTestOutput.CheckInitialized();

                UnitTestOutput.StartAddingOutput();
            }

            foreach (object item in outputObjectCollection) {
                this.writeSingleObject(cmdlet, item);
            }
            
            AfterWriteCollection(cmdlet, outputObjectCollection);
        }
        
        // 20121112
        public virtual void WriteObject(PSCmdletBase cmdlet, IEnumerable outputObjectCollection)
        {
            BeforeWriteCollection(cmdlet, outputObjectCollection);
            
            if (PSCmdletBase.UnitTestMode) {
                
                UnitTestOutput.CheckInitialized();

                UnitTestOutput.StartAddingOutput();
            }
            IEnumerator en = outputObjectCollection.GetEnumerator();
            while (en.MoveNext()) {
                this.writeSingleObject(cmdlet, en.Current);
            }
            
            AfterWriteCollection(cmdlet, outputObjectCollection);
        }
        
        // 20121112
        public virtual void WriteObject(PSCmdletBase cmdlet, string outputObject)
        {
            if (PSCmdletBase.UnitTestMode) {
                
                UnitTestOutput.CheckInitialized();

                UnitTestOutput.StartAddingOutput();
            }
            this.writeSingleObject(cmdlet, outputObject);
        }
        
        public virtual void WriteObject(PSCmdletBase cmdlet, ICollection outputObjectCollection)
        {
            BeforeWriteCollection(cmdlet, outputObjectCollection);
            
            if (PSCmdletBase.UnitTestMode) {
                
                UnitTestOutput.CheckInitialized();

                UnitTestOutput.StartAddingOutput();
            }
            foreach (var outputObject in outputObjectCollection) {
this.WriteVerbose(this, "something to output!!!!!!!!!!1");
                //WriteObject(cmdlet, outputObject);
                this.writeSingleObject(cmdlet, outputObject);
            }
            
            AfterWriteCollection(cmdlet, outputObjectCollection);
        }
        
        public virtual void WriteObject(PSCmdletBase cmdlet, Hashtable outputObjectCollection)
        {
            BeforeWriteCollection(cmdlet, outputObjectCollection);
            
            if (PSCmdletBase.UnitTestMode) {
                
                UnitTestOutput.CheckInitialized();

                UnitTestOutput.StartAddingOutput();
            }
            this.writeSingleObject(cmdlet, outputObjectCollection);
            
            AfterWriteCollection(cmdlet, outputObjectCollection);
        }
        
        private void writeSingleObject(PSCmdletBase cmdlet, object outputObject)
        {
            //if (WriteObjectMethod010CheckOutputObject(outputObject)) {
            if (CheckSingleObject(cmdlet, outputObject)) {

//                WriteObjectMethod020Highlight(cmdlet, outputObject);
//
//                WriteObjectMethod030RunScriptBlocks(cmdlet, outputObject);
//
//                WriteObjectMethod040SetTestResult(cmdlet, outputObject);
//
//                WriteObjectMethod045OnSuccessScreenshot(cmdlet, outputObject);
//
//                WriteObjectMethod050OnSuccessDelay(cmdlet, outputObject);
                
                BeforeWriteSingleObject(cmdlet, outputObject);

                //WriteSingleObject(cmdlet, outputObject);
                
                try {

                    if (PSCmdletBase.UnitTestMode) {

                        UnitTestOutput.Add(outputObject);

                    } else {

//                        WriteObjectMethod060OutputResult(cmdlet, outputObject);
                        WriteSingleObject(cmdlet, outputObject);

                    }
                }
                catch {}
                
                
                //WriteObjectMethod060OutputResult(cmdlet, outputObject);

                AfterWriteSingleObject(cmdlet, outputObject);

//                WriteObjectMethod070Report(cmdlet, outputObject);

                //WriteObjectMethod080ReportFailure();
            
            } else {

            }
        }
        



        protected abstract void WriteErrorMethod010RunScriptBlocks(PSCmdletBase cmdlet);
        protected abstract void WriteErrorMethod020SetTestResult(PSCmdletBase cmdlet, ErrorRecord errorRecord);
        protected abstract void WriteErrorMethod030ChangeTimeoutSettings(PSCmdletBase cmdlet, bool terminating);
        protected abstract void WriteErrorMethod040AddErrorToErrorList(PSCmdletBase cmdlet, ErrorRecord errorRecord);
        protected abstract void WriteErrorMethod045OnErrorScreenshot(PSCmdletBase cmdlet);
        protected abstract void WriteErrorMethod050OnErrorDelay(PSCmdletBase cmdlet);
        protected abstract void WriteErrorMethod060OutputError(PSCmdletBase cmdlet, ErrorRecord errorRecord, bool terminating);
        protected abstract void WriteErrorMethod070Report(PSCmdletBase cmdlet);
        
        //protected abstract void BeforeWriteError(PSCmdletBase cmdlet, ErrorRecord errorRecord);
        //protected abstract void WriteErrorObject(PSCmdletBase cmdlet, ErrorRecord errorRecord);
        //protected abstract void AfterWriteError(PSCmdletBase cmdlet, ErrorRecord errorRecord);
        
        public void WriteError(PSCmdletBase cmdlet, ErrorRecord errorRecord, bool terminating)
        {
            // run error script blocks
            WriteErrorMethod010RunScriptBlocks(cmdlet);;
            
            // add test result detail
            
            // close test result
            WriteErrorMethod020SetTestResult(cmdlet, errorRecord);
            
            // set the Turbo timeout
            WriteErrorMethod030ChangeTimeoutSettings(cmdlet, terminating);
            
            // write to the Error list
            WriteErrorMethod040AddErrorToErrorList(cmdlet, errorRecord);
            
            // on error screenshot
            WriteErrorMethod045OnErrorScreenshot(cmdlet);
            
            // on error sleep
            WriteErrorMethod050OnErrorDelay(cmdlet);
            
            
            //BeforeWriteError(cmdlet, errorRecord);
            
            // output the error
            
            //WriteErrorObject(cmdlet, errorRecord);
            
            WriteErrorMethod060OutputError(cmdlet, errorRecord, terminating);
            
            
            //AfterWriteError(cmdlet, errorRecord);
            
            // ? ? ? ? 
            WriteErrorMethod070Report(cmdlet); //, outputObject);
        }
        
        public void WriteError(PSCmdletBase cmdlet, string message, string errorId, ErrorCategory category, bool terminating)
        {
            ErrorRecord err = 
                new ErrorRecord(
                    new Exception(message),
                    errorId,
                    category,
                    null);
            err.ErrorDetails = 
                new ErrorDetails(message);
            WriteError(cmdlet, err, terminating);
        }
        
        // temporary
        public void WriteVerbose(PSCmdletBase cmdlet, string text)
        {
            // 20121117
            //if (!this.UnitTestMode) {
            if (!UnitTestMode) {
                try {
                string cmdletName = "? : ";
    
                if (null != cmdlet) {
    
                    try {
    
                        cmdletName = 
                            CmdletSignature(cmdlet);
    
                        cmdlet.WriteVerbose(cmdletName + text);
    
                    }
                    catch {
                        cmdlet.WriteVerbose(cmdletName + "failed");
                    }
                } else {
                    try {
                        cmdlet.WriteVerbose(cmdletName + text);
                    }
                    catch {}
                }
                
                // WriteLog(reportString);
                }
                catch (Exception eNotUsed) {
                    // ??
                }
            }
        }
        
        public void WriteVerbose(PSCmdletBase cmdlet, object obj)
        {

            string cmdletName = "? : ";
            string objectString = string.Empty;
            try {
                objectString = obj.ToString();
            }
            catch {}

            if (null != cmdlet) {

                try {

                    cmdletName = 
                        CmdletSignature(cmdlet);

                    cmdlet.WriteVerbose(cmdletName + objectString);
                }
                catch {
                    cmdlet.WriteVerbose(cmdletName + "failed");
                }
            } else {
                cmdlet.WriteVerbose(cmdletName + objectString);
            }
        }
        
        public void WriteTrace(PSCmdletBase cmdlet, object obj)
        {
            if (EnableTrace) {
                Console.WriteLine(obj);
            }
        }
        
        protected string GetGeneratedTestResultNameByPosition(
            string initialString, 
            int? position)
        {
            string result = string.Empty;
            
            if (!initialString.Contains("|")) {
                result = initialString;
            } else {
                try {
                    string[] results = 
                        initialString.Split('|');
                    result = results[(int)(position - 1)].Trim();
                }
                catch {
                    
                }
            }
            
            return result;
        }
        
            #region Cmdlet Signature
        //protected string CmdletName(PSCmdletBase cmdlet)
        public string CmdletName(PSCmdletBase cmdlet)
        {
            string result = String.Empty;
            if (null == cmdlet) return result;
            result = 
                cmdlet.GetType().Name;
            if (result.Contains("Se")) {
                result = 
                    result.Replace("Se", "-Se");
            } else if (result.Contains("UIA")) {
                result = 
                    result.Replace("UIA", "-UIA");
            } else if (result.Contains("TMX")) {
                result = 
                    result.Replace("TMX", "-TMX");
            } else if (result.Contains("AWS")) {
                result = 
                    result.Replace("AWS", "-AWS");
            } else if (result.Contains("ESXi")) {
                result = 
                    result.Replace("ESXi", "-ESXi");
            }
            else if (result.Contains("TAMS")) {
                result = 
                    result.Replace("TAMS", "-TAMS");
            }
            result = 
                result.Replace("Command", "");
            return result;
        }
            
        protected string CmdletSignature(PSCmdletBase cmdlet)
        {
            string result = this.CmdletName(cmdlet);
            result += ": ";
            return result;
        }
            #endregion Cmdlet Signature
        
        protected System.Collections.Generic.Dictionary<string, string> ConvertHashtableToDictionary(
            Hashtable hashtable)
        {
            System.Collections.Generic.Dictionary<string, string> dict = 
                new System.Collections.Generic.Dictionary<string, string>();
            
            this.WriteVerbose(this, hashtable.Keys.Count.ToString());
            
            foreach (string key1 in hashtable.Keys) {
                
                string keyUpper = key1.ToUpper();

                this.WriteVerbose(this, "found key: " + keyUpper);
                
                dict.Add(keyUpper, hashtable[key1].ToString());
                
                this.WriteVerbose(this, "added to the dictionary: " +
                                  keyUpper + " = " + dict[keyUpper].ToString());
            }
            
            return dict;
        }
        
        protected void runTwoScriptBlockCollections(
            ScriptBlock[] blocks1,
            ScriptBlock[] blocks2,
            PSCmdletBase cmdlet)
        {
            System.Collections.Generic.List<ScriptBlock> blocks =
                new System.Collections.Generic.List<ScriptBlock>();
            if (blocks1 != null &&
                blocks1.Length > 0) {
                foreach (ScriptBlock sb in blocks1) {
                    blocks.Add(sb);
                }
            }
            if (blocks2 != null &&
                blocks2.Length > 0) {
                foreach (ScriptBlock sb in blocks2) {
                    blocks.Add(sb);
                }
            }
            runScriptBlocks(blocks, cmdlet, false);
        }
        
        // 20120816
        public void runScriptBlocks(System.Collections.Generic.List<ScriptBlock >  blocks,
                                     PSCmdletBase cmdlet,
                                     bool eventHandlers)
        {
            try {
                if (blocks != null &&
                    blocks.Count > 0) {
                    // WriteVerbose(this, "runScriptBlocks 1 fired");
                    foreach (ScriptBlock sb in blocks) {
                        // WriteVerbose(this, "runScriptBlocks 2 fired");
                        if (sb != null) {
                            // WriteVerbose(this, "runScriptBlocks 3 fired");
                            try {
                                if (eventHandlers) {
                                    // WriteVerbose(this, "runScriptBlocks 4 fired");
                                    // runScriptBlock runner = new runScriptBlock(runSBEvent);
                                    // runScriptBlock runner = new runScriptBlock(runSBEvent);
                                    // test
                                    runScriptBlock runner = new runScriptBlock(runSBEvent);
                                    // WriteVerbose(this, "runScriptBlocks 5 fired");
                                    runner(sb, cmdlet.EventSource, cmdlet.EventArgs);
                                    // WriteVerbose(this, "runScriptBlocks 6 fired");
                                } else {
                                    // runScriptBlock runner = new runScriptBlock(runSB);
                                    runScriptBlock runner = new runScriptBlock(runSBAction);
                                    runner(sb, cmdlet.EventSource, cmdlet.EventArgs);
                                }
                            } catch (Exception eInner) {
                                ErrorRecord err = 
                                    new ErrorRecord(
                                        eInner,
                                        "InvokeException",
                                        ErrorCategory.OperationStopped,
                                        sb);
                                err.ErrorDetails = 
                                    new ErrorDetails("Error in " +
                                                     sb.ToString());
                                WriteError(this, err, false);
                            }
                        }
                    }
                }
            } catch (Exception eOuter) {
                WriteError(this, 
                           new ErrorRecord(eOuter, "runScriptBlocks", ErrorCategory.InvalidArgument, null),
                           true);
            }
        }
        //#endregion Invoke-UIAScript
        
        
        protected virtual void SaveEventInput(
            AutomationElement src,
            AutomationEventArgs e,
            string programmaticName,
            bool infoAdded)
        {
            
        }
        
        #region Event delegate
        private void runSBEvent(ScriptBlock sb, 
                                AutomationElement src,
                                AutomationEventArgs e)
        {
            
            // inform the Wait-UIAEventRaised cmdlet
            SaveEventInput(
                src,
                e,
                e.EventId.ProgrammaticName,
                true);
//            try {
//                CurrentData.LastEventSource = src; // as AutomationElement;
//                CurrentData.LastEventArgs = e; // as AutomationEventArgs;
//                CurrentData.LastEventType = e.EventId.ProgrammaticName;
//                CurrentData.LastEventInfoAdded = true;
//            }
//            catch {
//                //WriteVerbose(this, "failed to register an event in the collection");
//            }
            
            // 20120206 Collection<PSObject >  psObjects = null;
            try {
                System.Management.Automation.Runspaces.Runspace.DefaultRunspace =
                    RunspaceFactory.CreateRunspace();
                try {
                    System.Management.Automation.Runspaces.Runspace.DefaultRunspace.Open();
                } catch (Exception e1) {
                    ErrorRecord err = 
                        new ErrorRecord(e1,
                                        "ErrorOnOpeningRunspace",
                                        ErrorCategory.InvalidOperation,
                                        sb);
                    err.ErrorDetails = 
                        new ErrorDetails(
                            "Unable to run a scriptblock:\r\n" + 
                            sb.ToString());
                    WriteError(this, err, false);
                }
                try {
                    System.Collections.Generic.List<object >  inputParams = 
                        new System.Collections.Generic.List<object > ();
                    inputParams.Add(src);
                    inputParams.Add(e);
                    object[] inputParamsArray = inputParams.ToArray();
                    // psObjects = 
                        sb.InvokeReturnAsIs(inputParamsArray);
                        // sb.Invoke(inputParamsArray);
                    
                } catch (Exception e2) {
                    ErrorRecord err = 
                        new ErrorRecord(e2,
                                        "ErrorInOpenedRunspace",
                                        ErrorCategory.InvalidOperation,
                                        sb);
                    err.ErrorDetails = 
                        new ErrorDetails("Unable to run a scriptblock");
                    WriteError(this, err, true);
                }
// psObjects =
// sb.Invoke();
            } catch (Exception eOuter) {
                ErrorRecord err = 
                    new ErrorRecord(eOuter,
                                    "ErrorInInvokingScriptBlock", //"ErrorinCreatingRunspace",
                                    ErrorCategory.InvalidOperation,
                                    System.Management.Automation.Runspaces.Runspace.DefaultRunspace);
                err.ErrorDetails = 
                    new ErrorDetails("Unable to issue the following command:\r\n" + 
                                     "System.Management.Automation.Runspaces.Runspace.DefaultRunspace = RunspaceFactory.CreateRunspace();" +
                                     "\r\nException raised is\r\n" +
                                     eOuter.Message);
            }
        }
        #endregion Event delegate
        
        
        #region Action delegate
        private void runSBAction(ScriptBlock sb, 
                                 AutomationElement src,
                                 AutomationEventArgs e)
        {
            Collection<PSObject> psObjects = null;
            try {
                psObjects =
                    sb.Invoke();
// int counter = 0;
// foreach (PSObject pso in psObjects) {
//  //if pso.
// counter++;
// WriteVerbose("result " + counter.ToString() + ":");
// WriteVerbose(pso.ToString());
//  //WriteObject(pso.TypeNames
// foreach ( string typeName in pso.TypeNames) {
// WriteVerbose(typeName);
// }
// }
            } catch (Exception eOuter) {
                ErrorRecord err = 
                    new ErrorRecord(eOuter,
                                    "ErrorInInvokingScriptBlock",
                                    ErrorCategory.InvalidOperation,
                                    System.Management.Automation.Runspaces.Runspace.DefaultRunspace);
                err.ErrorDetails = 
                    new ErrorDetails(
                        "Unable to issue the following command:\r\n" +
                        sb.ToString() + 
                        "\r\nThe exception raised is\r\n" + 
                        eOuter.Message);
                                     //"System.Management.Automation.Runspaces.Runspace.DefaultRunspace = RunspaceFactory.CreateRunspace();");
                WriteError(err);
            }
        }
        #endregion Action delegate
    }
    
    #region Action delegate
    delegate void runScriptBlock(ScriptBlock sb, 
                                 AutomationElement src, 
                                 AutomationEventArgs e);
    #endregion Action delegate
}