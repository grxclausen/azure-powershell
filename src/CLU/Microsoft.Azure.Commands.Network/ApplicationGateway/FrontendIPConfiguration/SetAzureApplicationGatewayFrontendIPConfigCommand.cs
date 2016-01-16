﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Commands.Network.Models;

namespace Microsoft.Azure.Commands.Network
{
    [Cmdlet(VerbsCommon.Set, "AzureRmApplicationGatewayFrontendIPConfig"), OutputType(typeof(PSApplicationGateway))]
    [CliCommandAlias("network;application;gateway;frontend;ipconfig;set")]
    public class SetAzureApplicationGatewayFrontendIPConfigCommand : AzureApplicationGatewayFrontendIPConfigBase
    {
        [Parameter(
            Mandatory = true,
            ValueFromPipeline = true,
            HelpMessage = "The application gateway")]
        public PSApplicationGateway ApplicationGateway { get; set; }

        public override void ExecuteCmdlet()
        {
            base.ExecuteCmdlet();

            var oldFrontendIPConfig = this.ApplicationGateway.FrontendIPConfigurations.SingleOrDefault
                (resource => string.Equals(resource.Name, this.Name, System.StringComparison.CurrentCultureIgnoreCase));

            if (oldFrontendIPConfig == null)
            {
                throw new ArgumentException("FrontendIPConfiguration with the specified name does not exist");
            }

            var newFrontendIPConfig = base.NewObject();

            this.ApplicationGateway.FrontendIPConfigurations.Remove(oldFrontendIPConfig);
            this.ApplicationGateway.FrontendIPConfigurations.Add(newFrontendIPConfig);

            WriteObject(this.ApplicationGateway);
        }
    }
}
