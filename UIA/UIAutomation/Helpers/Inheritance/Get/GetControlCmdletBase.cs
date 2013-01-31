﻿/*
 * Created by SharpDevelop.
 * User: Alexander Petrovskiy
 * Date: 19/01/2012
 * Time: 11:32 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace UIAutomation
{
    using System;
    using System.Management.Automation;
    //using System.Windows.Automation;
    
    using System.Collections;

    /// <summary>
    /// Description of GetControlCmdletBase.
    /// </summary>
    public class GetControlCmdletBase : GetCmdletBase
    {
        #region Constructor
        public GetControlCmdletBase()
        {
            //this.WriteVerbose(this, "constructor");
            
            this.Class = String.Empty;
            // Title = String.Empty;
            this.Name = String.Empty;
            this.ControlType = String.Empty;
            this.AutomationId = String.Empty;
            // 20120824
            //this.InputObject = CurrentData.CurrentWindow;
            //WriteVerbose(this, "this.InputObject[0] = CurrentData.CurrentWindow;");
            //this.InputObject[0] = CurrentData.CurrentWindow;
            // 20120824
            this.InputObject = 
                new System.Windows.Automation.AutomationElement[] { CurrentData.CurrentWindow };
            
            // CacheRequest
            this.FromCache = Preferences.FromCache;
        }
        #endregion Constructor
        
        #region Parameters
        // 20130128
        // change the order
#region commented
//        [Parameter(Mandatory = false,
//                   ParameterSetName = "UIAuto")]
//        //[Parameter(Mandatory = false,
//        //           ParameterSetName = "First")]
//        // 20120824
//        //public string Class { get; set; }
//        public string Class { get; set; }
//        
//        [Parameter(Mandatory = false,
//                   ParameterSetName = "UIAuto")]
//        [Parameter(Mandatory = false,
//                   ParameterSetName = "Win32")]
//        //[Parameter(Mandatory = false,
//        //           ParameterSetName = "First")]
//        [Alias("Title")]
//        // 20120824
//        //public string Name { get; set; }
//        public string Name { get; set; }
//        
//        [Parameter(Mandatory = false,
//                   ParameterSetName = "UIAuto")]
//        //[Parameter(Mandatory = false,
//        //           ParameterSetName = "First")]
//        // 20120824
//        //public string ControlType { get; set; }
//        public string ControlType { get; set; }
//        
//        [Parameter(Mandatory = false,
//                   ParameterSetName = "UIAuto")]
//        //[Parameter(Mandatory = false,
//        //           ParameterSetName = "First")]
//        // 20120824
//        //public string AutomationId { get; set; }
//        public string AutomationId { get; set; }
#endregion commented
        
        [Parameter(Mandatory = false,
                   Position = 0,
                   ParameterSetName = "UIASearch")]
        public string ContainsText { get; set; }
        
        [Parameter(Mandatory = false,
                   ParameterSetName = "UIAWildCard")]
        [Parameter(Mandatory = false,
                   ParameterSetName = "Win32")]
        [Parameter(Mandatory = false,
                   ParameterSetName = "UIAClassic")]
        [Alias("Title")]
        public string Name { get; set; }
        
        [Parameter(Mandatory = false,
                   ParameterSetName = "UIAWildCard")]
        [Parameter(Mandatory = false,
                   ParameterSetName = "UIAClassic")]
        public string Value { get; set; }
        
        [Parameter(Mandatory = false,
                   ParameterSetName = "UIAWildCard")]
        [Parameter(Mandatory = false,
                   ParameterSetName = "UIAClassic")]
        public string AutomationId { get; set; }
        
        [Parameter(Mandatory = false,
                   ParameterSetName = "UIAWildCard")]
        [Parameter(Mandatory = false,
                   ParameterSetName = "UIAClassic")]
        [Alias("ClassName")]
        public string Class { get; set; }
        
        [Parameter(Mandatory = false,
                   ParameterSetName = "UIAWildCard")]
        [Parameter(Mandatory = false,
                   ParameterSetName = "UIAClassic")]
        public string ControlType { get; set; }
        
        [Parameter(Mandatory = false,
                   ParameterSetName = "Win32")]
        public SwitchParameter Win32 { get; set; }
        
        [Parameter (Mandatory = false,
                   ParameterSetName = "UIAWildCard")]
        [Parameter(Mandatory = false,
                   ParameterSetName = "UIAClassic")]
        public SwitchParameter FromCache { get; set; }
        
        //[Parameter(Mandatory = false,
        //           ParameterSetName = "First")]
        //public SwitchParameter First { get; set; }
        
        // 20130127
        [Parameter(Mandatory = false)]
        public SwitchParameter CaseSensitive { get; set; }
        #endregion Parameters

    }
}