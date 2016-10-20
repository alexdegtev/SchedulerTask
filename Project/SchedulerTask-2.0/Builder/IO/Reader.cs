using Builder.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Globalization;
using Builder.TimeCalendar;


namespace Builder.IO
{
    public class Reader
    {
        /// <summary>
        /// Список для хранения партий
        /// </summary>
        List<Party> partys;

        /// <summary>
        /// 
        /// </summary>
        Dictionary<int, Operation> operations;

        /// <summary>
        /// 
        /// </summary>
        Dictionary<int, IEquipment> equipments;



        XDocument sdata;
        XDocument tdata;
        DateTime begin;
        DateTime end;
        string datapattern = "dd.MM.yyyy";
        string dtpattern = "MM.dd.yyy H:mm:ss";
        Dictionary<int, IEquipment> eqdic;
        Dictionary<int, IOperation> opdic;
        List<Party> partlist;
        XNamespace df;


        /// <summary>
        /// констуктор ридера
        /// </summary>
        /// <param name="folderPath"> Путь к .xml файлам.</param>
        public Reader(string folderPath)
        {

            sdata = XDocument.Load(folderPath + "system.xml");
            tdata = XDocument.Load(folderPath + "tech.xml");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="partys"> Список партий.</param>
        /// <param name="operations">Список операций.</param>
        /// <param name="equipments">Список оборудований.</param>
        public void ReadData(out List<Party> partys, out Dictionary<int, IOperation> operations, out Dictionary<int, IEquipment> equipments)
        {
            partys = null;
            operations = null;
            equipments = null;


            List<Interval> intlist = new List<Interval>();
            List<Interval> doneintlist = new List<Interval>();
            equipments = new Dictionary<int, IEquipment>();

            DateTime start = new DateTime();
            DateTime end = new DateTime();
            XElement root = sdata.Root;
            df = sdata.Root.Name.Namespace;

            foreach (XElement elm in root.Descendants(df + "CalendarInformation"))
            {
                if (elm.Attribute("date_begin") != null)
                {
                    string date = elm.Attribute("date_begin").Value;
                    DateTime.TryParseExact(date, datapattern, null, DateTimeStyles.None, out start);
                }
                if (elm.Attribute("date_end") != null)
                {
                    string date = elm.Attribute("date_end").Value;
                    DateTime.TryParseExact(date, datapattern, null, DateTimeStyles.None, out end);
                }
                foreach (XElement eg in elm.Elements(df + "EquipmentGroup"))
                {
                    foreach (XElement inc in eg.Elements(df + "Include"))
                    {
                        DateTime tmpdata = start;
                        while (tmpdata != end)
                        {
                            if ((int)tmpdata.DayOfWeek == int.Parse(inc.Attribute("day_of_week").Value))
                            {
                                int ind = inc.Attribute("time_period").Value.IndexOf("-");
                                int sh = int.Parse(inc.Attribute("time_period").Value.Substring(0, 1));
                                int eh = int.Parse(inc.Attribute("time_period").Value.Substring(ind + 1, 2));

                                intlist.Add(new Interval(new DateTime(tmpdata.Year, tmpdata.Month, tmpdata.Day, sh, 0, 0), new DateTime(tmpdata.Year, tmpdata.Month, tmpdata.Day, eh, 0, 0)));
                            }
                            tmpdata = tmpdata.AddDays(1);
                        }
                    }
                    foreach (XElement exc in eg.Elements(df + "Exclude"))
                    {

                        foreach (Interval t in intlist)
                        {
                            if ((int)t.GetStartTime().DayOfWeek == int.Parse(exc.Attribute("day_of_week").Value))
                            {
                                int ind = exc.Attribute("time_period").Value.IndexOf("-");
                                int sh = int.Parse(exc.Attribute("time_period").Value.Substring(0, 2));
                                int eh = int.Parse(exc.Attribute("time_period").Value.Substring(ind + 1, 2));

                                DateTime dt = t.GetStartTime().AddHours(-t.GetStartTime().Hour);
                                Interval tmpint;
                                doneintlist.Add(SeparateInterval(t, dt.AddHours(sh), dt.AddHours(eh), out tmpint));
                                doneintlist.Add(tmpint);

                            }
                        }
                    }
                }


            }

            Calendar calendar = new Calendar(doneintlist);

            foreach (XElement elm in root.Descendants(df + "EquipmentInformation").Elements(df + "EquipmentGroup"))
            {
                GroupEquipment tmp = new GroupEquipment(calendar, int.Parse(elm.Attribute("id").Value), elm.Attribute("name").Value);
                foreach (XElement eg in elm.Elements(df + "EquipmentGroup"))
                {
                    GroupEquipment gtmp = new GroupEquipment(calendar, int.Parse(eg.Attribute("id").Value), eg.Attribute("name").Value);
                    foreach (XElement eq in eg.Elements(df + "Equipment"))
                    {
                        SingleEquipment stmp = new SingleEquipment(calendar, int.Parse(eq.Attribute("id").Value), eq.Attribute("name").Value);
                        equipments.Add(stmp.GetID(), stmp);
                        gtmp.AddEquipment(stmp);
                    }
                    tmp.AddEquipment(gtmp);
                    equipments.Add(gtmp.GetID(), gtmp);
                }
                equipments.Add(tmp.GetID(), tmp);
            }

            root = tdata.Root;
            df = root.Name.Namespace;
            partys = new List<Party>();
           
            operations = new Dictionary<int, IOperation>();
            List<IOperation> tmpop;
            foreach (XElement product in root.Descendants(df + "Product"))
            {
                foreach (XElement part in product.Elements(df + "Part"))
                {
                    DateTime.TryParseExact(part.Attribute("date_begin").Value, datapattern, null, DateTimeStyles.None, out begin);
                    DateTime.TryParseExact(part.Attribute("date_end").Value, datapattern, null, DateTimeStyles.None, out end);
                    Party parent = new Party(begin, end, int.Parse(part.Attribute("priority").Value), part.Attribute("name").Value, int.Parse(part.Attribute("num_products").Value));
                    tmpop = ReadOperations(part, parent, operations);
                    foreach (IOperation op in tmpop)
                    {
                        parent.addOperationToForParty(op);
                    }
                    foreach (XElement subpart in part.Elements(df + "SubPart"))
                    {
                        Party sp = new Party(subpart.Attribute("name").Value, int.Parse(subpart.Attribute("num_products").Value));
                        tmpop = ReadOperations(subpart, parent, operations);
                        foreach (IOperation op in tmpop)
                        {
                            sp.addOperationToForParty(op);
                        }
                        parent.addSubPArty(sp);
                        //partys.Add(sp);
                    }
                    partys.Add(parent);
                }
            }
            

        }


        private List<IOperation> ReadOperations(XElement part, Party parent, Dictionary<int, IOperation> opdic)
        {
            List<IOperation> tmpop = new List<IOperation>();
            foreach (XElement oper in part.Elements(df + "Operation"))
            {
                List<IOperation> pop = new List<IOperation>();
                if (oper.Elements(df + "Previous") != null)
                {
                    foreach (XElement prop in oper.Elements(df + "Previous"))
                    {
                        pop.Add(opdic[int.Parse(prop.Attribute("id").Value)]);
                    }
                }
                int id = int.Parse(oper.Attribute("id").Value);
                int duration = int.Parse(oper.Attribute("duration").Value);
                int group = int.Parse(oper.Attribute("equipmentgroup").Value);
                string name = oper.Attribute("name").Value;
                TimeSpan duration_t=new TimeSpan(duration, 0, 0);
                IEquipment equipment_ = eqdic[group];
                Operation tmp = new Operation(id, name,duration_t , pop, equipment_, parent);
                tmpop.Add(tmp);//new Operation(id, oper.Attribute("name").Value, new TimeSpan(duration, 0, 0), pop, eqdic[group], parent));
                opdic.Add(id, new Operation(id, oper.Attribute("name").Value, new TimeSpan(duration, 0, 0), pop, eqdic[group], parent));
            }
            return tmpop;

        }
        private Interval SeparateInterval(Interval ii, DateTime start, DateTime end, out Interval oi)
        {
            oi = new Interval(end, ii.GetEndTime());
            return new Interval(ii.GetStartTime(), start);
        }
    }
}

