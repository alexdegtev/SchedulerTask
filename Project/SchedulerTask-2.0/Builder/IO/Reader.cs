using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Builder;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace Debugger.IO
{
    public class Reader
    {
        List<Decision> decision;
        FileStream fs;
        string binput;
        XDocument ddata;
        
        /// <summary>
        /// Конструктор ридера
        /// </summary>
        /// <param name="folderPatch" путь к директории построенного расписания></param>
        public Reader(string binput, string dinput)
        {
            fs = new FileStream(dinput + "tech+soultuion.xml", FileMode.Open);
            ddata = XDocument.Load(dinput + "tech + soultuion.xml");

        }
        /// <summary>
        /// Считать данные
        /// </summary>
        /// <param name="decision" Список опердаций с построенным расписанием></param>
        public void ReadData(out List<Decision> decision)
        {
           
            decision = null;
            Builder.IO.Reader.SetFolderPath(binput);
            List<Party> partys;
            Dictionary<int, Builder.IOperation> opdic;
            Dictionary<int, Builder.Equipment.IEquipment> eqdic;
            Builder.IO.Reader.ReadData(out partys, out opdic, out eqdic);
            XElement root = ddata.Root;
            XNamespace df = ddata.Root.Name.Namespace;
            foreach (XElement elm in root.Descendants(df + "EquipmentInformation").Elements(df + "Product"))
            {
                foreach (XElement part in elm.Elements(df + "Part"))
                {
                    foreach (XElement op in part.Elements(df + "Operation"))
                    {
                        DateTime sdate = DateTime.Parse(op.Attribute("date_begin").Value);

                        DateTime edate = DateTime.Parse(op.Attribute("date_end").Value);
                        decision.Add(new Decision(sdate,edate,(Builder.Equipment.SingleEquipment)eqdic[Convert.ToInt32(op.Attribute("equipment").Value)],(Builder.Operation)opdic[Convert.ToInt32(Regex.Replace(op.Attribute("name").Value, @"[^\d]+", ""))]));
                    }
                    foreach (XElement subpart in part.Elements(df + "SubPart"))
                    {
                        foreach (XElement op in subpart.Elements(df + "Operation"))
                        {
                            DateTime sdate = DateTime.Parse(op.Attribute("date_begin").Value);

                            DateTime edate = DateTime.Parse(op.Attribute("date_end").Value);
                            decision.Add(new Decision(sdate, edate, (Builder.Equipment.SingleEquipment)eqdic[Convert.ToInt32(op.Attribute("equipment").Value)], (Builder.Operation)opdic[Convert.ToInt32(Regex.Replace(op.Attribute("name").Value, @"[^\d]+", ""))]));
                        }
                    }
                }
            }

        }

     
    }
    
}
