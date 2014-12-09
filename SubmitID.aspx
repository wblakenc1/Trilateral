<%@ Page Language="C#" AutoEventWireup="True"  CodeBehind="SubmitID.aspx.cs" Inherits="TMID.SubmitID" %>
<%@ Register Assembly="Trirand.Web" TagPrefix="trirand" Namespace="TMID"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

	<title>Trademark Identification List</title>
	<meta http-equiv="Content-Language" content="English" />
	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
	<meta name="author" content="Neale Faunt" />
	<meta name="description" content="TMID" />
	<meta name="keywords" content="TMID" />	
	<meta name="Robots" content="index,follow" />
	<meta name="Generator" content="sNews 1.5" />
	<link rel="stylesheet" type="text/css" href="style.css" media="screen" />
	<link rel="alternate" type="application/rss+xml" title="RSS 2.0" href="rss/" />

    <!-- The jQuery UI theme that will be used by the grid -->
    <link rel="stylesheet" type="text/css" media="screen" href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.10.0/themes/redmond/jquery-ui.css" />
    <!-- The jQuery UI theme extension jqGrid needs -->
    <link rel="stylesheet" type="text/css" media="screen" href="themes/ui.jqgrid.css" />
    <!-- jQuery runtime minified -->
    <script src="http://ajax.microsoft.com/ajax/jquery/jquery-1.9.0.min.js" type="text/javascript"></script>
    <!-- The localization file we need, English in this case -->
    <script src="js/trirand/i18n/grid.locale-en.js"type="text/javascript"></script>
    <!-- The jqGrid client-side javascript -->
    <script src="js/trirand/jquery.jqGrid.min.js" type="text/javascript"></script>
    <!-- The JSON javascript -->
    <script src="js/jquery.json-2.2.min.js" type="text/javascript"></script>
    <script src="js/trirand/jquery.jqDatePicker.min.js" type="text/javascript"></script>
    <script src="js/trirand/jquery.jqAutoComplete.min.js" type="text/javascript"></script>  
    <!-- The jQuery Control Stylesheet -->
    <link rel="stylesheet" href="js/jqtransformplugin/jqtransform.css" type="text/css" media="all" />
    <!-- The jQuery Control javascript -->
    <script type="text/javascript" src="js/jqtransformplugin/jquery.jqtransform.js" ></script>
       
    <script type="text/javascript">
    //$(function () {
    //    $('SubmitIDForm').jqTransform({ imgPath: 'jqtransformplugin/img/' });
    //});

    function showmodalpopup() {
        $("#popupdiv").dialog({
            title: "jQuery Popup from Server Side",
            width: 430,
           height: 250,
            modal: true,
            buttons: {
                Close: function () {
                    $(this).dialog('close');
                }
            }
        });
    };
    </script>

    <style type="text/css">
        .jqTransformButton {}
        </style>
</head>

<body>
	
<div class="content">			        
									            <%--<div id="bluemenu" class="bluetabs">
                                                    <ul>
                                                        <li><a href="#"  rel="dropmenu1_a"><img id="activeUserGif" height="15" src="images/tril_inactive.gif" width="15" alt="Active User" />&nbsp;&nbsp;</a></li>
                                                    </ul>
                                                </div>       
                                                <img height="21" src="images/spacer_topmenu.gif" width="1" alt="graphics spacer"/>&nbsp;&nbsp;--%>
								                <!--<asp:label id=Label1 runat="server">Welcome  </asp:label>-->
								                			            
	<div id="top">
				<div id="icons">
					<a href="/Main.aspx" title="Home page"><img src="images/home.gif" alt="Home" /></a>
					<a href="#" title="Contact us"><img src="images/contact.gif" alt="Contact" /></a>
                    <br/>
                    <asp:Label ID="lblUserName" runat="server" Text="Label" ForeColor="Gray"></asp:Label>
				</div>
				<h1>Trademark Identification List</h1>
				<h2>The Five TM Nations</h2>
	</div>
	<!-- end of the Top part -->
</div>	
<p></p>
<p></p>
<form id="SubmitIDForm" runat="server">

<div id="prec">
	<div id="wrap">
			<div id="menu">
						<ul>
                            <li><a href="/Main.aspx" title="Home">HOME</a></li>
                                <li><a href="#" title="User Guide">USER GUIDE</a></li>
						<li><a href="/Logon.aspx" title="Logout">LOGOUT</a></li>
						</ul>
			</div>
	</div>
</div>

<div class="content">

<asp:requiredfieldvalidator id="reqID" runat="server" Display="None" ControlToValidate="txtTrilateralID" ErrorMessage="Please Enter an Item."></asp:requiredfieldvalidator>
<asp:rangevalidator id="reqClass" runat="server" Display="None" ControlToValidate="cboClassNumber" ErrorMessage="Please Select a Class." Type="Integer"></asp:rangevalidator>
<asp:validationsummary id="ValidationSummary" runat="server" Width="472px" CssClass="normalfont" ShowSummary="True"></asp:validationsummary><asp:label id="lblConflictID" Runat="server" Visible="False"></asp:label><asp:label id="lblRejectedID" Runat="server" Visible="False"></asp:label>
<asp:label id="lblWarningID" Runat="server" Visible="False"></asp:label><asp:button id="btnContinue" runat="server" Visible="False" Text="Yes"></asp:button><asp:button id="btnCancel" runat="server" Visible="False" Text="No"></asp:button><asp:button id="btnBack" Runat="server" Visible="False" Text="Back"></asp:button>


<asp:Table ID="Table1" runat="server">

<asp:TableHeaderRow runat="server" ForeColor="Gray"> 
    <asp:TableHeaderCell HorizontalAlign="right" VerticalAlign="top"></asp:TableHeaderCell>
    <asp:TableHeaderCell HorizontalAlign="left"><asp:label id="lblInstructions" Runat="server" Width="408px">Type Proposed Item and Class Number then click "Add" </asp:label></asp:TableHeaderCell>
</asp:TableHeaderRow>

<asp:TableHeaderRow runat="server" ForeColor="Gray"> 
    <asp:TableHeaderCell HorizontalAlign="right" VerticalAlign="top"><asp:label id="lblCandidateID" runat="server" Font-Bold="True">Proposed Item</asp:label></asp:TableHeaderCell>
    <asp:TableHeaderCell><asp:textbox id="txtTrilateralID" runat="server" Width="408px" Columns="40"></asp:textbox></asp:TableHeaderCell>
    <asp:TableHeaderCell HorizontalAlign="right"><asp:label id="lblSubmissionCountMessage" Runat="server" Width="320px">Proposed Items submitted by partner group this month:</asp:label><asp:label id="lblSubmissionCount" Runat="server" Width="20px" ></asp:label></asp:TableHeaderCell>
</asp:TableHeaderRow>

<asp:TableHeaderRow runat="server" ForeColor="Gray"> 
    <asp:TableHeaderCell HorizontalAlign="right" VerticalAlign="top"><asp:label id="lblClass" runat="server" Font-Bold="True">Class&nbsp;Number&nbsp;</asp:label></asp:TableHeaderCell>
    <asp:TableHeaderCell HorizontalAlign="left"><asp:dropdownlist id="cboClassNumber" runat="server" onselectedindexchanged="cboClassNumber_SelectedIndexChanged"></asp:dropdownlist></asp:TableHeaderCell>
</asp:TableHeaderRow>
 
<asp:TableHeaderRow runat="server" ForeColor="Gray"> 
    <asp:TableHeaderCell HorizontalAlign="right" VerticalAlign="top"><asp:label id="lblComment" runat="server" Font-Bold="True">Comment&nbsp;(optional)</asp:label></asp:TableHeaderCell>
    <asp:TableHeaderCell><asp:textbox id="txtComment" runat="server" Width="408px" Columns="31" TextMode="MultiLine" Height="56px" Rows="4"></asp:textbox></asp:TableHeaderCell>     																
</asp:TableHeaderRow> 

<asp:TableHeaderRow runat="server" ForeColor="Gray"> 
    <asp:TableHeaderCell></asp:TableHeaderCell>
    <asp:TableHeaderCell HorizontalAlign="left"><asp:Button id="btnAdd" runat="server" Text="Add" onclick="btnAdd_Click"></asp:Button></asp:TableHeaderCell>   																
</asp:TableHeaderRow> 

<asp:TableHeaderRow runat="server" ForeColor="Gray"> 
<asp:TableHeaderCell><asp:label id="lblSubmissionError" ForeColor="Black" Runat="server" Width="40px" Visible="True"></asp:label></asp:TableHeaderCell>
<asp:TableHeaderCell HorizontalAlign="right" Width="408px">


        
</asp:TableHeaderCell>   																
</asp:TableHeaderRow> 

</asp:Table>


<div align="center">
<asp:datagrid id="DataGrid1" HorizontalAlign="Center" runat="server" Width="1000px" CssClass="ui-dialog-titlebar" UseAccessibleHeader="True"
    BackColor="White" CellPadding="3" BorderWidth="1px" BorderStyle="None" BorderColor="Black" AlternatingItemStyle-BackColor="#DDDDDD">
    <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
    <AlternatingItemStyle BackColor="#DDDDDD"></AlternatingItemStyle>
    <ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" ForeColor="White" CssClass="nowrap" BackColor="#6699FF" BorderStyle="None"></HeaderStyle>
    <FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
    <Columns>
    <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" HeaderText="Edit" CancelText="Cancel"
    EditText="Edit"></asp:EditCommandColumn>
    <asp:ButtonColumn Text="Delete" HeaderText="Delete" CommandName="Delete"></asp:ButtonColumn>
    </Columns>
</asp:datagrid>   

<asp:button id="btnRemove" runat="server" Visible="False" Text="Remove"></asp:button>
<asp:button id="btnEdit" runat="server" Visible="False" Text="Edit"></asp:button>
<asp:button id="btnYes" runat="server" Visible="False" Font-Bold="True" Text="Yes" CausesValidation="False" onclick="btnYes_Click"></asp:button>
<asp:button id="btnNo" runat="server" Visible="False" Font-Bold="True" Text="No" CausesValidation="False" onclick="btnNo_Click"></asp:button>    
       
</div>

<asp:Table ID="Table2" runat="server">

<asp:TableHeaderRow runat="server" ForeColor="Gray"> 
    <asp:TableHeaderCell></asp:TableHeaderCell>
    <asp:TableHeaderCell><asp:label id="lblListRecdCount" ForeColor="Black" Runat="server" Width="20px" Visible="False"></asp:label></asp:TableHeaderCell>
</asp:TableHeaderRow> 

<asp:TableHeaderRow runat="server" ForeColor="Gray"> 
    <asp:TableHeaderCell></asp:TableHeaderCell>
    <asp:TableHeaderCell><asp:label id="lblMessage" ForeColor="Red" Runat="server" CssClass="normalfont"></asp:label><asp:label id="lblRepeatIDs" ForeColor="Red" Runat="server" CssClass="normalfont"></asp:label><asp:label id="lblConfirmation" ForeColor="Black" Runat="server" CssClass="normalfont"></asp:label></asp:TableHeaderCell>
</asp:TableHeaderRow> 

<asp:TableHeaderRow runat="server" ForeColor="Gray"> 
    <asp:TableHeaderCell Width="110px"><asp:label id="LabelSubmitButtons" runat="server" Visible="False"></asp:label></asp:TableHeaderCell>
    <asp:TableHeaderCell><asp:button id="btnSubmitAll" runat="server" Visible="False" Text="Submit" CausesValidation="False" onclick="btnSubmitAll_Click"></asp:button></asp:TableHeaderCell>
    <asp:TableHeaderCell><asp:radiobutton id="rdoSubmitNow" runat="server" Visible="False" Text="Submit Now" Checked="True"

    GroupName="SubmitWhen" oncheckedchanged="rdoSubmitNow_CheckedChanged"></asp:radiobutton>
    <asp:radiobutton id="rdoSubmitFirstMonth" runat="server" Visible="False" Text="Submit First of the Month"
    GroupName="SubmitWhen" oncheckedchanged="rdoSubmitFirstMonth_CheckedChanged"></asp:radiobutton>
    </asp:TableHeaderCell>

    <asp:TableHeaderCell><asp:hyperlink id="InboxReturnLink" runat="server" Visible="False" NavigateUrl="Default.aspx">Return to Inbox</asp:hyperlink></asp:TableHeaderCell>

</asp:TableHeaderRow> 

</asp:Table>



<p><asp:label id="lblRowNum" ForeColor="Red" Runat="server" Width="191px" Visible="False"></asp:label><asp:label id="lblLogged_On_Partner_ID" ForeColor="Red" Runat="server" Width="216px" Visible="False"></asp:label></p>
<p></p>

<p><asp:label id="lblMultipleMessage" Runat="server" Width="784px" Visible="False"></asp:label></p>
<p><asp:label id="lbldgID" Runat="server" Visible="False"></asp:label><asp:label id="lbldgClass" Runat="server" Visible="False"></asp:label><asp:label id="lbldgComment" Runat="server" Visible="False"></asp:label><asp:label id="lblDBStatus" Runat="server" Visible="False"></asp:label></p>
<p><asp:button id="btnEnterNewID" runat="server" Width="86px" Visible="False" Text="Submit" BackColor="Red" onclick="btnEnterNewID_Click"></asp:button></p>

<p><asp:label id="lblTest" ForeColor="Red" Runat="server" Width="352px" Visible="False">Nothing</asp:label></p>

<p><asp:label id="lblDupID" ForeColor="Black" Runat="server" Width="240px" CssClass="normalfont"></asp:label></p>
<p><asp:label id="lblDupIDForComparison" ForeColor="Black" Runat="server" Width="240px" Visible="False" CssClass="normalfont"></asp:label></p>

<%--    <script type="text/C#" runat="server">
        protected void Page_Load(object send, EventArgs args)
        {

        }
    </script>--%>

</div>  

</form>

    <div id="footer1"><p></p></div>
	<div id="footer"></div>
	
<%--</div>--%>
</body>
</html>