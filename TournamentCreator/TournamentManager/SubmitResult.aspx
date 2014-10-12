<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubmitResult.aspx.cs" Inherits="TournamentCreator.TournamentManager.SubmitResult" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title>Tournament Creator :: Submit Result</title>
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
		<table style="width:100%" cellpadding=0 cellspacing=0>
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
        <form id="frmSubmitResult" runat="server">
        <asp:Label ID="lblEdit" runat="server" Text="0" Visible="false"></asp:Label>
        <asp:Label ID="lblResultID" runat="server" Text="0" Visible="false"></asp:Label>
			<table style="width:100%" cellpadding=0 cellspacing=0>
				<tr id="navigation">
					<td>
						<asp:Label CssClass="clblTop" runat="server" ID="lblNavigation"></asp:Label>
					</td>
				</tr>
                <tr>
                <td>
                 <div class="post">
                    &nbsp;&nbsp;<asp:Label CssClass="error" runat="server" ID="lblError"></asp:Label>
                    <br />
                        <div runat="server" id="SelectTournamentGame">
                        <table style="width:100%" cellpadding="0" cellspacing="0">
                        <tr>
                        <td> Tournament Name :
                        </td>
                        <td>
                            <asp:DropDownList AutoPostBack="true" ID="cmbTournamentName" runat="server" 
                                onselectedindexchanged="cmbTournamentName_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        </tr>
                        <tr>
                        <td> Game :
                        </td>
                        <td>
                            <asp:DropDownList AutoPostBack="true" ID="cmbGameNames" runat="server" 
                                onselectedindexchanged="cmbGameNames_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        </tr>
                        </table>
                        </div>


                    <br />
                    <br />
                    
                    <table style="width:100%" cellpadding="0" cellspacing="0" id="tblResultInput" runat="server">
                        
                        <tr>
                            <td class="frmTxt"> <asp:Label runat="server" ID="lblTxtFirstPlayerWonSet"></asp:Label> </td>
                            <td class="frmCont"><asp:TextBox runat="server" ID="txtFirstPlayerWonSet"></asp:TextBox> </td>
                            </td>
                        </tr>
                        <tr>
                            <td class="frmTxt"> <asp:Label runat="server" ID="lblTxtFirstPlayserLostSet"></asp:Label> </td>
                            <td class="frmCont"><asp:TextBox runat="server" ID="txtFirstPlayerLostSet"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="frmTxt"> <asp:Label runat="server" ID="lblTxtSecondPlayerWonSet"></asp:Label> </td>
                            <td class="frmCont"><asp:Label runat="server" ID="txtSecondPlayerWonSet"></asp:Label></td>
                        </tr>
                         <tr>
                            <td class="frmTxt"> <asp:Label runat="server" ID="lblTxtSecondPlayserLostSet"></asp:Label> </td>
                            <td class="frmCont"><asp:Label runat="server" ID="txtSecondPlayerLostSet"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="frmTxt"> <asp:Label runat="server" ID="lblTxtWinner"></asp:Label> </td>
                            <td class="frmCont">
                             <asp:radiobuttonlist id="rblWinner" runat="server">
                                    <asp:listitem id="rdFirstPlayer" runat="server"   ></asp:listitem>
                                    <asp:listitem id="rdSecondPlayer" runat="server"  ></asp:listitem>
                                    <asp:listitem id="rdDraw" runat="server" Selected="TRUE" value="0" Text="Draw"  ></asp:listitem>
                     
                                </asp:radiobuttonlist>
                            </td>
                        </tr>
                        <tr>
                            <td class="frmTxt"> <asp:Label runat="server" ID="Label1"></asp:Label> </td>
                            <td class="frmCont">
                                <asp:Button runat="server" ID="btnSubmit" Text="Submit" 
                                    onclick="btnSubmit_Click" />
                            </td>
                        </tr>
                    </table>
                    
                  
                </div> <!-- /post -->
                </td>
                </tr>
			</table>
            </form>
		</td>
		
        </tr>
		</table>
	</div>
    </body>

</html>
