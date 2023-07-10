using System;
using System.IO;
using System.Linq;
using OfficeOpenXml;

namespace ModificadorCC.Service;

public class Excel
{
    public static void Edit(Root root, string newPath)
    {
        FileInfo file = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plantilla", "PlantillaCC.xlsx"));
        using ExcelPackage excelPackage = new ExcelPackage(file);

        ExcelWorkbook excelWorkBook = excelPackage.Workbook;
        ExcelWorksheet excelWorksheet = excelWorkBook.Worksheets.First();
        var rootItem = root.Items[0];

        var clienteApi = rootItem.CustomFields.FirstOrDefault(x => x.Name == "Usuario Solicitante")?.Value.ToString()?.ToLower();
        var clientedoc = excelWorksheet.Cells["B7"].Value.ToString()?.ToLower();
        if (clienteApi != clientedoc)
        {
            excelWorksheet.Cells["B7"].Value = rootItem.CustomFields.FirstOrDefault(x => x.Name == "Usuario Solicitante")?.Value.ToString();
        }

        var solicitanteApi = rootItem.Assignments.Items.FirstOrDefault()?.GeneralUser;
        var solicitantedoc = excelWorksheet.Cells["B8"].Value.ToString()?.ToLower();
        var nombreApi = $"{solicitanteApi?.FirstName} {solicitanteApi?.LastName}";
        if (nombreApi != solicitantedoc)
        {
            excelWorksheet.Cells["B8"].Value = nombreApi;
        }

        var now = DateTime.Now;
        var nowPlus5Mins = now.AddMinutes(5);
        var currentDate = now.ToString("dd/MM/yyyy");
        var currentDatePlus5Mins = nowPlus5Mins.ToString("dd/MM/yyyy hh:mm");
        excelWorksheet.Cells["B3"].Value = $"CC-{currentDate.Replace("/", "")}-{root.Items.FirstOrDefault()?.Id}";
        excelWorksheet.Cells["B9"].Value = currentDate;
        excelWorksheet.Cells["D9"].Value = root.Items.FirstOrDefault()?.Id;
        excelWorksheet.Cells["B13"].Value = root.Items.FirstOrDefault()?.Name;
        excelWorksheet.Cells["B14"].Value = root.Items.FirstOrDefault()?.Description;
        excelWorksheet.Cells["B25"].Value = nombreApi;
        excelWorksheet.Cells["B30"].Value = currentDate;
        excelWorksheet.Cells["D30"].Value = nowPlus5Mins.ToString("hh:mm:ss tt");
        excelWorksheet.Cells["B43"].Value = currentDatePlus5Mins;
        excelWorksheet.Cells["D43"].Value = nombreApi;
        excelWorksheet.Cells["B44"].Value = currentDatePlus5Mins;
        excelWorksheet.Cells["D44"].Value = nombreApi;
        excelWorksheet.Cells["B75"].Value = nombreApi;
        excelWorksheet.Cells["D75"].Value = solicitanteApi!.Login;
        excelWorksheet.Cells["D88"].Value = currentDate;
        
        excelPackage.SaveAs(new FileInfo(newPath));
    }
}