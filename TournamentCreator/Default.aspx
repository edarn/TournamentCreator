<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="TournamentCreator._Default" %>

<html>
<head>
    <title>Tournament Creator :: Home</title>
    <link href="styles/stylesheet.css" rel="stylesheet" type="text/css" />
    <link href="styles/aspstyles.css" rel="stylesheet" type="text/css" />
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
			<table style="width:100%" cellpadding=0 cellspacing=0>
				<tr id="navigation">
					<td>
						<asp:Label CssClass="clblTop" runat="server" ID="lblNavigation"></asp:Label>
					</td>
				</tr>
                <tr>
                <td>
                 <div class="post">
                    <h1>Get Started</h1>
                    

                    <img class="alignleft" src="images/all.jpg"  alt="Photo Example" /><p>Online tournament system allows you to create tournaments and manage them, submit their results.</p>
                    <p>It also provides you the unique functionality to work without administrator and collaborate with other players for the submission of results.</p>
                    <p>To get started learn <a href="#">Step by Step guide.</a></p>
                  
                  
                </div> <!-- /post -->
                </td>
                </tr>
			</table>
		</td>
		
        </tr>
		</table>
	</div>
    </body>

</html>