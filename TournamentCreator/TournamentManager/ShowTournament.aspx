<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowTournament.aspx.cs" Inherits="TournamentCreator.TournamentManager.ShowTournament" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title>Tournament Creator :: Show Tournament</title>
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
            <asp:Label runat="server" Visible="false" id="lbltid"></asp:Label>
            <asp:Label runat="server" Visible="false" id="lbltname"></asp:Label>
			<table style="width:100%" cellpadding="0" cellspacing="0">
				<tr id="navigation">
					<td>
						<asp:Label CssClass="clblTop" runat="server" ID="lblNavigation"></asp:Label>
					</td>
				</tr>
                <tr>
                <td>
                 <div class="post">
                 <br />
                 <br />
                 &nbsp;&nbsp;<asp:Label CssClass="error" runat="server" ID="lblError"></asp:Label>
                 <br />
                 &nbsp;&nbsp;<span runat="server" id="spOptions"></span>
                 <br />
                 <table cellpadding="0" cellspacing="0" class="post">
                 <tr>
                 <td class="frmTxt">Name :</td>
                 <td class="frmCont"><asp:Label runat="server" ID="lblName"></asp:Label></td>
                 </tr>

                 <tr>
                 <td class="frmTxt">Description :</td>
                 <td class="frmCont" id="tdDesc" runat="server"><asp:Label runat="server" ID="lblDesc"></asp:Label></td>
                 </tr>

                 <tr>
                 <td class="frmTxt">Type :</td>
                 <td class="frmCont"><asp:Label runat="server" ID="lblType"></asp:Label></td>
                 </tr>

                 <tr>
                 <td class="frmTxt">Status :</td>
                 <td class="frmCont"><asp:Label runat="server" ID="lblStatus"></asp:Label></td>
                 </tr>

                 <tr>
                 <td class="frmTxt">Number of Players :</td>
                 <td class="frmCont"><asp:Label runat="server" ID="lblNumPlayers"></asp:Label></td>
                 </tr>

                 </table>


                 <div id="NonStartedTournament" runat="server">
                 <table cellpadding="0" cellspacing="0" class="post">
                 <tr>
                 <td class="frmTxt">Maximum Number of Players :</td>
                 <td class="frmCont"><asp:Label runat="server" ID="lblMaxNumPlayers"></asp:Label></td>
                 </tr>
                 
                 <tr id="trMustPlayedIn" runat="server">
                  <td class="frmTxt">Must be Played In :</td>
                 <td class="frmCont">
                 <span runat="server" id="spMustPlayedIn">
                 <asp:TextBox ID="txtMustPlayedIn" runat="server"></asp:TextBox>
                 <asp:Button Text="Save Changes" runat="server" onclick="Unnamed1_Click" />
                 </span>
                 <asp:Label runat="server" ID="lblMustPlayedIn"></asp:Label></td>
                 </tr>

                 <tr>
                  <td class="frmTxt">Start On :</td>
                 <td class="frmCont"><asp:Label runat="server" ID="lblStartOn"></asp:Label></td>
                 </tr>

                 <tr>
                  <td class="frmTxt">Stop On :</td>
                 <td class="frmCont"><asp:Label runat="server" ID="lblStopOn"></asp:Label></td>
                 </tr>

                 </table>
                 </div>
                 
                 <div id="StartedTournament" runat="server">
                 <br />
                 <table class="tblList" style="width:100%" cellpadding="0" cellspacing="0">
                   <thead id="thScoreBoard" runat="server"></thead>
                   <asp:Repeater ID="repScoreBoard" runat="server">
                        <ItemTemplate>
                            <tr class="trLight">
                            <td><%#DataBinder.Eval(Container.DataItem, "FirstName")%>&nbsp;<%#DataBinder.Eval(Container.DataItem, "LastName")%></td>
                            <td><%#DataBinder.Eval(Container.DataItem,"Score")%></td>
                            <td><%#DataBinder.Eval(Container.DataItem,"WonMatches")%></td>
                            <td><%#DataBinder.Eval(Container.DataItem,"LostMatches")%></td>
                            <td><%#DataBinder.Eval(Container.DataItem,"WonSets")%></td>
                            <td><%#DataBinder.Eval(Container.DataItem,"LostSets")%></td>
                            <td><%#DataBinder.Eval(Container.DataItem,"NumGamesPlayed")%></td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="trDark">
                            <td><%#DataBinder.Eval(Container.DataItem, "FirstName")%>&nbsp;<%#DataBinder.Eval(Container.DataItem, "LastName")%></td>
                            <td><%#DataBinder.Eval(Container.DataItem,"Score")%></td>
                            <td><%#DataBinder.Eval(Container.DataItem,"WonMatches")%></td>
                            <td><%#DataBinder.Eval(Container.DataItem,"LostMatches")%></td>
                            <td><%#DataBinder.Eval(Container.DataItem,"WonSets")%></td>
                            <td><%#DataBinder.Eval(Container.DataItem, "NumGamesPlayed")%></td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                    </table>
                    <br />
                    <br />

                    <table class="tblList" style="width:100%" cellpadding="0" cellspacing="0">
                    <thead id="thMatchResult" runat="server"></thead>
                   <asp:Repeater ID="repMatchResult" runat="server">
                        <ItemTemplate>
                            <tr class="trLight">
                            <td><a href="GameResult.aspx?rid=<%#DataBinder.Eval(Container.DataItem, "GameResultID")%>"><%#DataBinder.Eval(Container.DataItem,"FirstPlayer")%> vs <%#DataBinder.Eval(Container.DataItem,"SecondPlayer")%> </a></td>
                            <td><%#DataBinder.Eval(Container.DataItem, "WinnerName")%></td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="trDark">
                            <td><a href="GameResult.aspx?rid=<%#DataBinder.Eval(Container.DataItem, "GameResultID")%>"><%#DataBinder.Eval(Container.DataItem,"FirstPlayer")%> vs <%#DataBinder.Eval(Container.DataItem,"SecondPlayer")%> </a></td>
                            
                            <td><%#DataBinder.Eval(Container.DataItem,"WinnerName")%></td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                    </table>
                 
                 </div>
                 </div>
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