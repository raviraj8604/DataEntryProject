<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SchDataEntry._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
     <link href="Content/jquery-ui.css" rel="stylesheet" />
    <script src="Scripts/jquery-ui.js"></script>
<div class="panel panel-default">
    <div class="panel-heading">Filter</div>
    <div class="panel-body">
        <div class="col-lg-4">
            <asp:TextBox ID="txtFromDate" runat="server" autocomplete="off" placeholder="Start Date" CssClass="form-control datePicker" />
        </div>
          <div class="col-lg-4">
            <asp:TextBox ID="txtToDate" runat="server" autocomplete="off" placeholder="End Date" CssClass="form-control datePicker" />
        </div>
          <div class="col-lg-4">
          <asp:Button ID="btnSubmit" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSubmit_Click"/>

                   <asp:Button ID="btnExport" runat="server" Text="Export To Excel" CssClass="btn btn-warning" OnClick="btnExport_Click"/>
        </div>

    </div>
  </div>
    <br />
    <div class="panel panel-default">
    <div class="panel-heading">List</div>
    <div class="panel-body">
    <asp:GridView id="grdData" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered" EmptyDataText="No Record Found"> 
        <Columns>
              <asp:TemplateField HeaderText = "Row Number" ItemStyle-Width="100">
        <ItemTemplate>
            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>
    </asp:TemplateField>
       
                 <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" /> 
                 <asp:BoundField DataField="CountryName" HeaderText="Country Name" /> 
                 <asp:BoundField DataField="StateName" HeaderText=" StateName" /> 
                           <asp:BoundField DataField="Name" HeaderText="Row Number" /> 
                 <asp:BoundField DataField="Remarks" HeaderText="Remarks"/> 
        </Columns>
    </asp:GridView>
    
    </div>
  </div>

      <script>
  $( function() {
      

       $(".datePicker").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd/mm/yy'
            });
  } );
  </script>

</asp:Content>
