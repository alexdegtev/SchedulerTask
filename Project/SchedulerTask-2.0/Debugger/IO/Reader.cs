using System;
using System.Collections.Generic;
using System.Xml.Linq;
using CommonTypes.Decision;
using CommonTypes.Equipment;
using CommonTypes.Operation;
using CommonTypes.Party;

namespace Debugger.IO
{
    public class Reader
    {
        private string binput;
        private XDocument ddata;
        
        /// <summary>
        /// Конструктор ридера
        /// </summary>
        /// <param name="folderPatch">Путь к директории построенного расписания</param>
        public Reader(string binput, string dinput)
        {
            try
            {
                ddata = XDocument.Load(dinput + "tech+solution.xml");
            }
            catch (System.Exception e)
            {
                throw new System.Exception("По указанному пути не найден файл tech+solution.xml");
            }
            
            this.binput = binput;
        }

        /// <summary>
        /// Считать данные
        /// </summary>
        /// <param name="decisions">Список операций с построенным расписанием</param>
        public void ReadData(out List<IDecision> decisions)
        {
           
            decisions = new List<IDecision>();
            Builder.IO.Reader.SetFolderPath(binput);
            List<IParty> partys;
            Dictionary<int, IOperation> opdic;
            Dictionary<int, IEquipment> eqdic;
            Builder.IO.Reader.ReadData(out partys, out opdic, out eqdic);
            XElement root = ddata.Root;
            XNamespace df = root.Name.Namespace;
            foreach (XElement elm in root.Descendants(df + "Product"))
            {
                foreach (XElement part in elm.Elements(df + "Part"))
                {
                    foreach (XElement op in part.Elements(df + "Operation"))
                    {
                        DateTime sdate = DateTime.Parse(op.Attribute("date_begin").Value);

                        DateTime edate = DateTime.Parse(op.Attribute("date_end").Value);
                        decisions.Add(new Decision(
                            sdate, 
                            edate, 
                            (SingleEquipment)eqdic[Convert.ToInt32(op.Attribute("equipment").Value)], 
                            (Operation)opdic[Convert.ToInt32(op.Attribute("id").Value)]
                        ));
                    }
                    foreach (XElement subpart in part.Elements(df + "SubPart"))
                    {
                        foreach (XElement op in subpart.Elements(df + "Operation"))
                        {
                            DateTime sdate = DateTime.Parse(op.Attribute("date_begin").Value);

                            DateTime edate = DateTime.Parse(op.Attribute("date_end").Value);
                            decisions.Add(new Decision(
                                sdate, 
                                edate, 
                                (SingleEquipment)eqdic[Convert.ToInt32(op.Attribute("equipment").Value)],
                                (Operation)opdic[Convert.ToInt32(op.Attribute("id").Value)]
                            ));
                        }
                    }
                }
            }
        }
    }
}
