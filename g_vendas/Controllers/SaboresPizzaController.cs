using g_vendas.BLL_s;
using g_vendas.Logger;
using g_vendas.Models;
using System;
using System.Collections.Generic;

namespace g_vendas.Controllers
{
    internal class SaboresPizzaController
    {
        private readonly SaboresPizzaBLL _bll = new SaboresPizzaBLL();

        public List<SaboresPizza> searchAllFl()
        {
            return _bll.searchSabores();
        }

        public void insertFl(SaboresPizza sabores)
        {
            try
            {
                _bll.insertSabor(sabores);
            }
            catch (Exception ex)
            {
                loggerC_.Error("errors:", ex);
            }
        }

        public void exportToExcel(List<SaboresPizza> sabores)
        {
            SaboresPizzaBLL.ExportarParaExcel(sabores);
        }
    }
}
