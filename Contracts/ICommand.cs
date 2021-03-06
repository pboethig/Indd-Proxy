﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Command abilities
/// </summary>
namespace Indd.Contracts
{
    /// <summary>
    /// Interface for all commands
    /// </summary>
    public interface ICommand
    {

        /// <summary>
        /// Validates the commandRequest
        /// </summary>
        /// <param name="args"></param>
        /// <returns>bool</returns>
         bool validateRequest();
        
        /// <summary>
        /// Executes commandlogic
        /// </summary>
        /// <returns>bool</returns>
        bool execute();
        
        /// <summary>
        /// runs  sequence of commando factory pipeline
        /// </summary>
        /// <returns></returns>
        List<System.Exception> processSequence();
    }
}
