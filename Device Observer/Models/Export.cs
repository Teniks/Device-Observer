using ClosedXML.Excel;
using System;
using System.Windows.Controls;

namespace Device_Observer.Models
{
    internal class Export
    {
        public static void ExportToExcel(DataGrid dataGrid, string fileName)
        {
            // Создание нового рабочего листа Excel
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Данные");

            // Добавление заголовков столбцов
            for (int i = 0; i < dataGrid.Columns.Count; i++)
            {
                worksheet.Cell(1, i + 1).Value = dataGrid.Columns[i].Header.ToString();
            }

            // Добавление данных из DataGrid
            int row = 2;
            foreach (var item in dataGrid.ItemsSource)
            {
                for (int i = 0; i < dataGrid.Columns.Count; i++)
                {
                    // Получение значения ячейки
                    var cellValue = dataGrid.Columns[i].GetCellContent(item).ToString();

                    // Запись значения в ячейку
                    worksheet.Cell(row, i + 1).Value = cellValue;
                }
                row++;
            }

            // Сохранение файла Excel
            workbook.SaveAs(fileName);
        }
    }
}
