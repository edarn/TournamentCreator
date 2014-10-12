<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameResult.aspx.cs" Inherits="TournamentCreator.TournamentManager.GameResult" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title>Tournament Creator :: Game Result</title>
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
        <form id="frmTournaments" runat="server">
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
                    &nbsp;&nbsp;<span runat="server" id="spOptions"></span>
                    <br />
                    <br />
                    
                    <table style="width:100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="frmTxt"> <asp:Label runat="server" ID="lblTxtGameName"></asp:Label> </td>
                            <td class="frmCont"><asp:Label runat="server" ID="lblGameName"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="frmTxt"> <asp:Label runat="server" ID="lblTxtFirstPlayerWonSet"></asp:Label> </td>
                            <td class="frmCont"><asp:Label runat="server" ID="lblFirstPlayerWonSet"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="frmTxt"> <asp:Label runat="server" ID="lblTxtFirstPlayserLostSet"></asp:Label> </td>
                            <td class="frmCont"><asp:Label runat="server" ID="lblFirstPlayserLostSet"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="frmTxt"> <asp:Label runat="server" ID="lblTxtSecondPlayerWonSet"></asp:Label> </td>
                            <td class="frmCont"><asp:Label runat="server" ID="lblSecondPlayerWonSet"></asp:Label></td>
                        </tr>
                         <tr>
                            <td class="frmTxt"> <asp:Label runat="server" ID="lblTxtSecondPlayserLostSet"></asp:Label> </td>
                            <td class="frmCont"><asp:Label runat="server" ID="lblSecondPlayserLostSet"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="frmTxt"> <asp:Label runat="server" ID="lblTxtWinner" Text="Winner"></asp:Label> </td>
                            <td class="frmCont"><asp:Label runat="server" ID="lblWinner"></asp:Label></td>
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
