using System;
using zyh1005.model;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace model
{
    class Class2
    {
        static void Main(string[] args)
        {
            var nodes = findOpenData();
            showOpenData(nodes);
            Console.ReadKey();
        }

        static List<OpenData> findOpenData()
        {
            List<OpenData> result = new List<OpenData>();

            var xml = XElement.Load(@"Taichung.xml");
            var nodes = xml.Descendants("RECORD").ToList();

            for (var i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                OpenData item = new OpenData();

                item.名稱 = getValue(node, "名稱");
                item.地址 = getValue(node, "地址");
                item.電話 = getValue(node, "電話");
                item.傳真 = getValue(node, "傳真");
                item.服務區域 = getValue(node, "服務區域");


                result.Add(item);

            }
            return result;
        }

        private static string getValue(XElement node, string header)
        {
            return node.Element(header).Value;
        }

        public static void showOpenData(List<OpenData> nodes)
        {
            Console.WriteLine(string.Format("登記在台中市的就業服務據點，總共有{0}家", nodes.Count));
            nodes.GroupBy(node => node.服務區域).ToList()
                .ForEach(group =>
                {
                    var key = group.Key;
                    var allDatas = group.ToList();
                    var message = $"區域為:{key}的就業服務據點有{allDatas.Count()}家";
                    Console.WriteLine(message);
                });


        }
    }
}
