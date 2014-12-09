<%@ Page Language="C#" enableSessionState="true" AutoEventWireup="true"  CodeBehind="Main.aspx.cs" Inherits="TMID.Main" %>
<%@ Register Assembly="Trirand.Web" TagPrefix="trirand" Namespace="TMID"%>

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
    <link rel="stylesheet" type="text/css" media="screen" href="js/redmond/jquery-ui.css" />
    <!-- The jQuery UI theme extension jqGrid needs -->
    <link rel="stylesheet" type="text/css" media="screen" href="themes/ui.jqgrid.css" />
    <!-- jQuery runtime minified -->
    <script src="js/jquery-1.11.1.min.js" type="text/javascript"></script>
    <!-- The jQuery JQGrid javascript -->
    <script src="js/jquery.jqGrid.js" type="text/javascript"></script>
    <!-- The localization file we need, English in this case -->
    <script src="js/trirand/i18n/grid.locale-en.js"type="text/javascript"></script>
    <!-- The jqGrid client-side javascript -->
    <script src="js/trirand/jquery.jqGrid.min.js" type="text/javascript"></script>
    <!-- The JSON javascript -->
    <script src="js/json_sans_eval.js" type="text/javascript"></script>  
    <script src="js/jquery.json-2.4" type="text/javascript"></script>
    <script src="js/trirand/jquery.jqDatePicker.min.js" type="text/javascript"></script>
    <script src="js/trirand/jquery.jqAutoComplete.min.js" type="text/javascript"></script>  
    <!-- The jQuery Control Stylesheet -->
    <link rel="stylesheet" href="js/jqtransformplugin/jqtransform.css" type="text/css" media="all" />
    <!-- The jQuery Control javascript -->
    <script type="text/javascript" src="js/jqtransformplugin/jquery.jqtransform.js" ></script>
    <%--<script type="text/javascript" src="http://code.jquery.com/jquery-1.10.2.js"></script>--%>
    <script type="text/javascript" src="js/jquery-ui.js"></script>

<%--<script type="text/javascript">
        $(function () {
            $('GridForm').jqTransform({ imgPath: 'jqtransformplugin/img/' });
        });
    </script>--%>

<script type="text/javascript">
    $(function () {
        //$("#Grid1").jqGrid({}).setGridWidth(1200, false);
    });
</script>

    <script type="text/javascript">
        //$.jgrid.no_legacy_api = true;
        //$.jgrid.useJSON = true;
    </script>

<script type="text/javascript">
$(document).ready(function() {
    $("#grdSelectedWrapper").hide();
    $("#grdAlternateWrapper").hide();
    $("#btnPanel").hide();

});
</script>

<%--<script type="text/javascript">

    jQuery(document).ready(function() {
    jQuery("#grdMain").jqGrid({
        url: '',
        datatype: "json",
        colNames: ['ID', 'Item', 'Class', 'Status', 'Date Created',
                'Date Released', 'Date Approved', "Date Rejected", 'USPTO Vote',
                'PHILIPPINES Vote', 'JPO Vote', 'OHIM Vote', 'KOREA Vote', 'CHINA Vote', 
                'MEXICO Vote', 'SINGAPORE Vote', 'RUSSIA Vote'],

        colModel: [
        { name: '_ID', index: '_ID', width: 50, stype: 'text' },
        { name: 'Item', index: 'Item', width: 250 },
        { name: 'Class', index: 'Class', width: 50 },
        { name: 'Status', index: 'Status', width: 75 },
        { name: 'Date Created', index: 'Date Created', width: 100, align: "right" },
        { name: 'Date Released', index: 'Date Released', width: 100, align: "right" },
        { name: 'Date Approved', index: 'Date Approved', width: 100, align: "right" },
        { name: 'Date Rejected', index: 'Date Rejected', width: 100, sortable: false },
        { name: 'USPTO Vote', index: 'USPTO Vote', width: 100, sortable: false },
        { name: 'JPO Vote', index: 'JPO Vote', width: 100, sortable: false },
        { name: 'OHIM Vote', index: 'OHIM Vote', width: 100, sortable: false },
        { name: 'KOREA Vote', index: 'KOREA Vote', width: 100, sortable: false },
        { name: 'CHINA Vote', index: 'CHINA Vote', width: 100, sortable: false },
        { name: 'MEXICO Vote', index: 'MEXICO Vote', width: 100, sortable: false },
        { name: 'PHILIPPINES Vote', index: 'PHILIPPINES Vote', width: 100, sortable: false },
        { name: 'SINGAPORE Vote', index: 'SINGAPORE Vote', width: 100, sortable: false },
        { name: 'RUSSIA Vote', index: 'RUSSIA Vote', width: 100, sortable: false }

        ],
        rowNum: 10,
        sortname: 'ID',
        viewrecords: true,
        sortorder: "desc",
        caption: "Trademark ID",
        pager: jQuery('#pager')
    });})
</script>--%>

<script type="text/javascript">

    function ShowSelectedGrid(Action)
    {
        //document.getElementById("lblActionToPerform").innerHTML = 'Click Submit to ' + $('#btnShowGrid').val();

        document.getElementById('valButtonClicked').value = Action;
        document.getElementById("lblActionToPerform").innerHTML = 'Click Submit to ' + Action;
        $("#btnPanel").hide();
        $("#lnkPanel").hide();

        var myNewData = [];
        var grid = jQuery("#Grid1");
        var strarray = "["                                  // To Populate ItemPkgArray
        var ids = grid.jqGrid('getGridParam', 'selarrrow');
        if (ids.length > 0) {
            var names = [];
            for (var i = 0, il = ids.length; i < il; i++) {
                var idnum = grid.jqGrid('getCell', ids[i], 'ID');
                var name = grid.jqGrid('getCell', ids[i], 'Item');
                var ic = grid.jqGrid('getCell', ids[i], 'Class');
                var status = grid.jqGrid('getCell', ids[i], 'Status');
                myNewData.push({ ID: idnum, Item: name, Class: ic, Status: status, Comment: "", Translation: "" });

                // To Populate ItemPkgArray

                strarray = strarray + "{" +
                    "ID" + ":" + '"' + idnum + '"' + "," +
                "Item" + ":" + '"' + name + '"' + "," +
                "Class" + ":" + '"' + ic + '"' + "," +
                "Status" + ":" + '"' + status + '"' + 
                    "},"
                ;
            }
            // To Populate ItemPkgArray
            var strarraystring = strarray.substr(0, strarray.length - 1);
            strarraystring = strarraystring + "]";
            //alert(strarraystring);

            document.getElementById('<%=ItemPkgArray.ClientID %>').value = strarraystring;
        }
        $("#grdSelectedWrapper").show();

        jQuery("#grdSelected").jqGrid({
            datatype: "local",
            gridview: true,
            colNames: ['ID', 'Item', 'Class', 'Status', 'Comment', 'Translation'],
            colModel: [
            { name: 'ID', index: 'ID', width: 50, key: true },
            { name: 'Item', index: 'Item', width: 250 },
            { name: 'Class', index: 'Class', width: 75 },
            { name: 'Status', index: 'Status', width: 100 },
            { name: 'Comment', index: '', width: 250, editable: true },
            { name: 'Translation', index: '', width: 250, editable: true }
            ],

            height: 'auto',
            localReader: { id: "ID", repeatitems: false },
            repeatitems: false,
            viewrecords: true,
            edit: true,
            editurl: 'clientArray',
            cellsubmit: 'clientArray',
            multiselect: true,
            loadonce: false,
            pager: grdSelectedPager,
            caption: "Selected Items"
        }).navGrid('#grdSelectedPager', { add: false, edit: true, del: false, search: false, refresh: false });
 
        for (var i = 0; i <= myNewData.length; i++)
            jQuery("#grdSelected").jqGrid('addRowData', i + 1, myNewData[i]);

        $("#MainGridWrapper").hide();
    };
    function HideSelectedGrid() {
        $("#grdSelected").clearGridData();
        $("#grdSelectedWrapper").hide();
        $("#MainGridWrapper").show();
        // Show Button Panel
        $("#btnPanel").show();
        $("#lnkPanel").hide();
    };

</script>

<script type="text/javascript">

    //$(function () {
    //    $('GridForm').jqTransform({ imgPath: 'jqtransformplugin/img/' });
    //});

    function getSelectedRowData() {

        //var ID;
        //var Item;
        //var Class;
        //var Status;
        //var Comment;
        //var Translation;

        var grid = jQuery("#Grid1");
        var ids = grid.jqGrid('getGridParam', 'selarrrow');
        var strarray = "["
        if (ids.length > 0) {
            var names = [];
            for (var i = 0, il = ids.length; i < il; i++) {
                var idnum = grid.jqGrid('getCell', ids[i], 'ID');
                var name = grid.jqGrid('getCell', ids[i], 'Item');
                var ic = grid.jqGrid('getCell', ids[i], 'Class');
                var status = grid.jqGrid('getCell', ids[i], 'Status');
 

                strarray = strarray + "{" +                                 // no spaces and double quotes
                    "ID" + ":" + '"' + idnum + '"' + "," +
                "Item" + ":" + '"' + name + '"' + "," +
                "Class" + ":" + '"' + ic + '"' + "," +
                "Status" + ":" + '"' + status + '"' +
                "Comment" + ":" + '"' + comment + '"' + "," +
                "Translation" + ":" + '"' + translation + '"' +
                    "},"
                ;

                names.push(idnum,name,ic,status,comment,translation);
            }
            var strarraystring = strarray.substr(0, strarray.length - 1);
            strarraystring = strarraystring + "]";
            //alert(strarraystring);
        }
        document.getElementById('<%=ItemPkgArray.ClientID %>').value = strarraystring;
    };

    function GetGridData()
    {
        //$("#btnPanel").show();
        var haspartnervote;
        haspartnervote = document.getElementById('<%=valHasPartnerVote.ClientID %>').value;

        if (haspartnervote == "Partner") {
            document.getElementById("BtnAccept").disabled = false;
            document.getElementById("BtnReject").disabled = false;
            document.getElementById("BtnRemove").disabled = false;
            document.getElementById("BtnResubmit").disabled = false;
            document.getElementById("BtnWithdraw").disabled = false;
        };

        var pID = document.getElementById('valPartnerID').value;
        //var grid = jQuery("#Grid1");
        //var ids = grid.jqGrid('getGridParam', 'selarrrow');
        //var grdStatus = grid.jqGrid('getCell', ids, 'Status');
        var grid = jQuery("#Grid1");
        var ids = grid.jqGrid('getGridParam', 'selarrrow');
        var strarray = "["
        if (ids.length < 1) {
            // Hide Button Panel
            $("#btnPanel").hide();
            $("#lnkPanel").show();
        }
        if (ids.length > 0) {
            // Display Button Panel
            $("#btnPanel").show();
            $("#lnkPanel").hide();
            var names = [];
            for (var i = 0, il = ids.length; i < il; i++) {
                var idnum = grid.jqGrid('getCell', ids[i], 'ID');
                var name = grid.jqGrid('getCell', ids[i], 'Item');
                var ic = grid.jqGrid('getCell', ids[i], 'Class');
                var status = grid.jqGrid('getCell', ids[i], 'Status');
                var comment = "Comment";
                var translation = "Translation";
                var USPTOVote = grid.jqGrid('getCell', ids[i], 'USPTO');
                var JPOVote = grid.jqGrid('getCell', ids[i], 'JPO');
                var OHIMVote = grid.jqGrid('getCell', ids[i], 'OHIM');
                var KOREAVote = grid.jqGrid('getCell', ids[i], 'KOREA');
                var CHINAVote = grid.jqGrid('getCell', ids[i], 'CHINA');

                strarray = strarray + "{" +
                    "ID" + ":" + '"' + idnum + '"' + "," +
                    "Status" + ":" + '"' + status + '"' + "," +
                                        "Comment" + ":" + '"' + comment + '"' + "," +
                                        "Translation" + ":" + '"' + translation + '"' + "," +
                    "USPTO" + ":" + '"' + USPTOVote + '"' + "," +
                    "JPO" + ":" + '"' + JPOVote + '"' + "," +
                    "OHIM" + ":" + '"' + OHIMVote + '"' + "," +
                    "KOREA" + ":" + '"' + KOREAVote + '"' + "," +
                    "CHINA" + ":" + '"' + CHINAVote + '"' +
                    "},"

                // Button State
                if (haspartnervote == "Partner") {
                    if (status == "Pending") {
                        document.getElementById("BtnRemove").disabled = true;
                        document.getElementById("BtnResubmit").disabled = true;
                        document.getElementById("BtnWithdraw").disabled = true;
                    };
                    if (status == "Rejected") {
                        document.getElementById("BtnRemove").disabled = true;
                        document.getElementById("BtnWithdraw").disabled = true;
                        document.getElementById("BtnAccept").disabled = true;
                        document.getElementById("BtnReject").disabled = true;
                    };
                    if (status == "Removed") {
                        document.getElementById("BtnRemove").disabled = true;
                        document.getElementById("BtnWithdraw").disabled = true;
                        document.getElementById("BtnAccept").disabled = true;
                        document.getElementById("BtnReject").disabled = true;
                    };
                    if (status == "Approved") {
                        document.getElementById("BtnResubmit").disabled = true;
                        document.getElementById("BtnWithdraw").disabled = true;
                        document.getElementById("BtnAccept").disabled = true;
                        document.getElementById("BtnReject").disabled = true;
                    };
                };

                switch (pID) {
                    case "1":
                        //alert("USPTO");
                        if (status == "Pending" && USPTOVote != "Pending") {
                            document.getElementById("BtnAccept").disabled = true;
                            document.getElementById("BtnReject").disabled = true;
                        };
                        break;
                    case "3":
                        //alert("JPO");
                        if (status == "Pending" && JPOVote != "Pending") {
                            document.getElementById("BtnAccept").disabled = true;
                            document.getElementById("BtnReject").disabled = true;
                        };
                        break;
                    case "5":
                        //alert("OHIM");
                        if (status == "Pending" && OHIMVote != "Pending") {
                            document.getElementById("BtnAccept").disabled = true;
                            document.getElementById("BtnReject").disabled = true;
                        };
                        break;
                    case "7":
                        //alert("KOREA");
                        if (status == "Pending" && KOREAVote != "Pending") {
                            document.getElementById("BtnAccept").disabled = true;
                            document.getElementById("BtnReject").disabled = true;
                        };
                        break;
                    case "14":
                        //alert("CHINA");
                        if (status == "Pending" && CHINAVote != "Pending") {
                            document.getElementById("BtnAccept").disabled = true;
                            document.getElementById("BtnReject").disabled = true;
                        };
                        break;
                    default:
                        //alert("VISITOR");
                        break;
                }
                names.push(idnum, name, ic, status, comment, translation);
            }
            var strarraystring = strarray.substr(0, strarray.length - 1);
            strarraystring = strarraystring + "]";
            //alert(strarraystring);
            //alert(haspartnervote);
        }
    }

    function ButtonState()
    {
        //alert("Button State")
        document.getElementById("BtnWithdraw").disabled = true;
        var pID = document.getElementById('valPartnerID').value;
        switch (pID) {
            case "1":
                //alert("USPTO");
                break;
            case "3":
                //alert("JPO");
                break;
            case "5":
                //alert("OHIM");
                break;
            case "7":
                //alert("KOREA");
                break;
            case "14":
                //alert("CHINA");
                break;
            default:
                //alert("VISITOR");
                break;
        }
    }


    function createDialog(){
        var dialog = $('#popDialog');
        dialog.dialog({
            bgiframe: true,
            modal: true,
            width: 1000,
            title: "Perform Action",
            buttons:
            {
                Submit: function ()
                {
                },

                Cancel: function ()
                {
                    $(this).dialog('close');
                    //window.location.reload();
                }

            },
            open: function(event, ui) { initGrid(); }
    })
    }

    function editRow() {
        var grid = jQuery("#<%= Grid1.ClientID %>");
        var rowKey = grid.getGridParam("selrow");
        var editOptions = grid.getGridParam('editDialogOptions');
        if (rowKey) {
            grid.editGridRow(rowKey, editOptions);
        }
        else {
            alert("No rows are selected");
        }
    }
</script>

    <style type="text/css">
        .jqTransformButton {}
        .auto-style1 {}
        </style>

</head>

<body>

<form id="GridForm" runat="server" method="post">
<div class="content">
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

<%--<asp:Button ID="btnToServer" Runat="server" Text="To Server" />--%>

<asp:HiddenField ID="ItemPkgArray" runat="server" />
<asp:HiddenField ID="Hidden1" runat="server" /> 
<asp:HiddenField ID="valID" runat="server" /> 
<asp:HiddenField ID="valItem" runat="server" /> 
<asp:HiddenField ID="valPartnerID" runat="server" /> 
<asp:HiddenField ID="valHasPartnerVote" runat="server" /> 
<asp:HiddenField ID="valButtonClicked" runat="server" /> 
<asp:HiddenField ID="valRowIDForSubGrid" runat="server" /> 

    <div id="prec">
	<div id="wrap">
			<div id="menu">
						<ul>	
						<li><a href="/SubmitID.aspx" title="Submit IDs">SUBMIT IDS</a></li>
						<li><a href="#" title="User Guide">USER GUIDE</a></li>
                        <li><a href="/Logon.aspx" title="Logout">LOGOUT</a></li>
						</ul>
			</div>
	</div>
    </div>

<div class="content">
	<div id="grid">
    <asp:SqlDataSource ID="SqlDataSource1" 
                       runat="server" 
                       ConnectionString="<%$ ConnectionStrings:TrilateralConnectionString %>" 
                       SelectCommand="SELECT ti.i_Item_ID As [ID], 
                                    ti.u_Item_Name As [Item], 
                                    ti.i_Class_ID As [Class], 
                                    (select u_Code_Meaning
                                    from TRI_Lookup_Status
                                    where ti.i_Status = i_Code_Value) As [Status], 
                                    COUNT(tv.i_Item_ID) As [Vote Count],

                                    (CASE WHEN CAST (90-(DATEDIFF(day, ti.dt_Released, GETDATE())) as nvarchar) < 0 THEN '' ELSE CAST (90-(DATEDIFF(day, ti.dt_Released, GETDATE())) as nvarchar) END) As [Days Left],

                                    ISNULL((select top 1 u_vote_name
                                    from Tri_Vote_Types tvt
                                    join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id
                                    where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID
                                    and tvsq.i_Partner_ID = 1
                                    and tvsq.i_Item_ID = ti.i_Item_ID
                                    order by tvsq.dt_Created),'Pending') As [USPTO],

                                    ISNULL((select top 1 u_vote_name
                                    from Tri_Vote_Types tvt
                                    join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id
                                    where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID
                                    and tvsq.i_Item_ID = ti.i_Item_ID
                                    and tvsq.i_Partner_ID = 3
                                    order by tvsq.dt_Created),'Pending') As [JPO],

                                    ISNULL((select top 1 u_vote_name
                                    from Tri_Vote_Types tvt
                                    join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id
                                    where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID
                                    and tvsq.i_Item_ID = ti.i_Item_ID
                                    and tvsq.i_Partner_ID = 5
                                    order by tvsq.dt_Created),'Pending') As [OHIM],

                                    ISNULL((select top 1 u_vote_name
                                    from Tri_Vote_Types tvt
                                    join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id
                                    where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID
                                    and tvsq.i_Item_ID = ti.i_Item_ID
                                    and tvsq.i_Partner_ID = 7
                                    order by tvsq.dt_Created),'Pending') As [Korea],

                                    ISNULL((select top 1 u_vote_name
                                    from Tri_Vote_Types tvt
                                    join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id
                                    where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID
                                    and tvsq.i_Item_ID = ti.i_Item_ID
                                    and tvsq.i_Partner_ID = 9
                                    order by tvsq.dt_Created),'Pending') As [Mexico],
                                    
                                    ISNULL((select top 1 u_vote_name
                                    from Tri_Vote_Types tvt
                                    join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id
                                    where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID
                                    and tvsq.i_Item_ID = ti.i_Item_ID
                                    and tvsq.i_Partner_ID = 10
                                    order by tvsq.dt_Created),'Pending') As [Philippines],

                                    ISNULL((select top 1 u_vote_name
                                    from Tri_Vote_Types tvt
                                    join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id
                                    where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID
                                    and tvsq.i_Item_ID = ti.i_Item_ID
                                    and tvsq.i_Partner_ID = 11
                                    order by tvsq.dt_Created),'Pending') As [Singapore],

                                    ISNULL((select top 1 u_vote_name
                                    from Tri_Vote_Types tvt
                                    join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id
                                    where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID
                                    and tvsq.i_Item_ID = ti.i_Item_ID
                                    and tvsq.i_Partner_ID = 12
                                    order by tvsq.dt_Created),'Pending') As [Russia],

                                    ISNULL((select top 1 u_vote_name
                                    from Tri_Vote_Types tvt
                                    join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id
                                    where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID
                                    and tvsq.i_Item_ID = ti.i_Item_ID
                                    and tvsq.i_Partner_ID = 14
                                    order by tvsq.dt_Created),'Pending') As [China],

                                    ti.dt_Created As [Created], 
                                    ti.dt_Released As [Released], 
                                    ti.dt_Accepted As [Approved], 
                                    ti.dt_Rejected As [Rejected],
                                    ti.dt_Removed As [Removed]          
                                    FROM TRI_Items ti
                                    JOIN TRI_Votes tv on ti.i_Item_ID = tv.i_Item_ID

                                    group by ti.i_Item_ID, ti.u_Item_Name, ti.i_Class_ID, ti.i_Status, ti.dt_Created, ti.dt_Released, ti.dt_Accepted, ti.dt_Rejected, ti.dt_Removed" />
    
<%--     <asp:SqlDataSource runat="server" 
                        ID="SqlDataSource3" 
                        ConnectionString="<%$ ConnectionStrings:TrilateralConnectionString %>" 
                        SelectCommand="SELECT tt.dt_Created As 'Date Created', tu.u_UserName As 'User', tt.u_Translation As 'Translation', tvt.u_Vote_Name As 'Vote' 
                                       FROM TRI_Translations tt
                                       JOIN TRI_Users tu ON tu.i_User_ID = tt.i_User_ID_Created_By
                                       JOIN TRI_Items ti ON ti.i_Item_ID = tt.i_Item_ID 
                                       JOIN TRI_Votes tv on tv.i_Item_ID = tt.i_Item_ID
                                       JOIN TRI_Vote_Types tvt on tvt.i_Vote_Type_ID = tv.i_vote_type_id
                                       WHERE ti.i_Item_ID = @ID">
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="ID" DbType="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>--%>
    
     <asp:SqlDataSource runat="server" 
                        ID="SqlDataSource2" 
                        ConnectionString="<%$ ConnectionStrings:TrilateralConnectionString %>" 
                        SelectCommand="SELECT DISTINCT tt.i_Item_ID, tt.dt_Created As 'Created', tp.u_Partner_Name As 'Partner', tt.u_Translation As 'Translation' 
                                       FROM TRI_Translations tt
                                       JOIN TRI_Users tu ON tu.i_User_ID = tt.i_User_ID_Created_By
                                       JOIN TRI_Partners tp ON tp.i_Partner_ID = tu.i_Partner_ID
                                       JOIN TRI_Items ti ON ti.i_Item_ID = tt.i_Item_ID 
                                       JOIN TRI_Votes tv on tv.i_Item_ID = tt.i_Item_ID
                                       JOIN TRI_Vote_Types tvt on tvt.i_Vote_Type_ID = tv.i_vote_type_id
                                       WHERE ti.i_Item_ID = @ID">
                                       <%--AND ti.i_Status = 2">--%>
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="ID" DbType="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

     <asp:SqlDataSource runat="server" 
                        ID="SqlDataSource3" 
                        ConnectionString="<%$ ConnectionStrings:TrilateralConnectionString %>" 
                        SelectCommand="SELECT DISTINCT tc.i_Item_ID, tc.dt_Created As 'Created', tp.u_Partner_Name As 'Partner', tc.u_Comment As 'Comment' 
                                       FROM TRI_Comments tc
                                       JOIN TRI_Users tu ON tu.i_User_ID = tc.i_User_ID_Created_By
                                       JOIN TRI_Partners tp ON tp.i_Partner_ID = tu.i_Partner_ID
                                       JOIN TRI_Items ti ON ti.i_Item_ID = tc.i_Item_ID 
                                       JOIN TRI_Votes tv on tv.i_Item_ID = tc.i_Item_ID
                                       JOIN TRI_Vote_Types tvt on tvt.i_Vote_Type_ID = tv.i_vote_type_id
                                       WHERE ti.i_Item_ID = @ID">
                                       <%--AND ti.i_Status = 2">--%>
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="ID" DbType="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSourceMainPending" 
                       runat="server" 
                       ConnectionString="<%$ ConnectionStrings:TrilateralConnectionString %>" 
                       SelectCommand="SELECT ti.i_Item_ID As [ID], 
                                    ti.u_Item_Name As [Item], 
                                    ti.i_Class_ID As [Class], 
                                    (select u_Code_Meaning
                                    from TRI_Lookup_Status
                                    where ti.i_Status = i_Code_Value) As [Status], 
                                    COUNT(tv.i_Item_ID) As [Vote Count],

                                    (CASE WHEN CAST (90-(DATEDIFF(day, ti.dt_Released, GETDATE())) as nvarchar) < 0 THEN '' ELSE CAST (90-(DATEDIFF(day, ti.dt_Released, GETDATE())) as nvarchar) END) As [Days Left],

                                    ISNULL((select top 1 u_vote_name
                                    from Tri_Vote_Types tvt
                                    join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id
                                    where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID
                                    and tvsq.i_Partner_ID = 1
                                    and tvsq.i_Item_ID = ti.i_Item_ID
                                    order by tvsq.dt_Created),'Pending') As [USPTO],

                                    ISNULL((select top 1 u_vote_name
                                    from Tri_Vote_Types tvt
                                    join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id
                                    where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID
                                    and tvsq.i_Item_ID = ti.i_Item_ID
                                    and tvsq.i_Partner_ID = 3
                                    order by tvsq.dt_Created),'Pending') As [JPO],

                                    ISNULL((select top 1 u_vote_name
                                    from Tri_Vote_Types tvt
                                    join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id
                                    where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID
                                    and tvsq.i_Item_ID = ti.i_Item_ID
                                    and tvsq.i_Partner_ID = 5
                                    order by tvsq.dt_Created),'Pending') As [OHIM],

                                    ISNULL((select top 1 u_vote_name
                                    from Tri_Vote_Types tvt
                                    join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id
                                    where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID
                                    and tvsq.i_Item_ID = ti.i_Item_ID
                                    and tvsq.i_Partner_ID = 7
                                    order by tvsq.dt_Created),'Pending') As [Korea],

                                    ISNULL((select top 1 u_vote_name
                                    from Tri_Vote_Types tvt
                                    join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id
                                    where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID
                                    and tvsq.i_Item_ID = ti.i_Item_ID
                                    and tvsq.i_Partner_ID = 9
                                    order by tvsq.dt_Created),'Pending') As [Mexico],
                                    
                                    ISNULL((select top 1 u_vote_name
                                    from Tri_Vote_Types tvt
                                    join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id
                                    where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID
                                    and tvsq.i_Item_ID = ti.i_Item_ID
                                    and tvsq.i_Partner_ID = 10
                                    order by tvsq.dt_Created),'Pending') As [Philippines],

                                    ISNULL((select top 1 u_vote_name
                                    from Tri_Vote_Types tvt
                                    join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id
                                    where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID
                                    and tvsq.i_Item_ID = ti.i_Item_ID
                                    and tvsq.i_Partner_ID = 11
                                    order by tvsq.dt_Created),'Pending') As [Singapore],

                                    ISNULL((select top 1 u_vote_name
                                    from Tri_Vote_Types tvt
                                    join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id
                                    where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID
                                    and tvsq.i_Item_ID = ti.i_Item_ID
                                    and tvsq.i_Partner_ID = 12
                                    order by tvsq.dt_Created),'Pending') As [Russia],

                                    ISNULL((select top 1 u_vote_name
                                    from Tri_Vote_Types tvt
                                    join Tri_Votes tvsq on tvt.i_Vote_Type_ID = tvsq.i_vote_type_id
                                    where tvsq.i_Vote_Type_ID = tvt.i_Vote_Type_ID
                                    and tvsq.i_Item_ID = ti.i_Item_ID
                                    and tvsq.i_Partner_ID = 14
                                    order by tvsq.dt_Created),'Pending') As [China],

                                    ti.dt_Created As [Created], 
                                    ti.dt_Released As [Released], 
                                    ti.dt_Accepted As [Approved], 
                                    ti.dt_Rejected As [Rejected],
                                    ti.dt_Removed As [Removed]          
                                    FROM TRI_Items ti
                                    JOIN TRI_Votes tv on ti.i_Item_ID = tv.i_Item_ID
                                    WHERE ti.i_Status = 2
                                    group by ti.i_Item_ID, ti.u_Item_Name, ti.i_Class_ID, ti.i_Status, ti.dt_Created, ti.dt_Released, ti.dt_Accepted, ti.dt_Rejected, ti.dt_Removed" />
        <br />

<table>      
    <tr>
    <trirand:JQDatePicker 
        DisplayMode="ControlEditor"
        runat="server" 
        ID="DatePicker0" 
        DateFormat="MM-dd-yyyy"
        MinDate="2004-01-01" 
        MaxDate="2020-01-01" 
        ChangeYear="true"
        ShowOn="Focus" /> 

    <trirand:JQDatePicker 
        DisplayMode="ControlEditor"
        runat="server" 
        ID="DatePicker1" 
        DateFormat="MM-dd-yyyy"
        MinDate="2004-01-01" 
        MaxDate="2020-01-01" 
        ChangeYear="true"
        ShowOn="Focus" /> 
    </tr>
</table> 
<div id ="lnkPanel">
    <asp:LinkButton ID="lnkButtonPending" runat="server" Text="Pending" OnClick="lnkButtonPending_Click"></asp:LinkButton>&nbsp &nbsp
    <asp:LinkButton ID="lnkButtonAll" runat="server" Text="All" OnClick="lnkButtonAll_Click"></asp:LinkButton>
&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Label ID="lblExpandedID" runat="server" Text="" visible="false" ForeColor="Gray"></asp:Label>    
<input type="button" id="BtnGetHiddenRowID" style="visibility: hidden" value="GetVal" runat="server" onclick="GetHiddenRowID_Click"/>         
</div>
<div id ="btnPanel">
    <asp:Table ID="Table1" runat="server">
            <asp:TableHeaderRow runat="server" ForeColor="Gray">  
                <asp:TableHeaderCell>
                    <input type="button" id="BtnAccept" value="Accept" onclick="ShowSelectedGrid(document.getElementById('BtnAccept').value)"/></asp:TableHeaderCell>  
                <asp:TableHeaderCell>
                     <input type="button" id="BtnReject" value="Reject" onclick="ShowSelectedGrid(document.getElementById('BtnReject').value)"/></asp:TableHeaderCell> 
                <asp:TableHeaderCell>
                    <input type="button" id="BtnRemove" value="Remove" runat="server" onclick="ShowSelectedGrid(document.getElementById('BtnRemove').value)"/></asp:TableHeaderCell>
                <asp:TableHeaderCell>
                    <input type="button" id="BtnResubmit" value="Resubmit" runat="server" onclick="ShowSelectedGrid(document.getElementById('BtnResubmit').value)"/></asp:TableHeaderCell>
                <asp:TableHeaderCell>
                    <input type="button" id="BtnWithdraw" style="visibility: hidden" value="Withdraw" runat="server" onclick="ShowSelectedGrid(document.getElementById('BtnWithdraw').value)"/></asp:TableHeaderCell>
                <asp:TableHeaderCell>
                    <input type="button" id="btnShowGrid" style="visibility: hidden" value="Show Grid" onclick="ShowSelectedGrid()"/></asp:TableHeaderCell>                            
            </asp:TableHeaderRow>        
    </asp:Table>        
</div>
                
<br/>        
<asp:Label ID="lblSQLDatasource" runat="server"></asp:Label>                         
<asp:Label ID="lblSelected" runat="server"></asp:Label>
<div id="MainGridWrapper">     
<div style="width:1200px;overflow:auto;"><table id="MainGridContainer">                        
    <trirand:JQGrid runat="server" ID="Grid1" style="margin-bottom: 65px" MultiSelectMode="SelectOnRowClick" MultiSelect="true" AutoWidth="False" rownum="50" loadonce="false"
        OnRowEditing="Grid1_RowEditing" 
        OnDataRequesting="Grid1_DataRequesting"
        AppearanceSettings-ShrinkToFit="true" Height ="200px"  >
        <Columns>
            <trirand:JQGridColumn DataField="ID" Editable="false" Width="50"/>
            <trirand:JQGridColumn DataField="Days Left" Editable="false" Visible="true" Width="58"/>
            
<%--            <trirand:JQGridColumn Searchable="False" Visible="true" Width="58" HeaderText="Days Left">
            </trirand:JQGridColumn>--%>

            <trirand:JQGridColumn DataField="Item" Editable="false" Width="250"/>
            <trirand:JQGridColumn DataField="Class" Editable="false" Visible="true" Width="50"/>
            <trirand:JQGridColumn DataField="Status" Editable="false" Visible="true" Width="75"/>

            <trirand:JQGridColumn DataField="Created" Width="100"
                                  DataType="DateTime"
                                  Searchable="false" 
                                  Editable="false" 
                                  SearchType="DatePicker"
                                  SearchControlID="DatePicker1" 
                                  DataFormatString="{0:MM-dd-yyyy}" 
                                  Visible="false" />
            <trirand:JQGridColumn DataField="Released" Width="100"
                                  DataType="DateTime"
                                  Searchable="true"
                                  Editable="false" 
                                  SearchType="DatePicker" 
                                  SearchControlID="DatePicker1" 
                                  DataFormatString="{0:MM-dd-yyyy}" />
            <trirand:JQGridColumn DataField="Vote Count" Editable="false" Visible="true" Width="75"/>
            <trirand:JQGridColumn DataField="USPTO" Editable="false" Visible="true" Width="60"/>
            <trirand:JQGridColumn DataField="JPO" Editable="false" Visible="true" Width="60"/>
            <trirand:JQGridColumn DataField="OHIM" Editable="false" Visible="true" Width="60"/>
            <trirand:JQGridColumn DataField="KOREA" Editable="false" Visible="true" Width="60"/>
            <trirand:JQGridColumn DataField="CHINA" Editable="false" Visible="true" Width="60"/>
            <trirand:JQGridColumn DataField="MEXICO" Editable="false" Visible="true" Width="60"/>
            <trirand:JQGridColumn DataField="PHILIPPINES" Editable="false" Visible="true" Width="75"/>
            <trirand:JQGridColumn DataField="SINGAPORE" Editable="false" Visible="true" Width="75"/>
            <trirand:JQGridColumn DataField="RUSSIA" Editable="false" Visible="true" Width="75"/>
            <trirand:JQGridColumn DataField="Approved" Width="75"
                                  DataType="DateTime"
                                  Searchable="true" 
                                  Editable="false" 
                                  SearchType="DatePicker" 
                                  SearchControlID="DatePicker1" 
                                  DataFormatString="{0:MM-dd-yyyy}" 
                                  Visible="true" />
            <trirand:JQGridColumn DataField="Rejected" Width="75"
                                  DataType="DateTime"
                                  Searchable="true" 
                                  Editable="false" 
                                  SearchType="DatePicker" 
                                  SearchControlID="DatePicker1" 
                                  DataFormatString="{0:MM-dd-yyyy}" 
                                  Visible="true" />
            <trirand:JQGridColumn DataField="Removed" Width="75"
                                  DataType="DateTime"
                                  Searchable="true" 
                                  Editable="false" 
                                  SearchType="DatePicker" 
                                  SearchControlID="DatePicker1" 
                                  DataFormatString="{0:MM-dd-yyyy}" 
                                  Visible="true" />
        </Columns>

        <ClientSideEvents SubGridRowExpanded="showSubGrids" RowSelect="GetGridData" />
        <HierarchySettings HierarchyMode="Parent" />
        <ToolBarSettings ShowEditButton="False" ShowRefreshButton="True" ShowAddButton="False" ShowDeleteButton="False"
            ShowSearchButton="True" />
        <SearchDialogSettings MultipleSearch="true" />
        <EditDialogSettings CloseAfterEditing="true" Caption="Edit Item Data" />
        <AddDialogSettings CloseAfterAdding="true" /> 

        <%--<PivotSettings RowTotals="False" RowTotalsText="Total" ColTotals="False" GroupSummary="True" GroupSummaryPosition="Header" FrozenStaticCols="False"></PivotSettings>--%>
        <%--<PivotSettings RowTotals="False" RowTotalsText="Total" ColTotals="False" GroupSummary="True" GroupSummaryPosition="Header" FrozenStaticCols="False"></PivotSettings>--%>

<PivotSettings RowTotals="False" RowTotalsText="Total" ColTotals="False" GroupSummary="True" GroupSummaryPosition="Header" FrozenStaticCols="False"></PivotSettings>
        </trirand:JQGrid> 
</table></div>
<br/>

    <trirand:JQGrid runat="server" ID="Grid2" DataSourceID="SqlDataSource2"
        OnDataRequesting="JQGrid2_DataRequesting" OnRowAdding="JQGrid2_RowAdding" height='100%' Width="800" >        
        <Columns>
            <trirand:JQGridColumn DataField="Translation" Editable="true" Width="500"/>
            <trirand:JQGridColumn DataField="Partner" Width="100"/>
            <trirand:JQGridColumn DataField="Created" Width="200"/>
        </Columns>
        <HierarchySettings HierarchyMode="Child" />
    <ToolBarSettings ShowEditButton="False" ShowRefreshButton="False" ShowAddButton="True" ShowDeleteButton="False"
        ShowSearchButton="False" />
     <AddDialogSettings 
                 CancelText="Cancel"
                 Caption="Add Translation"
                 ClearAfterAdding="true"
                 CloseAfterAdding="true"
                 Draggable="true"
                 Height="100"
                 Width="300"

                 LoadingMessageText="Adding a new row"
                 Modal="true"
                 ReloadAfterSubmit="true"
                 Resizable="false"
                 SubmitText="Add"
        />
        <HierarchySettings HierarchyMode="Child" />
    </trirand:JQGrid>

    <%--<trirand:codetabs runat="server" id="tabs"></trirand:codetabs>--%> 
    <%--<PivotSettings RowTotals="False" RowTotalsText="Total" ColTotals="False" GroupSummary="True" GroupSummaryPosition="Header" FrozenStaticCols="False"></PivotSettings>--%>

    <trirand:JQGrid runat="server" ID="Grid3" DataSourceID="SqlDataSource3"
        OnDataRequesting="JQGrid3_DataRequesting" OnRowAdding="JQGrid3_RowAdding" height='100%' Width="800" >        
        <Columns>
            <trirand:JQGridColumn DataField="Comment" Editable="true" Width="500"/>
            <trirand:JQGridColumn DataField="Partner" Width="100"/>
            <trirand:JQGridColumn DataField="Created" Width="200"/>
        </Columns>
        <HierarchySettings HierarchyMode="Child" />
    <ToolBarSettings ShowEditButton="False" ShowRefreshButton="False" ShowAddButton="True" ShowDeleteButton="False"
        ShowSearchButton="False" />
    <AddDialogSettings 
                 CancelText="Cancel"
                 Caption="Add Comment"
                 ClearAfterAdding="true"
                 CloseAfterAdding="true"
                 Draggable="true"
                 Height="100"
                 Width="300"

                 LoadingMessageText="Adding a new row"
                 Modal="true"
                 ReloadAfterSubmit="true"
                 Resizable="false"
                 SubmitText="Add"
     />
    <HierarchySettings HierarchyMode="Child" />
    </trirand:JQGrid>

    <%--<trirand:codetabs runat="server" id="tabs"></trirand:codetabs>--%> 
    <%--<PivotSettings RowTotals="False" RowTotalsText="Total" ColTotals="False" GroupSummary="True" GroupSummaryPosition="Header" FrozenStaticCols="False"></PivotSettings>--%>

    <asp:Button Text="Export" OnClick="ExportToExcelButton_Click" runat="server" ID="ExportToExcelButton" CssClass="auto-style1" Width="80px" />
<br/>
<br/>
</div>

<div id ="grdSelectedWrapper">
<%--<table id="grdSelectedContainer">--%>
<asp:Label ID="lblActionToPerform" runat="server" Text="" ForeColor="Gray"></asp:Label>             
    <table id ="grdSelected">
    </table>
<%--</table>--%>
    <div id="grdSelectedPager" class="scroll" style="text-align: center;">
    </div>
<br/>
<asp:Button ID="btnInsertVotes" runat="server" Text="Submit" Width="80px" OnClick ="btnInsertVotes_Click"  />
<input type="button" value="Cancel" onclick="HideSelectedGrid()"/>
</div>

<br/>
<br/>

<div id="grdAlternateWrapper">
<table id="grdMainContainer">         
    <table id ="grdMain">
    </table>
</table>
    <div id="pager" class="scroll" style="text-align: center;">
    </div>
</div>
<br/> 

<br/>

<br/>

<asp:Label ID="lblUserInfo" runat="server" Text="Label" Visible="false"></asp:Label>
<br/> 
<asp:Label ID="lblCanVote" runat="server" Text="Testing" Visible ="false"></asp:Label>

<%--<PivotSettings RowTotals="False" RowTotalsText="Total" ColTotals="False" GroupSummary="True" GroupSummaryPosition="Header" FrozenStaticCols="False"></PivotSettings>--%><%--<ToolBarSettings ShowRefreshButton="true" ShowSearchButton="true" ToolBarPosition="Bottom">        
            <CustomButtons>
                <trirand:JQGridToolBarButton
                    Text = "Send Mail"
                    ToolTip = "Send Mail"
                    ButtonIcon = "ui-icon-mail-closed"
                    Position = "Last"
                />
                <trirand:JQGridToolBarButton
                   Text = ""
                   ToolTip = "Send Mail"
                   ButtonIcon = "ui-icon-pencil"
                   Position = "Last"
                />
            </CustomButtons>
        </ToolBarSettings>--%><%--<PivotSettings RowTotals="False" RowTotalsText="Total" ColTotals="False" GroupSummary="True" GroupSummaryPosition="Header" FrozenStaticCols="False"></PivotSettings>--%><%--<PivotSettings RowTotals="False" RowTotalsText="Total" ColTotals="False" GroupSummary="True" GroupSummaryPosition="Header" FrozenStaticCols="False"></PivotSettings>--%><%--<ToolBarSettings ShowRefreshButton="true" ShowSearchButton="true" ToolBarPosition="Bottom">        
            <CustomButtons>
                <trirand:JQGridToolBarButton
                    Text = "Send Mail"
                    ToolTip = "Send Mail"
                    ButtonIcon = "ui-icon-mail-closed"
                    Position = "Last"
                />
                <trirand:JQGridToolBarButton
                   Text = ""
                   ToolTip = "Send Mail"
                   ButtonIcon = "ui-icon-pencil"
                   Position = "Last"
                />
            </CustomButtons>
        </ToolBarSettings>--%>

    <script type="text/javascript">
        function showSubGrids(subgrid_id, row_id) {
            // the "showSubGrid_JQGrid2" function is autogenerated and available globally on the page by the second child grid. 
            // Calling it will place the child grid below the parent expanded row and will call the OnDataRequesting event 
            // of the child grid, with ID equal to the ID of the parent expanded row   
            var mainGridID = row_id;
            document.getElementById('<%= valRowIDForSubGrid.ClientID %>').value = mainGridID;
            var testHiddenField = document.getElementById('<%= valRowIDForSubGrid.ClientID %>').value;
            //document.getElementById("lblExpandedID").innerText = testHiddenField;
            showSubGrid_Grid2(subgrid_id, row_id, "<b><i>Translations</b><i/>", "Grid2");
            showSubGrid_Grid3(subgrid_id, row_id, "<b><i>Comments</b><i/>", "Grid3");
        }
    </script>
</div>
</div>
    </form>

    <%--<PivotSettings RowTotals="False" RowTotalsText="Total" ColTotals="False" GroupSummary="True" GroupSummaryPosition="Header" FrozenStaticCols="False"></PivotSettings>--%>
	
    <div id="footer">
	<%--<div class="right">Design: <a href="https://TMIDList.org">Neale Faunt</a></div>--%>
	<a href="#" title="Validate">XHTML</a> - <a href="#" title="Validate">CSS</a>
	</div>

</body>
</html>