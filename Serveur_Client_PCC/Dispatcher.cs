using Serveur_Client_PCC.Communication;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Serveur_Client_PCC
{
    class Dispatcher
    {
        public PanelControl formPanel;
        public static JavaScriptSerializer serializer = new JavaScriptSerializer();

        public Dispatcher(PanelControl form)
        {
            formPanel = form;
        }

        public void Dispatch(dynamic Objet)
        {
            var serializer = new JavaScriptSerializer();
            dynamic req = serializer.DeserializeObject(Objet["content"]);
            formPanel.setStatus("Réponse reçus resolution en cours...");
            formPanel.setEvent("Reçus " + Serveur_Client_PCC.Controller.FichiersController.getLength(Objet["content"].Length.ToString()));

            callMethod(UcFirst(req["controller"]) + "Controller", UcFirst(req["method"]), req["data"]);

            Thread.Sleep(1000);
            formPanel.setStatus("");
        }

        public string Request(string className, string methodName = null, params object[] parameters)
        {
            methodName = (string.IsNullOrEmpty(methodName)) ? UcFirst(className) : methodName;
            Type type;
            if (GetType("Serveur_Client_PCC.Controller." + UcFirst(className) + "Controller", out type))
            {
                object instance = Activator.CreateInstance(type, null);
                //Mettre form dans controller
                MethodInfo method = type.GetMethod("setThis");
                method.Invoke(instance, new object[] { formPanel });

                method = type.GetMethod("Req" + methodName);
                object result = null;
                //string args = ControllerBase.Encode(parameters);

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
                        result = method.Invoke(instance, parameters.Length == 0 ? null : (object[])parameters);
                    }
                }
                else
                {
                    throw new System.ArgumentException("Methode non trouvé", methodName);
                }
                return (string)result;
            }
            else
            {
                throw new System.ArgumentException("Instance non trouvé", className);
            }
            return null;
        }
        public void callMethod(string className, string methodName, string parameters)
        {
            methodName = (string.IsNullOrEmpty(methodName)) ? UcFirst(className) : methodName;
            Type type;
            if (GetType("Serveur_Client_PCC.Controller." + className, out type))
            {
                object instance = Activator.CreateInstance(type, null);
                MethodInfo method = type.GetMethod("setThis");
                method.Invoke(instance, new object[] { formPanel });
                method = type.GetMethod(methodName);

                if (method != null)
                {
                    ParameterInfo[] paramsInfos = method.GetParameters();
                    object param;

                    switch (paramsInfos.Length)
                    {
                        case 0:
                            method.Invoke(instance, null);
                            break;
                        case 1:
                            param = new object[] { parameters };
                            method.Invoke(instance, (object[])param);
                            break;
                        default:
                            break;
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
        }

        public void decode(string blob)
        {
            dynamic data = serializer.DeserializeObject(blob);
            string json = Encryption.Decrypt(Parametre.encryptionKey, data["d"]);
            data = serializer.DeserializeObject(json);

            if (data["success"] == true)
            {
                setPcStatus(Convert.ToString(data["pc"]["last_time"]));
                foreach (dynamic item in data["results"])
                {
                    Dispatch(item);
                }
            }
            else
            {
                MessageBox.Show(data["message"], "Erreur lors du decodage d'un packet");
                Auth auth = new Auth();
                auth.reconnexion();
            }
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

        private void setPcStatus(string status_temp)
        {
            Int32 temp = Convert.ToInt32(status_temp);
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            string stat = (unixTimestamp - temp > 180) ? "Deconnecter" : "En ligne";

            try
            {
                formPanel.txt_pc_statut.Invoke((MethodInvoker)(() =>
                {
                    formPanel.txt_pc_statut.Text = stat;
                }));
            }
            catch
            { }
        }
    }
}
