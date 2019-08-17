using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageYourSelfMVC.Controllers
{
    public class StoreHoghoghController : Controller
    {
        // GET: StoreHoghogh
        public ActionResult ShowMazayaStoreHoghogh()
        {
            #region ShowPivot
            ViewModels.VMStoreHoghoghPivot V = new ViewModels.VMStoreHoghoghPivot();
            Models.ADO.UIDSConnection U = new Models.ADO.UIDSConnection();
            DataTable DT = U.Select(@"
DECLARE @cols AS NVARCHAR(max)

DECLARE @sql AS NVARCHAR(max)

SELECT @cols=STUFF((SELECT ',' +
QUOTENAME((SELECT mazayaname FROM Mazaya WHERE Mazayaid=StoreHoghogh.Mazayaid)) mazayaname 
FROM StoreHoghogh --where personelid=997
GROUP BY Mazayaid
FOR XML PATH(''),TYPE).value('.','NVARCHAR(max)')
,1,1,'')
--SELECT @cols

SELECT @sql=N'
SELECT * FROM 
(
select 
(select PersonelName FROM [5069_ManageYourSelf].[dbo].[MasterData] where personelid=S.personelid) FullName
,S.SHdate
,M.MazayaName,cast(round(S.MazRyialMah,0) as int) MazRyialMah 
from [StoreHoghogh] S inner join [Mazaya] M
on S.MazayaId=M.MazayaId
--order by shdate

)as OrginalTable
PIVOT
(
SUM([MazRyialMah])
FOR mazayaname IN('+@cols+')
 ) AS PivotTable
'
--print @sql
EXECUTE(@sql)
");
            DataTable reversedDt = new DataTable();
            reversedDt = DT.Clone();
            for (var row = DT.Rows.Count - 1; row >= 0; row--)
                reversedDt.ImportRow(DT.Rows[row]);

            V.ShowPivot = reversedDt;
            #endregion
            return PartialView(V);
        }
    }
}