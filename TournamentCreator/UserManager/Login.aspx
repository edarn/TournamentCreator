<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TournamentCreator.UserManager.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title>Tournament Creator :: Sign In</title>
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
                        <asp:Label runat="server" id="lblError" CssClass="error"></asp:Label>
                        </td>
                        </tr>
                        
                       
                       
                        <tr>
                        <td class="frmTxt">Email Address : 
                        </td>
                        <td class="frmCont">
                            <asp:TextBox runat="server" ID="txtEmailAddress"></asp:TextBox><span class="caution">&nbsp;*</span>
                            <asp:RequiredFieldValidator CssClass="caution" Text="(Invalid Email Address)" runat="server" ID="rfvEmail" ControlToValidate="txtEmailAddress"></asp:RequiredFieldValidator>
                            <br /><asp:RegularExpressionValidator CssClass="caution" Text="(Invalid Email Address)" runat="server" ID="revEmail" ControlToValidate="txtEmailAddress" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </td>
                        </tr>
                        <tr>
                        <td class="frmTxt">Password : 
                        </td>
                        <td class="frmCont">
                            <asp:TextBox runat="server" ID="txtPassword" TextMode="Password"></asp:TextBox><span class="caution">&nbsp;*</span>
                            <asp:RequiredFieldValidator CssClass="caution" Text="(Invalid Password)" runat="server" ID="rfvPassword" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                        </td>
                        </tr>
                       
                        <tr>
                        <td class="frmTxt">
                        </td>
                        <td class="frmCont">
                        Not a member yet? <a href="Signup.aspx">Signup</a>
                        <br />
                            <a href="ForgetPassword.aspx">Forgot Password?</a>
                        </td>
                        </tr>
                        
                        <tr>
                        <td></td>
                        <td class="frmCont">
                            <asp:Button Text="Sign In" runat="server" id="btnLogin" onclick="btnLogin_Click"
                                 /></td>
                        </tr>

                        <tr>
                        <td></td>
                        <td class="frmCont"><span class="caution">* indicates required fields</span></td>
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
