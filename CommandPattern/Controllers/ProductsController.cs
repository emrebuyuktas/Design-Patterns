using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using CommandPattern.Commands;
using CommandPattern.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommandPattern.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppIdenitytDbContext _context;

        public ProductsController(AppIdenitytDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        public async Task<IActionResult> CreateFile(int type)
        {
            var products = await _context.Products.ToListAsync();
            var excelFile = new ExcelFile<Product>(products);
            var pdfFile = new PdfFile<Product>(products,HttpContext);
            
            var fileCreateInvoker = new FileCreateInvoker();
            
            var fileType= (EFileType)type;

            switch (fileType)
            {
                case EFileType.Excel:
                    fileCreateInvoker.SetCommand(new CreateExcelTableActionCommand<Product>(excelFile));
                    break;
                case EFileType.Pdf:
                    fileCreateInvoker.SetCommand(new CreatePdfTableActionCommand<Product>(pdfFile));
                    break;
                default:
                    return NotFound();
            }

            return fileCreateInvoker.CreateFile();
        }
        
        public async Task<IActionResult> CreateFiles()
        {
            var products = await _context.Products.ToListAsync();
            ExcelFile<Product> excelFile = new(products);
            PdfFile<Product> pdfFile = new(products, HttpContext);
            FileCreateInvoker fileCreateInvoker = new();

            fileCreateInvoker.AddCommand(new CreateExcelTableActionCommand<Product>(excelFile));
            fileCreateInvoker.AddCommand(new CreatePdfTableActionCommand<Product>(pdfFile));

            var filesResult = fileCreateInvoker.CreateFiles();

            using (var zipMemoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(zipMemoryStream, ZipArchiveMode.Create))
                {
                    foreach (var item in filesResult)
                    {
                        var fileContent = item as FileContentResult;

                        var zipFile = archive.CreateEntry(fileContent.FileDownloadName);

                        using (var zipEntryStream = zipFile.Open())
                        {
                            await new MemoryStream(fileContent.FileContents).CopyToAsync(zipEntryStream);
                        }
                    }
                }

                return File(zipMemoryStream.ToArray(), "application/zip", "all.zip");
            }
        }
    }
}