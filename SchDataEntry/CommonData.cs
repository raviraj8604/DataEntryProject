using CT.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SchDataEntry
{
    public class CommonData
    {
        string strCodeFile = "CommonData";
        public DataTable GetCommonFilters(string mode,int CountryID=0)
        {
            DataTable dt = new DataTable();
            try
            {
                SQLDBPro oDB = new SQLDBPro();
                SQLParam[] argParams = {
                                     new SQLParam("@ViewMode", mode),
                                      new SQLParam("@CountryID", CountryID)


                };
                DataSet DS = oDB.ExecuteSP_GetDataSet("uspSETUP_Common_Get", argParams);
               
                if (DS != null)
                {
                    dt = DS.Tables[0];
                }
                oDB = null;
               
            }
            catch (Exception ex)
            {
                CTException.WriteDBLog(strCodeFile, "GetCompanyDivisonList", ex.Message);
               
            }
            return dt;
        }


        public DataTable GetData(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            try
            {
                SQLDBPro oDB = new SQLDBPro();
                SQLParam[] argParams = {
                                     new SQLParam("@ViewMode", 1),
                                    new SQLParam("@FromDate", FromDate),
                                     new SQLParam("@ToDate", ToDate)


                };
                DataSet DS = oDB.ExecuteSP_GetDataSet("uspCountryData_Get", argParams);
                if (DS != null)
                {
                    dt = DS.Tables[0];
                }
                oDB = null;

            }
            catch (Exception ex)
            {
                CTException.WriteDBLog(strCodeFile, "GetCompanyDivisonList", ex.Message);

            }
            return dt;
        }


       
        public int SaveData(string pBulkData,string EntryDate)
        {
            int result = 0;
            try
            {
            
                SQLDBPro oDB = new SQLDBPro();
                SQLParam[] argParams ={
                                              new SQLParam("@Mode"                , 1)
                                             ,new SQLParam("@strData"           , pBulkData)
                                              ,new SQLParam("@EntryDate"           , EntryDate)

                                        };
                oDB.ExecuteSP("[uspSaveDataEntry]", argParams);

                if (oDB.SPStatus == SQLDBPro.enumSPStatus.Final)
                {
                    result = int.Parse(oDB.SPResult[0]);
                    
                }
                else
                {
                    result = int.Parse( oDB.SPResult[0]);
                   
                }
            }
            catch (Exception ex)
            {
                CTException.WriteDBLog(strCodeFile, "AddCompanyBranch", ex.Message);
                result = 0;
            }
            return result;
        }

    }
}