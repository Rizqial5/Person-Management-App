﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersoneManagement.Web.Models.Interfaces
{
    public interface IExportExcelService
    {
        void CreateSheets(string sheetName);


    }
}
