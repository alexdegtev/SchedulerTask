using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Builder;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Debugger.IO
{
    public class Reader
    {
        List<Decision> decision;
        FileStream fs;
        /// <summary>
        /// Конструктор ридера
        /// </summary>
        /// <param name="folderPatch" путь к директории построенного расписания></param>
        public Reader(string folderPatch)
        {
            fs = new FileStream(folderPatch + "tech+soultuion.xml", FileMode.Open);

        }
        /// <summary>
        /// Считать данные
        /// </summary>
        /// <param name="decision" Список опердаций с построенным расписанием></param>
        public void ReadData(out List<Decision> decision)
        {
            decision = null;
            XmlSerializer formatter = new XmlSerializer(typeof(Product));
            Product product = (Product)formatter.Deserialize(fs);

        }

        private TimeSpan CreateSpan(int k)
        {
            return new TimeSpan(0, k, 0);
        }
        private DateTime CreateDate(string str)
        {
            return new DateTime();
        }
    }
    [Serializable, XmlType("Product"), XmlRoot("InformationModel")]
    public class Product
    {
        [XmlElement("Part")]
        public List<Part> parts { get; set; }

        [XmlAttribute("name")]
        public string name { get; set; }

        public Product()
        {
            parts = new List<Part>();

        }
    }


    [Serializable, XmlType("Part")]
    public class Part
    {
        [XmlAttribute("date_begin")]
        public string date_begin { get; set; }
        [XmlAttribute("date_end")]
        public string date_end { get; set; }
        [XmlAttribute("name")]
        public string name { get; set; }
        [XmlAttribute("priority")]
        public int priority { get; set; }
        [XmlAttribute("num_products")]
        public int num_products { get; set; }
        [XmlAttribute("num_plate")]
        public int num_plate { get; set; }

        [XmlElement("SubPart")]
        public List<Part> subs { get; set; }

        [XmlElement("Operation")]
        List<operation> ops { get; set; }
        public Part() { ops = new List<operation>(); subs = new List<Part>(); }
    }

    [Serializable, XmlType("Operation")]
    public class operation
    {
        [XmlAttribute("date_begin")]
        public string date_begin { get; set; }
        [XmlAttribute("date_end")]
        public string date_end { get; set; }
        [XmlAttribute("name")]
        public string name { get; set; }
        [XmlAttribute("duration")]
        public int duration { get; set; }
        [XmlAttribute("equipment")]
        public int equipment { get; set; }
        [XmlAttribute("id")]
        public int id { get; set; }
        [XmlAttribute("state")]
        public string state { get; set; }
        [XmlElement("Previous")]
        public List<Previous> prevs { get; set; }
        public operation() { prevs = new List<Previous>(); }
    }
    [Serializable, XmlType("Previous")]
    public class Previous
    {
        [XmlAttribute("id")]
        public int id { get; set; }
        public Previous() { }
    }
}
