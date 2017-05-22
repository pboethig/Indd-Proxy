﻿/****************************** Module Header ******************************\
* Module Name:  Program.cs
* Project:      CSWindowsService
* Copyright (c) Microsoft Corporation.
* 
* The file defines the entry point of the application.
* 
* This source is subject to the Microsoft Public License.
* See http://www.microsoft.com/en-us/openness/resources/licenses.aspx#MPL.
* All other rights reserved.
* 
* THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
* EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
* WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

#region Using directives
using System.ServiceProcess;
#endregion


namespace RabbitMQConsumerService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase service = new RabbitMQConsumerService();
            ServiceBase.Run(service);
        }
    }
}