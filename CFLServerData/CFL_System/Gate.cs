using System;
using System.IO;
using System.Windows;
using CFL_1.CFLGraphics;
using CFL_1.CFL_Data;
using MSTD;
using System.Collections.Generic;

namespace CFL_1.CFL_System
{
    public static class Gate
    {
        /// <summary>
        /// Crèe si necessaire et retourne le chemin vers le dossier pour la sauvegarde de données nécessaires en local,
        /// (ex avant connection, comme la config qui donne les éléments de connection à la DB).
        /// </summary>
        /// <returns></returns>
        public static string localPath()
        {
            string _dir = string.Empty;
            try
            {
                /* si l'on décide que le dossier local soit dans le dossier d'instalation de l'appli :
                 * dir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\CFL_local" */
                _dir = @"C:\CFL_local";
                Directory.CreateDirectory(_dir);
                return _dir;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.TargetSite + " : " + e.Message + "\n Dir path : " + _dir);
                return "";
            }
        }

        public static string configPath()
        {
            return Path.Combine(localPath(), "Config.txt");
        }

        public static class load
        {
            public static bool config(ref CFLConfig _config)
            {
                if(!File.Exists(configPath()))
                    return false;
                _config = JSONRelation<CFLConfig>.deserialize(File.ReadAllText(Path.Combine(localPath(), "Config.txt")));
                return true;
            }

            public static bool Communes(ref List<Tuple<string, string>> _list)
            {
                _list = new List<Tuple<string, string>>();
                string _path = localPath();
                string _path_noms = _path + @"\cfl_Communes";
                string _path_cp = _path + @"\cfl_CodesPostaux";
                if(!File.Exists(_path_noms) || !File.Exists(_path_cp))
                {
                    MessageBox.Show(@"Fichier d'initialisation des communes manquants : " + Environment.NewLine +
                                     _path_noms + Environment.NewLine + 
                                     _path_cp );
                    return false;
                }

                string _nom = "";
                string _cp = "";

                try
                {
                    StreamReader _sr_noms = new StreamReader(_path_noms);
                    StreamReader _sr_cp = new StreamReader(_path_cp);

                     while((_nom = _sr_noms.ReadLine()) != null)
                    {
                        if((_cp = _sr_cp.ReadLine()) == null)
                        {
                            MessageBox.Show("Fichier cfl_CodesPostaux corrompu.");
                            return false;
                        }
                        _list.Add(new Tuple<string, string>(_nom, _cp));
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show("Gate.load.communes(ref List<Tuple<string, string>> _list) : " + e.Message);
                    return false;
                }
                return true;
            }

            
        } 

        public static class save
        {
            public static void config(CFLConfig _config)
            {
                File.WriteAllText(configPath(), JSONRelation<CFLConfig>.serialize(_config));
            }
        }
    }
}
