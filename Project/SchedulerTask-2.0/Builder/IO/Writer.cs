using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Builder.IO
{
    public class Writer
    {
        /// <summary>
        /// конструктор Writer
        /// </summary>
        /// <param name="folderParth"> Путь к сохранению. </param>
        /// 

        XDocument document;
        string folderPath;

        public Writer(string folderParth)
        {
            this.folderPath = folderParth;
            //TODO: Создать файл
            document = XDocument.Load(folderParth + "tech+solution.xml");
            //document = new XDocument();
        }

        /// <summary>
        /// Записать результат.
        /// </summary>
        /// <param name="operations"></param>
        public void WriteData(Dictionary<int, IOperation> operations)
        {
            XElement root = document.Root;
            XNamespace df = root.Name.Namespace;
            foreach (KeyValuePair<int, IOperation> o in operations)
            {
                Decision d = o.Value.GetDecision();
                if (d == null) continue;
                string id = Convert.ToString(d.GetOperation().GetID());
                bool found = false;
                foreach (XElement product in root.Descendants(df + "Product"))
                {
                    foreach (XElement part in product.Elements(df + "Part"))
                    {
                        foreach (XElement op in part.Elements(df + "Operation"))
                        {
                            if (op.Attribute("id").Value == id)
                            {
                                found = true;
                                op.Add(new XAttribute("equipment", d.GetEquipment().GetID()));
                                op.Add(new XAttribute("date_begin", d.GetStartTime()));
                                op.Add(new XAttribute("date_end", d.GetEndTime()));
                                op.Attribute("state").Value = "SCHEDULED";
                                XAttribute attr = op.Attribute("equipmentgroup");
                                attr.Remove();
                                break;
                            }
                        }
                        if (found) break;
                        foreach (XElement sp in part.Elements(df + "SubPart"))
                        {
                            foreach (XElement op in sp.Elements(df + "Operation"))
                            {
                                if (op.Attribute("id").Value == id)
                                {
                                    found = true;
                                    op.Add(new XAttribute("equipment", d.GetEquipment().GetID()));
                                    op.Add(new XAttribute("date_begin", d.GetStartTime()));
                                    op.Add(new XAttribute("date_end", d.GetEndTime()));
                                    XAttribute attr = op.Attribute("equipmentgroup");
                                    attr.Remove();
                                    op.Attribute("state").Value = "SCHEDULED";
                                    break;
                                }
                            }
                            if (found) break;
                        }
                        if (found) break;
                    }
                    if (found) break;
                }
            }
            document.Save(folderPath + "tech+solution.xml");

        }

        public void WriteData(List<IOperation> operations)
        {
            XElement root = document.Root;
            XNamespace df = root.Name.Namespace;
            foreach (IOperation o in operations)
            {
                Decision d = o.GetDecision();
                if (d == null) continue;
                string id = Convert.ToString(d.GetOperation().GetID());
                bool found = false;
                foreach (XElement product in root.Descendants(df + "Product"))
                {
                    foreach (XElement part in product.Elements(df + "Part"))
                    {
                        foreach (XElement op in part.Elements(df + "Operation"))
                        {
                            if (op.Attribute("id").Value == id)
                            {
                                found = true;
                                op.Add(new XAttribute("equipment", d.GetEquipment().GetID()));
                                op.Add(new XAttribute("date_begin", d.GetStartTime()));
                                op.Add(new XAttribute("date_end", d.GetEndTime()));
                                op.Attribute("state").Value = "SCHEDULED";
                                XAttribute attr = op.Attribute("equipmentgroup");
                                attr.Remove();
                                break;
                            }
                        }
                        if (found) break;
                        foreach (XElement sp in part.Elements(df + "SubPart"))
                        {
                            foreach (XElement op in sp.Elements(df + "Operation"))
                            {
                                if (op.Attribute("id").Value == id)
                                {
                                    found = true;
                                    op.Add(new XAttribute("equipment", d.GetEquipment().GetID()));
                                    op.Add(new XAttribute("date_begin", d.GetStartTime()));
                                    op.Add(new XAttribute("date_end", d.GetEndTime()));
                                    XAttribute attr = op.Attribute("equipmentgroup");
                                    attr.Remove();
                                    op.Attribute("state").Value = "SCHEDULED";
                                    break;
                                }
                            }
                            if (found) break;
                        }
                        if (found) break;
                    }
                    if (found) break;
                }
            }
            document.Save(folderPath + "tech+solution.xml");

        }

        public void WriteData(List<Party> partys)
        {
            foreach (var party in partys)
            {
                WriteData(party.getPartyOperations());
                foreach (var part in party.getSubParty())
                {
                    WriteData(part.getPartyOperations());
                }
            }
        }
    }
}
