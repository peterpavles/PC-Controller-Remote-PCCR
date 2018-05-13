using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ClIENT_PC
{
    class ControllerBase
    {
        protected HttpRequester hr = new HttpRequester();
        public JavaScriptSerializer serializer = new JavaScriptSerializer();

        public string Encode(object obj)
        {
            return serializer.Serialize(obj);
        }

        public void send(string data, bool field_pc_id = true)
        {
            if (field_pc_id)
            {
                data = "content=" + data;
            }
            string rps = hr.post(Router.url("send_command"), data);
        }

        public void CallMySubMethod(Type type, string data)
        {
            var serializer = new JavaScriptSerializer();
            dynamic obj = serializer.DeserializeObject(data);
            object instance = Activator.CreateInstance(type, null);
            MethodInfo method = type.GetMethod(obj["method"], BindingFlags.NonPublic | BindingFlags.Instance);

            if (method != null)
            {
                ParameterInfo[] paramsInfos = method.GetParameters();
                //object[] paramObj = new object[] { objet.Skip(1).Take(objet.Length).ToArray() };
                method.Invoke(instance, paramsInfos.Length == 0 ? null : obj["params"]);
            }
            else
            {
                throw new System.ArgumentException("Methode non trouvé", obj[0]);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod(int indexCaller = 2)
        {
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(indexCaller).GetMethod();
            return methodBase.Name;
        }
        
    }
}
