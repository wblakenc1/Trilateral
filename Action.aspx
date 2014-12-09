<%@ Page Language="C#" AutoEventWireup="True"  CodeBehind="Action.aspx.cs" Inherits="TMID.Action" %>
<%@ Register Assembly="Trirand.Web" TagPrefix="trirand" Namespace="TMID"%>
<%@ PreviousPageType VirtualPath="~/Main.aspx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

	<title>Trademark Identification List</title>
	<meta http-equiv="Content-Language" content="English" />
	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
	<meta name="author" content="Neale Faunt (www.free-css-templates.com)" />
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
    <script src="js/trirand/jquery.json-2.2.min.js" type="text/javascript"></script>
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

    function GetSelectedComment() {
        var ddlval = document.getElementById("cboComment").Text
        var ddltext = document.getElementById("cboComment").options[document.getElementById('cboComment').selectedIndex].text;
        //var SelectedRow = Grid2D.SelectedRow.Cells(1).Text;
        alert(ddltext);
        //alert(SelectedRow);
    };

    function GetSelectedRecords()
    {
        {
            var SelectedIDs = "";
            var grid = document.getElementById('<%=Grid2D.ClientID %>');

            for (var i = 1; i < grid.rows.length; i++) {
                var Row = grid.rows[i];
                var SelectedID = Row.cells[1].innerText;
                var ctrl = Row.cells[0].childNodes[0];
                if (ctrl.type == "checkbox") {
                    if (ctrl.checked) {
                        SelectedIDs += SelectedID.toString() + ',';
                    }
                }
            }

            SelectedIDs = SelectedIDs.substring(0, SelectedIDs.length - 1);

            //window.open("MapCustomers.aspx?CustomerIDs=" + customerIDs);
        }
    };

    </script>

    <style type="text/css">
        .jqTransformButton {}
        </style>
</head>

<body>
	
<div class="content">			        
			                			            
	<div id="top">
				<div id="icons">
					<a href="/Main.aspx" title="Home page"><img src="images/home.gif" alt="Home" /></a>

				</div>
				<h1>Trademark Identification List</h1>
				<h2>The Five TM Nations</h2>
	</div>
	<!-- end of the Top part -->
</div>	
<p></p>
<asp:label id="lblUserName" runat="server" style="font-size: x-small"></asp:label>
<p></p>
<form id="Action" runat="server">

<div id="prec">
	<div id="wrap">
			<div id="menu">
						<ul>
                            <li><a href="/Main.aspx" title="Home">HOME</a></li>
                            <li><a href="/Main.aspx" title="Home">LOGOUT</a></li>
						</ul>
			</div>
	</div>

    <asp:Table ID="Table1" runat="server">
            <asp:TableHeaderRow runat="server" ForeColor="Gray">  
                <asp:TableHeaderCell>
                    <asp:label id="lblArray" runat="server" Text="Array Text"/></asp:TableHeaderCell><asp:TableHeaderCell>
                    </asp:TableHeaderCell></asp:TableHeaderRow></asp:Table></div><div class="content">

                       
<asp:DropDownList ID="cboComment" runat="server" onchange="GetSelectedComment()" Height="16px" Width="580px"><asp:ListItem Text = "Terms too broad (more than one type of good or service) or subject matter per national policy" Value = "1"></asp:ListItem><asp:ListItem Text = "Terms too vague (goods and services are not determined)" Value = "2"></asp:ListItem><asp:ListItem Text = "Terms are in wrong class" Value = "3"></asp:ListItem><asp:ListItem Text = "Repetition of terms (redundancy)" Value = "4"></asp:ListItem><asp:ListItem Text = "More than one good or service in one term" Value = "5"></asp:ListItem><asp:ListItem Text = "Against national policy" Value = "6"></asp:ListItem><asp:ListItem Text = "Trademark term" Value = "7"></asp:ListItem><asp:ListItem Text = "Wording not clear" Value = "8"></asp:ListItem></asp:DropDownList>
<asp:Label ID="lblSelected" runat="server" Text="Selected"></asp:Label>
<br /><asp:Label ID="lblMessage" runat="server" Text="Label"></asp:Label><br />                                    
<asp:GridView ID="Grid2D" runat="server" 
    AutoGenerateColumns = "False" Font-Names = "Arial" 
    Font-Size = "11pt" AlternatingRowStyle-BackColor = "#C2D69B"  
    DataKeyNames="ID"
    HeaderStyle-BackColor = "blue" AllowPaging ="True" OnRowCommand="Grid2D_RowCommand"><AlternatingRowStyle BackColor="#C2D69B"></AlternatingRowStyle>
<Columns>
    <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="SelectCheckBox" runat="server" />
            </ItemTemplate>
    </asp:TemplateField>

    <asp:BoundField ItemStyle-Width = "150px" 
    DataField = "ID" HeaderText = "ID" ReadOnly="True"><ItemStyle Width="150px"></ItemStyle>
    </asp:BoundField>

    <asp:BoundField ItemStyle-Width = "150px" 
    DataField = "Item" HeaderText = "Item" ReadOnly="True"><ItemStyle Width="150px"></ItemStyle>
    </asp:BoundField>

    <asp:BoundField ItemStyle-Width = "150px" 
    DataField = "Class" HeaderText = "Class" ReadOnly="True"><ItemStyle Width="150px"></ItemStyle>
    </asp:BoundField>

    <asp:BoundField ItemStyle-Width = "150px" 
    DataField = "Status" HeaderText = "Status" ReadOnly="True"><ItemStyle Width="150px"></ItemStyle>
    </asp:BoundField>

    <asp:TemplateField ItemStyle-Width = "150px" 
    HeaderText = "Comment" ><ItemStyle Width="250px"></ItemStyle>

<%--    <HeaderTemplate>
        </HeaderTemplate>--%>

    <ItemTemplate>
    <asp:TextBox id="txtComment" Runat="server" Text="Comment" Width="250px" BorderStyle="None">
    </asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField ItemStyle-Width = "150px" 
    HeaderText = "Translation" ><ItemStyle Width="250px"></ItemStyle>
    <ItemTemplate>
    <asp:TextBox id="txtTranslation" Runat="server" Text="Translation" Width="250px" BorderStyle="None">
    </asp:TextBox></ItemTemplate></asp:TemplateField></Columns><HeaderStyle BackColor="Green"></HeaderStyle>

</asp:GridView>

<p><asp:Button ID="btnVote" runat="server" Text="Submit" />&nbsp<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />&nbsp<asp:Button ID="btnSaveList" runat="server" Text="Save List" /></p>

<br />

<asp:Button ID="btnGetSelected" runat="server" Text="GetSelected" OnClientClick="GetSelectedRecords()" />

</div>  

<div id="footer1">

</div>
	</form>

    <div id="footer"></div>
	
<%--    <asp:BoundField ItemStyle-Width = "150px" 
    DataField = "Comment" HeaderText = "Comment" ><ItemStyle Width="150px"></ItemStyle>
    </asp:BoundField>

    <asp:BoundField ItemStyle-Width = "150px" 
    DataField = "Translation" HeaderText = "Translation" ><ItemStyle Width="150px"></ItemStyle>
    </asp:BoundField>--%></body>
</html>