﻿using System.Management.Automation;
using Microsoft.SharePoint.Client;
using OfficeDevPnP.PowerShell.CmdletHelpAttributes;
using OfficeDevPnP.PowerShell.Commands.Base.PipeBinds;
using OfficeDevPnP.PowerShell.Commands.Enums;
using Resources = OfficeDevPnP.PowerShell.Commands.Properties.Resources;

namespace OfficeDevPnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Remove, "SPOCustomAction", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    [CmdletHelp("Removes a custom action", 
        Category = CmdletHelpCategory.Branding)]
    public class RemoveCustomAction : SPOWebCmdlet
    {
        [Parameter(Mandatory = true, Position=0, ValueFromPipeline=true, HelpMessage = "The identifier of the CustomAction that needs to be removed")]
        public GuidPipeBind Identity;

        [Parameter(Mandatory = false, HelpMessage = "Define if the CustomAction is to be found at the web or site collection scope. Specify All to allow deletion from either web or site collection.")]
        public CustomActionScope Scope = CustomActionScope.Web;

        [Parameter(Mandatory = false, HelpMessage = "Use the -Force flag to bypass the confirmation question")]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (Identity != null)
            {
                if (Force || ShouldContinue(Resources.RemoveCustomAction, Resources.Confirm))
                {
                    if (Scope == CustomActionScope.All || Scope == CustomActionScope.Web)
                    {
                        SelectedWeb.DeleteCustomAction(Identity.Id);
                    }
                    if (Scope == CustomActionScope.All || Scope == CustomActionScope.Site)
                    {
                        ClientContext.Site.DeleteCustomAction(Identity.Id);
                    }
                }
            }
        }
    }
}
