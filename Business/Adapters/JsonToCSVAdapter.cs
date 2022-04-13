using System.Text;
using System.Linq;
//using ChoETL;

namespace CSVConverter.Business.Adapter
{
    public class JsonToCSVAdapter : FileAdapterBase
    {
        public override string Convert(string content)
        {

            // JArray obj = JArray.Parse(content);

            //  // Collect column titles: all property names whose values are of type JValue, distinct, in order of encountering them.
            // var values = obj.DescendantsAndSelf()
            //         .OfType<JProperty>()
            //         .Where(p => p.Value is JValue)
            //         .Select(p => p.Name)
            //         .ToList();

            // var columns = values.ToArray();
            

            // // Collect all data rows: for every object, go through the column titles and get the value of that property in the closest ancestor or self that has a value of that name.
            // var rows = obj
            //     .DescendantsAndSelf()
            //     .OfType<JObject>()
            //     .Where(o => o.PropertyValues().OfType<JValue>().Any())
            //     .Select(o => columns.Select(c => o.AncestorsAndSelf()
            //         .OfType<JObject>()
            //         .Select(parent => parent[c])
            //         .Where(v => v is JValue)
            //         .Select(v => (string)v)
            //         .FirstOrDefault())
            //         .Reverse() // Trim trailing nulls
            //         .SkipWhile(s => s == null)
            //         .Reverse());

            // // Convert to CSV
            // var csvRows = new[] { columns }.Concat(rows).Select(r => string.Join(",", r));
            // var csv = string.Join("\n", csvRows);

            // return csv;


            var csv = new StringBuilder();            
             /*using (var r = ChoETL.ChoJSONReader.LoadText(content)
                 )
             {
                 using (var w = new ChoCSVWriter(csv)
                    .WithFirstLineHeader()
                    .Configure(c => c.QuoteChar = '\'')
                    )
                    w.Write(r.Select(r1 =>
                    {
                        IDictionary<string, object> dict = r1 as IDictionary<string, object>;
                        foreach (var key in dict.Keys)
                        {
                            Type mt = r1.GetMemberType(key);
                            if (mt != null && mt.IsSimple())
                            {

                            }
                            else
                            {
                                r1[key] = JsonConvert.SerializeObject(r1[key]);
                            }
                        }

                        return r1;
                    }));
            }*/

            return csv.ToString();

        }

    }
}