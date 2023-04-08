public class SQLDBPro : CT.DataLink.CTSQLPro
{
    public SQLDBPro()
    {

    }
    public SQLDBPro(string pDBName)
    {

    }
}

public class SQLParam : CT.DataLink.CTParam
{
    public SQLParam(object pParamName, object pParamValue)
    {
        ParamName = pParamName;
        ParamValue = pParamValue;
    }
}