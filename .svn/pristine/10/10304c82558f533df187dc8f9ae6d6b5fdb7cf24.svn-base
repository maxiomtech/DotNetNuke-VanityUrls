<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="InspectorIT.VanityURLs.Settings" %>

<div class="dnnForm">
<p>
    <h2>Installation</h2>
    <p>
        Enabling a provider will modify your web.config. You should enable only one provider. Enabling the standard provider will take presidence over the iFinity provider.
    </p>
    
    <p>
        <em>Please backup your web.config before performing any of these operations. </em>
    </p>
</p>

 <div>
     <strong>Standard Provider</strong>
     <p>
         Standard provider allows for Vanity Urls without iFinity Url Module. Requires IIS7 or higher.
     </p>
     <p>
         <asp:LinkButton ID="btnToggleStandardProvider" OnClick="btnToggleStandardProvider_Click" runat="server" CssClass="dnnSecondaryAction">Standard Provider</asp:LinkButton>
     </p>
 </div>

<div>
    <strong>iFinity Provider</strong>
    <p>
        iFinity provider hooks into the iFinity Friendly Url module pipeline for handling vanity urls.
    </p>
    <p>
        <asp:LinkButton ID="btnToggleIfinityProvider" OnClick="btnToggleIfinityProvider_Click" runat="server" CssClass="dnnSecondaryAction">iFinity Provider</asp:LinkButton>
    </p>
</div>


</div>