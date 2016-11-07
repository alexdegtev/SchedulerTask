using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using CommonTypes.Decision;
using CommonTypes.Equipment;
using CommonTypes.Operation;
using CommonTypes.Party;

namespace Debugger.IO
{
    public class Reader
    {
        //List<IDecision> decision;
        //FileStream fs;
        string binput;
        XDocument ddata;
        
        /// <summary>
        /// Конструктор ридера
        /// </summary>
        /// <param name="folderPatch">Путь к директории построенного расписания</param>
        public Reader(string binput, string dinput)
        {
            //fs = new FileStream(dinput + "tech+solution.xml", FileMode.Open);
            ddata = XDocument.Load(dinput + "tech+solution.xml");
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
            df = root.Name.Namespace;
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

            Dictionary<int, IOperation> operations = new Dictionary<int, IOperation>();
            DateTime begin = new DateTime();
            DateTime end = new DateTime();
            List<IOperation> tmpOperationsp;
            string datapattern = "dd.MM.yyyy";
        //    foreach (XElement product in root.Descendants(df + "Product"))
        //    {
        //        foreach (XElement part in product.Elements(df + "Part"))
        //        {
        //            DateTime.TryParseExact(part.Attribute("date_begin").Value, datapattern, null, System.Globalization.DateTimeStyles.None, out begin);
        //            DateTime.TryParseExact(part.Attribute("date_end").Value, datapattern, null, System.Globalization.DateTimeStyles.None, out end);
        //            Party parent = new Party(begin, end, int.Parse(part.Attribute("priority").Value), part.Attribute("name").Value, int.Parse(part.Attribute("num_products").Value));
        //            tmpOperationsp = ReadOperations(part, parent, operations);
        //            foreach (IOperation op in tmpOperationsp)
        //            {
        //                parent.AddOperationToForParty(op);
        //            }
        //            foreach (XElement subpart in part.Elements(df + "SubPart"))
        //            {
        //                Party sp = new Party(subpart.Attribute("name").Value, int.Parse(subpart.Attribute("num_products").Value));
        //                tmpOperationsp = ReadOperations(subpart, parent, operations);
        //                foreach (IOperation op in tmpOperationsp)
        //                {
        //                    sp.AddOperationToForParty(op);
        //                }
        //                parent.AddSubParty(sp);
        //                //partys.Add(sp);
        //            }
        //            partys.Add(parent);
        //        }
        //    }
        }

        XNamespace df;

        //private List<IOperation> ReadOperations(XElement part, Party parent, Dictionary<int, IOperation> opdic)
        //{
        //    Dictionary<int, List<int>> pop = new Dictionary<int, List<int>>();
        //    List<IOperation> tmpop = new List<IOperation>();
        //    foreach (XElement oper in part.Elements(df + "Operation"))
        //    {
        //        List<int> tmp_ = new List<int>();
        //        if (oper.Elements(df + "Previous") != null)
        //        {
        //            foreach (XElement prop in oper.Elements(df + "Previous"))
        //            {
        //                tmp_.Add(int.Parse(prop.Attribute("id").Value));
        //            }
        //            pop.Add(int.Parse(oper.Attribute("id").Value), tmp_);
        //        }

        //        int id = int.Parse(oper.Attribute("id").Value);
        //        int duration = int.Parse(oper.Attribute("duration").Value);
        //        int group = int.Parse(oper.Attribute("equipmentgroup").Value);
        //        string name = oper.Attribute("name").Value;
        //        TimeSpan duration_t = new TimeSpan(duration, 0, 0);
        //        IEquipment equipment_ = equipments[group];



        //        Operation tmp = new Operation(id, name, duration_t, new List<IOperation>(), equipment_, parent);
        //        tmpop.Add(tmp);
        //        opdic.Add(id, tmp);
        //    }
        //    foreach (IOperation o in tmpop)
        //    {
        //        if (pop[o.GetId()] != null)
        //        {
        //            for (int i = 0; i < pop[o.GetId()].Count; i++)
        //            {
        //                o.AddPrevOperation(opdic[pop[o.GetId()][i]]);
        //            }
        //        }
        //    }
        //    return tmpop;
        //}

    }
}
