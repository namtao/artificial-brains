﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.DuAn
{
    public class BinhPhuoc
    {
        public void ThongKeSKHDT()
        {
            var arrPathJpg = Directory.GetFiles(@"C:\Users\ADMIN\Downloads\Data so hoa (k xoa)", "*.*",
                SearchOption.AllDirectories).Where(s => s.EndsWith(".pdf")).ToList();

            object[,] arr = new object[100, 4];
            arr[0, 0] = "STT";
            arr[0, 1] = "Mã doanh nghiệp";
            arr[0, 2] = "Số hồ sơ";
            arr[0, 3] = "Dung lượng file";

            for(int i = 0; i < arrPathJpg.Count; i++)
            {
                arr[i + 1, 0] = i + 1;
                arr[i + 1, 1] = arrPathJpg[i];
                arr[i + 1, 2] = Path.GetFileName(arrPathJpg[i]);
                arr[i + 1, 3] = new System.IO.FileInfo(arrPathJpg[i]).Length;
            }

            Utils.ExportExcel(arr, "Sheet", 3, 1, 100, 4);
        }
    }
}
