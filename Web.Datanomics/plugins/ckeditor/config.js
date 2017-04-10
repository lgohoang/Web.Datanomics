/// <reference path="../ckfinder/ckfinder.html" />
/// <reference path="../ckfinder/ckfinder.html" />
/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    config.filebrowserBrowseUrl = window.location.origin + "/plugins/ckfinder/ckfinder.html";
    config.filebrowserImageBrowseUrl = window.location.origin + "/plugins/ckfinder/ckfinder.html?Type=Images";
    config.filebrowserFlashBrowseUrl = window.location.origin + "/plugins/ckfinder/ckfinder.html?Type=Flash";
    config.filebrowserUploadUrl = window.location.origin + "/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files";
    config.filebrowserImageUploadUrl = window.location.origin + "/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images";
    config.filebrowserFlashUploadUrl = window.location.origin + "/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash";
    
};


