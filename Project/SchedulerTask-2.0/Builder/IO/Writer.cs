using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;

namespace Builder.IO
{
    public class Writer
    {
        XDocument document;
        string folderPath;

        /// <summary>
        /// конструктор Writer
        /// </summary>
        /// <param name="output">Путь к сохранению</param>
        /// <param name="input">Путь к входным данным</param>
        /// 
        public Writer(string input, string output)
        {
            folderPath = output;
            if (File.Exists(output + "tech+solution.xml")) File.Delete(output + "tech+solution.xml");
            File.Copy(input + "tech.xml", output + "tech+solution.xml");
            document = XDocument.Load(output + "tech+solution.xml");
        }

        /// <summary>
        /// Записать результат
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
                string id = Convert.ToString(d.GetOperation().GetId());
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
                string id = Convert.ToString(d.GetOperation().GetId());
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
                WriteData(party.GetPartyOperations());
                foreach (var part in party.GetSubParty())
                {
                    WriteData(part.GetPartyOperations());
                }
            }
        }
    }
}
