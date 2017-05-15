using System;

namespace Indd.Service.Commands.Graphics {

    /// <summary>
    /// Options to generate proxy
    /// </summary>
    public class SetLinks : Indd.Service.Commands.Images.Base,  Contracts.ICommand
    {
        /// <summary>
        /// Saves dynamic command 
        /// </summary>
        /// <param name="commandRequests"></param>
        public SetLinks(dynamic commandRequest) : base ((object)commandRequest){}

        /// <summary>C:\Users\win10\Documents\Visual Studio 2017\Projects\Indd-Proxy\Service\Commands\Graphics\SetLinks.cs
        /// Open a document, if its not allready open
        /// </summary>
        /// <returns></returns>
        public override bool execute()
        {
            try
            {
                this.openDocument();

                string basePath, fileName;

                int objectId;

                bool result;

                foreach (dynamic item in commandRequest.objectToImageLinkMap)
                {
                    basePath = (string)item.basePath;

                    fileName = (string)item.imageId + "." + (string)item.type;

                    objectId=(int)item.objectId;

                    result = base.relink(this.document, basePath, fileName, objectId);

                    if (!result)
                    {
                        throw new SystemException("Object with id: " + objectId + " not relinkable for this document"); 
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new SystemException("Images.SetLinks: " + ex.Message);
            }

            return true;
        }
        
        /// <summary>
        /// Validate Request
        /// </summary>
        /// <param name="args"></param>
        /// <returns>dynamic</returns>
        public override bool validateRequest() 
        {
            base.validateRequest();
            
            if (this.commandRequest.objectToImageLinkMap==null)
            {
                throw new System.Exception("property objectToImageLinkMap missing Jobticket: " + this.commandRequest.uuid);
            }
            
            return true;
        }
    }
}