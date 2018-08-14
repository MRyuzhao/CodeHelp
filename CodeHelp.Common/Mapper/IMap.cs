using System;
using System.Diagnostics;
using System.Reflection;

namespace CodeHelp.Common.Mapper
{
    public interface IMap
    {
        T Map<T>(object source) where T : class;

        ///// <summary>
        ///// Tries to map source the return type of the calling method
        ///// </summary>
        ///// <param name="source"></param>
        ///// <returns></returns>
        //dynamic MapToReturn(object source);
    }

    public class Mapper : IMap
    {
        public T Map<T>(object source) where T : class
        {
            if (source.GetType() == typeof(T))
            {
                return source as T;
            }

            return AutoMapper.Mapper.Map<T>(source);
        }

        /// <summary>
        /// Tries to map source the return type of the calling method
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public dynamic MapToReturn(object source)
        {
            // Get callstack
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();

            Type returnType = null;
            int i = 1;
            while (true)
            {
                var method = stackFrames[i].GetMethod() as MethodInfo;
                returnType = method.ReturnType;

                if (returnType.FullName == null || returnType.IsEquivalentTo(typeof(System.Object)))
                {
                    i++;
                }
                else
                {
                    break;
                }
            }

            return AutoMapper.Mapper.Map(source, source.GetType(), returnType);
        }
    }
}