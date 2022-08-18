using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Epos.Utilities.Infrastructure.Extensions
{
    public static class ObjectHelper
    {
        public static Dictionary<string,string> GetProperties(this object sourceObject)
        {
            var propsInfo = new Dictionary<string, string>();

            Type sourceType = sourceObject.GetType();

            PropertyDescriptorCollection sourceProperties = TypeDescriptor.GetProperties(sourceType);

            foreach (PropertyDescriptor sourceProp in sourceProperties)
            {
                propsInfo.Add(sourceProp.Name, sourceProp.GetValue(sourceObject).ToString());
            }

            return propsInfo;

        }

    }
}
