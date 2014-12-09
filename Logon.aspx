<%@ Page Language="C#" enableSessionState="true" AutoEventWireup="true"  CodeBehind="Logon.aspx.cs" Inherits="TMID.Logon" %>
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
    <link rel="stylesheet" type="text/css" media="screen" href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.10.0/themes/redmond/jquery-ui.css" />
    <!-- The jQuery UI theme extension jqGrid needs -->
    <link rel="stylesheet" type="text/css" media="screen" href="themes/ui.jqgrid.css" />
    <!-- jQuery runtime minified -->
    <script src="http://ajax.microsoft.com/ajax/jquery/jquery-1.9.0.min.js" type="text/javascript"></script>
    <script src="js/jquery-1.11.1.min.js" type="text/javascript"></script>
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
    $(function () {
        $('LogonForm').jqTransform({ imgPath: 'jqtransformplugin/img/' });
    });

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
	<div id="top">
				<div id="icons">
					<a href="#" title="Home page"><img src="images/home.gif" alt="Home" /></a>
					<a href="#" title="Contact us"><img src="images/contact.gif" alt="Contact" /></a>
				</div>
				<h1>Trademark Identification List</h1>
				<h2>The Five TM Nations</h2>
	</div>
	<!-- end of the Top part -->
</div>	

		<form id="LogonForm" runat="server">

<div id="prec" style="height: 70px;">
	<div id="wrap">
			<div id="menu">
						<ul>
                            <li><a href="#" title="Forgot Password">FORGOT PASSWORD</a></li>
                            <%--<li><a href="#" title=""></a></li>
						    <li><a href="#" title=""></a></li>--%>
						</ul>
			</div>
	</div>
</div>

<div class="content">
                                                <p></p>
                                                <asp:Label ID = "lblMainMessageLabel" runat="server" Text="Enter your username and password for access:"></asp:Label>
<br>
												<asp:Label id="lblMessageLabel" runat="server" Width="680px"></asp:Label>
												<p></p>
												<asp:Textbox id="txtUserName" runat="server"  Width="125px" TabIndex="1"></asp:Textbox> <asp:label id="lblUN" Text="User Name" runat="server"></asp:label>
												<p></p>
												<asp:Textbox id="txtPwd" runat="server" TextMode="Password"  Width="125px" TabIndex="2"></asp:Textbox> <asp:label id="lblPW" Text="Password" runat="server"></asp:label>
<br>
<br>													
                                                <asp:Button id="btnSubmitLogon" runat="server" Text="Submit" TabIndex="3" onclick="btnSubmitLogon_Click"></asp:Button>
<br>
<br>
</div>

    <script type="text/C#" runat="server">
        protected void Page_Load(object send, EventArgs args)
        {

        }
    </script>

</form>

    <div id="footer1"><p></p></div>
	<div id="footer">
	
	<a href="http://validator.w3.org/check?uri=referer" title="Validate">RESTRICTED</a> <a href="http://jigsaw.w3.org/css-validator/check/referer" title="Validate">ACCESS</a>
	<p>THIS WEBSITE CONTAINS INFORMATION THAT IS PRIVILEGED AND CONFIDENTIAL. IF THE READER OF THIS MESSAGE IS NOT AN AUTHORIZED EMPLOYEE OR AGENT OF THE OFFICE OF THE UNITED STATES PATENT AND TRADEMARK OFFICE, THE OFFICE OF HARMONIZATION OF INTERNAL MARKETS (OHIM), THE JAPAN PATENT OFFICE (JPO), THE KOREAN INTELLECTUAL PROPERTY OFFICE (KIPO), OR THE STATE ADMINISTRATION FOR INDUSTRY AND COMMERCE OF THE PEOPLE'S REPUBLIC OF CHINA (SAIC) YOU ARE HEREBY NOTIFIED THAT ANY TAMPERING WITH ANY INFORMATION HEREIN, DISSEMINATION, DISTRIBUTION, COPYING OR COMMUNICATION OF ANY INFORMATION FROM THIS WEBSITE IS STRICTLY PROHIBITED. UNAUTHORIZED ATTEMPTS TO UPLOAD INFORMATION OR CHANGE INFORMATION ON THIS SERVICE ARE STRICTLY PROHIBITED AND MAY BE PUNISHABLE UNDER THE COMPUTER FRAUD AND ABUSE ACT OF 1986 AND THE NATIONAL INFORMATION INFRASTRUCTURE PROTECTION ACT AS WELL AS LAWS OF OTHER JURISDICTIONS IN EUROPE, JAPAN, KOREA AND CHINA.</p>
    </div>
<%--</div>--%>
</body>
</html>