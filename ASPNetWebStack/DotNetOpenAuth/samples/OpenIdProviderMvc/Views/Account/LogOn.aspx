﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Log On
</asp:Content>

<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Log On</h2>
    <p>
        Please enter your username and password. 
        <%--<%= Html.ActionLink("Register", "Register") %> if you don't have an account.--%>
    </p>
    <%= Html.ValidationSummary("Login was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) { %>
        <div>
            <fieldset>
                <legend>Account Information</legend>
                <p>
                    <label for="username">Username:</label>
                    <%= Html.TextBox("username") %>
                    <%= Html.ValidationMessage("username") %>
                </p>
                <p>
                    <label for="password">Password:</label>
                    <%= Html.Password("password") %>
                    <%= Html.ValidationMessage("password") %>
                </p>
                <p>
                    <%= Html.CheckBox("rememberMe") %> <label class="inline" for="rememberMe">Remember me?</label>
                </p>
                <p>
                    <input type="submit" value="Log On" />
                </p>
            </fieldset>
        </div>
    <% } %>

	<p>Credentials to try (each with their own OpenID)</p>
	<table>
		<tr><td>Username</td><td>Password</td></tr>
		<tr><td>bob</td><td>test</td></tr>
		<tr><td>bob1</td><td>test</td></tr>
		<tr><td>bob2</td><td>test</td></tr>
		<tr><td>bob3</td><td>test</td></tr>
		<tr><td>bob4</td><td>test</td></tr>
	</table>
</asp:Content>
