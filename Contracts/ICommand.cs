using System;
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
    interface ICommand
    {
        /// <summary>
        /// Validates the commandRequest
        /// </summary>
        /// <param name="args"></param>
        /// <returns>dynamic</returns>
         bool validateRequest();
        
    }
}
