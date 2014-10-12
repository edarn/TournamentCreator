<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="TournamentCreator.UserManager.Signup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html>
<head>
    <title>Tournament Creator :: Signup</title>
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
                        <tr >
                        <td class="frmTxt">First Name : 
                        </td>
                        <td class="frmCont">
                            <asp:TextBox runat="server" ID="txtFirstName"></asp:TextBox><span class="caution">&nbsp;*</span>
                            <asp:RequiredFieldValidator CssClass="caution" Text="(Invalid First Name)" runat="server" ID="rfvFirstName" ControlToValidate="txtFirstName"></asp:RequiredFieldValidator>
                        </td>
                        </tr>    
                        <tr>
                        <td class="frmTxt">Last Name : 
                        </td>
                        <td class="frmCont">
                            <asp:TextBox runat="server" ID="txtLastName"></asp:TextBox><span class="caution">&nbsp;*</span>
                            <asp:RequiredFieldValidator CssClass="caution" Text="(Invalid Last Name)" runat="server" ID="rfvLastName" ControlToValidate="txtLastName"></asp:RequiredFieldValidator>
                        </td>
                        </tr>    
                        <tr>
                        <td class="frmTxt">Address : 
                        </td>
                        <td class="frmCont">
                            <asp:TextBox TextMode="MultiLine" runat="server" ID="txtAddress"></asp:TextBox><span class="caution">&nbsp;*</span>
                            <asp:RequiredFieldValidator CssClass="caution" Text="(Invalid Address)" runat="server" ID="rfvAddress" ControlToValidate="txtAddress"></asp:RequiredFieldValidator>
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
                        <td class="frmTxt">Confirm Password : 
                        </td>
                        <td class="frmCont">
                            <asp:TextBox runat="server" ID="txtConfirmPassword" TextMode="Password"></asp:TextBox><span class="caution">&nbsp;*</span>
                            <asp:RequiredFieldValidator CssClass="caution" Text="(Invalid Confirm Password)" runat="server" ID="rfvCPassword" ControlToValidate="txtConfirmPassword"></asp:RequiredFieldValidator>
                            <br /><asp:CompareValidator CssClass="caution" Text="(Password and confirm password must match)" runat="server" ID="cvPassword" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" ></asp:CompareValidator>
                        </td>
                        </tr>
                        <tr>
                        <td class="frmTxt">Phone Number : 
                        </td>
                        <td class="frmCont">
                            <asp:TextBox runat="server" ID="txtPhoneNumber"></asp:TextBox><span class="caution">&nbsp;*</span>
                            <asp:RequiredFieldValidator CssClass="caution" Text="(Invalid Phone Number)" runat="server" ID="rfvPhone" ControlToValidate="txtPhoneNumber"></asp:RequiredFieldValidator>
                        </td>
                         </tr>
                        
                        <tr>
                        <td></td>
                        <td class="frmCont">
                            <asp:Button Text="Sign Up" runat="server" id="btnSignup" onclick="btnSignup_Click"
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
