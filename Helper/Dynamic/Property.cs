using System;
using System.Collections;
using System.Collections.Specialized;
using System.Reflection;

namespace Indd.Helper.Dynamic
{
    class Property
    {

        /// --------------------------------------------------------------------
        /// <summary>
        /// Determine if a property exists in an object
        /// </summary>
        /// <param name="propertyName">Name of the property </param>
        /// <param name="srcObject">the object to inspect</param>
        /// <returns>true if the property exists, false otherwise</returns>
        /// <exception cref="ArgumentNullException">if srcObject is null</exception>
        /// <exception cref="ArgumentException">if propertName is empty or null </exception>
        /// --------------------------------------------------------------------
        public static bool isset(string propertyName, object srcObject)
        {
            if (srcObject == null)
                throw new System.ArgumentNullException("srcObject");

            if ((propertyName == null) || (propertyName == String.Empty) || (propertyName.Length == 0))
                throw new System.ArgumentException("Property name cannot be empty or null.");

            PropertyInfo propInfoSrcObj = srcObject.GetType().GetProperty(propertyName);

            return (propInfoSrcObj != null);
        }

        /// <summary>
        /// returns value by propertyname
        /// </summary>
        /// <param name="srcObject"></param>
        /// <param name="propertyName"></param>
        /// <returns>dynamic</returns>
        public static dynamic getValue(dynamic srcObject, string propertyName)
        {
               System.Type type = srcObject.GetType();
            
                PropertyInfo property = type.GetProperty(propertyName);

                if (property == null) throw new System.Exception("No property: " + propertyName);

                dynamic value = property.GetValue(srcObject, null);

                return value;
        }
    }
}
