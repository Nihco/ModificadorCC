using System;
using System.IO;
using System.Linq;
using ModificadorCC.Model;
using OfficeOpenXml;

namespace ModificadorCC.Service;

public class Excel
{
    public static void Edit(Root root)
    {
        FileInfo file = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plantilla", "Plantilla CC.xlsx"));
        using ExcelPackage excelPackage = new ExcelPackage(file);
        
        ExcelWorkbook excelWorkBook = excelPackage.Workbook;
        ExcelWorksheet excelWorksheet = excelWorkBook.Worksheets.First();
        var usuarioSolicitante= root.Items[0].CustomFields.FirstOrDefault(x => x.Name == "Usuario Solicitante")!.Value.ToLower();
        var solicitante=  excelWorksheet.Cells["B7"].Value.ToString()?.ToLower();
        if (usuarioSolicitante != solicitante)
        {
            excelWorksheet.Cells["B7"].Value = usuarioSolicitante;
        }
    }
}