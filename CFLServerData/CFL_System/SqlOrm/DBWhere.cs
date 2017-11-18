using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFL_1.CFL_System.SqlOrm
{
    /// <summary>
    /// Soit une composition de classes comme suit :
    /// 
    /// public classe Commune : Base
    /// {
    ///     public string Nom{ get; set; }
    /// }
    /// 
    /// public class Lieu : Base
    /// {
    ///     public string Adresse { get; set; }
    ///     public Commune Commune { get; set; }
    /// }
    /// 
    /// public class Action : Base
    /// {
    ///     public DateTime? Date { get; set; } 
    ///     public Lieu Lieu { get; set; }
    /// }
    /// 
    /// La table action sera être comme suit :
    /// 
    /// tablename | id | date | class_lieu_lieu |
    /// 
    /// Nous souhaitons obtenir les actions qui ont lieu le 01/01/2017 et
    /// dont la commune du Lieu a pour nom "Paris".
    /// 
    /// SELECT * FROM action WHERE date = '01/01/2017'
    ///                      AND class_lieu_lieu = 
    ///                                 (SELECT id FROM lieu WHERE class_commune_commune = 
    ///                                 (SELECT id FROM commune WHERE nom = 'Paris'));
    /// <=> select * from action where(Action.Date == "01/01/2017" and Action.Lieu.Commune.nom == "Paris")
    /// 
    /// </summary>
    public class DBWhere
    {
    }
}
