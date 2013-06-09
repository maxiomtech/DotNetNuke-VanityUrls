<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Admin.ascx.cs" Inherits="InspectorIT.VanityURLs.views.Admin" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<dnn:DnnCssInclude ID="DnnCssInclude1" runat="server" FilePath="~/DesktopModules/InspectorIT/VanityUrls/css/jquery-ui-1.9.1.custom.css"></dnn:DnnCssInclude>
<dnn:DnnJsInclude ID="DnnJsInclude2" runat="server" FilePath="~/DesktopModules/InspectorIT/VanityUrls/js/knockout.js" Priority="2" />
<dnn:DnnJsInclude ID="DnnJsInclude3" runat="server" FilePath="~/DesktopModules/InspectorIT/VanityUrls/js/knockout.mapping.js" Priority="3" />
<dnn:DnnJsInclude ID="DnnJsInclude4" runat="server" FilePath="~/DesktopModules/InspectorIT/VanityUrls/js/moment.js" />
<dnn:DnnJsInclude ID="DnnJsInclude6" runat="server" FilePath="~/DesktopModules/InspectorIT/VanityUrls/js/jquery.placeholder.js" />
<dnn:DnnJsInclude ID="DnnJsInclude5" runat="server" FilePath="~/DesktopModules/InspectorIT/VanityUrls/js/jquery.timepicker.js" />
<dnn:DnnJsInclude ID="DnnJsInclude1" runat="server" FilePath="~/DesktopModules/InspectorIT/VanityUrls/js/VanityUrls.Admin.js" Priority="103"  />

<asp:PlaceHolder runat="server" ID="phCanEdit" Visible="false">

<table border="0" cellspacing="2" cellpadding="2" class="iit_Admin">
    <tr>
        <td valign="top" class="iit_LeftColumn">
            <input type="text" id="iit_SearchUrls" data-bind="value: searchValue, valueUpdate:'afterkeydown'" name="iit_SearchUrls" class="iit_largeTextbox iitPlaceholder" defaultvalue="Search"  />

            <div class="iit_feedList">
                <ul data-bind="foreach: searchUrls">
                    <li data-bind="visible: VanityUrl()!='', click: $parent.selectUrl, css: {selected: $parent.getSelectedUrl().ID == ID}"><div class="iit_colorblock" /><a href="#" data-bind="text: VanityUrl, click: $parent.selectUrl"></a></li>
                </ul>
            </div>
        </td>
        <td valign="top">
            <div class="iit_feedDetails">
                <div id="iit_tabs">
	                <ul>
		                <li><a href="#tabs-1">General</a></li>
		                <li><a href="#tabs-2">Campaign</a></li>
                        <li><a href="#tabs-3">Settings</a></li>
                        <li style="float:right"><a href="#tabs-4" id="iit_NewFeed" data-bind="event: {click: newUrl}">New</a></li>
	                </ul>
	                <div id="tabs-1">
		                <div class="iit_headingLabel">General Configuration</div>
                        
                        <p>
                            <strong>Enter vanity url or <a href="#" data-bind="event: {click: makeid}">generate one</a></strong>
                            <input type="text" id="iit_VanityUrlName" data-bind="value: getSelectedUrl().VanityUrl,  valueUpdate:'afterkeydown', css: getSelectedUrl().VanityUrl.cssClass" name="iit_VanityUrlName" class="iit_largeTextbox" />
                        </p>
                        
                        <p>
                            <strong>Enter a destination url for the vanity url above.</strong>
                            <input type="text" id="iit_RedirectUrl" data-bind="value: getSelectedUrl().RedirectUrl, valueUpdate:'afterkeydown', css: getSelectedUrl().RedirectUrl.cssClass" name="iit_RedirectUrl" class="iit_largeTextbox" />
                        </p>
                        <p>
                            <strong>Enter a description for what this vanity url is used for.</strong>
                            <textarea id="iit_feedDesc" name="iit_feedDesc" data-bind="value: getSelectedUrl().Description" class="iit_largeTextbox iit_textArea"></textarea>
                        </p>
                        
                        <div data-bind="if: getSelectedUrl().VanityUrl">
                            <!-- ko with: getSelectedUrl() -->
                            <div data-bind="if: ID()!=-1" class="dnnClear">
                                <div class="iit_footerLeft">
                                    <div data-bind="if: LastAccessedDate()"><strong>Last Used On: </strong><span data-bind="text:LastAccessedDate()"></span></div>
                                    <div><strong>Last Modified On: </strong><span data-bind="text: ModifiedOnDate"></span></div>
                                    <div><strong>Last Modified By: </strong><span data-bind="text: ModifiedByUserName"></span></div>
                                </div>
                                <div class="iit_footerRight">
                                    <div><strong>Created On: </strong><span data-bind="text: CreatedOnDate"></span></div>
                                    <div><strong>Created By: </strong><span data-bind="text: CreatedByUserName"></span></div>
                                </div>

                            </div>
                            <!-- /ko -->
                        </div>

	                </div>
	                <div id="tabs-2">
		                <div class="iit_headingLabel">Campaign Settings</div>
                        <div class="dnnClear">
                            <div class="iit_CampaignLeft">
                                <fieldset>
                                    <legend>Required Campaign Attributes</legend>
                                    
                                    <p>
                                        <strong>UTM Source</strong>
                                        <div>
                                            <select id="iit_utmSource" name="iit_utmSource" data-bind="options: UTM_Sources" class="iit_largeTextbox">
                                            </select>
                                        </div>
                                    </p>
                                    <p>
                                        <strong>UTM Medium</strong>
                                        <select id="iit_utmMedium" name="iit_utmMedium" data-bind="options: UTM_Mediums" class="iit_largeTextbox">
                                        </select>
                                    </p>
                                    <p>
                                        <strong>UTM Campaign</strong>
                                        <select id="iit_utmCampaign" name="iit_utmCampaign" data-bind="options: UTM_Campaigns" class="iit_largeTextbox">
                                        </select>
                                    </p>

                                </fieldset>
                            </div>
                        
                            <div class="iit_CampaignRight">
                                <fieldset>
                                    <legend>Optional Campaign Attributes</legend>
                                    
                                    <p>
                                        <strong>UTM Term</strong>
                                        <select id="iit_utmTerm" name="iit_utmTerm" data-bind="options: UTM_Terms" class="iit_largeTextbox">
                                        </select>
                                    </p>
                                    
                                    <p>
                                        <strong>UTM Content</strong>
                                        <select id="iit_utmContent" name="iit_utmContent" data-bind="options: UTM_Contents" class="iit_largeTextbox">
                                        </select>
                                    </p>

                                </fieldset>
                            </div>
                        </div>
                        
	                </div>
                    <div id="tabs-3">
                        <div class="iit_headingLabel">Additional Settings</div>
                        
                        <p>
                            <strong>Start Date</strong><br />
                            <input type="text" id="iit_StartDate" data-bind="value: getSelectedUrl().ActiveStartDate" name="iit_EndDate" class="iit_smallTextbox"/>
                        </p>
                        <p>
                            <strong>End Date</strong><br />
                            <input type="text" id="iit_EndDate" data-bind="value: getSelectedUrl().ActiveEndDate" name="iit_EndDate" class="iit_smallTextbox" />
                        </p>

                    </div>
                    <div id="tabs-4"></div>
                </div>

                <div class="iit_alerts">
                    <p>Your settings have been saved.</p>
                </div>
    
                <ul class="dnnActions">
                    <li><a href="#" id="iit_LnkSave" data-bind="event: {click: save}" class="dnnPrimaryAction">Save</a></li>
                    <li><a href="#" id="iit_LnkDelete" data-bind="visible: getSelectedUrl().ID() !=-1, event: {click:deleteUrl}" class="dnnSecondaryAction">Delete</a></li>
                    <li><a href="#" id="iit_LnkExport" data-bind="visible: urls().length > 1" class="dnnSecondaryAction">Export</a></li>
                </ul>
            </div>
        </td>
    </tr>
</table>

<script type="text/javascript">
    var myData;
    $(document).ready(function () {
        var options = {
            servicePath: '<%= servicePath %>',
            PortalId: <%=PortalId %>
        };
        $.VanityUrls.Admin.Init(options);

    });

</script>
</asp:PlaceHolder>