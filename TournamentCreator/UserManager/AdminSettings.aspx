<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminSettings.aspx.cs" Inherits="TournamentCreator.UserManager.AdminSettings" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title>Tournament Creator :: Admin Settings</title>
    <link href="../styles/stylesheet.css" rel="stylesheet" type="text/css" />
    <link href="../styles/aspstyles.css" rel="stylesheet" type="text/css" />
    </head>

<body>
    <div id="container">
		<div id="bar">
			<div id="barcontents">
				<asp:Label runat="server" ID="lblTop" CssClass="clblTop"></asp:Label>
			</div>
		</div>
	</div>
	<div id="banner">
	<div id="bannerInner"><asp:Label ID="lblHeading" runat="server"></asp:Label></div>
	</div>
	<div id="wrapper">
		<table style="width:100%" cellpadding="0" cellspacing="0">
		<tr>
		<td style="width:25%" valign="top" id="menu" >
		<br/><br/>
		<div class="sb_content">

                  <!--##########-->
                  <!-- Start Menu -->
                  <ul id="navlist" runat="server">
  		  			
  	  			  </ul>  

        </div>
		</td>
		<td valign="top" style="width:70%; background-color:white;">
			<table style="width:100%" cellpadding=0 cellspacing=0>
				<tr id="navigation">
					<td>
						<asp:Label CssClass="clblTop" runat="server" ID="lblNavigation"></asp:Label>
					</td>
				</tr>
                <tr>
                <td>
                   <form id="frmSignup" runat = "server">
                      <table style="width:100%" cellpadding="1" cellspacing="1" class="post">
                        <tr>
                        <td style="text-align:left; vertical-align:top; height:30px" colspan="2">
                        <a href="../AdminSteps.aspx">Step by Step Admin Guide</a>
                        </td>
                        </tr>
                        
                       
                       
                        <tr>
                        <td class="frmTxt"> 
                        </td>
                        <td class="frmCont">
                            <asp:CheckBox id="cbAllowUserResult" Text="Allow Users to Submit Results" runat="server" />
                        </td>
                        </tr>

                        <tr>
                        <td class="frmTxt"> 
                        </td>
                        <td class="frmCont">
                            &nbsp;&nbsp;<asp:Label CssClass="error" runat="server" ID="lblError"></asp:Label>
                        </td>
                        </tr>
                       
                        
                        <tr>
                        <td></td>
                        <td class="frmCont">
                            <asp:Button Text="Submit" runat="server" id="btnSubmit" onclick="btnSubmit_Click" 
                                 /></td>
                        </tr>

                       
                   </table>
                   
                   </form>
                </td>
                </tr>
			</table>
		</td>
		
        </tr>
		</table>
	</div>
    </body>

</html>
