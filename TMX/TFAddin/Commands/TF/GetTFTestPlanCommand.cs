﻿/*
 * Created by SharpDevelop.
 * User: Alexander Petrovskiy
 * Date: 11/9/2012
 * Time: 6:50 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace TMX
{
    using System;
    using System.Management.Automation;
    
    /// <summary>
    /// Description of GetTFTestPlanCommand.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "TFTestPlan")]
    public class GetTFTestPlanCommand : TFTestPlanCmdletBase
    {
        public GetTFTestPlanCommand()
        {
        }
        
        protected override void BeginProcessing()
        {
            this.CheckCmdletParameters();
            
            TFSrvGetTestPlanCommand command =
                new TFSrvGetTestPlanCommand(this);
            command.Execute();
        }
    }
}
