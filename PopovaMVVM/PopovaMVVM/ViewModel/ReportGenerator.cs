using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using Excel = Microsoft.Office.Interop.Excel;

namespace PopovaMVVM.ViewModel
{
    public class ReportGenerator
    {
        private readonly AppDbContext _context;

        public ReportGenerator(AppDbContext context)
        {
            _context = context;
        }

        // Общий метод для создания Excel-приложения
        private Excel.Application CreateExcelApp()
        {
            return new Excel.Application()
            {
                Visible = false,
                DisplayAlerts = false
            };
        }

        private void ReleaseExcelObjects(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.FinalReleaseComObject(obj);
                }
            }
            catch { }
            finally
            {
                obj = null;
            }
        }

        // Метод для создания всех отчетов в одном файле
        public void GenerateAllReportsInOneFile(string filePath)
        {
            Excel.Application app = null;
            Excel.Workbook workbook = null;

            try
            {
                app = CreateExcelApp();
                workbook = app.Workbooks.Add();

                // Создаем листы для каждого отчета
                Excel.Worksheet positionSheet = (Excel.Worksheet)workbook.Sheets.Add();
                positionSheet.Name = "Должности";
                GeneratePositionReport(positionSheet);

                Excel.Worksheet combinedSheet = (Excel.Worksheet)workbook.Sheets.Add();
                combinedSheet.Name = "Сводный отчет";
                GenerateCombinedReport(combinedSheet);

                Excel.Worksheet groupedSheet = (Excel.Worksheet)workbook.Sheets.Add();
                groupedSheet.Name = "Группировка по отделам";
                GenerateEmployeeChildrenReport(groupedSheet);

                // Удаляем стандартный пустой лист
                ((Excel.Worksheet)workbook.Sheets[1]).Delete();

                // Сохранение
                workbook.SaveAs(filePath);
            }
            finally
            {
                // Освобождение ресурсов в правильном порядке
                if (workbook != null)
                {
                    workbook.Close(false);
                    ReleaseExcelObjects(workbook);
                }
                if (app != null)
                {
                    app.Quit();
                    ReleaseExcelObjects(app);
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        // 1. Отчет по должностям (для листа)
        private void GeneratePositionReport(Excel.Worksheet sheet)
        {
            Excel.Range range = null;

            try
            {
                var positions = _context.Positions
                    .Include(p => p.Department)
                    .ToList();

                // Заголовок отчета
                range = sheet.Range["A1", "C1"];
                range.Merge();
                range.Value = "Справочник должностей";
                range.Font.Bold = true;
                range.Font.Size = 14;
                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                range.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                range.Interior.Color = Excel.XlRgbColor.rgbLightGray;
                ReleaseExcelObjects(range);

                // Заголовки столбцов
                string[] headers = { "№", "Наименование должности", "Отдел" };
                for (int i = 0; i < headers.Length; i++)
                {
                    range = sheet.Cells[3, i + 1];
                    range.Value = headers[i];
                    range.Font.Bold = true;
                    range.Borders.Weight = Excel.XlBorderWeight.xlThin;
                    range.Interior.Color = Excel.XlRgbColor.rgbGainsboro;
                    ReleaseExcelObjects(range);
                }

                // Данные
                int row = 4;
                foreach (var pos in positions)
                {
                    range = sheet.Cells[row, 1];
                    range.Value = row - 3;
                    ReleaseExcelObjects(range);

                    range = sheet.Cells[row, 2];
                    range.Value = pos.Title;
                    ReleaseExcelObjects(range);

                    range = sheet.Cells[row, 3];
                    range.Value = pos.Department?.Name;
                    ReleaseExcelObjects(range);

                    // Форматирование строки
                    for (int col = 1; col <= 3; col++)
                    {
                        range = sheet.Cells[row, col];
                        range.Borders.Weight = Excel.XlBorderWeight.xlThin;
                        ReleaseExcelObjects(range);
                    }
                    row++;
                }

                // Итоговая строка
                range = sheet.Range[$"A{row}", $"B{row}"];
                range.Merge();
                range.Value = "ИТОГО:";
                range.Font.Bold = true;
                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                ReleaseExcelObjects(range);

                range = sheet.Cells[row, 3];
                range.Value = $"{positions.Count} должностей";
                range.Font.Bold = true;
                ReleaseExcelObjects(range);

                // Автоподбор ширины столбцов
                sheet.Columns.AutoFit();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка генерации отчета по должностям: {ex.Message}");
            }
        }

        // 2. Сводный отчет (для листа)
        private void GenerateCombinedReport(Excel.Worksheet sheet)
        {
            Excel.Range range = null;

            try
            {
                var employees = _context.Employees
                    .Include(e => e.Position)
                    .ThenInclude(p => p.Department)
                    .Select(e => new
                    {
                        e.LastName,
                        e.FirstName,
                        e.MiddleName,
                        Position = e.Position.Title,
                        Department = e.Position.Department.Name,
                        e.Salary
                    })
                    .ToList();

                // Заголовок
                range = sheet.Range["A1", "G1"];
                range.Merge();
                range.Value = "Сводный отчет по сотрудникам";
                range.Font.Size = 14;
                range.Font.Bold = true;
                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                range.Interior.Color = Excel.XlRgbColor.rgbLightSteelBlue;
                ReleaseExcelObjects(range);

                // Заголовки столбцов
                string[] headers = { "№", "Фамилия", "Имя", "Отчество", "Должность", "Отдел", "Оклад" };
                for (int i = 0; i < headers.Length; i++)
                {
                    range = sheet.Cells[3, i + 1];
                    range.Value = headers[i];
                    range.Font.Bold = true;
                    range.Borders.Weight = Excel.XlBorderWeight.xlThin;
                    range.Interior.Color = Excel.XlRgbColor.rgbLightGray;
                    ReleaseExcelObjects(range);
                }

                // Данные
                int row = 4;
                decimal totalSalary = 0;
                foreach (var emp in employees)
                {
                    range = sheet.Cells[row, 1];
                    range.Value = row - 3;
                    ReleaseExcelObjects(range);

                    range = sheet.Cells[row, 2];
                    range.Value = emp.LastName;
                    ReleaseExcelObjects(range);

                    range = sheet.Cells[row, 3];
                    range.Value = emp.FirstName;
                    ReleaseExcelObjects(range);

                    range = sheet.Cells[row, 4];
                    range.Value = emp.MiddleName;
                    ReleaseExcelObjects(range);

                    range = sheet.Cells[row, 5];
                    range.Value = emp.Position;
                    ReleaseExcelObjects(range);

                    range = sheet.Cells[row, 6];
                    range.Value = emp.Department;
                    ReleaseExcelObjects(range);

                    range = sheet.Cells[row, 7];
                    range.Value = emp.Salary;
                    range.NumberFormat = "#,##0.00";
                    ReleaseExcelObjects(range);

                    // Форматирование строки
                    for (int col = 1; col <= 7; col++)
                    {
                        range = sheet.Cells[row, col];
                        range.Borders.Weight = Excel.XlBorderWeight.xlThin;
                        ReleaseExcelObjects(range);
                    }

                    totalSalary += emp.Salary;
                    row++;
                }

                // Итоговая строка
                range = sheet.Range[$"A{row}", $"F{row}"];
                range.Merge();
                range.Value = "ИТОГО:";
                range.Font.Bold = true;
                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                range.Interior.Color = Excel.XlRgbColor.rgbLightGreen;
                ReleaseExcelObjects(range);

                range = sheet.Cells[row, 7];
                range.Value = totalSalary;
                range.NumberFormat = "#,##0.00";
                range.Font.Bold = true;
                range.Interior.Color = Excel.XlRgbColor.rgbLightGreen;
                ReleaseExcelObjects(range);

                // Автоподбор ширины
                sheet.Columns.AutoFit();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка генерации сводного отчета: {ex.Message}");
            }
        }

        private void GenerateEmployeeChildrenReport(Excel.Worksheet sheet)
        {
            Excel.Range range = null;

            try
            {
                // Получаем сотрудников с детьми и связанными данными
                var employees = _context.Employees
                    .Include(e => e.Children)
                    .Include(e => e.Position)
                    .ThenInclude(p => p.Department)
                    .OrderBy(e => e.LastName)
                    .ThenBy(e => e.FirstName)
                    .ToList();

                // Заголовок отчета
                range = sheet.Range["A1", "G1"];
                range.Merge();
                range.Value = "Отчет по сотрудникам и их детям";
                range.Font.Size = 14;
                range.Font.Bold = true;
                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                range.Interior.Color = Excel.XlRgbColor.rgbLightSteelBlue;
                ReleaseExcelObjects(range);

                // Заголовки столбцов
                int row = 3;
                string[] headers = { "№", "Сотрудник", "Должность", "Отдел", "Дети", "Дата рождения ребенка", "Возраст ребенка" };
                for (int i = 0; i < headers.Length; i++)
                {
                    range = sheet.Cells[row, i + 1];
                    range.Value = headers[i];
                    range.Font.Bold = true;
                    range.Borders.Weight = Excel.XlBorderWeight.xlThin;
                    range.Interior.Color = Excel.XlRgbColor.rgbLightGray;
                    ReleaseExcelObjects(range);
                }
                row++;

                // Проверка наличия данных
                if (!employees.Any())
                {
                    range = sheet.Range[$"A{row}", $"G{row}"];
                    range.Merge();
                    range.Value = "Нет данных для отображения";
                    range.Font.Color = Excel.XlRgbColor.rgbRed;
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ReleaseExcelObjects(range);
                    return;
                }

                // Счетчик сотрудников
                int employeeCounter = 1;

                foreach (var emp in employees)
                {
                    // Строка сотрудника
                    range = sheet.Cells[row, 1]; // №
                    range.Value = employeeCounter++;
                    range.Font.Bold = true;
                    range.Interior.Color = Excel.XlRgbColor.rgbLightGreen;
                    ReleaseExcelObjects(range);

                    range = sheet.Cells[row, 2]; // ФИО
                    range.Value = emp.FullName;
                    range.Font.Bold = true;
                    range.Interior.Color = Excel.XlRgbColor.rgbLightGreen;
                    ReleaseExcelObjects(range);

                    range = sheet.Cells[row, 3]; // Должность
                    range.Value = emp.Position?.Title;
                    range.Font.Bold = true;
                    range.Interior.Color = Excel.XlRgbColor.rgbLightGreen;
                    ReleaseExcelObjects(range);

                    range = sheet.Cells[row, 4]; // Отдел
                    range.Value = emp.Position?.Department?.Name;
                    range.Font.Bold = true;
                    range.Interior.Color = Excel.XlRgbColor.rgbLightGreen;
                    ReleaseExcelObjects(range);

                    // Объединяем ячейки для детей
                    range = sheet.Range[sheet.Cells[row, 5], sheet.Cells[row, 7]];
                    range.Merge();
                    range.Value = emp.Children.Count > 0 ? $"{emp.Children.Count} детей" : "Нет детей";
                    range.Font.Italic = true;
                    range.Interior.Color = Excel.XlRgbColor.rgbLightGreen;
                    ReleaseExcelObjects(range);

                    row++;

                    // Дети сотрудника
                    if (emp.Children != null && emp.Children.Any())
                    {
                        foreach (var child in emp.Children)
                        {
                            // Пустая ячейка вместо номера
                            range = sheet.Cells[row, 1];
                            range.Interior.Color = Excel.XlRgbColor.rgbLightYellow;
                            ReleaseExcelObjects(range);

                            // Пустая ячейка вместо ФИО сотрудника
                            range = sheet.Cells[row, 2];
                            range.Interior.Color = Excel.XlRgbColor.rgbLightYellow;
                            ReleaseExcelObjects(range);

                            // Пустая ячейка вместо должности
                            range = sheet.Cells[row, 3];
                            range.Interior.Color = Excel.XlRgbColor.rgbLightYellow;
                            ReleaseExcelObjects(range);

                            // Пустая ячейка вместо отдела
                            range = sheet.Cells[row, 4];
                            range.Interior.Color = Excel.XlRgbColor.rgbLightYellow;
                            ReleaseExcelObjects(range);

                            // Имя ребенка
                            range = sheet.Cells[row, 5];
                            range.Value = child.FullName;
                            range.IndentLevel = 2; // Отступ для визуальной группировки
                            range.Interior.Color = Excel.XlRgbColor.rgbLightYellow;
                            ReleaseExcelObjects(range);

                            // Дата рождения ребенка
                            range = sheet.Cells[row, 6];
                            range.Value = child.DateOfBirth;
                            range.NumberFormat = "dd.MM.yyyy";
                            range.Interior.Color = Excel.XlRgbColor.rgbLightYellow;
                            ReleaseExcelObjects(range);

                            // Возраст ребенка
                            range = sheet.Cells[row, 7];
                            range.Value = child.Age;
                            range.Interior.Color = Excel.XlRgbColor.rgbLightYellow;
                            ReleaseExcelObjects(range);

                            row++;
                        }
                    }
                }

                // Автоподбор ширины столбцов
                sheet.Columns.AutoFit();

                // Добавление итоговой информации
                range = sheet.Cells[row, 1];
                range.Value = $"Всего сотрудников: {employees.Count}";
                range.Font.Bold = true;
                ReleaseExcelObjects(range);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Ошибка: {ex.Message}\n{ex.StackTrace}");

                // Вывод ошибки на лист
                range = sheet.Range["A1"];
                range.Value = $"Ошибка генерации: {ex.Message}";
                range.Font.Color = Excel.XlRgbColor.rgbRed;
            }
            finally
            {
                if (range != null) ReleaseExcelObjects(range);
            }
        }
    }
}