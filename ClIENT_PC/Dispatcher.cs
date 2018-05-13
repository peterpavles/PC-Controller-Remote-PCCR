using ClIENT_PC.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ClIENT_PC.Controller
{
    class Dispatcher
    {
        private HttpRequester hr = new HttpRequester();
        
        public string Dispatch(dynamic Objet)
        {
            var serializer = new JavaScriptSerializer();
            dynamic req = serializer.DeserializeObject(Objet["content"]);

            List<string> funcParam = new List<string>();
            foreach (var item in req["parameters"])
            {
                funcParam.Add(item);
            }

            return Call(LcFirst(req["controller"]) + "Controller", UcFirst(req["method"]), funcParam.ToArray());
        }

        public string Call(string className, string methodName, params object[] parameters)
        {
            methodName = (string.IsNullOrEmpty(methodName)) ? UcFirst(className) : methodName;
            Type type;
            
            if (GetType("ClIENT_PC.Controller." + UcFirst(className), out type))
            {
                object instance = Activator.CreateInstance(type, null);
                //Mettre form dans controller
                methodName = UcFirst(methodName);
                MethodInfo method = type.GetMethod(methodName);

                if (method != null)
                {
                    ParameterInfo[] paramsInfos = method.GetParameters();

                    //The invoke does NOT work it throws "Object does not match target type"   
                    if (method.ReturnType == typeof(void))
                    {
                        method.Invoke(instance, (object[])parameters);
                    }
                    else
                    {
                        var returnValue = method.Invoke(instance, paramsInfos.Length == 0 ? null : (object[])parameters);
                        return returnValue.ToString();                   
                    }
                }
                else
                {
                    throw new System.ArgumentException("Methode non trouvé", methodName);
                }
            }
            else
            {
                throw new System.ArgumentException("Instance non trouvé", className);
            }
            return null;
        }

        public string Send(string post)
        {
            string response = hr.post(Router.url("send_result"), post);
            return response;
        }

        public string Get()
        {
            return hr.get(Router.url("get_command"));
        }

        static Dictionary<string, Type> typeCache = new Dictionary<string, Type>();
        public static bool GetType(string typeName, out Type t)
        {
            lock (typeCache)
            {
                if (!typeCache.TryGetValue(typeName, out t))
                {
                    foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        t = a.GetType(typeName);
                        if (t != null)
                            break;
                    }
                    typeCache[typeName] = t; // perhaps null
                }
            }
            return t != null;
        }
        private string UcFirst(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            return char.ToUpper(str[0]) + str.Substring(1);
        }
        private string LcFirst(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            return char.ToLower(str[0]) + str.Substring(1);
        }

    }
}
