﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TournamentsList.aspx.cs" Inherits="TournamentCreator.TournamentManager.TournamentsList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title>Tournament Creator :: Tournaments</title>
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
                    &nbsp;&nbsp;<span runat="server" id="spCreateTournament"><a href="CreateTournament.aspx" id="aCreateTournament" runat="server">Create New Tournament</a> |</span> Show Tournaments 
                     <asp:DropDownList ID="cmbShowTournaments" AutoPostBack="true" runat="server" 
                         onselectedindexchanged="cmbShowTournaments_SelectedIndexChanged"></asp:DropDownList>
                    <br />
                    <br />
                    
                    <table class="tblList" style="width:100%" cellpadding="0" cellspacing="0">
                   <thead id="thTournamentsList" runat="server"></thead>
                    <asp:Repeater ID="repTournametList" runat="server">
                        <ItemTemplate>
                            <tr class="trLight">
                            <td><a href="ShowTournament.aspx?tn=<%#DataBinder.Eval(Container.DataItem,"TournamentName")%>"><%#DataBinder.Eval(Container.DataItem, "TournamentName")%></a></td>
                            <td><%#DataBinder.Eval(Container.DataItem,"TournamentType")%></td>
                            <td><%# (DataBinder.Eval(Container.DataItem, "IsStarted").ToString().ToLower() == "false") ? "<span style=color:Green>No</span>" : "<span style=color:Red>Yes</span>"%></td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="trDark">
                            <td><a href="ShowTournament.aspx?tn=<%#DataBinder.Eval(Container.DataItem,"TournamentName")%>"><%#DataBinder.Eval(Container.DataItem, "TournamentName")%></a></td>
                            <td><%#DataBinder.Eval(Container.DataItem,"TournamentType")%></td>
                            <td><%# (DataBinder.Eval(Container.DataItem, "IsStarted").ToString().ToLower() == "false") ? "<span style=color:Green>No</span>" : "<span style=color:Red>Yes</span>"%></td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                    <tr>
                    <td class="norecord" colspan="5" runat="server" id="tdNoRecord">
                        &nbsp;&nbsp;&nbsp;No Tournaments to display.
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
