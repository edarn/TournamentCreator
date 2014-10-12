<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateTournament.aspx.cs" Inherits="TournamentCreator.TournamentManager.CreateTournament" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title>Tournament Creator :: Create Tournament</title>
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
                   <asp:Label ID="lblEdit" runat="server" Text="0" Visible="false"></asp:Label>
                   <asp:Label ID="lblTID" runat="server" Text="0" Visible="false"></asp:Label>
                        <table style="width:60%" cellpadding="1" cellspacing="1" class="post">
                            <tr>
                            <td colspan=2 style="text-align:left;">
                            <asp:Label ID="lblError" runat="server" CssClass="error"></asp:Label>
                            </td>
                            </tr>
                            <tr><td style="text-align:left; vertical-align:top; height:20px" colspan="2"></td></tr>
                            <tr>
                            <td class="frmTxt" style="width:30%;"> Name : 
                            </td>
                            <td class="frmCont">
                                <asp:TextBox id="txtName" runat="server" Width="180px"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator runat="server" CssClass="caution" ID="rfvTxtName" ControlToValidate="txtName">Invalid Tournament Name</asp:RequiredFieldValidator>
                            </td>
                            </tr>

                            <tr>
                            <td class="frmTxt" style="width:30%;"> Maximum Number of Players : 
                            </td>
                            <td class="frmCont">
                                <asp:TextBox id="txtMaxNum" runat="server" Width="180px"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator runat="server" CssClass="caution" ID="rfvMaxNum" ControlToValidate="txtMaxNum">Invalid Number</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" CssClass="caution" ID="revMaxNum" ValidationExpression="^\d+$" ControlToValidate="txtMaxNum">Invalid Number</asp:RegularExpressionValidator >
                            </td>
                            </tr>
                            
                            <tr><td style="text-align:left; vertical-align:top; height:20px" colspan="2"></td></tr>
                
                            <tr>
                            <td class="frmTxt"> Start Date : 
                            </td>
                            <td class="frmCont">
                                <asp:radiobuttonlist id="rblStartDate" runat="server">
                                    <asp:listitem id="liStartMan" runat="server" Selected="TRUE" value="startman" >Start Manually</asp:listitem>
                                    <asp:listitem id="liStartAuto" runat="server" value="startauto" >Start Automatically On</asp:listitem>
                     
                                </asp:radiobuttonlist>
                                <asp:Calendar ID="cldStartDate" runat="server"></asp:Calendar>
                            </td>
                            </tr>

                            <tr><td style="text-align:left; vertical-align:top; height:20px" colspan="2"></td></tr>

                            <tr class="frmTxt">
                            <td class="frmCont"> Stop Date : 
                            </td>
                            <td class="frmCont">
                                <asp:radiobuttonlist id="rblStopDate" runat="server">
                                    <asp:listitem id="liStopMan" runat="server" Selected="TRUE" value="stopman" >Stop Manually</asp:listitem>
                                    <asp:listitem id="liStopAuto" runat="server" value="stopauto" >Stop Automatically On</asp:listitem> 
                                </asp:radiobuttonlist>
                                <asp:Calendar ID="cldStopDate" runat="server"></asp:Calendar>
                            </td>
                            </tr>
                
                            <tr><td style="text-align:left; vertical-align:top; height:20px" colspan="2"></td></tr>

                            <tr>
                            <td class="frmTxt">Tournament Type :</td>
                            <td class="frmCont">
                                <asp:radiobuttonlist id="rblTournamentType" runat="server">
                                    <asp:listitem id="liCup" runat="server" Selected="TRUE" value="cup" >Cup</asp:listitem>
                                    <asp:listitem id="liEAE" runat="server" value="eae" >Everyone Against Everyone</asp:listitem> 
                                </asp:radiobuttonlist>
                            </td>
                            </tr>

                            <tr><td style="text-align:left; vertical-align:top; height:20px" colspan="2"></td></tr>

                            <tr>
                                <td class="frmTxt"> Tournament Description (HTML allowed) : 
                                </td>
                                <td class="frmCont">
                                <asp:TextBox TextMode="MultiLine" ID="txtDesc" runat="server"></asp:TextBox>       
                                <br />
                                <asp:RequiredFieldValidator ID="rfvDesc" ControlToValidate="txtDesc" runat="server" CssClass="caution">Invalid Description Text</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                            <td></td>
                            <td class="frmCont"><asp:Button runat="server" id="btnSubmit" 
                                    Text="Create Tournament" onclick="btnSubmit_Click"/></td>
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
