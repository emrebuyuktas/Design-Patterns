using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CommandPattern.Commands
{
    public class ExcelFile<T>
    {
        public readonly List<T> _list;

        public string FileName => $"{typeof(T).Name}.xlsx";
        public string FileType => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public ExcelFile(List<T> list)
        {
            _list = list;
        }

        public MemoryStream Create()
        {
            var wb= new XLWorkbook();
            var ds = new DataSet();
            ds.Tables.Add(GetTable());
            wb.Worksheets.Add(ds);
            
            var excel= new MemoryStream();
            wb.SaveAs(excel);
            return excel;
        }

        private DataTable GetTable()
        {
            var table = new DataTable();

            var type = typeof(T);

            type.GetProperties().ToList().ForEach(x => table.Columns.Add(x.Name, x.PropertyType));

            _list.ForEach(x =>
            {
                var values = type.GetProperties().Select(properyInfo => properyInfo.GetValue(x, null)).ToArray();

                table.Rows.Add(values);
            });
            return table;
        }
    }
}
