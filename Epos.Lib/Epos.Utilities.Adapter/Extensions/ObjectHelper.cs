using System;
using System.ComponentModel;
using System.Reflection;

namespace Epos.Utilities.Adapter.Extensions
{
    public static class ObjectHelper
    {
        public static object GetInstance(string strFullyQualifiedName, object constructorParam)
        {
            Type type = Type.GetType(strFullyQualifiedName);
            if (type != null)
                return Activator.CreateInstance(type);
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = asm.GetType(strFullyQualifiedName);
                if (type != null && constructorParam != null)
                    return Activator.CreateInstance(type,
                        BindingFlags.CreateInstance |
                        BindingFlags.Public |
                        BindingFlags.Instance |
                        BindingFlags.OptionalParamBinding,
                        null, new Object[] { constructorParam }, null);

                else if (type != null)
                {
                    return Activator.CreateInstance(type);
                }
            }
            return null;
        }
        public static T GetInstance<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
        public static T SetAction<T>(Action<T> action)
        {
            var instance = GetInstance<T>();
            action.Invoke(instance);

            return instance;
        }

        public static void Map(object sourceObject, object destObject)
        {
            Type sourceType = sourceObject.GetType();
            Type destType = destObject.GetType();

            PropertyDescriptorCollection sourceProperties = TypeDescriptor.GetProperties(sourceType);
            PropertyDescriptorCollection destProperties = TypeDescriptor.GetProperties(destType);

            foreach (PropertyDescriptor sourceProp in sourceProperties)
            {
                foreach (PropertyDescriptor destProp in destProperties)
                {
                    if (sourceProp.Name == destProp.Name)
                    {
                        var sourceVal = sourceProp.GetValue(sourceObject);
                        destProp.SetValue(destObject, sourceVal);
                    }

                }
            }

        }


    }
}
