<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddData.aspx.cs" Inherits="SchDataEntry.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/jquery-ui.css" rel="stylesheet" />
    <script src="Scripts/jquery-ui.js"></script>
    <h2><%: Title %>.</h2>
    <h3>Your contact page.</h3>
    <div class="row">
        <div class="col-lg-4">
            <asp:TextBox runat="server" placeholder="Select Date" autocomplete="off" ClientIDMode="Static"  ID="txtDate" CssClass="form-control"/>
        </div>
        <div class="col-lg-4">
            <asp:Button ID="btnSaveData" OnClick="btnSaveData_Click" Text="Save Data" runat="server" CssClass="btn btn-primary" />
        </div>
         <div class="col-lg-4">
            
              <asp:Button ID="ButtonAdd"  runat="server" Text="Add Row" CssClass="btn btn-success pull-right" onclick="ButtonAdd_Click" /> 
            
        </div>
    </div>
   <asp:GridView ID="grdDataEntry" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false" EnableViewState="true"
    OnRowDataBound="grdDataEntry_RowDataBound">
    <Columns>
        <asp:BoundField DataField="RowNumber" HeaderText="Row Number" /> 
        <asp:TemplateField HeaderText="Country">
            <ItemTemplate>
                <asp:DropDownList ID="ddlCountries" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCountries_SelectedIndexChanged">
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="State">
            <ItemTemplate>
                <asp:DropDownList ID="ddlState" CssClass="form-control" runat="server"  >
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Name">
            <ItemTemplate>
               <asp:TextBox ID="txtName" placeholder="Name" CssClass="form-control" runat="server"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Remarks">
            <ItemTemplate>
               <asp:TextBox ID="txtRemarks"  placeholder="Remarks" CssClass="form-control" runat="server"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
      
          
    </Columns>
</asp:GridView>

    <script>
  $( function() {
      

       $("#txtDate").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd/mm/yy'
            });
  } );
  </script>
</asp:Content>
