﻿// #region License
// 
// 
// /*
//     Copyright [2011] [Jeffrey Cameron]
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//        http://www.apache.org/licenses/LICENSE-2.0
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// */
// #endregion

using System;
using System.Deployment.Application;
using System.IO;
using System.Reflection;

namespace Pickles.UserInterface.Settings
{
    public static class DataDirectoryDeriver
    {
        public static string DeriveDataDirectory()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                return ApplicationDeployment.CurrentDeployment.DataDirectory;
            }
            else
            {
                Assembly entryAssembly = Assembly.GetEntryAssembly();

                if (entryAssembly != null)
                {
                    return Path.GetDirectoryName(entryAssembly.Location);
                }

                return string.Empty;
            }
        }
    }
}