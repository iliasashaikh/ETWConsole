using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trycatchthat.Util
{
    public interface IFastMethod
    {
        TReturn RunMethod<TSource, TReturn>(string methodName, TSource source, object[] paramTypes);
    }

    public class FastMethod : IFastMethod
    {

        static Dictionary<(Type, string), Delegate> delegateCache = new Dictionary<(Type, string), Delegate>();
        public TReturn RunMethod<TSource, TReturn>(string methodName, TSource source, object[] args)
        {
            var t = typeof(TSource);
            if (delegateCache.TryGetValue((t, methodName), out Delegate d))
                return (TReturn)d.DynamicInvoke(args);


            var mi = t.GetMethod(methodName, System.Reflection.BindingFlags.Instance |
                                    System.Reflection.BindingFlags.Static |
                                    System.Reflection.BindingFlags.NonPublic |
                                    System.Reflection.BindingFlags.Public );
            int numArgs = args?.Length ?? 0;
            bool isStatic = mi.IsStatic;

            if ((source == null) && (!mi.IsStatic))
                throw new InvalidOperationException("An instance method requires an instance to be provided.");

            switch (numArgs)
            {
                case 0:
                    d = mi.CreateDelegate(typeof(Func<TReturn>), source);
                    break;
                case 1:
                    d = mi.CreateDelegate(typeof(Func<TReturn, object>),source);
                    break;
                case 2:
                    d = mi.CreateDelegate(typeof(Func<TReturn, object, object>), source);
                    break;
                case 3:
                    d = mi.CreateDelegate(typeof(Func<TReturn, object, object, object>), source);
                    break;
                case 4:
                    d = mi.CreateDelegate(typeof(Func<TReturn, object, object,object, object>), source);
                    break;
                default:
                    throw new NotImplementedException();
            }

            delegateCache[(t, methodName)] = d;
            return (TReturn)d.DynamicInvoke(args);
        }
    }
}
